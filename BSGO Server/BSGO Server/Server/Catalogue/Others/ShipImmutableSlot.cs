namespace BSGO_Server
{
    internal class ShipImmutableSlot : IProtocolWrite
    {
        public ushort SlotId { get; set; }

        public ShipSlotType SystemType { get; set; }

        public ushort ObjectPointServerHash { get; set; }

        public uint SystemKey { get; set; }

        public uint SystemLevel { get; set; }

        public uint ConsumableKey { get; set; }

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
