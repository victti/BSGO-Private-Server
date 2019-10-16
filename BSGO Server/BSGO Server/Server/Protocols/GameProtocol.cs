using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace BSGO_Server
{
    class GameProtocol : Protocol
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
            ushort msgType = (ushort)br.ReadUInt16();

            switch ((Request)msgType)
            {
                // I'm not sure if the JumpIn request did send the WhoIs of the player, but it works I guess.
                // Also I don't know if it was sent to everyone or just to the client. Probably everyone on the sector,
                // but since we are doing this offline for now, let's keep it still only client.
                case Request.JumpIn:
                    SendWhoIsPlayer(index);
                    SetTimeOrigin(index);

                    SyncMove(index, SpaceEntityType.Player, (uint)index, new Vector3(0, 100f, 100f));
                    break;
                default:
                    Log.Add(LogSeverity.ERROR, string.Format("Unknown msgType \"{0}\" on {1}Protocol.", (Request)msgType, protocolID));
                    break;
            }
        }

        // No idea why but this makes the game load (???)
        // I guess it spawns the player in a position inside the Sector
        private void SyncMove(int index, SpaceEntityType spaceEntityType, uint objectId, Vector3 position, Vector3 eulert = default(Vector3))
        {
            BgoProtocolWriter buffer = NewMessage();

            buffer.Write((ushort)Reply.SyncMove);
            buffer.Write((uint)spaceEntityType + objectId);
            buffer.Write((int)1); // tick

            //position
            buffer.Write(new Vector3());

            //euler3
            buffer.Write(eulert);
            
            //linearSpeed
            buffer.Write(new Vector3());

            //strafeSpeed
            buffer.Write(new Vector3());

            //euler3speed
            buffer.Write(new Vector3());

            // mode
            buffer.Write((byte)2);

            buffer.Write((byte)2);
            buffer.Write((byte)2);
            buffer.Write((int)1);

            //position
            buffer.Write(new Vector3());

            //euler3
            buffer.Write(eulert);

            SendMessageToUser(index, buffer);
        }

        public void SendWhoIsPlayer(int index)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.WhoIs);
            buffer.Write((uint)SpaceEntityType.Player + (uint)index);
            buffer.Write((byte)CreatingCause.JumpIn);
            buffer.Write((uint)index); // The OwnerGUID. Since idk what it could be, just using his index
            buffer.Write((uint)Server.GetClientByIndex(index).Character.WorldCardGUID); // The WorldCardGUID which is the spaceship loaded.

            //nothing yet
            buffer.Write((ushort)0);
            buffer.Write((ushort)0);

            buffer.Write(Server.GetClientByIndex(index).playerId); //player id. Just using his index
            buffer.Write((uint)0x10); //player role //developer 0x10
            buffer.Write(true);

            SendMessageToUser(index, buffer);
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
