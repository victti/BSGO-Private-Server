using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    class ShopItemCard : Card
    {
        public ShopCategory Category;

        public ShopItemType ItemType;

        public byte Tier;

        public string[] SortingNames;

        public int SortingWeight;

        public Price BuyPrice = new Price();

        public Price UpgradePrice = new Price();

        public Price SellPrice = new Price();

        public Faction Faction;

        public bool CanBeSold;

        public ShopItemCard(uint cardGUID, CardView cardView, ShopCategory category, ShopItemType itemType, byte tier, string[] sortingNames, int sortingWeight, Price buyPrice, Price upgradePrice, Price sellPrice, Faction faction, bool canBeSold)
    : base(cardGUID, cardView)
        {
            Category = category;
            ItemType = itemType;
            Tier = tier;
            SortingNames = sortingNames;
            SortingWeight = sortingWeight;
            BuyPrice = buyPrice;
            UpgradePrice = upgradePrice;
            SellPrice = sellPrice;
            Faction = faction;
            CanBeSold = canBeSold;
        }

        public override void Write(BgoProtocolWriter w)
        {
            base.Write(w);
            w.Write((byte)Category);
            w.Write((byte)ItemType);
            w.Write(Tier);
            w.Write((byte)Faction);
            w.Write(SortingNames.Length);
            foreach(string str in SortingNames)
            {
                w.Write(str);
            }
            w.Write(SortingWeight);
            BuyPrice.Write(w);
            UpgradePrice.Write(w);
            SellPrice.Write(w);
            w.Write(CanBeSold);
        }
    }
}
