using System.Threading.Tasks;

namespace BSGO_Server
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            // Since the original game was based on protocols, we have to first setup the server protocols
            // to receive/send what the game wants.
            ProtocolManager.InitProtocols();
            // We are using a database called MongoDB instead of MySQL since Mongo makes it a lot easier to
            // work with unplanned things like the unknown number of cols and tables. So anyone can just run
            // a local Mongo that this server will handle everything else.
            Database.Database.Start();
            // The game had this weird (imo) card system so we have to remake it in order to make the game work.
            Catalogue.SetupCards();
            // Now we have to make the server accept connections and make it handle them.
            Server.InitServer();
            // This line should keep the server alive in order to use it.
            await Task.Delay(-1);
        }
    }
}
