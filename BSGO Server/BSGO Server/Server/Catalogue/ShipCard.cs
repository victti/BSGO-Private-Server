using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    class ShipCard : Card
    {
        public uint ShipObjectKey;
        public byte Level;
        public byte MaxLevel;
        public byte LevelRequeriment;
        public byte HangarID;
        public uint next; // it's called num on the code and fetches other shipcard if not equals 0
        public float Durability;
        public byte Tier;
        public ShipRole[] ShipRoles;
        public ShipRoleDeprecated ShipRoleDeprecated;
        public string PaperdollUiLayoutfile;
        public List<ShipSlotCard> Slots;
        public bool CubitOnlyRepair;
        public List<uint> VariantHangarIDs;
        public int ParentHangerID;
        public ObjectStats Stats;
        public Faction Faction;
        public List<ShipImmutableSlot> ImmutableSlots;

        public ShipCard(uint cardGUID, CardView cardView, uint shipObjectKey, byte level, byte maxLevel, byte levelRequeriment, byte hangarID, uint next, float durability, byte tier, ShipRole[] shipRoles, ShipRoleDeprecated shipRoleDeprecated, string paperdollUiLayoutfile, List<ShipSlotCard> slots, bool cubitOnlyRepair, List<uint> variantHangarIDs, int parentHangerID, ObjectStats stats, Faction faction, List<ShipImmutableSlot> immutableSlots)
            : base(cardGUID, cardView)
        {
            ShipObjectKey = shipObjectKey;
            Level = level;
            MaxLevel = maxLevel;
            LevelRequeriment = levelRequeriment;
            HangarID = hangarID;
            this.next = next;
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
            w.Write(next);
            w.Write(Durability);
            w.Write(Tier);
            int num = ShipRoles.Length;
            w.Write((ushort)num);
            for(int i = 0; i < num; i++)
            {
                w.Write((byte)ShipRoles[i]);
            }
            w.Write((byte)ShipRoleDeprecated);
            w.Write(PaperdollUiLayoutfile);
            int num2 = Slots.Count;
            w.Write((ushort)num2);
            for(int j= 0; j < num2; j++)
            {
                Slots[j].Write(w);
            }
            w.Write(CubitOnlyRepair);
            int num3 = VariantHangarIDs.Count;
            w.Write((ushort)num3);
            for (int k = 0; k < num3; k++)
            {
                w.Write(VariantHangarIDs[k]);
            }
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
