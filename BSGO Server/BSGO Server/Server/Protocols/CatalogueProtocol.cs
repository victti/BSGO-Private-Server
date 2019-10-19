namespace BSGO_Server
{
    internal class CatalogueProtocol : Protocol
    {
        public enum Request : byte
        {
            Card = 1
        }

        public enum Reply : byte
        {
            Card = 2
        }

        public CatalogueProtocol()
            : base(ProtocolID.Catalogue)
        {
        }

        public static CatalogueProtocol GetProtocol()
        {
            return ProtocolManager.GetProtocol(ProtocolID.Catalogue) as CatalogueProtocol;
        }

        public override void ParseMessage(int index, BgoProtocolReader br)
        {
            ushort msgType = br.ReadUInt16();

            switch (msgType)
            {
                case 1:
                    ushort num = br.ReadUInt16();

                    for(int i = 0; i < num; i++)
                    {
                        uint key = br.ReadUInt32();
                        ushort value = br.ReadUInt16();
                        SendCard(index, value, key);
                    }
                    break;
                default:
                    Log.Add(LogSeverity.ERROR, string.Format("Unknown msgType \"{0}\" on {1}Protocol.", msgType, ProtocolID));
                    break;
            }
        }

        private void SendCard(int index, ushort cardView, uint cardGuid)
        {
            using BgoProtocolWriter buffer = NewMessage();
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
