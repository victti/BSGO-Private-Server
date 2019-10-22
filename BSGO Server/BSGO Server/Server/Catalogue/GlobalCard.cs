using System.Collections.Generic;

namespace BSGO_Server
{
    internal class GlobalCard : Card
    {
        public float TitaniumRepairCard { get; set; } = 1f;
        public float CubitsRepairCard { get; set; } = 1f;
        public uint CapitalShipPrice { get; set; }
        public float UndockTimeout { get; set; }
        public RewardCard FriendBonus { get; set; }
        public Dictionary<int, RewardCard> SpecialFriendBonus { get; set; } = new Dictionary<int, RewardCard>();

        public GlobalCard(uint cardGUID, CardView cardView, float titaniumRepairCard, float cubitsRepairCard, uint capitalShipPrice, float undockTimeout, RewardCard friendBonus, Dictionary<int, RewardCard> specialFriendBonus)
            : base(cardGUID, cardView)
        {
            TitaniumRepairCard = titaniumRepairCard;
            CubitsRepairCard = cubitsRepairCard;
            CapitalShipPrice = capitalShipPrice;
            UndockTimeout = undockTimeout;
            FriendBonus = friendBonus;
            SpecialFriendBonus = specialFriendBonus;
        }

        public override void Write(BgoProtocolWriter w)
        {
            base.Write(w);
            w.Write(TitaniumRepairCard);
            w.Write(CubitsRepairCard);
            w.Write(CapitalShipPrice);
            w.Write(UndockTimeout);
            w.Write(FriendBonus.CardGUID);
            w.Write(SpecialFriendBonus.Count);

            foreach (KeyValuePair<int, RewardCard> friendBonus in SpecialFriendBonus)
            {
                w.Write((byte)friendBonus.Key);
                w.Write(friendBonus.Value.CardGUID);
            }
        }
    }
}
