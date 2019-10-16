using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace BSGO_Server
{
    class MapStarDesc : IProtocolWrite
    {
        public uint Id;

        public Vector2 Position;

        public byte GUIIndex;

        public Faction StarFaction;

        public int ColonialThreatLevel;

        public int CylonThreatLevel;

        public uint SectorGUID;

        public bool CanColonialOutpost;

        public bool CanCylonOutpost;

        public bool CanColonialJumpBeacon;

        public bool CanCylonJumpBeacon;

        public MapStarDesc(uint id, Vector2 position, byte gUIIndex, Faction starFaction, int colonialThreatLevel, int cylonThreatLevel, uint sectorGUID, bool canColonialOutpost, bool canCylonOutpost, bool canColonialJumpBeacon, bool canCylonJumpBeacon)
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
