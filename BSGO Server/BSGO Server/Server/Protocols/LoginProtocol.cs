using System;

namespace BSGO_Server
{
    internal class LoginProtocol : Protocol
    {
        public enum Reply : ushort
        {
            Hello,
            Init,
            Error,
            Player,
            Wait,
            Echo
        }

        public enum Request : ushort
        {
            Init = 1,
            Player = 2,
            Echo = 5
        }

        public LoginProtocol()
            : base(ProtocolIDType.Login) {}

        public static LoginProtocol GetProtocol() =>
            ProtocolManager.GetProtocol(ProtocolIDType.Login) as LoginProtocol;
        
        public override void ParseMessage(int index, BgoProtocolReader br)
        {
            ushort msgType = br.ReadUInt16();

            switch ((Request)msgType)
            {
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
                                Server.GetClientByIndex(index).Character = new Character(index);
                                SendPlayer(index);
                                /*
                                if (Database.Database.CheckCharacterExistanceById(playerId.ToString())) {

                                }
                                */
                            }
                            else
                                SendError(index, (byte)LoginError.WrongSession);
                            break;
                        default:
                            Log.Add(LogSeverity.ERROR, "Unknown Connection Type " + connectType + " on Login Protocol");
                            break;
                    }
                       
                    break;
                default:
                    Log.Add(LogSeverity.ERROR, string.Format("Unknown msgType \"{0}\" on {1}Protocol.", (Request)msgType, ProtocolID));
                    break;
            }
        }

        public void SendConnectionOK(int index)
        {
            using BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)0);
            SendMessageToUser(index, buffer);
        }

        // Here we have to send the Server Revision. The latest game version have the revision number 4578.
        private void SendInit(int index)
        {
            using BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Request.Init);
            buffer.Write((uint)4578);
            SendMessageToUser(index, buffer);
        }

        // I'm not too sure about what the game wants here so I'm just sending the current time. It needs
        // 6 ints (years, months, days, hours, minutes, seconds), a long (serverconnectionTime) and your role
        // on the server. In this case we are going to send a dev role.
        private void SendPlayer(int index)
        {
            using BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)3);

            DateTime now = DateTime.Now;
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
            using BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.Error);

            buffer.Write(errorCode);

            SendMessageToUser(index, buffer);
        }
    }
}
