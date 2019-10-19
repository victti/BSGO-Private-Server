using System;

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
            : base(ProtocolIDType.Subscribe) { }
        

        public static SubscribeProtocol GetProtocol() =>
            ProtocolManager.GetProtocol(ProtocolIDType.Subscribe) as SubscribeProtocol;
        
        public override void ParseMessage(int index, BgoProtocolReader br)
        {
            ushort msgType = br.ReadUInt16();

            switch ((Request)msgType)
            {
                case Request.Info:
                    /* ?
                    uint playerId = br.ReadUInt32();
                    uint flags = br.ReadUInt32();
                    */
                    break;
                default:
                    Log.Add(LogSeverity.ERROR, string.Format("Unknown msgType \"{0}\" on {1}Protocol.", (Request)msgType, ProtocolID));
                    break;
            }
        }
        /* ?
        public void SendInfo(int index)
        {

        }
        */
    }
}
