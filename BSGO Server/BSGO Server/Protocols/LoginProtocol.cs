using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    class LoginProtocol : Protocol
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
            ushort msgType = (ushort)br.ReadUInt16();

            switch ((Request)msgType)
            {
                case Request.Init:
                    SendInit(index);
                    break;
                case Request.Player:
                    SendPlayer(index);
                    break;
                default:
                    Log.Add(LogSeverity.ERROR, string.Format("Unknown msgType \"{0}\" on {1}Protocol.", (Request)msgType, protocolID));
                    break;
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

            DateTime now = DateTime.Now;
            buffer.Write(now.Year);
            buffer.Write(now.Month);
            buffer.Write(now.Day);
            buffer.Write(now.Hour);
            buffer.Write(now.Minute);
            buffer.Write(now.Second);
            buffer.Write((long)now.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);
            buffer.Write((uint)0x10); //0x10 is Dev Role

            SendMessageToUser(index, buffer);
        }
    }
}
