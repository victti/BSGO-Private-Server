using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    internal class DutyCard : Card
    {
        public byte Level;
        public byte MaxLevel;
        public uint next;
        public uint cardGUID2;
        public int CounterValue;
        public int Experience;
        public uint cardGUID3;

        public DutyCard(uint cardGUID, CardView cardView, byte level, byte maxLevel, uint next, uint cardGUID2, int counterValue, int experience, uint cardGUID3)
            : base(cardGUID, cardView)
        {
            Level = level;
            MaxLevel = maxLevel;
            this.next = next;
            this.cardGUID2 = cardGUID2;
            CounterValue = counterValue;
            Experience = experience;
            this.cardGUID3 = cardGUID3;
        }

        public override void Write(BgoProtocolWriter w)
        {
            base.Write(w);
            w.Write(Level);
            w.Write(MaxLevel);
            w.Write(next);
            w.Write(cardGUID2);
            w.Write(CounterValue);
            w.Write(Experience);
            w.Write(cardGUID3);
        }
    }
}
