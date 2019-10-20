using System;
using System.Net;
using System.Net.Sockets;

namespace BSGO_Server
{
    class Server
    {
        private static readonly Socket _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        // Default port should be 27050 since the game connects to that port by default.
        private static readonly int port = 27050;

        // The buffer size IS meant to be 65535 since the game expects to receive that length.
        private static readonly byte[] buffer = new byte[65535];

        // Since we only need at least one player to run the game, I'll make the array of clients have the
        // length of 5 if we ever need to test multiplayer features such as sync and squads.
        private static readonly int maxPlayers = 5;
        private static readonly Client[] clients = new Client[maxPlayers];

        // The game will connect to: SERVERIP:27050
        /// <summary>
        /// Initializes the server.
        /// </summary>
        public static void InitServer()
        {
            Log.Add(LogSeverity.SERVERINFO, "Initializing the server");

            for (int i = 0; i < maxPlayers; i++)
                clients[i] = new Client();
            
            _serverSocket.NoDelay = true;
            _serverSocket.Bind(new IPEndPoint(IPAddress.Any, port));
            _serverSocket.Listen(10);
            _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);

            Log.Add(LogSeverity.SERVERINFO, string.Format("The server is now running on port {0}", port));
        }

        private static void AcceptCallback(IAsyncResult ar)
        {
            Socket socket = _serverSocket.EndAccept(ar);
            _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);

            for (int i = 1; i < maxPlayers; i++)
            {
                if (clients[i].Socket == null)
                {
                    clients[i].Socket = socket;
                    clients[i].Index = i;
                    clients[i].Ip = socket.RemoteEndPoint.ToString();
                    clients[i].StartClient();

                    Log.Add(LogSeverity.INFO, string.Format("Connection from '{0}' received.", clients[i].Ip));

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
            foreach (Client currentClient in clients)
                if (currentClient.Socket != null && !currentClient.Closing && currentClient.Index == index)
                    currentClient.Socket.Send(message.GetBuffer(), 0, message.GetLength(), SocketFlags.None);
        }

        /// <summary>
        /// Returns a Client by searching his index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Client GetClientByIndex(int index)
        {
            foreach (Client client in clients)
            {
                if (client.Index == index)
                    return client;
            }
            return null;
        }

        /// <summary>
        /// Returns a Client by searching his player Id.
        /// </summary>
        /// <returns></returns>
        public static Client GetClientByPlayerId(string id)
        {
            foreach (Client client in clients)
                if (client.PlayerId == uint.Parse(id))
                    return client;
            
            return null;
        }
    }
}
