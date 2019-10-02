using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace BSGO_Server
{
    class Server
    {
        private static Socket _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


        // Default port should be 27050 since the game connects to that port by default.
        private static int PORT = 27050;

        // The buffer size IS meant to be 65535 since the game expects to receive that length.
        private static byte[] _buffer = new byte[65535];

        // Since we only need at least one player to run the game, I'll make the array of clients have the
        // length of 2 if we ever need to test multiplayer features such as sync and squads.
        private static int MaxPlayers = 2;
        private static Client[] _clients = new Client[MaxPlayers];

        // The game will connect to: SERVERIP:27050
        public static void InitServer()
        {
            Log.Add(LogSeverity.SERVERINFO, "Initializing the server");

            for (int i = 0; i < MaxPlayers; i++)
            {
                _clients[i] = new Client();
            }
            _serverSocket.NoDelay = true;
            _serverSocket.Bind(new IPEndPoint(IPAddress.Any, PORT));
            _serverSocket.Listen(10);
            _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);

            Log.Add(LogSeverity.SERVERINFO, string.Format("The server is now running on port {0}", PORT));
        }

        private static void AcceptCallback(IAsyncResult ar)
        {
            Socket socket = _serverSocket.EndAccept(ar);
            _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);

            for (int i = 1; i < MaxPlayers; i++)
            {
                if (_clients[i].socket == null)
                {
                    _clients[i].socket = socket;
                    _clients[i].index = i;
                    _clients[i].ip = socket.RemoteEndPoint.ToString();
                    _clients[i].StartClient();

                    // We should send the ConnectionOK method from the LoginProtocol otherwise the game
                    // will be stuck on a "connecting" screen with no errors since it is just waiting for
                    // this to be sent.
                    LoginProtocol.GetProtocol().SendConnectionOK(i);

                    Log.Add(LogSeverity.INFO, string.Format("Connection from '{0}' received.", _clients[i].ip));
                    return;
                }
            }
        }

        // This method will be ran by the protocols in order to send the message to ONE specific Client
        // determined by his index on the client array.
        // In order to send data to a Sector or to other Clients use the other methods.
        public static void SendDataTo(int index, BgoProtocolWriter message)
        {
            foreach (Client clients in _clients)
            {
                if (clients.socket != null && !clients.closing && clients.index == index)
                {
                    clients.socket.Send(message.GetBuffer(), 0, message.GetLength(), SocketFlags.None);
                }
            }
        }

        // This method will return you the client by his index. Commonly used since the received data from
        // the clients gives you the index of the client who sent data.
        public static Client GetClientByIndex(int index)
        {
            foreach(Client client in _clients)
            {
                if (client.index == index)
                    return client;
            }
            return null;
        }
    }
}
