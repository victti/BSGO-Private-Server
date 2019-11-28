using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    internal class DebugProtocol : Protocol
    {
        public enum Request : ushort
        {
            Command = 1,
            Activity = 12,
            ProcessState = 14,
            UpgradeSystem = 17
        }

        public enum Reply : ushort
        {
            Command = 2,
            Message = 3,
            Counters = 9,
            ProcessState = 0xF,
            UpdateRoles = 0x10
        }

        public DebugProtocol()
    : base(ProtocolID.Debug)
        {
        }

        public static DebugProtocol GetProtocol()
        {
            return ProtocolManager.GetProtocol(ProtocolID.Debug) as DebugProtocol;
        }

        public override void ParseMessage(int index, BgoProtocolReader br)
        {
            ushort msgType = br.ReadUInt16();

            switch ((Request)msgType)
            {
                case Request.Command:
                    switch (br.ReadString())
                    {
                        case "sector_op":
                            string soFaction = br.ReadString();
                            string soAddPoints = br.ReadString();

                            Faction soRealFaction = soFaction == "colonial" ? Faction.Colonial : Faction.Cylon;

                            Server.GetSectorByClientIndex(index).SetOutpost(soRealFaction, int.Parse(soAddPoints));
                            break;
                    }
                    break;
                default:
                    Log.Add(LogSeverity.ERROR, string.Format("Unknown msgType \"{0}\" on {1}Protocol.", (Request)msgType, protocolID));
                    break;
            }
        }
    }
}
