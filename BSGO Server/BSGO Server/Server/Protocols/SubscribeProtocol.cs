using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    internal class SubscribeProtocol : Protocol
    {
        public enum Reply : ushort
        {
            PlayerName = 1,
            PlayerFaction,
            PlayerAvatar,
            PlayerShips,
            PlayerStatus,
            PlayerLocation,
            PlayerLevel,
            PlayerGuild,
            PlayerStats,
            PlayerTitle,
            PlayerMedal,
            PlayerLogout,
            [Obsolete("Now implemented in ZoneProtocol")]
            PlayerTournamentIndicator
        }

        public enum Request : ushort
        {
            Info = 1,
            Subscribe,
            Unsubscribe,
            SubscribeStats,
            UnsubscribeStats
        }

        public SubscribeProtocol()
            : base(ProtocolID.Subscribe)
        {
        }

        public static SubscribeProtocol GetProtocol()
        {
            return ProtocolManager.GetProtocol(ProtocolID.Subscribe) as SubscribeProtocol;
        }

        public override void ParseMessage(int index, BgoProtocolReader br)
        {
            ushort msgType = br.ReadUInt16();

            switch ((Request)msgType)
            {
                case Request.Info:
                    uint playerId = br.ReadUInt32();
                    uint flags = br.ReadUInt32();
                    break;
                case Request.Subscribe:
                    uint playerId2 = br.ReadUInt32();
                    uint flags2 = br.ReadUInt32();
                    break;
                default:
                    Log.Add(LogSeverity.ERROR, string.Format("Unknown msgType \"{0}\" on {1}Protocol.", (Request)msgType, protocolID));
                    break;
            }
        }

        public void SendName(int index, uint playerId)
        {
            BgoProtocolWriter buffer = NewMessage();

            buffer.Write((ushort)Reply.PlayerName);
            buffer.Write(playerId);
            buffer.Write("teste");

            SendMessageToUser(index, buffer);
        }

        public void SendFaction(int index, uint playerId)
        {
            BgoProtocolWriter buffer = NewMessage();

            buffer.Write((ushort)Reply.PlayerFaction);
            buffer.Write(playerId);
            buffer.Write((byte)Faction.Colonial);

            SendMessageToUser(index, buffer);
        }

        public void SendPlayerStatus(int index, uint playerId)
        {
            BgoProtocolWriter buffer = NewMessage();

            buffer.Write((ushort)Reply.PlayerStatus);
            buffer.Write(playerId);
            buffer.Write(true);

            SendMessageToUser(index, buffer);
        }

        public void SendLocation(int index, uint playerId)
        {
            BgoProtocolWriter buffer = NewMessage();

            buffer.Write((ushort)Reply.PlayerLocation);
            buffer.Write(playerId);
            buffer.Write((byte)1);
            buffer.Write((uint)1427);

            SendMessageToUser(index, buffer);
        }
    }
}
