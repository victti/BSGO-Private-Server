using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    internal class SyncProtocol : Protocol
    {
        public enum Request : ushort
        {
            SyncRequest
        }

        public enum Reply : ushort
        {
            SyncReply = 1
        }

        public SyncProtocol()
            : base(ProtocolID.Sync)
        {
        }

        public static SyncProtocol GetProtocol()
        {
            return ProtocolManager.GetProtocol(ProtocolID.Sync) as SyncProtocol;
        }

        public override void ParseMessage(int index, BgoProtocolReader br)
        {
            ushort msgType = br.ReadUInt16();

            switch ((Request)msgType)
            {
                case Request.SyncRequest:
                    SendSync(index);
                    break;
                default:
                    Log.Add(LogSeverity.ERROR, string.Format("Unknown msgType \"{0}\" on {1}Protocol.", msgType, protocolID));
                    break;
            }
        }

        // Sends the current milliseconds of the server to the client. It is requested every few seconds
        // 3 times in a row.
        private void SendSync(int index)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)1);
            buffer.Write((ulong)DateTime.UtcNow.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);
            SendMessageToUser(index,buffer);
        }
    }
}
