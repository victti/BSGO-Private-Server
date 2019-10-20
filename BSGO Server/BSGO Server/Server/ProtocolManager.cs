using System;
using System.Collections.Generic;

namespace BSGO_Server
{
    internal static class ProtocolManager
    {
        private static Dictionary<Protocol.ProtocolIDType, Protocol> protocols;

        public static void InitProtocols()
        {
            Log.Add(LogSeverity.SERVERINFO, "Initializing Protocols");

            protocols = new Dictionary<Protocol.ProtocolIDType, Protocol>();
            RegisterProtocol(new Protocol[] {
                new LoginProtocol(),
                new SyncProtocol(),
                new SceneProtocol(),
                new SettingProtocol(),
                new CatalogueProtocol(),
                new GameProtocol(),
                new PlayerProtocol(),
                new ShopProtocol(),
                new CommunityProtocol(),
                new FeedbackProtocol(),
                new StoryProtocol(),
                new SubscribeProtocol()
            });
            Log.Add(LogSeverity.SERVERINFO, "Finished Initializing the Protocols");
        }

        public static void HandleNetworkInformation(int index, byte[] data)
        {
            using BgoProtocolReader buffer = new BgoProtocolReader(data);
            buffer.ReadUInt16();
            byte protocolID = buffer.ReadByte();

            Log.Add(LogSeverity.INFO, Log.LogDir.In, string.Format("Protocol ID: {0} ({1})", protocolID, (Protocol.ProtocolIDType)protocolID));

            try
            {
                GetProtocol((Protocol.ProtocolIDType)protocolID).ParseMessage(index, buffer);
            }
            catch (Exception ex)
            {
                string text = string.Empty;
                /*
                 * stop using empty catch blocks eventually
                 */
                try
                {
                    text += "Couldn't handle message for " + (Protocol.ProtocolIDType)protocolID + " Protocol";
                } catch
                {
                    // throw;
                }
                try
                {
                    text += " (msgType: " + buffer.ReadUInt16() + "). ";
                } catch
                {
                    // throw;
                }
                if (GetProtocol((Protocol.ProtocolIDType)protocolID) is null)
                {
                    text = text + protocolID + " Protocol is not (any more) registered. ";
                }
                text = text + "\nCaught Exception: " + ex;
                Log.Add(LogSeverity.ERROR, text);
            }
        }

        public static Protocol GetProtocol(Protocol.ProtocolIDType protoID)
        {
            if (!protocols.ContainsKey(protoID))
                return null;
            
            return protocols[protoID];
        }

        private static void RegisterProtocol(params Protocol[] passedProtocols)
        {
            foreach (Protocol current in passedProtocols)
                protocols.Add(current.ProtocolID, current);
        }
        
    }
}
