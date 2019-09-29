using System;

namespace BSGO_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            // Since the original game was based on protocols, we have to first setup the server protocols
            // to receive/send what the game wants.
            ProtocolManager.InitProtocols();
            // The game had this weird (imo) card system so we have to remake it in order to make the game.
            Catalogue.SetupCards();
            // Now we have to make the server accept connections and make it handle them.
            Server.InitServer();
            // The program should have this line of code, that should be changed to something more complex
            // like a commands section in the future, in order to keep the server alive.
            Console.ReadLine();
        }
    }
}
