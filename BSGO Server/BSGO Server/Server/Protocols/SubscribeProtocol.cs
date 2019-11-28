using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    internal class SubscribeProtocol : Protocol
    {
        public enum Reply : ushort
        {
            PlayerName = 1,
            PlayerFaction,
            PlayerAvatar,
            PlayerShips,
            PlayerStatus,
            PlayerLocation,
            PlayerLevel,
            PlayerGuild,
            PlayerStats,
            PlayerTitle,
            PlayerMedal,
            PlayerLogout,
            [Obsolete("Now implemented in ZoneProtocol")]
            PlayerTournamentIndicator
        }

        public enum Request : ushort
        {
            Info = 1,
            Subscribe,
            Unsubscribe,
            SubscribeStats,
            UnsubscribeStats
        }

        public SubscribeProtocol()
            : base(ProtocolID.Subscribe)
        {
        }

        public static SubscribeProtocol GetProtocol()
        {
            return ProtocolManager.GetProtocol(ProtocolID.Subscribe) as SubscribeProtocol;
        }

        public override void ParseMessage(int index, BgoProtocolReader br)
        {
            ushort msgType = br.ReadUInt16();

            switch ((Request)msgType)
            {
                case Request.Info:
                    uint playerId = br.ReadUInt32();
                    uint flags = br.ReadUInt32();
                    Console.WriteLine("Info: PlayerId {0} flags {1}", playerId, flags);

                    SendName(index, playerId);
                    SendFaction(index, playerId);
                    SendPlayerAvatar(index, playerId);
                    SendPlayerLevel(index, playerId);
                    SendPlayerStatus(index, playerId, true);
                    CatalogueProtocol.GetProtocol().SendCard(index, CardView.GUI, (uint)Server.GetClientByPlayerId(playerId.ToString()).index);

                    switch (flags)
                    {
                        default:
                            Log.Add(LogSeverity.ERROR, "Unknown flag " + flags + " on Subscribe Info");
                            break;
                    }
                    break;
                case Request.Subscribe:
                    uint playerId2 = br.ReadUInt32();
                    uint flags2 = br.ReadUInt32();
                    Console.WriteLine("Subscribe: PlayerId {0} flags {1}", playerId2, flags2);

                    switch (flags2)
                    {
                        default:
                            Log.Add(LogSeverity.ERROR, "Unknown flag " + flags2 + " on Subscribe Info");
                            break;
                    }
                    break;
                case Request.SubscribeStats:
                    uint playerId3 = br.ReadUInt32();
                    Console.WriteLine("SubscribeStats: PlayerId {0}", playerId3);
                    break;
                default:
                    Log.Add(LogSeverity.ERROR, string.Format("Unknown msgType \"{0}\" on {1}Protocol.", (Request)msgType, protocolID));
                    break;
            }
        }

        public void SendName(int index, uint playerId)
        {
            BgoProtocolWriter buffer = NewMessage();

            buffer.Write((ushort)Reply.PlayerName);
            buffer.Write(playerId);
            buffer.Write(Server.GetClientByPlayerId(playerId.ToString()).Character.name);

            SendMessageToUser(index, buffer);
        }

        public void SendFaction(int index, uint playerId)
        {
            BgoProtocolWriter buffer = NewMessage();

            buffer.Write((ushort)Reply.PlayerFaction);
            buffer.Write(playerId);
            buffer.Write((byte)Server.GetClientByPlayerId(playerId.ToString()).Character.Faction);

            SendMessageToUser(index, buffer);
        }

        public void SendPlayerAvatar(int index, uint playerId)
        {
            BgoProtocolWriter buffer = NewMessage();

            buffer.Write((ushort)Reply.PlayerFaction);
            buffer.Write(playerId);

            Dictionary<AvatarItem, string> items = Server.GetClientByPlayerId(playerId.ToString()).Character.items;

            buffer.Write((ushort)items.Count);
            foreach (KeyValuePair<AvatarItem, string> item in items)
            {
                buffer.Write((byte)item.Key);
                buffer.Write(item.Value);
            }
            buffer.Write((ushort)0);
            buffer.Write((byte)0);

            SendMessageToUser(index, buffer);
        }

        public void SendPlayerLevel(int index, uint playerId)
        {
            BgoProtocolWriter buffer = NewMessage();

            buffer.Write((ushort)Reply.PlayerLevel);
            buffer.Write(playerId);
            buffer.Write((byte)1);

            SendMessageToUser(index, buffer);
        }

        public void SendPlayerStatus(int index, uint playerId, bool online)
        {
            BgoProtocolWriter buffer = NewMessage();

            buffer.Write((ushort)Reply.PlayerStatus);
            buffer.Write(playerId);
            buffer.Write(online);

            SendMessageToUser(index, buffer);
        }

        public void SendLocation(int index, uint playerId)
        {
            BgoProtocolWriter buffer = NewMessage();

            buffer.Write((ushort)Reply.PlayerLocation);
            buffer.Write(playerId);
            buffer.Write((byte)1);
            buffer.Write((uint)1427);

            SendMessageToUser(index, buffer);
        }
    }
}
