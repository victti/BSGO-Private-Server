using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    class TitleCard : Card
    {
        public byte Level;

        public ObjectStats StaticBuff;

        public ObjectStats MultiplyBuff;

        public TitleCard(uint cardGUID, CardView cardView, byte level, ObjectStats staticBuff, ObjectStats multiplyBuff)
             : base(cardGUID, cardView)
        {
            Level = level;
            StaticBuff = staticBuff;
            MultiplyBuff = multiplyBuff;
        }

        public override void Write(BgoProtocolWriter w)
        {
            base.Write(w);
            w.Write(Level);
            w.Write("");
            StaticBuff.Write(w);
            MultiplyBuff.Write(w);
        }
    }
}
