namespace BSGO_Server
{
    internal abstract class Card : IProtocolWrite
    {
        public uint CardGUID { get; set; }
        public CardView CardView { get; set; }

        protected Card(uint cardGUID, CardView cardView)
        {
            CardGUID = cardGUID;
            CardView = cardView;
        }

        public virtual void Write(BgoProtocolWriter w)
        {
            w.Write(CardGUID);
            w.Write((ushort)CardView);
        }
    }
}
