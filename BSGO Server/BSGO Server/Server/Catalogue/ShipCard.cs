using System.Collections.Generic;

namespace BSGO_Server
{
    internal class ShipCard : Card
    {
        public uint ShipObjectKey { get; set; }
        public byte Level { get; set; }
        public byte MaxLevel { get; set; }
        public byte LevelRequeriment { get; set; }
        public byte HangarID { get; set; }
        public uint Next { get; set; } // it's called num on the code and fetches other shipcard if not equals 0
        public float Durability { get; set; }
        public byte Tier { get; set; }
        public ShipRole[] ShipRoles { get; set; }
        public ShipRoleDeprecated ShipRoleDeprecated { get; set; }
        public string PaperdollUiLayoutfile { get; set; }
        public List<ShipSlotCard> Slots { get; set; }
        public bool CubitOnlyRepair { get; set; }
        public List<uint> VariantHangarIDs { get; set; }
        public int ParentHangerID { get; set; }
        public ObjectStats Stats { get; set; }
        public Faction Faction { get; set; }
        public List<ShipImmutableSlot> ImmutableSlots { get; set; }

        public ShipCard(uint cardGUID, CardView cardView, uint shipObjectKey, byte level, byte maxLevel, byte levelRequeriment, byte hangarID, uint next, float durability, byte tier, ShipRole[] shipRoles, ShipRoleDeprecated shipRoleDeprecated, string paperdollUiLayoutfile, List<ShipSlotCard> slots, bool cubitOnlyRepair, List<uint> variantHangarIDs, int parentHangerID, ObjectStats stats, Faction faction, List<ShipImmutableSlot> immutableSlots)
            : base(cardGUID, cardView)
        {
            ShipObjectKey = shipObjectKey;
            Level = level;
            MaxLevel = maxLevel;
            LevelRequeriment = levelRequeriment;
            HangarID = hangarID;
            this.Next = next;
            Durability = durability;
            Tier = tier;
            ShipRoles = shipRoles;
            ShipRoleDeprecated = shipRoleDeprecated;
            PaperdollUiLayoutfile = paperdollUiLayoutfile;
            Slots = slots;
            CubitOnlyRepair = cubitOnlyRepair;
            VariantHangarIDs = variantHangarIDs;
            ParentHangerID = parentHangerID;
            Stats = stats;
            Faction = faction;
            ImmutableSlots = immutableSlots;
        }

        public override void Write(BgoProtocolWriter w)
        {
            base.Write(w);
            w.Write(ShipObjectKey);
            w.Write(Level);
            w.Write(MaxLevel);
            w.Write(LevelRequeriment);
            w.Write(HangarID);
            w.Write(Next);
            w.Write(Durability);
            w.Write(Tier);
            int num = ShipRoles.Length;
            w.Write((ushort)num);
            for(int i = 0; i < num; i++)
                w.Write((byte)ShipRoles[i]);
            
            w.Write((byte)ShipRoleDeprecated);
            w.Write(PaperdollUiLayoutfile);
            int num2 = Slots.Count;
            w.Write((ushort)num2);
            for(int j= 0; j < num2; j++)
                Slots[j].Write(w);
            
            w.Write(CubitOnlyRepair);
            int num3 = VariantHangarIDs.Count;
            w.Write((ushort)num3);
            for (int k = 0; k < num3; k++)
                w.Write(VariantHangarIDs[k]);
            
            w.Write(ParentHangerID);
            Stats.Write(w);
            w.Write((byte)Faction);
            int num4 = ImmutableSlots.Count;
            w.Write((ushort)num4);
            for (int l = 0; l < num4; l++)
            {
                ImmutableSlots[l].Write(w);
            }
            w.Write((uint)1); // empty on the code
        }
    }
}
