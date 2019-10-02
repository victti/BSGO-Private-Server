using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    class OwnerCard : Card
    {
        public bool IsDockable;

        public float DockRange;

        public byte Level;

        public OwnerCard(uint cardGUID, CardView cardView, bool isDockable, float dockRange, byte level)
    : base(cardGUID, cardView)
        {
            IsDockable = isDockable;
            DockRange = dockRange;
            Level = level;
        }

        public override void Write(BgoProtocolWriter w)
        {
            base.Write(w);
            w.Write(IsDockable);
            w.Write(DockRange);
            w.Write(Level);
        }
    }
}
