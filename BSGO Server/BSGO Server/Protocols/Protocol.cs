using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    class Protocol
    {
        public enum ProtocolID : byte
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

        public readonly ProtocolID protocolID;

        private bool enabled;

        public Protocol(ProtocolID protocolID)
        {
            this.protocolID = protocolID;
            enabled = true;
        }

        public virtual void ParseMessage(int index, BgoProtocolReader br) { }

        protected BgoProtocolWriter NewMessage()
        {
            BgoProtocolWriter bgoProtocolWriter = new BgoProtocolWriter();
            bgoProtocolWriter.Write((byte)protocolID);
            return bgoProtocolWriter;
        }

        protected void SendMessageToUser(int index, BgoProtocolWriter bw)
        {
            if (enabled)
            {
                Server.SendDataTo(index, bw);
            }
            else
            {
                Log.Add(LogSeverity.ERROR, string.Format("Trying to send message to \"{0}\" for disabled protocol \"{1}\"", index, protocolID));
            }
        }

        protected void SendMessageToSector(int index, BgoProtocolWriter bw)
        {
            if (enabled)
            {

            }
            else
            {
                //Log.Add(LogSeverity.ERROR, string.Format("Trying to send message to the Sector \"{0}\" for disabled protocol \"{1}\"", Server.Sectors[index], protocolID));
            }
        }

        protected void SendMessageToEveryone(BgoProtocolWriter bw)
        {
            if (enabled)
            {

            }
            else
            {
                Log.Add(LogSeverity.ERROR, string.Format("Trying to send message to everyone for disabled protocol \"{0}\"", protocolID));
            }
        }
    }
}
