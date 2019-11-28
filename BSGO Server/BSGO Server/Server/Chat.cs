using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace BSGO_Server
{
    internal class Chat
    {
        public int index { get; private set; }
        public int cIndex { get; set; }
        public string ChatSessionId
        {
            get
            {
                return cIndex.ToString();
            }
        }
        public Socket socket { get; set; }
        public bool IsDebug = true;
        private byte eofMsgCode;
        private List<byte> buffer;

        private Loop loop = new Loop();
        private bool firstUpdateRan = false;
        public Chat(int index)
        {
            this.index = index;
            byte[] bytes = Encoding.UTF8.GetBytes("#");
            eofMsgCode = bytes[0];
            buffer = new List<byte>();
            loop.OnUpdated = FirstUpdate;
            loop.Initialize();

        }

        public void FirstUpdate(float dt)
        {
            if (firstUpdateRan)
                return;

            if (socket.Connected)
            {
                int available = socket.Available;
                if (available > 0)
                {
                    byte[] array = new byte[available];
                    int num = socket.Receive(array, available, SocketFlags.None);
                    for (int i = 0; i < num; i++)
                    {
                        if (array[i] != 0)
                        {
                            buffer.Add(array[i]);
                        }
                    }
                    int num2;
                    do
                    {
                        num2 = -1;
                        for (int j = 0; j < buffer.Count; j++)
                        {
                            if (buffer[j] != eofMsgCode)
                            {
                                num2 = available - 1;
                                byte[] array2 = new byte[num2];
                                buffer.CopyTo(0, array2, 0, num2);
                                buffer.RemoveRange(0, num2 + 1);
                                string @string = Encoding.UTF8.GetString(array2);
                                if (IsDebug)
                                {
                                    Log.Add(LogSeverity.INFO, "ChatProto, read string:>" + @string + "<");
                                }
                                OnMessage(@string);
                                //for (int k = 0; k < parsers.Count; k++)
                                //{
                                //    if (parsers[k].TryParse(@string))
                                //    {
                                //        OnMessage(parsers[k]);
                                //        break;
                                //    }
                                //}
                                break;
                            }
                        }
                    }
                    while (num2 != -1);
                }
            }
        }

        public void Update(float dt)
        {
            if (socket.Connected)
            {
                int available = socket.Available;
                if (available > 0)
                {
                    byte[] array = new byte[available];
                    int num = socket.Receive(array, available, SocketFlags.None);
                    for (int i = 0; i < num; i++)
                    {
                        if (array[i] != 0)
                        {
                            buffer.Add(array[i]);
                        }
                    }
                    int num2;
                    do
                    {
                        num2 = -1;
                        for (int j = 0; j < buffer.Count; j++)
                        {
                            if (buffer[j] != eofMsgCode)
                            {
                                num2 = available - 1;
                                byte[] array2 = new byte[num2];
                                buffer.CopyTo(0, array2, 0, num2);
                                buffer.RemoveRange(0, num2 + 1);
                                string @string = Encoding.UTF8.GetString(array2);
                                if (IsDebug)
                                {
                                    Log.Add(LogSeverity.INFO, "ChatProto, read string:>" + @string + "<");
                                }
                                OnMessage(@string);
                                //for (int k = 0; k < parsers.Count; k++)
                                //{
                                //    if (parsers[k].TryParse(@string))
                                //    {
                                //        OnMessage(parsers[k]);
                                //        break;
                                //    }
                                //}
                                break;
                            }
                        }
                    }
                    while (num2 != -1);
                }
            }
        }

        private void OnMessage(string message)
        {
            string[] parsedMessage = message.Split('%');
            string messageProtocol = parsedMessage[0];
            int roomId = int.Parse(parsedMessage[1]);
            switch (messageProtocol)
            {
                case "bu": // Login Request
                    string[] buParsedBody = parsedMessage[2].Split('@');
                    string buUsername = buParsedBody[0];
                    string buUserId = buParsedBody[1];
                    string buChatSessionId = buParsedBody[2];
                    string buChatProjectId = buParsedBody[3];
                    string buChatLanguage = buParsedBody[4];
                    string buClan = buParsedBody[5];
                    string buIdk = buParsedBody[6];
                    string buIdk2 = buParsedBody[7];
                    string buFaction = buParsedBody[8];
                    string buIdk3 = buParsedBody[9];

                    Client buOwner = Server.GetClientByPlayerId(buUserId);
                    if (buChatProjectId == Server.ChatProjectID.ToString() && buOwner != null && buOwner.Character.name == buUsername && buChatSessionId == buOwner.index.ToString() && ((byte)buOwner.Character.Faction).ToString() == buFaction)
                    {
                        buOwner.Chat = this;
                    }

                    firstUpdateRan = true;
                    break;
                default:

                    break;
            }
        }
    }
}
