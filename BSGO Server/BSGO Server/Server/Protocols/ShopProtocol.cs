using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    internal class ShopProtocol : Protocol
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
            ushort msgType = br.ReadUInt16();

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
            Faction faction = Server.GetClientByIndex(index).Character.Faction;

            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.Items);
            buffer.Write((ushort)17);
            //ships
            if(faction == Faction.Colonial)
                writeColShips(buffer);
            else if (faction == Faction.Cylon)
                writeCylShips(buffer);

            buffer.Write((ushort)436);
            buffer.Write((byte)2);
            buffer.Write(215278030); //titanium
            buffer.Write((uint)100);

            buffer.Write((ushort)438);
            buffer.Write((byte)2);
            buffer.Write(207047790); //titanium
            buffer.Write((uint)100);

            SendMessageToUser(index, buffer);
        }

        private void writeCylShips(BgoProtocolWriter buffer)
        {
            buffer.Write((ushort)1);
            buffer.Write((byte)4);
            buffer.Write(1427261742u); //raider

            buffer.Write((ushort)2);
            buffer.Write((byte)4);
            buffer.Write(22131202u); //heavy raider

            //buffer.Write((ushort)3);
            //buffer.Write((byte)4);
            //buffer.Write(22131202u); //heavy raider FR

            buffer.Write((ushort)4);
            buffer.Write((byte)4);
            buffer.Write(22131204u); // Marauder

            buffer.Write((ushort)5);
            buffer.Write((byte)4);
            buffer.Write(22131206u); // Raider mk2

            buffer.Write((ushort)6);
            buffer.Write((byte)4);
            buffer.Write(22131210u); // Malefactor

            buffer.Write((ushort)7);
            buffer.Write((byte)4);
            buffer.Write(22131208u); // advWarRaider

            buffer.Write((ushort)8);
            buffer.Write((byte)4);
            buffer.Write(22131214u); // Wraith

            buffer.Write((ushort)9);
            buffer.Write((byte)4);
            buffer.Write(22131212u); // Banshee

            buffer.Write((ushort)10);
            buffer.Write((byte)4);
            buffer.Write(22131216u); // Spectre

            buffer.Write((ushort)11);
            buffer.Write((byte)4);
            buffer.Write(22131218u); // Liche

            buffer.Write((ushort)12);
            buffer.Write((byte)4);
            buffer.Write(22131220u); // Fenrir

            buffer.Write((ushort)13);
            buffer.Write((byte)4);
            buffer.Write(22131224u); // Hel

            buffer.Write((ushort)14);
            buffer.Write((byte)4);
            buffer.Write(22131222u); // Jormung

            buffer.Write((ushort)15);
            buffer.Write((byte)4);
            buffer.Write(22131226u); // Nidhogg

            buffer.Write((ushort)16);
            buffer.Write((byte)4);
            buffer.Write(22131228u); // Surtur
        }

        private void writeColShips(BgoProtocolWriter buffer)
        {
            buffer.Write((ushort)1);
            buffer.Write((byte)4);
            buffer.Write(22131170u); //mk2

            buffer.Write((ushort)2);
            buffer.Write((byte)4);
            buffer.Write(22131172u); //raptor

            //buffer.Write((ushort)3);
            //buffer.Write((byte)4);
            //buffer.Write(22131200u); //raptor FR

            buffer.Write((ushort)4);
            buffer.Write((byte)4);
            buffer.Write(22131174u); // Rhino

            buffer.Write((ushort)5);
            buffer.Write((byte)4);
            buffer.Write(22131176u); // Mk3

            buffer.Write((ushort)6);
            buffer.Write((byte)4);
            buffer.Write(22131178u); // Raven

            buffer.Write((ushort)7);
            buffer.Write((byte)4);
            buffer.Write(22131180u); // advMk7

            buffer.Write((ushort)8);
            buffer.Write((byte)4);
            buffer.Write(22131182u); // Maul

            buffer.Write((ushort)9);
            buffer.Write((byte)4);
            buffer.Write(22131184u); // Foice

            buffer.Write((ushort)10);
            buffer.Write((byte)4);
            buffer.Write(22131186u); // Gladius

            buffer.Write((ushort)11);
            buffer.Write((byte)4);
            buffer.Write(22131188u); // Haldberd

            buffer.Write((ushort)12);
            buffer.Write((byte)4);
            buffer.Write(22131190u); // Aesir

            buffer.Write((ushort)13);
            buffer.Write((byte)4);
            buffer.Write(22131192u); // Vanir

            buffer.Write((ushort)14);
            buffer.Write((byte)4);
            buffer.Write(22131194u); // Jotunn

            buffer.Write((ushort)15);
            buffer.Write((byte)4);
            buffer.Write(22131196u); // Gungnir

            buffer.Write((ushort)16);
            buffer.Write((byte)4);
            buffer.Write(22131198u); // Brimir
        }
    }
}
