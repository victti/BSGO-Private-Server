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
            : base(ProtocolIDType.Setting) { }

        public static SettingProtocol GetProtocol()
            => ProtocolManager.GetProtocol(ProtocolIDType.Setting) as SettingProtocol;
        
        public override void ParseMessage(int index, BgoProtocolReader br)
        {
            ushort msgType = br.ReadUInt16();

            // ?
            switch (msgType)
            {
                default:
                    Log.Add(LogSeverity.ERROR, string.Format("Unknown msgType \"{0}\" on {1}Protocol.", (Request)msgType, ProtocolID));
                    break;
            }
        }
    }
}
