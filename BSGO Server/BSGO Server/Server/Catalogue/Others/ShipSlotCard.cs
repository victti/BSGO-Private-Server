namespace BSGO_Server
{
    internal class ShipSlotCard : IProtocolWrite
    {
        public byte Level { get; set; }
        public ushort SlotId { get; set; }
        public ShipSlotType SystemType { get; set; }
        public string ObjectPoint { get; set; }
        public ushort ObjectPointServerHash { get; set; }

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
