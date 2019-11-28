using System;
using System.Collections.Generic;
using System.Text;
using BSGO_Server._3dAlgorithm;
using System.Threading.Tasks;

namespace BSGO_Server
{
    internal class GameProtocol : Protocol
    {
        public enum Reply : ushort
        {
            Info = 2,
            WhoIs = 4,
            Move = 6,
            ObjectLeft = 7,
            WeaponShot = 13,
            MissileDecoyed = 18,
            SyncMove = 20,
            Cast = 22,
            StopSlotAbility = 24,
            Scan = 34,
            CombatInfo = 40,
            AskStartQueue = 47,
            AskJump = 49,
            Collide = 55,
            FTLCharge = 58,
            VirusBlocked = 59,
            RemoveMe = 69,
            TimeOrigin = 70,
            StopGroupJump = 76,
            LeaderStopGroupJump = 77,
            NotEnoughTylium = 81,
            UpdateRoles = 83,
            [Obsolete("Covered by ObjectState now")]
            PaintTheTarget = 84,
            [Obsolete("Covered by ObjectState now")]
            UnpaintTheTarget = 85,
            StopJump = 86,
            ChangeVisibility = 87,
            UpdateFactionGroup = 88,
            MineField = 90,
            ObjectState = 91,
            FlareReleased = 92,
            LostAbilityTarget = 93,
            LostJumpTransponder = 94,
            DockingDelay = 95,
            ChangedPlayerSpeed = 96,
            ShortCircuitResult = 97,
            OutpostStateBroadcast = 98,
            RespawnOptions = 99,
            AnchorDeclined = 100,
            DetachedToSpace = 104,
            RetachedToSpace = 105,
            CargoInteraction = 106
        }

        public enum Request : ushort
        {
            WhoIs = 3,
            SubscribeInfo = 10,
            UnSubscribeInfo = 11,
            MoveToDirection = 12,
            MoveToDirectionWithoutRoll = 13,
            CastSlotAbility = 21,
            CastImmutableSlotAbility = 22,
            LockTarget = 25,
            WASD = 29,
            QWEASD = 30,
            Mining = 35,
            Loot = 41,
            TakeLootItems = 43,
            Dock = 45,
            Jump = 46,
            AnsStartQueue = 48,
            AnsJump = 50,
            [Obsolete]
            Follow = 52,
            Quit = 54,
            SetSpeed = 56,
            SetGear = 57,
            JumpIn = 61,
            MoveInfo = 0x3F,
            StopJump = 65,
            SelectRespawnLocation = 70,
            GroupJump = 72,
            StopGroupJump = 73,
            RequestJumpToTarget = 75,
            CompleteJump = 76,
            RequestUnanchor = 77,
            RequestAnchor = 78,
            RequestLaunchStrikes = 79,
            CancelMiningRequest = 82,
            RequestJumpToBeacon = 85,
            ToggleAbilityOn = 86,
            ToggleAbilityOff = 87,
            UpdateAbilityTargets = 88,
            GroupJumpToBeacon = 89,
            TurnToDirectionStrikes = 100,
            TurnByPitchYawStrikes = 101,
            CancelDocking = 102,
            GroupJumpToTarget = 103,
            CargoInteraction = 106
        }

        public GameProtocol()
            : base(ProtocolID.Game)
        {
        }

        public static GameProtocol GetProtocol()
        {
            return ProtocolManager.GetProtocol(ProtocolID.Game) as GameProtocol;
        }

        public override void ParseMessage(int index, BgoProtocolReader br)
        {
            ushort msgType = br.ReadUInt16();

            switch ((Request)msgType)
            {
                case Request.JumpIn:
                    Client c = Server.GetClientByIndex(index);
                    Server.GetSectorById(c.Character.PlayerShip.sectorId).JoinSector(c);
                    break;
                case Request.CompleteJump:
                    PlayerProtocol.GetProtocol().SendUnanchor(index, Server.GetObjectId(index));
                    StoryProtocol.GetProtocol().EnableGear(index, true);

                    Server.GetClientByIndex(index).Character.PlayerShip.isVisible = false;
                    SendChangeVisibility(index, Server.GetObjectId(index), Server.GetClientByIndex(index).Character.PlayerShip.isVisible, 0);
                    Server.GetClientByIndex(index).Character.PlayerShip.jumpInTime = DateTime.Now.AddSeconds(10);
                    break;
                case Request.SetSpeed:
                    Client setSpeedClient = Server.GetClientByIndex(index);
                    CheckIfVisibleAndSetIfNot(setSpeedClient);
                    byte mode = br.ReadByte();
                    float speed = br.ReadSingle();
                    setSpeedClient.Character.PlayerShip.shipMode = mode;
                    setSpeedClient.Character.PlayerShip.shipSpeed = speed;
                    setSpeedClient.Character.PlayerShip.MovementOptions.speed = setSpeedClient.Character.PlayerShip.shipGear == Gear.Regular ? setSpeedClient.Character.PlayerShip.shipSpeed : setSpeedClient.Character.PlayerShip.currentShipStats.BoostSpeed;
                    SyncMove(index, SpaceEntityType.Player, Server.GetObjectId(index));
                    break;
                case Request.SetGear:
                    Client setGearClient = Server.GetClientByIndex(index);
                    CheckIfVisibleAndSetIfNot(setGearClient);
                    setGearClient.Character.PlayerShip.shipGear = (Gear)br.ReadByte();
                    setGearClient.Character.PlayerShip.MovementOptions.speed = setGearClient.Character.PlayerShip.shipGear == Gear.Regular ? setGearClient.Character.PlayerShip.shipSpeed : setGearClient.Character.PlayerShip.currentShipStats.BoostSpeed;
                    SyncMove(index, SpaceEntityType.Player, Server.GetObjectId(index));
                    break;
                case Request.WASD:
                    Client wasdClient = Server.GetClientByIndex(index);
                    Sector wasdServer = Server.GetSectorById(wasdClient.Character.PlayerShip.sectorId);
                    CheckIfVisibleAndSetIfNot(wasdClient);
                    wasdClient.Character.PlayerShip.qweasd.Bitmask = br.ReadByte();

                    wasdClient.Character.PlayerShip.ManeuverController.AddManeuver(new TurnManeuver(ManeuverType.Turn, wasdServer.Tick.Current.value, wasdClient.Character.PlayerShip.qweasd, wasdClient.Character.PlayerShip.MovementOptions));
                    
                    SyncMove(index, SpaceEntityType.Player, Server.GetObjectId(index));
                    break;
                case Request.MoveToDirection:
                    Client directionalClient = Server.GetClientByIndex(index);
                    Sector directionalServer = Server.GetSectorById(directionalClient.Character.PlayerShip.sectorId);
                    CheckIfVisibleAndSetIfNot(directionalClient);
                    directionalClient.Character.PlayerShip.direction = br.ReadEuler();

                    directionalClient.Character.PlayerShip.ManeuverController.AddManeuver(new DirectionalManeuver(ManeuverType.Directional, directionalServer.Tick.Current.value, directionalClient.Character.PlayerShip.direction, directionalClient.Character.PlayerShip.MovementOptions));

                    SyncMove(index, SpaceEntityType.Player, Server.GetObjectId(index));
                    break;
                case Request.MoveToDirectionWithoutRoll:
                    Client directionalWrClient = Server.GetClientByIndex(index);
                    Sector directionalWrServer = Server.GetSectorById(directionalWrClient.Character.PlayerShip.sectorId);
                    CheckIfVisibleAndSetIfNot(directionalWrClient);
                    directionalWrClient.Character.PlayerShip.direction = br.ReadEuler();

                    directionalWrClient.Character.PlayerShip.ManeuverController.AddManeuver(new DirectionalWithoutRollManeuver(ManeuverType.DirectionalWithoutRoll, directionalWrServer.Tick.Current.value, directionalWrClient.Character.PlayerShip.direction, directionalWrClient.Character.PlayerShip.MovementOptions));

                    SyncMove(index, SpaceEntityType.Player, Server.GetObjectId(index));
                    break;
                case Request.QWEASD:
                    Client qweasdClient = Server.GetClientByIndex(index);
                    Sector qweasdServer = Server.GetSectorById(qweasdClient.Character.PlayerShip.sectorId);
                    CheckIfVisibleAndSetIfNot(qweasdClient);
                    qweasdClient.Character.PlayerShip.qweasd.Bitmask = br.ReadByte();

                    qweasdClient.Character.PlayerShip.ManeuverController.AddManeuver(new TurnQweasdManeuver(ManeuverType.TurnQweasd, qweasdServer.Tick.Current.value, qweasdClient.Character.PlayerShip.qweasd, qweasdClient.Character.PlayerShip.MovementOptions));

                    SyncMove(index, SpaceEntityType.Player, Server.GetObjectId(index));
                    break;
                case Request.Dock:
                    uint objId = br.ReadUInt32();
                    float delay = br.ReadSingle();
                    SendDock(index, delay);
                    break;
                case Request.Quit:
                    Client quitClient = Server.GetClientByIndex(index);
                    quitClient.Character.PlayerShip.isSpawned = false;
                    if (quitClient.Character.PlayerShip.requestedJumpSectorId == -1)
                    {
                        quitClient.Character.GameLocation = GameLocation.Room;
                    }
                    else
                    {
                        quitClient.Character.PlayerShip.requestedJumpSectorId = -1;
                        Database.Database.SaveSector(quitClient.playerId.ToString(), quitClient.Character.PlayerShip.sectorId);
                        quitClient.Character.GameLocation = GameLocation.Space;
                        quitClient.Character.PlayerShip.shipGear = Gear.Regular;
                        quitClient.Character.PlayerShip.shipSpeed = 0;
                    }
                    break;
                case Request.Jump:
                    Client jumpClient = Server.GetClientByIndex(index);
                    CheckIfVisibleAndSetIfNot(jumpClient);

                    SendJump(index, br.ReadUInt32(), true);
                    break;
                case Request.StopJump:
                    Client stopJumpClient = Server.GetClientByIndex(index);
                    if (stopJumpClient.Character.PlayerShip.requestedJumpSectorId != -1)
                    {
                        SendStopJump(index);
                    }
                    break;
                default:
                    Log.Add(LogSeverity.ERROR, string.Format("Unknown msgType \"{0}\" on {1}Protocol.", (Request)msgType, protocolID));
                    break;
            }
        }

        private void CheckIfVisibleAndSetIfNot(Client client)
        {
            if (!client.Character.PlayerShip.isVisible && client.Character.PlayerShip.jumpInTime > DateTime.Now)
            {
                client.Character.PlayerShip.isVisible = true;
                SendChangeVisibility(client.index, Server.GetObjectId(client.index), client.Character.PlayerShip.isVisible, 1);
                client.Character.PlayerShip.jumpInTime = DateTime.Now;
            }
        }

        public void SyncMove(int index, SpaceEntityType spaceEntityType, uint objectId)
        {
            Client c = Server.GetClientByIndex(index);
            c.lastSyncSendTime = DateTime.UtcNow;

            if (!c.Character.PlayerShip.isSpawned && c.Character.PlayerShip.MovementOptions.speed > 0)
            {
                c.Character.PlayerShip.isSpawned = true;
                c.Character.PlayerShip.ManeuverController.AddManeuver(new TurnManeuver(ManeuverType.Turn, Server.GetSectorById(c.Character.PlayerShip.sectorId).Tick.Current.value, c.Character.PlayerShip.qweasd, c.Character.PlayerShip.MovementOptions)); // Avoid problems with setting speed and not moving due to being at the rest maneuver
            }

            BgoProtocolWriter buffer = NewMessage();

            buffer.Write((ushort)Reply.SyncMove);
            buffer.Write(objectId);

            Sector s = Server.GetSectorById(c.Character.PlayerShip.sectorId);

            buffer.Write(s.Tick.Current - 1); // tick

            c.Character.PlayerShip.MovementFrame.Write(buffer);
            //c.Character.ManeuverController.GetTickFrame(s.Tick.Current - 1).Write(buffer);

            Maneuver lastManeuverAtTick = c.Character.PlayerShip.ManeuverController.GetLastManeuverAtTick(s.Tick.Current);
            ManeuverType maneuverType = lastManeuverAtTick.ManeuverType;

            buffer.Write((byte)maneuverType);
            buffer.Write(lastManeuverAtTick.GetStartTick());

            switch (maneuverType)
            {
                case ManeuverType.Rest:
                    buffer.Write(c.Character.PlayerShip.MovementFrame.position);
                    buffer.Write(c.Character.PlayerShip.MovementFrame.euler3);
                    break;
                case ManeuverType.Turn:
                case ManeuverType.TurnQweasd:
                    buffer.Write((byte)c.Character.PlayerShip.qweasd.Bitmask);
                    break;
                case ManeuverType.Directional:
                case ManeuverType.DirectionalWithoutRoll:
                    buffer.Write(c.Character.PlayerShip.direction);
                    break;
            }

            ObjectStats currentShipStats = c.Character.PlayerShip.currentShipStats;

            //gear
            buffer.Write((byte)c.Character.PlayerShip.shipGear);
            //speed
            buffer.Write(c.Character.PlayerShip.shipGear == Gear.Regular ? c.Character.PlayerShip.shipSpeed : currentShipStats.BoostSpeed);
            //acceleration
            buffer.Write(currentShipStats.Acceleration);
            //inertiaCompensation
            buffer.Write(currentShipStats.InertiaCompensation);
            //pitchAcceleration
            buffer.Write(currentShipStats.PitchAcceleration);
            //pitchMaxSpeed
            buffer.Write(currentShipStats.PitchMaxSpeed);
            //yawAcceleration
            buffer.Write(currentShipStats.YawAcceleration);
            //yawMaxSpeed
            buffer.Write(currentShipStats.YawMaxSpeed);
            //rollAcceleration
            buffer.Write(currentShipStats.RollAcceleration);
            //rollMaxSpeed
            buffer.Write(currentShipStats.RollMaxSpeed);
            //strafeAcceleration
            buffer.Write(currentShipStats.StrafeAcceleration);
            //strafeMaxSpeed
            buffer.Write(currentShipStats.StrafeMaxSpeed);

            SendMessageToSector(index, buffer);
        }

        // I guess it spawns the obj in a position inside the Sector
        public void SendRestManeuverToSector(int index, SpaceEntityType spaceEntityType, uint objectId, Vector3 position, Vector3 euler3 = default(Vector3))
        {
            BgoProtocolWriter buffer = NewMessage();

            buffer.Write((ushort)Reply.SyncMove);
            buffer.Write(objectId);
            buffer.Write(Server.GetSectorByClientIndex(index).Tick); // tick

            Server.GetClientByIndex(index).Character.PlayerShip.MovementFrame.Write(buffer);

            buffer.Write((byte)2);
            buffer.Write(Server.GetSectorByClientIndex(index).Tick); //startTick

            //position
            buffer.Write(position);

            //euler3
            buffer.Write(euler3);

            Server.GetClientByIndex(index).lastSyncSendTime = DateTime.UtcNow;

            SendMessageToSector(index, buffer);
        }

        public void SendRestManeuverToSector(uint index, SpaceEntityType spaceEntityType, uint objectId, Vector3 position, Vector3 euler3, MovementFrame movementFrame)
        {
            BgoProtocolWriter buffer = NewMessage();

            buffer.Write((ushort)Reply.SyncMove);
            buffer.Write(objectId);
            buffer.Write(Server.GetSectorById(index).Tick); // tick

            movementFrame.Write(buffer);

            buffer.Write((byte)2);
            buffer.Write(Server.GetSectorById(index).Tick); //startTick

            //position
            buffer.Write(position);

            //euler3
            buffer.Write(euler3);

            SendMessageToSector(index, buffer);
        }

        public void SendRestManeuverToPlayer(int index, SpaceEntityType spaceEntityType, uint objectId, Vector3 position, Vector3 euler3 = default(Vector3))
        {
            BgoProtocolWriter buffer = NewMessage();

            buffer.Write((ushort)Reply.SyncMove);
            buffer.Write(objectId);
            buffer.Write(Server.GetSectorByClientIndex(index).Tick); // tick

            Server.GetClientByIndex(index).Character.PlayerShip.MovementFrame.Write(buffer);

            buffer.Write((byte)2);
            buffer.Write(Server.GetSectorByClientIndex(index).Tick); //startTick

            //position
            buffer.Write(position);

            //euler3
            buffer.Write(euler3);

            SendMessageToUser(index, buffer);
        }

        public void SendWhoIsPlayerToSector(int index, SpaceEntityType spaceEntityType, uint objectId, uint ownerGuid, uint WorldCard)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.WhoIs);
            buffer.Write(objectId);
            buffer.Write((byte)CreatingCause.AlreadyExists);
            buffer.Write(ownerGuid); // The OwnerGUID. Since idk what it could be, just using his index
            buffer.Write(WorldCard); // The WorldCardGUID which is the spaceship loaded.

            //nothing yet
            buffer.Write((ushort)0);
            buffer.Write((ushort)0);

            buffer.Write(Server.GetClientByIndex(index).playerId); //player id
            buffer.Write((uint)BgoAdminRoles.Developer); //player role
            buffer.Write(Server.GetClientByIndex(index).Character.PlayerShip.isVisible);

            SendMessageToSector(index, buffer);
        }

        public void SendWhoIsPlayerToPlayer(int connectionId, int index, SpaceEntityType spaceEntityType, uint objectId, uint ownerGuid, uint WorldCard)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.WhoIs);
            buffer.Write(objectId);
            buffer.Write((byte)CreatingCause.AlreadyExists);
            buffer.Write(ownerGuid); // The OwnerGUID. Since idk what it could be, just using his index
            buffer.Write(WorldCard); // The WorldCardGUID which is the spaceship loaded.

            //nothing yet
            buffer.Write((ushort)0);
            buffer.Write((ushort)0);

            buffer.Write(Server.GetClientByIndex(index).playerId); //player id
            buffer.Write((uint)BgoAdminRoles.Developer); //player role
            buffer.Write(Server.GetClientByIndex(index).Character.PlayerShip.isVisible);

            SendMessageToUser(connectionId, buffer);
        }

        public void SendChangeVisibility(int index, uint objId, bool isVisible, byte reason)
        {
            Client c = Server.GetClientByIndex(index);
            if(c.Character.PlayerShip.isVisible != isVisible)
                c.Character.PlayerShip.isVisible = isVisible;

            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.ChangeVisibility);
            buffer.Write(objId);
            buffer.Write(isVisible);
            buffer.Write(reason);

            SendMessageToSector(index, buffer);
        }

        public void SendObjectLeft(int index, ushort quantity, SpaceEntityType[] spaceEntityType, uint[] objectId, RemovingCause[] removingCause)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.ObjectLeft);
            buffer.Write(quantity);

            for (int i = 0; i < quantity; i++)
            {
                buffer.Write(objectId[i]);
                buffer.Write(Server.GetSectorByClientIndex(index).Tick);
                buffer.Write((byte)removingCause[i]);
            }

            SendMessageToSector(index, buffer);
        }

        public void SpawnPlanetoid(int connectionId, uint objectId, Vector3 position)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.WhoIs);
            buffer.Write((uint)SpaceEntityType.Planetoid + objectId);
            buffer.Write((byte)CreatingCause.JumpIn);
            buffer.Write((uint)9988); // The OwnerGUID
            buffer.Write((uint)270503); // The WorldCardGUID

            buffer.Write(position);
            buffer.Write((float)0.5);
            buffer.Write(0f);
            SendMessageToUser(connectionId, buffer);
        }

        public void SetTimeOrigin(int index)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.TimeOrigin);

            long timeOrigin = (long)Server.GetSectorByClientIndex(index).CreatedTime.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
            buffer.Write(timeOrigin);

            SendMessageToUser(index, buffer);

            Server.GetClientByIndex(index).Character.PlayerShip.timeOrigin = timeOrigin;
        }

        private void SendDock(int index, float dockDelay)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.DockingDelay);

            buffer.Write(dockDelay);

            SendMessageToUser(index, buffer);
            SendRemoveMe(index, 5);
        }

        public void SendRemoveMe(int index, byte cause)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.RemoveMe);
            buffer.Write(cause);

            switch (cause)
            {
                case 5:
                    Server.GetSectorByClientIndex(index).LeaveSector(Server.GetClientByIndex(index), RemovingCause.Dock);
                    break;
                case 7:
                    Server.GetSectorByClientIndex(index).LeaveSector(Server.GetClientByIndex(index), RemovingCause.JumpOut);
                    break;
                default:

                    break;
            }

            SendMessageToUser(index, buffer);
        }

        private void SendJump(int index, uint sectorId, bool soloJump)
        {
            Client c = Server.GetClientByIndex(index);

            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.FTLCharge);

            c.Character.PlayerShip.requestedJumpSectorId = (int)sectorId;
            buffer.Write(15f);
            c.Character.PlayerShip.ftlTime = DateTime.Now.AddSeconds(15);
            buffer.Write(Server.GetSectorById(sectorId).sectorGuid);
            buffer.Write(soloJump);

            SendMessageToUser(index, buffer);
        }

        private void SendStopJump(int index)
        {
            Client c = Server.GetClientByIndex(index);

            BgoProtocolWriter buffer2 = NewMessage();
            buffer2.Write((ushort)Reply.StopJump);

            c.Character.PlayerShip.requestedJumpSectorId = -1;
            c.Character.PlayerShip.ftlTime = DateTime.Now;

            SendMessageToUser(index, buffer2);
        }

        public void SpawnOutpost(int index, Faction faction)
        {
            uint objId = Server.GetOutPostObjectId(index, SpaceEntityType.Outpost, faction);

            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.WhoIs);
            buffer.Write(objId);
            buffer.Write((byte)CreatingCause.JumpIn);
            buffer.Write((uint)(faction == Faction.Colonial ? 997 : 998)); // The OwnerGUID. Since idk what it could be, just using his index
            buffer.Write((uint)(faction == Faction.Colonial ? 997 : 998)); // The WorldCardGUID which is the spaceship loaded.

            //nothing yet
            buffer.Write((ushort)0);
            buffer.Write((ushort)0);

            buffer.Write((uint)(faction == Faction.Colonial ? 1 : 2)); //player id. Just using his index
            buffer.Write((uint)0x10); //player role //developer 0x10
            buffer.Write(true);

            SendMessageToUser(index, buffer);

            SetOutpost(index);
            SendRestManeuverToPlayer(index, SpaceEntityType.Outpost, Server.GetOutPostObjectId(index, SpaceEntityType.Outpost, faction), new Vector3(100, 100, 100), new Vector3(0, 0, 0));
        }

        public void SpawnOutpost(uint index, Faction faction)
        {
            uint objId = Server.GetOutPostObjectId(index, SpaceEntityType.Outpost, faction);

            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.WhoIs);
            buffer.Write(objId);
            buffer.Write((byte)CreatingCause.JumpIn);
            buffer.Write((uint)(faction == Faction.Colonial ? 997 : 998)); // The OwnerGUID. Since idk what it could be, just using his index
            buffer.Write((uint)(faction == Faction.Colonial ? 997 : 998)); // The WorldCardGUID which is the spaceship loaded.

            //nothing yet
            buffer.Write((ushort)0);
            buffer.Write((ushort)0);

            buffer.Write((uint) (faction == Faction.Colonial ? 1 : 2)); //player id. Just using his index
            buffer.Write((uint)0x10); //player role //developer 0x10
            buffer.Write(true);

            SendMessageToSector(index, buffer);

            SetOutpost(index);
            SendRestManeuverToSector(index, SpaceEntityType.Outpost, Server.GetOutPostObjectId(index, SpaceEntityType.Outpost, faction), new Vector3(100, 100, 100), new Vector3(0,0,0), new MovementFrame(new Vector3(100,100,100), Euler3.zero, Vector3.zero, Vector3.zero, Euler3.zero));
        }

        private void SetOutpost(int index)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.OutpostStateBroadcast);
            buffer.Write((ushort)1000); //outpost points
            buffer.Write((float)1);
            buffer.Write((ushort)300); //outpost points
            buffer.Write((float)1);
            SendMessageToUser(index, buffer);
        }

        private void SetOutpost(uint index)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.OutpostStateBroadcast);
            buffer.Write((ushort)1000); //outpost points
            buffer.Write((float)1);
            buffer.Write((ushort)1000); //outpost points
            buffer.Write((float)1);
            SendMessageToSector(index, buffer);
        }
    }
}
