using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace BSGO_Server
{
    class Client
    {
        public int index;
        public string ip;
        public Socket socket;
        public bool closing = false;
        private byte[] _buffer = new byte[65535];
        private Character character;
        public Character Character => character;

        public void StartClient()
        {
            // We are going to use the index as the character id for debug purposes and the fake database.
            character = new Character(index, index, Database.GetLastGameLocation());

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
                Log.Add(LogSeverity.ERROR, "Exception thrown " + ex.ToString());
                CloseClient(index);
            }
        }

        private void CloseClient(int index)
        {
            Log.Add(LogSeverity.INFO, string.Format("Connection from {0} has been terminated.", ip));
            closing = true;
            socket.Close();
        }
    }
}
