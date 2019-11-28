using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BSGO_Server
{
    internal class ProtocolManager
    {
        private static Dictionary<Protocol.ProtocolID, Protocol> protocols;

        public static void InitProtocols()
        {
            Log.Add(LogSeverity.SERVERINFO, "Initializing Protocols");

            protocols = new Dictionary<Protocol.ProtocolID, Protocol>();
            RegisterProtocol(new Protocol[] {
                new LoginProtocol(), //
                new SyncProtocol(), //
                new SceneProtocol(), //
                new SettingProtocol(), //
                new CatalogueProtocol(), //
                new GameProtocol(), //
                new PlayerProtocol(), //
                new ShopProtocol(), //
                new CommunityProtocol(), //
                new FeedbackProtocol(), //
                new StoryProtocol(), //
                new SubscribeProtocol(), //
                new RoomProtocol(), //
                new UniverseProtocol(), //
                new DebugProtocol(), //
            });

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
                string text = "";
                try
                {
                    text += "Couldn't handle message for " + (Protocol.ProtocolID)protocolID + " Protocol";
                } catch
                {

                }
                try
                {
                    text += " (msgType: " + buffer.ReadUInt16() + "). ";
                } catch
                {

                }
                if (GetProtocol((Protocol.ProtocolID)protocolID) == null)
                {
                    text = text + protocolID + " Protocol is not (any more) registered. ";
                }
                text = text + "\nCaught Exception: " + ex;
                Log.Add(LogSeverity.ERROR, text);
            }
        }

        public static void HandleNetworkInformation(int index, BgoProtocolReader buffer)
        {
            byte b = buffer.ReadByte();
            Log.Add(LogSeverity.INFO, Log.LogDir.In, string.Format("Protocol ID: {0} ({1})", b, (Protocol.ProtocolID)b));
            try
            {
                GetProtocol((Protocol.ProtocolID)b).ParseMessage(index, buffer);
            }
            catch (Exception ex)
            {
                string text = "Couldn't handle message for " + (Protocol.ProtocolID)b + " Protocol (msgType:" + buffer.ReadUInt16() + "). ";
                if (GetProtocol((Protocol.ProtocolID)b) == null)
                {
                    text = text + b + " Protocol is not (any more) registered. ";
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

        private static void RegisterProtocol(params Protocol[] passedProtocols)
        {
            foreach (Protocol current in passedProtocols)
                protocols.Add(current.protocolID, current);
        }
    }
}
