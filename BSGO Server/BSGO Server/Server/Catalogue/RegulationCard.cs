﻿using System.Collections.Generic;

namespace BSGO_Server
{
    internal class RegulationCard : Card
    {
        public ConsumableEffectType[] EffectTypeBlacklist { get; set; }
        public Dictionary<uint, HashSet<ShipAbilitySide>> AbilityTargetRelations { get; set; }
        public Dictionary<uint, HashSet<ShipAbilityTarget>> AbilityTargetTypes { get; set; }
        public TargetBracketMode TargetBracketMode { get; set; }
        public bool SectorMapEnabled { get; set; }

        public RegulationCard(uint cardGUID, CardView cardView, ConsumableEffectType[] effectTypeBlacklist, Dictionary<uint, HashSet<ShipAbilitySide>> abilityTargetRelations, Dictionary<uint, HashSet<ShipAbilityTarget>> abilityTargetTypes, TargetBracketMode targetBracketMode, bool sectorMapEnabled)
            : base(cardGUID, cardView)
        {
            this.EffectTypeBlacklist = effectTypeBlacklist;
            AbilityTargetRelations = abilityTargetRelations;
            AbilityTargetTypes = abilityTargetTypes;
            TargetBracketMode = targetBracketMode;
            SectorMapEnabled = sectorMapEnabled;
        }

        // I'm not sure how to send the hashset since the game only shows how to read them and tbh
        // I don't see how to make it.
        public override void Write(BgoProtocolWriter w)
        {
            base.Write(w);
            w.Write((byte)TargetBracketMode);
            w.Write(SectorMapEnabled);
            ushort num = (ushort)AbilityTargetRelations.Count;
            foreach (KeyValuePair<uint, HashSet<ShipAbilitySide>> pair in AbilityTargetRelations)
            {
                w.Write(pair.Key);

                foreach (ShipAbilitySide abilitySide in pair.Value)
                {
                    w.Write((ushort)abilitySide);
                }

                foreach (ShipAbilityTarget abilityTarget in AbilityTargetTypes[pair.Key])
                {
                    w.Write((ushort)abilityTarget);
                }
            }

            int num2 = EffectTypeBlacklist.Length;
            w.Write(num2);
            for (int j = 0; j < num2; j++)
            {
                w.Write((byte)EffectTypeBlacklist[j]);
            }
        }
    }
}
