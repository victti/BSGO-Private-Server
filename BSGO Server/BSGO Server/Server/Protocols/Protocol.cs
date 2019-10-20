using System;

namespace BSGO_Server
{
    internal abstract class Protocol
    {
        public enum ProtocolIDType : byte
        {
            Login,
            Universe,
            Game,
            Sync,
            Player,
            Debug,
            Catalogue,
            Ranking,
            Story,
            Scene,
            Room,
            Community,
            Shop,
            Setting,
            Ship,
            Dialog,
            Market,
            Notification,
            Subscribe,
            Feedback,
            [Obsolete("Tournament functionality is now implemented as part of ZoneProtocol")]
            Tournament,
            Arena,
            Battlespace,
            Wof,
            Zone
        }

        public ProtocolIDType ProtocolID { get; }

        private readonly bool enabled;

        protected Protocol(ProtocolIDType protocolID)
        {
            ProtocolID = protocolID;
            enabled = true;
        }

        public abstract void ParseMessage(int index, BgoProtocolReader br);

        protected BgoProtocolWriter NewMessage()
        {
            BgoProtocolWriter bgoProtocolWriter = new BgoProtocolWriter();
            bgoProtocolWriter.Write((byte)ProtocolID);
            return bgoProtocolWriter;
        }

        protected void SendMessageToUser(int index, BgoProtocolWriter bw)
        {
            if (enabled)
            {
                Server.SendDataToClient(index, bw);
                DebugMessage(bw);
            }
            else
                Log.Add(LogSeverity.ERROR, string.Format("Trying to send message to \"{0}\" for disabled protocol \"{1}\"", index, ProtocolID));

            bw.Dispose();
        }

        // unused params, method is unfinished?
        protected void SendMessageToSector(/* int index, BgoProtocolWriter bw */)
        {
            // ?
            if (enabled)
            {

            }
            else
            {
                //Log.Add(LogSeverity.ERROR, string.Format("Trying to send message to the Sector \"{0}\" for disabled protocol \"{1}\"", Server.Sectors[index], protocolID));
            }
        }

        // unused params, method is unfinished?
        protected void SendMessageToEveryone(/* BgoProtocolWriter bw */)
        {
            // ?
            if (enabled)
            {

            }
            else
            {
                Log.Add(LogSeverity.ERROR, string.Format("Trying to send message to everyone for disabled protocol \"{0}\"", ProtocolID));
            }
        }

        private void DebugMessage(BgoProtocolWriter w)
        {
            using BgoProtocolReader buffer = new BgoProtocolReader(w.GetBuffer());
            buffer.ReadUInt16();
            byte protocolId = buffer.ReadByte();
            Log.Add(LogSeverity.INFO, Log.LogDir.Out, string.Format("Protocol ID: {0} ({1}) - msgType: {2}", protocolId, (ProtocolIDType)protocolId, buffer.ReadUInt16()));
        }
    }
}
