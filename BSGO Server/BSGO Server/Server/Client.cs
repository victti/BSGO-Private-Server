using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
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
        public Character Character { get; set; }

        public Client(int index)
        {
            this.index = index;
        }

        public void StartClient()
        { 
            socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
            closing = false;
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
                    ProtocolManager.HandleNetworkInformation(index, databuffer);
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
    }
}
