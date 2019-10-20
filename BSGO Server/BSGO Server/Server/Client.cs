using System;
using System.Net.Sockets;

namespace BSGO_Server
{
    internal class Client
    {
        public int Index { get; set; }
        public string Ip { get; set; }
        public Socket Socket { get; set; }
        public bool Closing { get; set; } = false;
        private readonly byte[] _buffer = new byte[65535];

        public uint PlayerId { get; set; }
        public Character Character { get; set; }

        public void StartClient()
        { 
            Socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), Socket);
            Closing = false;
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            Socket socket = (Socket)ar.AsyncState;

            try
            {
                int received = socket.EndReceive(ar);
                if (received <= 0)
                {
                    CloseClient(Index);
                }
                else
                {
                    byte[] databuffer = new byte[received];
                    Array.Copy(_buffer, databuffer, received);
                    ProtocolManager.HandleNetworkInformation(Index, databuffer);
                    socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
                }
            }
            catch (Exception ex)
            {
                Log.Add(LogSeverity.ERROR, "Exception thrown " + ex);
                CloseClient(Index);
            }
        }

        // Unused param index, wip?
        private void CloseClient(int index)
        {
            Log.Add(LogSeverity.INFO, string.Format("Connection from {0} has been terminated.", Ip));
            Closing = true;
            Socket.Close();
        }
    }
}
