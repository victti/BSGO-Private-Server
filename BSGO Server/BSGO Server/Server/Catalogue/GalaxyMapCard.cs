﻿using System.Collections.Generic;

namespace BSGO_Server
{
    internal class GalaxyMapCard : Card
    {
        public Dictionary<uint, MapStarDesc> Stars { get; set; } = new Dictionary<uint, MapStarDesc>();

        public int[] Tiers { get; set; }

        public int BaseScalingMultiplier { get; set; }

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
            w.Write((ushort)Stars.Count);
            foreach(KeyValuePair<uint, MapStarDesc> star in Stars)
            {
                star.Value.Write(w);
            }
            int num2 = Tiers.Length;
            w.Write((ushort)num2);
            for (int j = 0; j < num2; j++)
            {
                w.Write((ushort)Tiers[j]);
            }
            w.Write(BaseScalingMultiplier);

            //Idk what's this about so I'll just send a length of 0 to cancel it
            w.Write((ushort)0);
            w.Write(100);
            w.Write(200);
        }

        public MapStarDesc GetStar(uint sectorId) =>
            Stars[sectorId];
        
    }
}
