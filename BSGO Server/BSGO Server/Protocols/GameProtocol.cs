using System;
using System.Collections.Generic;
using System.Text;

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
                    break;
                default:
                    Log.Add(LogSeverity.ERROR, string.Format("Unknown msgType \"{0}\" on {1}Protocol.", (Request)msgType, protocolID));
                    break;
            }
        }

        public void SendWhoIsPlayer(int index)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.WhoIs);
            buffer.Write((uint)SpaceEntityType.Player);
            buffer.Write((byte)CreatingCause.JumpIn);
            buffer.Write((uint)index); // The OwnerGUID. Since idk what it could be, just using his index
            buffer.Write((uint)Server.GetClientByIndex(index).Character.WorldCardGUID); // The WorldCardGUID which is the spaceship loaded.

            //nothing yet
            buffer.Write((ushort)0);
            buffer.Write((ushort)0);

            buffer.Write((uint)index); //player id. Just using his index
            buffer.Write((uint)0x10); //player role //developer 0x10
            buffer.Write(true);

            SendMessageToUser(index, buffer);
        }

        private void SetTimeOrigin(int index)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.TimeOrigin);
            buffer.Write((long)DateTime.UtcNow.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);

            SendMessageToUser(index, buffer);
        }
    }
}
