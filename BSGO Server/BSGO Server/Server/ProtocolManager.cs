using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    class ProtocolManager
    {
        private static Dictionary<Protocol.ProtocolID, Protocol> protocols;

        public static void InitProtocols()
        {
            Log.Add(LogSeverity.SERVERINFO, "Initializing Protocols");

            protocols = new Dictionary<Protocol.ProtocolID, Protocol>();
            RegisterProtocol(new LoginProtocol());
            RegisterProtocol(new SyncProtocol());
            RegisterProtocol(new SceneProtocol());
            RegisterProtocol(new SettingProtocol());
            RegisterProtocol(new CatalogueProtocol());
            RegisterProtocol(new GameProtocol());
            RegisterProtocol(new PlayerProtocol());
            RegisterProtocol(new ShopProtocol());
            RegisterProtocol(new CommunityProtocol());
            Log.Add(LogSeverity.SERVERINFO, "Finished Initializing the Protocols");
        }

        public static void HandleNetworkInformation(int index, byte[] data)
        {
            BgoProtocolReader buffer = new BgoProtocolReader(data);
            buffer.ReadUInt16();
            byte protocolID = buffer.ReadByte();

            Log.Add(LogSeverity.INFO, Log.LogDir.In, string.Format("Protocol ID: {0} ({1})", protocolID, (Protocol.ProtocolID)protocolID));

            try
            {
                GetProtocol((Protocol.ProtocolID)protocolID).ParseMessage(index, buffer);
            }
            catch (Exception ex)
            {
                string text = "Couldn't handle message for " + (Protocol.ProtocolID)protocolID + " Protocol (msgType:" + buffer.ReadUInt16() + "). ";
                if (GetProtocol((Protocol.ProtocolID)protocolID) == null)
                {
                    text = text + protocolID + " Protocol is not (any more) registered. ";
                }
                text = text + "\nCaught Exception: " + ex.ToString();
                Log.Add(LogSeverity.ERROR, text);
            }
            buffer.Dispose();
        }

        public static Protocol GetProtocol(Protocol.ProtocolID protoID)
        {
            if (!protocols.ContainsKey(protoID))
            {
                return null;
            }
            return protocols[protoID];
        }

        private static void RegisterProtocol(Protocol protocol)
        {
            protocols.Add(protocol.protocolID, protocol);
        }
    }
}
