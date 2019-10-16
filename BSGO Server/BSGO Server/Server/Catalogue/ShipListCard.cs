using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    class ShipListCard : Card
    {
        public List<ShipCard> ShipCards;

        public List<ShipCard> UpgradeShipCards;

        public List<ShipCard> VariantShipCards;

        public ShipListCard(uint cardGUID) : base(cardGUID, CardView.ShipList)
        {
            ShipCards = new List<ShipCard>();
            UpgradeShipCards = new List<ShipCard>();
        }

        public void AddShip(ShipCard shipCard)
        {
            ShipCards.Add(shipCard);
            UpgradeShipCards.Add(shipCard);
        }

        public override void Write(BgoProtocolWriter w)
        {
            base.Write(w);

            w.Write((ushort)ShipCards.Count);

            foreach (var shipCard in ShipCards)
            {
                w.Write(shipCard.CardGUID);
                w.Write(shipCard.CardGUID); // Should be fixed later
            }
        }
    }
}
