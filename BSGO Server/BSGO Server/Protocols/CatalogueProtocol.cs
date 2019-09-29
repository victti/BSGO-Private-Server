using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    class CatalogueProtocol : Protocol
    {
        public enum Request : ushort
        {
            Card = 1
        }

        public enum Reply : ushort
        {
            Card = 2
        }

        public CatalogueProtocol()
    : base(ProtocolID.Catalogue)
        {
        }

        public override void ParseMessage(int index, BgoProtocolReader br)
        {
            ushort msgType = (ushort)br.ReadUInt16();

            switch (msgType)
            {
                case 1:
                    ushort num = br.ReadUInt16();

                    for(int i = 0; i < num; i++)
                    {
                        uint key = br.ReadUInt32();
                        ushort value = br.ReadUInt16();

                        Log.Add(LogSeverity.WARNING, string.Format("Received a card request: Num={0}, Key={1}, Value={2}", num, key, value));

                        SendCard(index, value, key);
                    }

                    break;
                default:
                    Log.Add(LogSeverity.ERROR, string.Format("Unknown msgType \"{0}\" on {1}Protocol.", msgType, protocolID));
                    break;
            }
        }

        private void SendCard(int index, ushort cardView, uint cardGuid)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.Card);

            Card card = Catalogue.FetchCard(cardGuid, (CardView)cardView);
            if (card != null)
            {
                card.Write(buffer);
                SendMessageToUser(index, buffer);
            }
        }
    }
}
