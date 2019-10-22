using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    internal class SettingProtocol : Protocol
    {
        public enum Request : ushort
        {
            SaveSettings = 1,
            SaveKeys = 2,
            SetSyfyShip = 5,
            SetFullScreen = 6
        }

        public enum Reply : ushort
        {
            Settings = 3,
            Keys
        }

        public SettingProtocol()
            : base(ProtocolID.Setting)
        {
        }

        public static SettingProtocol GetProtocol()
        {
            return ProtocolManager.GetProtocol(ProtocolID.Setting) as SettingProtocol;
        }

        public override void ParseMessage(int index, BgoProtocolReader br)
        {
            ushort msgType = br.ReadUInt16();

            switch ((Request)msgType)
            {
                default:
                    Log.Add(LogSeverity.ERROR, string.Format("Unknown msgType \"{0}\" on {1}Protocol.", (Request)msgType, protocolID));
                    break;
            }
        }
    }
}
