using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    class GlobalCard : Card
    {
        public float TitaniumRepairCard = 1f;
        public float CubitsRepairCard = 1f;
        public uint CapitalShipPrice;
        public float UndockTimeout;
        public RewardCard FriendBonus;
        public Dictionary<int, RewardCard> SpecialFriendBonus = new Dictionary<int, RewardCard>();

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
