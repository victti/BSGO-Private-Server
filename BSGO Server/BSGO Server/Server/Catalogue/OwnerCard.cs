namespace BSGO_Server
{
    internal class OwnerCard : Card
    {
        public bool IsDockable { get; set; }

        public float DockRange { get; set; }

        public byte Level { get; set; }

        public OwnerCard(uint cardGUID, CardView cardView, bool isDockable, float dockRange, byte level)
            : base(cardGUID, cardView)
        {
            IsDockable = isDockable;
            DockRange = dockRange;
            Level = level;
        }

        public override void Write(BgoProtocolWriter w)
        {
            base.Write(w);
            w.Write(IsDockable);
            w.Write(DockRange);
            w.Write(Level);
        }
    }
}
