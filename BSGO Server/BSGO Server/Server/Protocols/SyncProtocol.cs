using System;

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
            : base(ProtocolIDType.Sync) { }

        public static SyncProtocol GetProtocol() =>
            ProtocolManager.GetProtocol(ProtocolIDType.Sync) as SyncProtocol;
        
        public override void ParseMessage(int index, BgoProtocolReader br)
        {
            ushort msgType = br.ReadUInt16();

            switch ((Request)msgType)
            {
                case Request.SyncRequest:
                    SendSync(index);
                    break;
                default:
                    Log.Add(LogSeverity.ERROR, string.Format("Unknown msgType \"{0}\" on {1}Protocol.", msgType, ProtocolID));
                    break;
            }
        }

        // Sends the current milliseconds of the server to the client. It is requested every few seconds
        // 3 times in a row.
        private void SendSync(int index)
        {
            using BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)1);
            buffer.Write((ulong)DateTime.UtcNow.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);
            SendMessageToUser(index,buffer);
        }
    }
}
