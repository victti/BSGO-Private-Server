namespace BSGO_Server
{
    internal class WorldCard : Card
    {
        public string PrefabName { get; set; }
        public int LODCount { get; set; }
        public float Radius { get; set; }
        public SpotDesc[] Spots { get; set; }
        public string SystemMapTexture { get; set; }
        public sbyte FrameIndex { get; set; } = -1;
        public sbyte SecondaryFrameIndex { get; set; }
        public bool Targetable { get; set; } = true;
        public bool ShowBracketWhenInRange { get; set; } = true;
        public bool ForceShowOnMap { get; set; }

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
                spot.Write(w);
            
            w.Write(SystemMapTexture);
            w.Write(FrameIndex);
            w.Write(SecondaryFrameIndex);
            w.Write(Targetable);
            w.Write(ShowBracketWhenInRange);
            w.Write(ForceShowOnMap);
        }
    }
}
