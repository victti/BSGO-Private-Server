using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{ 
    internal class RoomCard : Card
    {
        //doors
        //npcs
        //music

        public RoomCard(uint cardGUID, CardView cardView)
            : base(cardGUID, cardView)
        {

        }

        public override void Write(BgoProtocolWriter w)
        {
            base.Write(w);
            //doors
            w.Write((ushort)0);
            //npcs
            w.Write((ushort)0);
            //music
            w.Write("");
        }
    }
}
