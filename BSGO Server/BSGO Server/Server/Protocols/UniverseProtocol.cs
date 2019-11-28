using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    internal class UniverseProtocol : Protocol
    {
        public enum Request : ushort
        {
            SubscribeGalaxyMap = 5,
            UnsubscribeGalaxyMap
        }

        public enum Reply : ushort
        {
            Update = 7
        }

        public UniverseProtocol()
    : base(ProtocolID.Universe)
        {
        }

        public static UniverseProtocol GetProtocol()
        {
            return ProtocolManager.GetProtocol(ProtocolID.Universe) as UniverseProtocol;
        }

        public override void ParseMessage(int index, BgoProtocolReader br)
        {
            ushort msgType = br.ReadUInt16();

            switch ((Request)msgType)
            {
                case Request.SubscribeGalaxyMap:
                    uint[] sectorIds = new uint[Server.Sectors.Count];
                    int num = 0;
                    foreach (Sector sector in Server.Sectors)
                    {
                        sectorIds[num] = sector.sectorId;
                        num++;
                    }
                    SendSectorPlayerSlotsToPlayer(index, sectorIds, Server.GetClientByIndex(index).Character.Faction, SectorSlotCapType.Total);
                    break;
                default:
                    Log.Add(LogSeverity.ERROR, string.Format("Unknown msgType \"{0}\" on {1}Protocol.", (Request)msgType, protocolID));
                    break;
            }
        }
        public void SendSectorPlayerSlotsToPlayer(int index, uint[] sectorIds, Faction faction, SectorSlotCapType sectorSlotCapType)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.Update);

            buffer.Write((ushort)sectorIds.Length);
            foreach (uint sectorId in sectorIds)
            {
                buffer.Write((byte)GalaxyUpdateType.SectorPlayerSlots);
                buffer.Write(sectorId);
                buffer.Write((byte)faction);
                buffer.Write((ushort)1);
                buffer.Write((byte)0);
                buffer.Write((byte)sectorSlotCapType);
                buffer.Write((uint)Server.GetSectorById(sectorId).clients.Count);
                buffer.Write(2147483647u);
            }

            SendMessageToUser(index, buffer);
        }
    }
}
