using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    class WorldCard : Card
    {
        public string PrefabName;
        public int LODCount;
        public float Radius;
        public SpotDesc[] Spots;
        public string SystemMapTexture;
        public sbyte FrameIndex = -1;
        public sbyte SecondaryFrameIndex;
        public bool Targetable = true;
        public bool ShowBracketWhenInRange = true;
        public bool ForceShowOnMap;

        public WorldCard(uint cardGUID, CardView cardView, string prefabName, int lODCount, float radius, SpotDesc[] spots, string systemMapTexture, sbyte frameIndex, sbyte secondaryFrameIndex, bool targetable, bool showBracketWhenInRange, bool forceShowOnMap)
            : base(cardGUID, cardView)
        {
            PrefabName = prefabName;
            LODCount = lODCount;
            Radius = radius;
            Spots = spots;
            SystemMapTexture = systemMapTexture;
            FrameIndex = frameIndex;
            SecondaryFrameIndex = secondaryFrameIndex;
            Targetable = targetable;
            ShowBracketWhenInRange = showBracketWhenInRange;
            ForceShowOnMap = forceShowOnMap;
        }

        public override void Write(BgoProtocolWriter w)
        {
            base.Write(w);
            w.Write(PrefabName);
            w.Write((byte)LODCount);
            w.Write(Radius);
            w.Write((ushort)Spots.Length);
            foreach(SpotDesc spot in Spots)
            {
                spot.Write(w);
            }
            w.Write(SystemMapTexture);
            w.Write(FrameIndex);
            w.Write(SecondaryFrameIndex);
            w.Write(Targetable);
            w.Write(ShowBracketWhenInRange);
            w.Write(ForceShowOnMap);
        }
    }
}
