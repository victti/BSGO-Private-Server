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

        public uint playerId { get; set; }
        public TimeSync TimeSync;
        public Character Character { get; set; }

        public Client(int index)
        {
            this.index = index;
        }

        public void StartClient()
        {
            TimeSync = new TimeSync();
            // Use this for Async
            //AsyncStart();

            // User this for not Async
            SyncStart();

            closing = false;
        }

        private void AsyncStart()
        {
            socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            Socket socket = (Socket)ar.AsyncState;

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
                    Array.Copy(_buffer, databuffer, received);
                    Task.Run(() => ProtocolManager.HandleNetworkInformation(index, databuffer));
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
            Server.GetSectorById(Character.sectorId).LeaveSector(this);
            Log.Add(LogSeverity.INFO, string.Format("Connection from {0} has been terminated.", ip));
            closing = true;
            socket.Close();
            socket = null;
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
                            Server.GetSectorById(Character.sectorId).LeaveSector(this);
                            socket.Shutdown(SocketShutdown.Both);
                            socket.Close();
                            closing = true;
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
    }
}
