using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    class SubscribeProtocol : Protocol
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
            ushort msgType = (ushort)br.ReadUInt16();

            switch ((Request)msgType)
            {
                case Request.Info:
                    uint playerId = br.ReadUInt32();
                    uint flags = br.ReadUInt32();
                    break;
                default:
                    Log.Add(LogSeverity.ERROR, string.Format("Unknown msgType \"{0}\" on {1}Protocol.", (Request)msgType, protocolID));
                    break;
            }
        }

        public void SendInfo(int index)
        {

        }
    }
}
