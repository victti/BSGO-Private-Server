using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
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
                // I'm not sure if the JumpIn request did send the WhoIs of the player, but it works I guess.
                // Also I don't know if it was sent to everyone or just to the client. Probably everyone on the sector,
                // but since we are doing this offline for now, let's keep it still only client.
                case Request.JumpIn:
                    Client c = Server.GetClientByIndex(index);
                    Task.Run(() =>Server.GetSectorById(c.Character.sectorId).JoinSector(c));
                    //SendWhoIsPlayer(index, SpaceEntityType.Player, (uint)index, (uint)index, Server.GetClientByIndex(index).Character.WorldCardGUID);
                    SetTimeOrigin(index);
                    PlayerProtocol.GetProtocol().SendStats(index); // These are the stats of your ship, not the base ones.
                    PlayerProtocol.GetProtocol().SendShipInfo(index);
                    //SyncMove(index, SpaceEntityType.Player, (uint)index, new Vector3(0, 0f, 0f));
                    break;
                case Request.CompleteJump:
                    PlayerProtocol.GetProtocol().SendUnanchor(index, (uint)index);
                    StoryProtocol.GetProtocol().EnableGear(index, true);

                    //SendWhoIsPlayer(index, SpaceEntityType.Player, (uint)10, 10, 22131177);
                    //SyncMove(index, SpaceEntityType.Player, (uint)10, new Vector3(0, 100f, 100f));
                    break;
                case Request.SetSpeed:
                    byte mode = br.ReadByte();
                    float speed = br.ReadSingle();
                    Server.GetClientByIndex(index).Character.shipMode = mode;
                    Server.GetClientByIndex(index).Character.shipSpeed = speed;
                    SyncMove(index, SpaceEntityType.Player, (uint)index);
                    break;
                case Request.WASD:
                    Server.GetClientByIndex(index).Character.qweasd = br.ReadByte();
                    SyncMove(index, SpaceEntityType.Player, (uint)index);
                    break;
                default:
                    Log.Add(LogSeverity.ERROR, string.Format("Unknown msgType \"{0}\" on {1}Protocol.", (Request)msgType, protocolID));
                    break;
            }
        }

        public void SyncMove(int index, SpaceEntityType spaceEntityType, uint objectId)
        {
            BgoProtocolWriter buffer = NewMessage();

            buffer.Write((ushort)Reply.SyncMove);
            buffer.Write((uint)spaceEntityType + objectId);
            buffer.Write((int)1); // tick

            //position
            buffer.Write(new Vector3());

            //euler3
            buffer.Write(new Vector3());

            //linearSpeed
            buffer.Write(new Vector3());

            //strafeSpeed
            buffer.Write(new Vector3());

            //euler3speed
            buffer.Write(new Vector3());

            // mode
            buffer.Write((byte)2);

            buffer.Write((byte)8);
            buffer.Write(1);

            //qweasd
            buffer.Write(Server.GetClientByIndex(index).Character.qweasd);

            ObjectStats currentShipStats = ((ShipCard)Catalogue.FetchCard(Server.GetClientByIndex(index).Character.WorldCardGUID, CardView.Ship)).Stats;

            //gear
            buffer.Write((byte)0);
            //speed
            buffer.Write(Server.GetClientByIndex(index).Character.shipSpeed);
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

        // No idea why but this makes the game load (???)
        // I guess it spawns the player in a position inside the Sector
        public void SyncMove(int index, SpaceEntityType spaceEntityType, uint objectId, Vector3 position, Vector3 euler3 = default(Vector3))
        {
            BgoProtocolWriter buffer = NewMessage();

            buffer.Write((ushort)Reply.SyncMove);
            buffer.Write((uint)spaceEntityType + objectId);
            buffer.Write((int)1); // tick

            //position
            buffer.Write(position);

            //euler3
            buffer.Write(euler3);
            
            //linearSpeed
            buffer.Write(new Vector3());

            //strafeSpeed
            buffer.Write(new Vector3());

            //euler3speed
            buffer.Write(new Vector3());

            // mode
            buffer.Write((byte)2);

            buffer.Write((byte)2);
            buffer.Write((int)1);

            //position
            buffer.Write(position);

            //euler3
            buffer.Write(euler3);


            SendMessageToSector(index, buffer);
        }

        public void SendWhoIsPlayer(int index, SpaceEntityType spaceEntityType, uint objectId, uint ownerGuid, uint WorldCard)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.WhoIs);
            buffer.Write((uint)spaceEntityType + (uint)objectId);
            buffer.Write((byte)CreatingCause.JumpIn);
            buffer.Write(ownerGuid); // The OwnerGUID. Since idk what it could be, just using his index
            buffer.Write(WorldCard); // The WorldCardGUID which is the spaceship loaded.

            //nothing yet
            buffer.Write((ushort)0);
            buffer.Write((ushort)0);

            buffer.Write(Server.GetClientByIndex(index).playerId); //player id
            buffer.Write((uint)BgoAdminRoles.Developer); //player role
            buffer.Write(true);

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

        private void SetTimeOrigin(int index)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.TimeOrigin);

            buffer.Write((long)DateTime.UtcNow.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);

            SendMessageToUser(index, buffer);
        }

        public void SpawnOutpost(int connectionId, uint objectId)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.WhoIs);
            buffer.Write((uint)SpaceEntityType.Outpost + objectId);
            buffer.Write((byte)CreatingCause.JumpIn);
            buffer.Write((uint)997); // The OwnerGUID. Since idk what it could be, just using his index
            buffer.Write((uint)997); // The WorldCardGUID which is the spaceship loaded.

            //nothing yet
            buffer.Write((ushort)0);
            buffer.Write((ushort)0);

            buffer.Write((uint)connectionId + objectId); //player id. Just using his index
            buffer.Write((uint)0x10); //player role //developer 0x10
            buffer.Write(true);

            SendMessageToUser(connectionId, buffer);

            SetOutpost(connectionId);
        }

        public void SetOutpost(int connectionId)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.OutpostStateBroadcast);
            //int oP = (int)br.ReadUInt16();
            //float colonialDelta = br.ReadSingle();
            //int oP2 = (int)br.ReadUInt16();
            //float cylonDelta = br.ReadSingle();
            //uint serverID = SpaceLevel.GetLevel().ServerID;
            buffer.Write((ushort)1000); //outpost points
            buffer.Write((float)1);
            buffer.Write((ushort)300); //outpost points
            buffer.Write((float)1);
            SendMessageToUser(connectionId, buffer);
        }
    }
}
