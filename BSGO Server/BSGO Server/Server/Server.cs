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
        // length of 5 if we ever need to test multiplayer features such as sync and squads.
        private static int MaxPlayers = 5;
        private static Client[] _clients = new Client[MaxPlayers];

        // The game will connect to: SERVERIP:27050
        /// <summary>
        /// Initializes the server.
        /// </summary>
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
                if (_clients[i].Socket == null)
                {
                    _clients[i].Socket = socket;
                    _clients[i].Index = i;
                    _clients[i].Ip = socket.RemoteEndPoint.ToString();
                    _clients[i].StartClient();

                    Log.Add(LogSeverity.INFO, string.Format("Connection from '{0}' received.", _clients[i].Ip));

                    // We should send the ConnectionOK method from the LoginProtocol otherwise the game
                    // will be stuck on a "connecting" screen with no errors since it is just waiting for
                    // this to be sent.
                    LoginProtocol.GetProtocol().SendConnectionOK(i);
                    return;
                }
            }
        }

        /// <summary>
        /// Sends a message to one specific client by his index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="message"></param>
        public static void SendDataToClient(int index, BgoProtocolWriter message)
        {
            foreach (Client clients in _clients)
            {
                if (clients.Socket != null && !clients.Closing && clients.Index == index)
                {
                    clients.Socket.Send(message.GetBuffer(), 0, message.GetLength(), SocketFlags.None);
                }
            }
        }

        /// <summary>
        /// Returns a Client by searching his index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Client GetClientByIndex(int index)
        {
            foreach (Client client in _clients)
            {
                if (client.Index == index)
                    return client;
            }
            return null;
        }

        /// <summary>
        /// Returns a Client by searching his player Id.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Client GetClientByPlayerId(string id)
        {
            foreach (Client client in _clients)
            {
                if (client.playerId == uint.Parse(id))
                    return client;
            }
            return null;
        }
    }
}
