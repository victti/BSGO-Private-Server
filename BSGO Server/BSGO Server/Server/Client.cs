using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BSGO_Server
{
    class Client
    {
        public int index { get; private set; }
        public string ip { get; set; }
        public Socket socket { get; set; }
        public bool closing { get; set; } = false;
        private readonly byte[] _buffer = new byte[65535];

        public readonly Dictionary<long, Card> cards = new Dictionary<long, Card>();

        public uint playerId { get; set; }
        private Loop loop = new Loop();
        public TimeSync TimeSync;
        public Character Character { get; set; }
        public Chat Chat { get; set; }

        private int needToReceive;
        private bool isReadLength;
        public uint debugReceiveData;
        private int packetLength;
        public uint debugReceiveDataMax;
        public Client(int index)
        {
            this.index = index;
        }

        public void StartClient()
        {
            TimeSync = new TimeSync(index);
            loop.OnUpdated = Update;
            loop.Initialize();

            if (Server.Async)
                AsyncStart();
            else
            {
                if(!Server.AlternativeSync)
                    SyncStart();
            }

            closing = false;
        }

        public DateTime lastSyncSendTime = DateTime.UtcNow;
        public void Update(float dt)
        {
            if (Server.AlternativeSync)
                HandleAlternativeConnection();

            if (!closing && Character != null && Character.GameLocation == GameLocation.Space && Character.PlayerShip.ManeuverController != null)
            {
                Character.PlayerShip.Update(dt);
                if (lastSyncSendTime.Subtract(DateTime.UtcNow).TotalSeconds < -0.5)
                {
                    lastSyncSendTime = DateTime.UtcNow;
                    GameProtocol.GetProtocol().SyncMove(index, SpaceEntityType.Player, Server.GetObjectId(index));
                }
            }
            if (!closing && Chat != null)
                Chat.Update(dt);
        }

        private void AsyncStart()
        {
            socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                int received = socket.EndReceive(ar);
                if (received <= 0)
                {
                    CloseClient(index);
                }
                else
                {
                    byte[] databuffer = new byte[received];
                    Buffer.BlockCopy(_buffer, 0, databuffer,0, received);

                    byte[] incomingMsg = new byte[2];
                    incomingMsg[0] = databuffer[0];
                    incomingMsg[1] = databuffer[1];
                    ushort packetLength = (ushort)(BgoProtocolReader.ReadBufferSize(incomingMsg) + 2);
                    byte[] parsedArray = new byte[packetLength];
                    Array.Copy(databuffer, 0, parsedArray, 0, packetLength);

                    // The first packet should always be right.
                    ProtocolManager.HandleNetworkInformation(index, parsedArray);

                    // If the first packet length isn't equal the received buffer length, then it received more than
                    // one packet on the buffer and it should be parsed to avoid any kind of problem.
                    // That's how Async works xd. There might be a better solution but this is what I could do.
                    if (packetLength != databuffer.Length)
                    {
                        ushort prevPacketLength = 0;
                        while (prevPacketLength + packetLength != databuffer.Length)
                        {
                            prevPacketLength += packetLength;
                            incomingMsg = new byte[2];
                            incomingMsg[0] = databuffer[prevPacketLength + 0];
                            incomingMsg[1] = databuffer[prevPacketLength + 1];
                            packetLength = (ushort)(BgoProtocolReader.ReadBufferSize(incomingMsg) + 2);
                            parsedArray = new byte[packetLength];
                            Array.Copy(databuffer, prevPacketLength, parsedArray, 0, packetLength);

                            ProtocolManager.HandleNetworkInformation(index, parsedArray);
                        }
                    }

                    socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
                }
            }
            catch (Exception ex)
            {
                Log.Add(LogSeverity.ERROR, "Exception thrown " + ex);
                CloseClient(index);
            }
        }

        private void CloseClient(int index)
        {
            if(Character != null)
                Server.GetSectorById(Character.PlayerShip.sectorId).LeaveSector(this, RemovingCause.JustRemoved);
            Log.Add(LogSeverity.INFO, string.Format("Connection from {0} has been terminated.", ip));
            closing = true;
            socket.Close();
            socket = null;
            Character = null;
        }

        public void CloseClient()
        {
            Server.GetSectorById(Character.PlayerShip.sectorId).LeaveSector(this, RemovingCause.JustRemoved);
            Log.Add(LogSeverity.INFO, string.Format("Connection from {0} has been terminated.", ip));
            closing = true;
            socket.Close();
            socket = null;
            Character = null;
            Server.Clients.Remove(index);
        }

        private void SyncStart()
        {
            HandleConnection();
        }

        private void HandleConnection()
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = tokenSource.Token;
            Task.Factory.StartNew(delegate
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    bool lockTaken = false;
                    try
                    {
                        Monitor.Enter(socket, ref lockTaken);
                        if (socket != null && socket.Connected)
                        {
                            if (socket.Poll(1000, SelectMode.SelectRead))
                            {
                                byte[] buffer = new byte[1];
                                if (socket.Receive(buffer, SocketFlags.Peek) == 0)
                                {
                                    throw new Exception("The socket has been closed");
                                }
                                byte[] array = new byte[2];
                                if (socket.Receive(array, 0, array.Length, SocketFlags.None) != 2)
                                {
                                    throw new Exception("Error receiving: packetLenght != 4");
                                }
                                int num = BgoProtocolReader.ReadBufferSize(array);
                                byte[] array2 = new byte[num];
                                if (socket.Receive(array2, 0, array2.Length, SocketFlags.None) != num)
                                {
                                    throw new Exception("Error receiving: receive length != packet length");
                                }
                                BgoProtocolReader buffer2 = new BgoProtocolReader(array2);
                                ProtocolManager.HandleNetworkInformation(index, buffer2);
                            }
                        }
                        else
                        {
                            tokenSource.Cancel();
                            tokenSource.Dispose();
                        }
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            Console.WriteLine("Exception: " + ex.Message);
                            Log.Add(LogSeverity.INFO, string.Format("Connection from {0} has been terminated.", ip));
                            Server.GetSectorById(Character.PlayerShip.sectorId).LeaveSector(this, RemovingCause.JustRemoved);
                            socket.Shutdown(SocketShutdown.Both);
                            socket.Close();
                            closing = true;
                            Character = null;
                            Server.Clients.Remove(index);
                        }
                        finally
                        {
                            if (socket != null)
                            {
                                socket.Dispose();
                            }
                        }
                    }
                    finally
                    {
                        if (lockTaken)
                        {
                            Monitor.Exit(socket);
                        }
                    }
                }
            }, cancellationToken).ContinueWith(delegate (Task x)
            {
                x.Dispose();
            });
        }

        private void HandleAlternativeConnection()
        {
            List<BgoProtocolReader> list = RecvMessage();
            foreach (BgoProtocolReader item in list)
            {
                Protocol.ProtocolID protocolID = (Protocol.ProtocolID)item.ReadByte();

                Log.Add(LogSeverity.INFO, Log.LogDir.In, string.Format("Protocol ID: {0} ({1})", (byte)protocolID, protocolID));

                //ushort num = item.ReadUInt16();
                try
                {
                    Protocol protocol = ProtocolManager.GetProtocol(protocolID);
                    protocol.ParseMessage(index, item);
                }
                catch (Exception ex)
                {
                    string text = "Couldn't handle message for " + protocolID + " Protocol. ";
                    if (ProtocolManager.GetProtocol(protocolID) == null)
                    {
                        text = text + protocolID + " Protocol is not (any more) registered. ";
                    }
                    text = text + "Caught Exception: " + ex.ToString();
                    Log.Add(LogSeverity.ERROR, text);
                }
            }
        }

        public List<BgoProtocolReader> RecvMessage()
        {
            List<BgoProtocolReader> list = new List<BgoProtocolReader>();
            try
            {
                if (!IsSocketConnected())
                {
                    throw new Exception("Connected is false");
                }
                if (socket.Available < 4)
                {
                    return list;
                }
                needToReceive = socket.Available;
                while (true)
                {
                    BgoProtocolReader bgoProtocolReader = RecvCurrentMessage();
                    if (bgoProtocolReader == null)
                    {
                        break;
                    }
                    list.Add(bgoProtocolReader);
                }
                return list;
            }
            catch (Exception arg)
            {
                //Disconnect("Error receiving: " + arg);
                CloseClient();
                return list;
            }
        }

        private BgoProtocolReader RecvCurrentMessage()
        {
            if (needToReceive < 4)
            {
                return null;
            }
            if (!isReadLength)
            {
                byte[] array = new byte[2];
                int num = socket.Receive(array, 0, array.Length, SocketFlags.None);
                needToReceive -= num;
                debugReceiveData += (uint)array.Length;
                if (num != 2)
                {
                    throw new Exception("receive length != 2");
                }
                packetLength = BgoProtocolReader.ReadBufferSize(array);
                isReadLength = true;
            }
            if (packetLength <= socket.Available)
            {
                byte[] array2 = new byte[packetLength];
                debugReceiveDataMax = Math.Max(debugReceiveDataMax, (uint)packetLength);
                int num2 = socket.Receive(array2, 0, array2.Length, SocketFlags.None);
                needToReceive -= num2;
                debugReceiveData += (uint)array2.Length;
                if (num2 != packetLength)
                {
                    throw new Exception("receive length != packet lenght");
                }
                isReadLength = false;
                return new BgoProtocolReader(array2);
            }
            return null;
        }

        private bool IsSocketConnected()
        {
            return socket != null && socket.Connected;
        }
    }
}
