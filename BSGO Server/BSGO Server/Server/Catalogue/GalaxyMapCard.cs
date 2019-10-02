using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    class GalaxyMapCard : Card
    {
        public Dictionary<uint, MapStarDesc> Stars = new Dictionary<uint, MapStarDesc>();

        public int[] Tiers;

        public int BaseScalingMultiplier;

        public GalaxyMapCard(uint cardGUID, CardView cardView, Dictionary<uint, MapStarDesc> stars, int[] tiers, int baseScalingMultiplier)
            : base(cardGUID, cardView)
        {
            Stars = stars;
            Tiers = tiers;
            BaseScalingMultiplier = baseScalingMultiplier;
        }

        public override void Write(BgoProtocolWriter w)
        {
            base.Write(w);
            w.Write(Stars.Count);
            foreach(KeyValuePair<uint, MapStarDesc> star in Stars)
            {
                star.Value.Write(w);
            }
            int num2 = Tiers.Length;
            w.Write(num2);
            for (int j = 0; j < num2; j++)
            {
                w.Write(Tiers[j]);
            }
            w.Write(BaseScalingMultiplier);

            //Idk what's this about so I'll just send a length of 0 to cancel it
            w.Write((ushort)0);
        }

        public MapStarDesc GetStar(uint sectorId)
        {
            return Stars[sectorId];
        }
    }
}
