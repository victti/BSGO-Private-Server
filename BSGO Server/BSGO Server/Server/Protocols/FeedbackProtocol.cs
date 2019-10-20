namespace BSGO_Server
{
    class FeedbackProtocol : Protocol
    {
        public enum MessageId : ushort
        {
            UiElementShown,
            UiElementHidden
        }

        public enum UiElementId : ushort
        {
            ShopWindow,
            RepairWindow,
            ShipShop,
            ShipCustomizationWindow,
            HangarWindow,
            ChangeAmmoMenu,
            InflightShop
        }

        public FeedbackProtocol()
            : base(ProtocolIDType.Feedback)
        {
        }

        public static FeedbackProtocol GetProtocol() =>
            ProtocolManager.GetProtocol(ProtocolIDType.Feedback) as FeedbackProtocol;
        
        public override void ParseMessage(int index, BgoProtocolReader br)
        {
            ushort uiElement = br.ReadUInt16();
            ushort messageId = br.ReadUInt16();

            switch (messageId)
            {
                case 0:
                    Log.Add(LogSeverity.INFO, string.Format("The element {0} is shown.", (UiElementId)uiElement));
                    break;
                case 1:
                    Log.Add(LogSeverity.INFO, string.Format("The element {0} is hidden.", (UiElementId)uiElement));
                    break;
            }
        }
    }
}
