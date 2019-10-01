using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    class ShipConsumableCard : Card
    {
        public ushort ConsumableType;

        public ObjectStats ItemBuffMultiply;

        public ObjectStats ItemBuffAdd;

        public byte Tier;

        public AugmentActionType Action;

        public bool IsAugment;

        public bool AutoConsume;

        public bool Trashable;

        public uint buyCount;

        public ConsumableAttribute[] sortingAttributes;

        public ConsumableEffectType effectType;

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
