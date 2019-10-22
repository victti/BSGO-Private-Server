using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    internal class FeedbackProtocol : Protocol
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
            : base(ProtocolID.Feedback)
        {
        }

        public static FeedbackProtocol GetProtocol()
        {
            return ProtocolManager.GetProtocol(ProtocolID.Feedback) as FeedbackProtocol;
        }

        public override void ParseMessage(int index, BgoProtocolReader br)
        {
            ushort messageId = br.ReadUInt16();
            ushort uiElement = br.ReadUInt16();

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
