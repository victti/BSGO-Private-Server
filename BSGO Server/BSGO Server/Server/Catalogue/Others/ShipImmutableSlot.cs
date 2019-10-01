using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    class ShipImmutableSlot : IProtocolWrite
    {
        public ushort SlotId;

        public ShipSlotType SystemType;

        public ushort ObjectPointServerHash;

        public uint SystemKey;

        public uint SystemLevel;

        public uint ConsumableKey;

        public void Write(BgoProtocolWriter w)
        {
            w.Write(SlotId);
            w.Write(ObjectPointServerHash);
            w.Write((byte)SystemType);
            w.Write(SystemKey);
            w.Write((ushort)SystemLevel);
            w.Write(ConsumableKey);
        }
    }
}
