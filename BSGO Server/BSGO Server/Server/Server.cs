using BSGO_Server._3dAlgorithm;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Sockets;

namespace BSGO_Server
{
    class Server
    {
        public static readonly bool Async = true;
        public static readonly bool AlternativeSync = false;

        public static readonly uint ChatProjectID = 1;

        public static DateTime serverStartTime;

        private static readonly Socket _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        private static readonly Socket _chatSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        // Default port should be 27050 since the game connects to that port by default.
        private static readonly int serverPort = 27050;

        // Default chat port should be 9338 since the game connects to that port by default.
        private static readonly int chatPort = 9338;

        // The buffer size IS meant to be 65535 since the game expects to receive that length.
        private static readonly byte[] _buffer = new byte[65535];

        private static readonly Dictionary<int, Client> _clients = new Dictionary<int, Client>();
        public static Dictionary<int, Client> Clients
        {
            get
            {
                return _clients;
            }
        }

        private static readonly Dictionary<int, Chat> _chatClients = new Dictionary<int, Chat>();
        public static Dictionary<int, Chat> ChatClients
        {
            get
            {
                return _chatClients;
            }
        }

        private static List<Sector> _sectors = new List<Sector>();
        public static List<Sector> Sectors
        {
            get
            {
                return _sectors;
            }
        }

        private static Dictionary<uint, Party> _parties = new Dictionary<uint, Party>();
        public static Dictionary<uint, Party> Parties
        {
            get
            {
                return _parties;
            }
        }

        // The game will connect to: SERVERIP:27050
        /// <summary>
        /// Initializes the server.
        /// </summary>
        public static void InitServer()
        {
            Log.Add(LogSeverity.SERVERINFO, "Initializing the server... Might take a while. Wait until the running confirmation to launch the game!");

            serverStartTime = DateTime.UtcNow.ToUniversalTime();

            CreateSectors();

            _serverSocket.NoDelay = true;
            _serverSocket.Bind(new IPEndPoint(IPAddress.Any, serverPort));
            _serverSocket.Listen(128);
            _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);

            _chatSocket.Bind(new IPEndPoint(IPAddress.Any, chatPort));
            _chatSocket.Listen(128);
            _chatSocket.BeginAccept(new AsyncCallback(AcceptChatCallback), null);

            Log.Add(LogSeverity.SERVERINFO, string.Format("The server is now running on port {0}", serverPort));
        }

        private static void CreateSectors()
        {
            _sectors.Add(new Sector("Alpha Ceti", 0, 163231265, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("Beta Antini", 1, 163231266, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("Balent", 2, 163231267, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("Denebol", 3, 163231268, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("69 Otaan", 4, 163231269, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("103 Heleb", 5, 163231270, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("242 Apollid", 6, 163231271, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("Tau Nehmet", 7, 163231272, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("Epsilon Krau", 8, 163231273, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("Alpha Ceti", 9, 163231274, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("Tannhauser", 10, 195781329, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("71 Duneyrr", 11, 195781330, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("Fenris", 12, 195781331, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("101 Crucis", 13, 195781332, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("Sigma Lyraes", 14, 195781333, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("Tau Carinai", 15, 195781334, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("Omicron Decimus", 16, 195781335, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("Tau Altaar", 17, 195781336, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("84 Cerbero", 18, 195781337, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("Anachron", 19, 195781338, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("74 Imsida", 20, 1957811313, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("Omicron Percei", 21, 1957811314, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("Wegelin", 22, 1957811315, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("Marsamxett", 23, 1957811316, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("Raastaban", 24, 1957811317, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("51 Bonamist", 25, 1957811318, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("68 Lepporis", 26, 1957811319, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("Spectris", 27, 1957811320, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("Epsilon Iordiani", 28, 1957811321, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("Asterian", 29, 1957811322, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("Kryphon", 30, 195781361, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("Nastrond", 31, 195781362, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("32 Serpentos", 32, 195781363, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("Zeidian", 33, 195781364, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("21 Tiche", 34, 195781365, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("Beta Pleiadis", 35, 195781366, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("Gamma Gurun", 36, 195781367, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("Huginn", 37, 195781368, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("Muninn", 38, 195781369, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("82 Tenland", 39, 195781370, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("169 Aretis", 40, 195781345, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("276 Exrun", 41, 195781346, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("Delta Aurican", 42, 195781347, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("Geirrod", 43, 195781348, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("Abalon", 44, 195781349, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("Hatir", 45, 195781350, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("Calibaan", 46, 195781351, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("Vidofnir", 47, 195781352, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("227 Gemino", 48, 195781353, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("Delta Canopis", 49, 195781354, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("47 Tartalon", 50, 195781137, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("Muspell", 51, 195781138, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("Nilfhel", 52, 195781139, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("Rayet", 110, 179711473, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("Canaris", 111, 179711474, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("Carillon", 112, 179711475, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("Exomera", 113, 179711476, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
            _sectors.Add(new Sector("14 Toah", 114, 179711477, Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), Color.FromArgb(255, 100, 100, 100), new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0)), new MovingNebulaDesc[0], new LightDesc[0], new SunDesc[0], new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0), new JCameraFx(false)));
        }

        private static void AcceptChatCallback(IAsyncResult ar)
        {
            Socket chatSocket = _chatSocket.EndAccept(ar);
            _chatSocket.BeginAccept(new AsyncCallback(AcceptChatCallback), null);

            int chatIndex = 1;

            while (_chatClients.ContainsKey(chatIndex))
                chatIndex++;

            Chat chat = new Chat(chatIndex);
            chat.socket = chatSocket;

            _chatClients.Add(chatIndex, chat);

            Log.Add(LogSeverity.INFO, string.Format("Chat Connection({1}) from '{0}' received.", chat.socket.RemoteEndPoint.ToString(), chatIndex));
        }

        private static void AcceptCallback(IAsyncResult ar)
        {
            Socket socket = _serverSocket.EndAccept(ar);
            _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);

            int cIndex = 1;

            while (_clients.ContainsKey(cIndex))
                cIndex++;

            Client newClient = new Client(cIndex);
            newClient.socket = socket;
            newClient.ip = socket.RemoteEndPoint.ToString();
            newClient.StartClient();

            _clients.Add(cIndex, newClient);

            Log.Add(LogSeverity.INFO, string.Format("Connection({1}) from '{0}' received.", newClient.ip, cIndex));

            // We should send the ConnectionOK method from the LoginProtocol otherwise the game
            // will be stuck on a "connecting" screen with no errors since it is just waiting for
            // this to be sent.
            LoginProtocol.GetProtocol().SendConnectionOK(cIndex);
        }

        //Async
        private static void SendToClientAsync(Client client, BgoProtocolWriter message)
        {
            try
            {
                client.socket.BeginSend(message.GetBuffer(), 0, message.GetLength(), SocketFlags.None, new AsyncCallback(SendCallback), client.socket);
                
                // Part of the never implemented alternative async.
                //SocketAsyncEventArgs socketAsyncData = new SocketAsyncEventArgs();
                //socketAsyncData.SetBuffer(message.GetBuffer(), 0, message.GetLength());
                //client.socket.SendAsync(socketAsyncData);
            }
            catch
            {
                client.CloseClient();
            }
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = handler.EndSend(ar);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        //Sync
        private static void SendToClient(Client client, BgoProtocolWriter message)
        {
            try
            {
                client.socket.Send(message.GetBuffer(), 0, message.GetLength(), SocketFlags.None);
            }
            catch
            {
                client.closing = true;
            }
        }

        /// <summary>
        /// Sends a message to one specific client by his index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="message"></param>
        public static void SendDataToClient(int index, BgoProtocolWriter message)
        {
            if (_clients[index].socket != null && !_clients[index].closing && _clients[index].index == index)
            {
                if (Async)
                    SendToClientAsync(_clients[index], message);
                else
                    SendToClient(_clients[index], message);
            }
        }

        /// <summary>
        /// Sends a message to all clients in a sector but himself by his index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="message"></param>
        public static void SendDataToSectorButClient(int index, BgoProtocolWriter message)
        {
            uint sectorid = GetClientByIndex(index).Character.PlayerShip.sectorId;
            Dictionary<int, Client> clients = _clients;
            foreach (KeyValuePair<int, Client> currClient in clients)
            {
                Client client = currClient.Value;
                if (client.socket != null && !client.closing && client.index != index && client.Character != null && client.Character.PlayerShip.sectorId == sectorid)
                {
                    if (Async)
                        SendToClientAsync(client, message);
                    else
                        SendToClient(client, message);
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
            Dictionary<int, Client> clients = _clients;
            foreach (KeyValuePair<int, Client> currClient in clients)
            {
                Client client = currClient.Value;
                if (client.socket != null && !client.closing && client.Character != null && client.Character.PlayerShip.sectorId == index)
                {
                    if (Async)
                        SendToClientAsync(client, message);
                    else
                        SendToClient(client, message);
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
            if (_clients.ContainsKey(index))
                return _clients[index];
            return null;
        }

        /// <summary>
        /// Returns a Client by searching his player Id.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Client GetClientByPlayerId(string id)
        {
            Dictionary<int, Client> clients = _clients;
            foreach (KeyValuePair<int, Client> currClient in clients)
            {
                Client client = currClient.Value;
                if (client.playerId == uint.Parse(id))
                    return client;
            }
            return null;
        }

        public static Sector GetSectorById(uint id)
        {
            List<Sector> sectors = _sectors;
            foreach (Sector sector in sectors)
            {
                if (sector.sectorId == id)
                    return sector;
            }
            return null;
        }

        public static Sector GetSectorByClientIndex(int index)
        {
            return GetSectorById(_clients[index].Character.PlayerShip.sectorId);
        }

        public static uint GetObjectId(int index)
        {
            Client client = _clients[index];
            if (client.Character.Faction == Faction.Colonial)
                return 1073741824u + (uint)index + (uint)SpaceEntityType.Player;
            else if (client.Character.Faction == Faction.Cylon)
                return 2147483648u + (uint)index + (uint)SpaceEntityType.Player;

            return 0;
        }

        public static uint GetOutPostObjectId(int index, SpaceEntityType spaceEntityType, Faction faction)
        {
            uint sectorId = GetSectorById(GetClientByIndex(index).Character.PlayerShip.sectorId).sectorId;
            if (faction == Faction.Colonial)
                return 1073741824u + (uint)1 + sectorId + (uint)spaceEntityType;
            else if (faction == Faction.Cylon)
                return 2147483648u + (uint)2 + sectorId + (uint)spaceEntityType;
            return 0;
        }

        public static uint GetOutPostObjectId(uint index, SpaceEntityType spaceEntityType, Faction faction)
        {
            uint sectorId = index;
            if (faction == Faction.Colonial)
                return 1073741824u + (uint)1 + sectorId + (uint)spaceEntityType;
            else if (faction == Faction.Cylon)
                return 2147483648u + (uint)2 + sectorId + (uint)spaceEntityType;
            return 0;
        }

        public static Party GetPartyById(uint partyId)
        {
            if (_parties.ContainsKey(partyId))
                return _parties[partyId];
            return null;
        }
    }
}
