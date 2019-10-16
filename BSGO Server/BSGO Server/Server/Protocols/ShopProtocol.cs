using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    class ShopProtocol : Protocol
    {
        public enum Request : ushort
        {
            Items = 1,
            Close = 11,
            BoughtShipSaleOffer = 13,
            EventShopItems = 0xF,
            AllSales = 17
        }

        public enum Reply : ushort
        {
            Items = 2,
            Sales = 3,
            UpgradeSales = 4,
            ShopPrices = 8,
            BoughtShipSaleOffer = 12,
            EventShopItems = 14,
            EventShopAvailable = 0x10
        }

        public ShopProtocol()
    : base(ProtocolID.Shop)
        {
        }

        public static ShopProtocol GetProtocol()
        {
            return ProtocolManager.GetProtocol(ProtocolID.Shop) as ShopProtocol;
        }

        public override void ParseMessage(int index, BgoProtocolReader br)
        {
            ushort msgType = (ushort)br.ReadUInt16();

            switch ((Request)msgType)
            {
                case Request.Items:
                    SendItems(index);
                    break;
                default:
                    Log.Add(LogSeverity.ERROR, string.Format("Unknown msgType \"{0}\" on {1}Protocol.", (Request)msgType, protocolID));
                    break;
            }
        }

        private void SendItems(int index)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.Items);
            buffer.Write((ushort)0); // No items since I don't know what to send.

            SendMessageToUser(index, buffer);
        }
    }
}
