using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Sockets;

namespace BSGO_Server
{
    class Server
    {
        public static DateTime serverStartTime;

        private static readonly Socket _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        // Default port should be 27050 since the game connects to that port by default.
        private static readonly int PORT = 27050;

        // The buffer size IS meant to be 65535 since the game expects to receive that length.
        private static readonly byte[] _buffer = new byte[65535];

        // Since we only need at least one player to run the game, I'll make the array of clients have the
        // length of 5 if we ever need to test multiplayer features such as sync and squads.
        private static readonly int MaxPlayers = 5;
        private static readonly Client[] _clients = new Client[MaxPlayers];

        private static List<Sector> _sectors = new List<Sector>();

        // The game will connect to: SERVERIP:27050
        /// <summary>
        /// Initializes the server.
        /// </summary>
        public static void InitServer()
        {
            Log.Add(LogSeverity.SERVERINFO, "Initializing the server");

            serverStartTime = DateTime.UtcNow.ToUniversalTime();
            _sectors.Add(new Sector("Alpha Ceti", 163231265, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new System.Numerics.Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new System.Numerics.Vector3(0, 0, 0)), new BackgroundDesc("stars", new System.Numerics.Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new System.Numerics.Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new System.Numerics.Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new System.Numerics.Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new System.Numerics.Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new System.Numerics.Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));

            for (int i = 0; i < MaxPlayers; i++)
                _clients[i] = new Client(i);

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
                    _clients[i].ip = socket.RemoteEndPoint.ToString();
                    _clients[i].StartClient();

                    Log.Add(LogSeverity.INFO, string.Format("Connection from '{0}' received.", _clients[i].ip));

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
            foreach (Client client in _clients)
            {
                if (client.socket != null && !client.closing && client.index == index)
                {
                    client.socket.Send(message.GetBuffer(), 0, message.GetLength(), SocketFlags.None);
                }
            }
        }

        /// <summary>
        /// Sends a message to all clients in a sector but himself by his index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="message"></param>
        public static void SendDataToSectorButClient(int index, BgoProtocolWriter message)
        {
            uint sectorid = GetClientByIndex(index).Character.sectorId;
            foreach (Client client in _clients)
            {
                if (client.socket != null && !client.closing && client.index != index && client.Character != null && client.Character.sectorId == sectorid)
                {
                    client.socket.Send(message.GetBuffer(), 0, message.GetLength(), SocketFlags.None);
                }
            }
        }

        /// <summary>
        /// Sends a message to everyone in a sector.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="message"></param>
        public static void SendDataToSector(uint index, BgoProtocolWriter message)
        {
            foreach (Client client in _clients)
            {
                if (client.socket != null && !client.closing && client.Character != null && client.Character.sectorId == index)
                {
                    client.socket.Send(message.GetBuffer(), 0, message.GetLength(), SocketFlags.None);
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
            foreach(Client client in _clients)
            {
                if (client.index == index)
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

        public static Sector GetSectorById(uint id)
        {
            foreach(Sector sector in _sectors)
            {
                if (sector.sectorId == id)
                    return sector;
            }
            return null;
        }
    }
}
