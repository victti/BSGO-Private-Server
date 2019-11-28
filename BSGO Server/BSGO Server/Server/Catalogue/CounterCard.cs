using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    internal class CounterCard : Card
    {
        public string Name;

        public CounterCard(uint cardGUID, CardView cardView, string Name)
    : base(cardGUID, cardView)
        {
            this.Name = Name;
        }

        public override void Write(BgoProtocolWriter w)
        {
            base.Write(w);
            w.Write(Name);
        } 
    }
}
