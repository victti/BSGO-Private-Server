using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    abstract class Card : IProtocolWrite
    {
        private uint cardGUID;
        private CardView cardView;
        public uint CardGUID
        {
            get
            {
                return cardGUID;
            }
            set
            {
                cardGUID = value;
            }
        }
        public CardView CardView
        {
            get
            {
                return cardView;
            }
            set
            {
                cardView = value;
            }
        }

        public Card(uint cardGUID, CardView cardView)
        {
            this.cardGUID = cardGUID;
            this.cardView = cardView;
        }

        public virtual void Write(BgoProtocolWriter w)
        {
            w.Write(cardGUID);
            w.Write((ushort)cardView);
        }
    }
}
