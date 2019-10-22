namespace BSGO_Server
{
    internal class StickerListCard : Card
    {
        public StickerListCard(uint cardGUID, CardView cardView)
            : base(cardGUID, cardView)
        {

        }

        //Since we don't have any stickers, let's just send empty lists
        public override void Write(BgoProtocolWriter w)
        {
            base.Write(w);
            w.Write((ushort)0);
            w.Write((ushort)0);
        }
    }
}
