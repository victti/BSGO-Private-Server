namespace BSGO_Server
{
    internal class ShopItemCard : Card
    {
        public ShopCategory Category { get; set; }

        public ShopItemType ItemType { get; set; }

        public byte Tier { get; set; }

        public string[] SortingNames { get; set; }

        public int SortingWeight { get; set; }

        public Price BuyPrice { get; set; } = new Price();

        public Price UpgradePrice { get; set; } = new Price();

        public Price SellPrice { get; set; } = new Price();

        public Faction Faction { get; set; }

        public bool CanBeSold { get; set; }

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
            w.Write((ushort)SortingNames.Length);
            foreach(string str in SortingNames)
            {
                w.Write(str);
            }
            w.Write((ushort)SortingWeight);
            BuyPrice.Write(w);
            UpgradePrice.Write(w);
            SellPrice.Write(w);
            w.Write(CanBeSold);
        }
    }
}
