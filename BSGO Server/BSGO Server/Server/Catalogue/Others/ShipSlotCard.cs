using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    class ShipSlotCard : IProtocolWrite
    {
        public byte Level;
        public ushort SlotId;
        public ShipSlotType SystemType;
        public string ObjectPoint;
        public ushort ObjectPointServerHash;

        public ShipSlotCard(byte level, ushort slotId, ShipSlotType systemType, string objectPoint, ushort objectPointServerHash)
        {
            Level = level;
            SlotId = slotId;
            SystemType = systemType;
            ObjectPoint = objectPoint;
            ObjectPointServerHash = objectPointServerHash;
        }

        public void Write(BgoProtocolWriter w)
        {
            w.Write(SlotId);
            w.Write(ObjectPoint);
            w.Write(ObjectPointServerHash);
            w.Write((byte)SystemType);
            w.Write(Level);
        }
    }
}
