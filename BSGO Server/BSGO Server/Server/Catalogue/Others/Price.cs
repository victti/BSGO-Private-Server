using System.Collections.Generic;

namespace BSGO_Server
{
    internal class Price : IProtocolWrite
    {
        public Dictionary<ShipConsumableCard, float> Items { get; set; } = new Dictionary<ShipConsumableCard, float>();

        public Price()
        {
        }

        public Price(Dictionary<ShipConsumableCard, float> items)
        {
            Items = items;
        }

        public void Write(BgoProtocolWriter w)
        {
            w.Write(Items.Count);
            foreach(KeyValuePair<ShipConsumableCard, float> item in Items)
            {
                w.Write(item.Key.CardGUID);
                w.Write(item.Value);
            }
        }
    }
}
