using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    class Price : IProtocolWrite
    {
        public Dictionary<ShipConsumableCard, float> items = new Dictionary<ShipConsumableCard, float>();

        public Price()
        {

        }

        public Price(Dictionary<ShipConsumableCard, float> items)
        {
            this.items = items;
        }

        public void Write(BgoProtocolWriter w)
        {
            w.Write(items.Count);
            foreach(KeyValuePair<ShipConsumableCard, float> item in items)
            {
                w.Write(item.Key.CardGUID);
                w.Write(item.Value);
            }
        }
    }
}
