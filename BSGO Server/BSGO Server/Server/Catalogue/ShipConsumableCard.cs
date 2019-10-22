namespace BSGO_Server
{
    internal class ShipConsumableCard : Card
    {
        public ushort ConsumableType { get; set; }

        public ObjectStats ItemBuffMultiply { get; set; }

        public ObjectStats ItemBuffAdd { get; set; }

        public byte Tier { get; set; }

        public AugmentActionType Action { get; set; }

        public bool IsAugment { get; set; }

        public bool AutoConsume { get; set; }

        public bool Trashable { get; set; }

        public uint buyCount { get; set; }

        public ConsumableAttribute[] sortingAttributes { get; set; }

        public ConsumableEffectType effectType { get; set; }

        public ShipConsumableCard(uint cardGUID, CardView cardView, ushort consumableType, ObjectStats itemBuffMultiply, ObjectStats itemBuffAdd, byte tier, AugmentActionType action, bool isAugment, bool autoConsume, bool trashable, uint buyCount, ConsumableAttribute[] sortingAttributes, ConsumableEffectType effectType)
    : base(cardGUID, cardView)
        {
            ConsumableType = consumableType;
            ItemBuffMultiply = itemBuffMultiply;
            ItemBuffAdd = itemBuffAdd;
            Tier = tier;
            Action = action;
            IsAugment = isAugment;
            AutoConsume = autoConsume;
            Trashable = trashable;
            this.buyCount = buyCount;
            this.sortingAttributes = sortingAttributes;
            this.effectType = effectType;
        }

        public override void Write(BgoProtocolWriter w)
        {
            base.Write(w);
            w.Write(ConsumableType);
            w.Write(Tier);
            ItemBuffMultiply.Write(w);
            ItemBuffAdd.Write(w);
            w.Write((byte)Action);
            w.Write(IsAugment);
            w.Write(AutoConsume);
            w.Write(Trashable);
            w.Write(buyCount);
            w.Write(sortingAttributes.Length);
            foreach(ConsumableAttribute atr in sortingAttributes)
            {
                w.Write(atr.Attribute);
            }
            w.Write((byte)effectType);
        }
    }
}
