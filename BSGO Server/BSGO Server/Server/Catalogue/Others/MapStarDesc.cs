using BSGO_Server._3dAlgorithm;

namespace BSGO_Server
{
    internal class MapStarDesc : IProtocolWrite
    {
        public uint Id { get; set; }

        public System.Numerics.Vector2 Position { get; set; }

        public byte GUIIndex { get; set; }

        public Faction StarFaction { get; set; }

        public int ColonialThreatLevel { get; set; }

        public int CylonThreatLevel { get; set; }

        public uint SectorGUID { get; set; }

        public bool CanColonialOutpost { get; set; }

        public bool CanCylonOutpost { get; set; }

        public bool CanColonialJumpBeacon { get; set; }

        public bool CanCylonJumpBeacon { get; set; }

        public MapStarDesc(uint id, System.Numerics.Vector2 position, byte gUIIndex, Faction starFaction, int colonialThreatLevel, int cylonThreatLevel, uint sectorGUID, bool canColonialOutpost, bool canCylonOutpost, bool canColonialJumpBeacon, bool canCylonJumpBeacon)
        {
            Id = id;
            Position = position;
            GUIIndex = gUIIndex;
            StarFaction = starFaction;
            ColonialThreatLevel = colonialThreatLevel;
            CylonThreatLevel = cylonThreatLevel;
            SectorGUID = sectorGUID;
            CanColonialOutpost = canColonialOutpost;
            CanCylonOutpost = canCylonOutpost;
            CanColonialJumpBeacon = canColonialJumpBeacon;
            CanCylonJumpBeacon = canCylonJumpBeacon;
        }

        public void Write(BgoProtocolWriter w)
        {
            w.Write(Id);
            w.Write(Position);
            w.Write(GUIIndex);
            w.Write((byte)StarFaction);
            w.Write((short)ColonialThreatLevel);
            w.Write((short)CylonThreatLevel);
            w.Write(SectorGUID);
            w.Write(CanColonialOutpost);
            w.Write(CanCylonOutpost);
            w.Write(CanColonialJumpBeacon);
            w.Write(CanCylonJumpBeacon);
        }
    }
}
