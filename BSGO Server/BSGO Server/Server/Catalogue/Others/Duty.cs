using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    internal class Duty : IProtocolWrite
    {
        public ushort serverID;
        public uint cardGUID;
        
        public Duty(ushort serverID, uint cardGUID)
        {
            this.serverID = serverID;
            this.cardGUID = cardGUID;
        }

        public void Write(BgoProtocolWriter w)
        {
            w.Write(serverID);
            w.Write(cardGUID);
        }
    }
}
