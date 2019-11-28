using System;

namespace BSGO_Server
{
    internal class LoginProtocol : Protocol
    {
        enum Reply : ushort
        {
            Hello,
            Init,
            Error,
            Player,
            Wait,
            Echo
        }

        enum Request : ushort
        {
            Init = 1,
            Player = 2,
            Echo = 5
        }

        public LoginProtocol()
            : base(ProtocolID.Login)
        {
        }

        public static LoginProtocol GetProtocol()
        {
            return ProtocolManager.GetProtocol(ProtocolID.Login) as LoginProtocol;
        }

        public override void ParseMessage(int index, BgoProtocolReader br)
        {
            ushort msgType = br.ReadUInt16();

            switch ((Request)msgType)
            {
                case Request.Echo:
                    BgoProtocolWriter buffer = NewMessage();
                    buffer.Write((ushort)Reply.Echo);
                    SendMessageToUser(index, buffer);
                    break;
                case Request.Init:
                    SendInit(index);
                    break;
                case Request.Player:
                    // This is the ConnectType, but we aren't checking for that yet
                    ConnectType connectType = (ConnectType)br.ReadByte();
                    // Check if the player exists on our database. We'll have checks for client connected later, but it's
                    // not necessary yet
                    uint playerId = br.ReadUInt32();
                    string playerName = br.ReadString();
                    string sessionCode = br.ReadString();
                    switch (connectType) {
                        case ConnectType.Web:
                            if (Database.Database.CheckSessionCodeExistance(sessionCode))
                            {
                                playerId = Convert.ToUInt32(Database.Database.GetUserBySession(sessionCode).PlayerId);
                                Server.GetClientByIndex(index).playerId = playerId;
                                InitLogin(index, playerId, playerName, sessionCode);
                            }
                            else
                            {
                                SendError(index, (byte)LoginError.WrongSession);
                                break;
                            }
                            break;
                        case ConnectType.DebugPlayerId:
                            if (Database.Database.CheckPlayerIdExistance(playerId))
                            {
                                Server.GetClientByIndex(index).playerId = playerId;
                                InitLogin(index, playerId, playerName, sessionCode);
                            }
                            else
                            {
                                SendError(index, (byte)LoginError.WrongPlayerId);
                                break;
                            }
                            break;
                        default:
                            Log.Add(LogSeverity.ERROR, "Unknown Connection Type " + connectType + " on Login Protocol");
                            break;
                    }
                       
                    break;
                default:
                    Log.Add(LogSeverity.ERROR, string.Format("Unknown msgType \"{0}\" on {1}Protocol.", (Request)msgType, protocolID));
                    break;
            }
        }

        private void InitLogin(int index, uint playerId, string playerName, string sessionCode)
        {
            Server.GetClientByIndex(index).Character = new Character(index);
            SendPlayer(index);

            if (Database.Database.CheckCharacterExistanceById(playerId.ToString()))
            {
                CatalogueProtocol.GetProtocol().SendCard(index, CardView.GUI, 130920111u);
                CatalogueProtocol.GetProtocol().SendCard(index, CardView.GUI, 264733124u);
                CatalogueProtocol.GetProtocol().SendCard(index, CardView.GUI, 215278030u);
                CatalogueProtocol.GetProtocol().SendCard(index, CardView.GUI, 207047790u);
                CatalogueProtocol.GetProtocol().SendCard(index, CardView.GUI, 130762195u);

                PlayerProtocol.GetProtocol().SendPlayerId(index);
                PlayerProtocol.GetProtocol().SendName(index);
                PlayerProtocol.GetProtocol().SendAvatar(index);
                PlayerProtocol.GetProtocol().SendFaction(index);
                if (Server.GetClientByIndex(index).Character.Faction == Faction.Colonial)
                {
                    PlayerProtocol.GetProtocol().SendPlayerShips(index, 11, 22131180u);
                    PlayerProtocol.GetProtocol().SendPlayerShips(index, 17, 22131178u);
                    PlayerProtocol.GetProtocol().SendPlayerShips(index, 14, 22131196u);
                }
                else if (Server.GetClientByIndex(index).Character.Faction == Faction.Cylon)
                {
                    PlayerProtocol.GetProtocol().SendCylonDuties(index); // I only have Cylon duties
                    PlayerProtocol.GetProtocol().SendPlayerShips(index, 11, 22131208u);
                    PlayerProtocol.GetProtocol().SendPlayerShips(index, 17, 22131210u);
                    PlayerProtocol.GetProtocol().SendPlayerShips(index, 14, 22131226u);
                }
                PlayerProtocol.GetProtocol().SetActivePlayerShip(index, 11);

                PlayerProtocol.GetProtocol().SendItems(index);

                Database.Entities.Users user = Database.Database.GetUserById(Server.GetClientByIndex(index).playerId.ToString());
                SettingProtocol.ReadSettingsFromDatabase(index, user.settings);
                if (user.controlKeys != null)
                    SettingProtocol.ReadControlKeysFromDatabase(index, user.controlKeys);
                SettingProtocol.GetProtocol().SendSettings(index);
                if (Server.GetClientByIndex(index).Character.controlKeys.Count > 0)
                    SettingProtocol.GetProtocol().SendKeys(index);
                CommunityProtocol.GetProtocol().SendChatSessionId(index);
            }
        }

        public void SendConnectionOK(int index)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)0);
            SendMessageToUser(index, buffer);
        }

        // Here we have to send the Server Revision. The latest game version have the revision number 4578.
        private void SendInit(int index)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Request.Init);
            buffer.Write((uint)4578);
            SendMessageToUser(index, buffer);
        }

        // I'm not too sure about what the game wants here so I'm just sending the current time. It needs
        // 6 ints (years, months, days, hours, minutes, seconds), a long (serverconnectionTime) and your role
        // on the server. In this case we are going to send a dev role.
        private void SendPlayer(int index)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)3);

            DateTime now = Server.serverStartTime;
            buffer.Write(now.Year);
            buffer.Write(now.Month);
            buffer.Write(now.Day);
            buffer.Write(now.Hour);
            buffer.Write(now.Minute);
            buffer.Write(now.Second);
            buffer.Write((long)now.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);
            buffer.Write((uint)BgoAdminRoles.Console); //0x10 is Dev Role

            SendMessageToUser(index, buffer);
        }

        private void SendError(int index, byte errorCode)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.Error);

            buffer.Write(errorCode);

            SendMessageToUser(index, buffer);
        }
    }
}
