using System;
using System.Collections.Generic;
using System.Drawing;
using BSGO_Server._3dAlgorithm;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BSGO_Server
{
    class Sector
    {
        public uint sectorId { get; private set; }
        public uint sectorGuid { get; private set; }
        public string Name { get; private set; }
        public DateTime CreatedTime { get; private set; }
        public Loop loop = new Loop();
        public int Tick
        {
            get
            {
                double time = DateTime.UtcNow.ToUniversalTime().Subtract(CreatedTime).TotalSeconds;
                return (int)Math.Floor(time * 10.0);
            }
        }
        private int PrevTick;
        public List<Client> clients = new List<Client>();

        public Sector(string Name, uint sectorId, uint sectorGuid, Color ambientColor, Color fogColor, Color dustColor, BackgroundDesc backgroundDesc, BackgroundDesc starsDesc, BackgroundDesc starsMult, BackgroundDesc starsVariance, MovingNebulaDesc[] movingNebulas, LightDesc[] lightDescs, SunDesc[] sunDescs, JGlobalFog jGlobalFog, JCameraFx jCameraFx)
        {
            this.sectorId = sectorId;
            this.sectorGuid = sectorGuid;
            this.Name = Name;
            CreatedTime = DateTime.UtcNow.ToUniversalTime();
            loop.OnUpdated = teste;
            loop.Initialize();

            SectorCard sector = new SectorCard(sectorGuid, CardView.Sector, 1000, 1000, 1000, sectorGuid, ambientColor, fogColor, 12, dustColor, 12, backgroundDesc, starsDesc, starsMult, starsVariance, movingNebulas, lightDescs, sunDescs, jGlobalFog, jCameraFx, new string[0]);
            GUICard sectorGUI = new GUICard(sectorGuid, CardView.GUI, serverSectorName, 0, "", 0, "", "", "", new string[0]);
            RegulationCard sectorReg = new RegulationCard(sectorGuid, CardView.Regulation, new ConsumableEffectType[0], new Dictionary<uint, HashSet<ShipAbilitySide>>(), new Dictionary<uint, HashSet<ShipAbilityTarget>>(), TargetBracketMode.Default, true);

            Catalogue.AddCard(sector);
            Catalogue.AddCard(sectorGUI);
            Catalogue.AddCard(sectorReg);
        }

        private void teste(float dt)
        {
            Parallel.Invoke(() =>
            {
                if (Tick != PrevTick)
                {
                    //Parallel.ForEach(clients, (client) =>
                    //{
                    //    client.Character.MovementFrame = Simulation.WASD(client.Character.MovementFrame, client.Character.qweasd.Pitch, client.Character.qweasd.Yaw, client.Character.qweasd.Roll, client.Character.MovementOptions);
                    //});
                    foreach(Client client in clients)
                    {
                        client.Character.MovementFrame = Simulation.WASD(client.Character.MovementFrame, client.Character.qweasd.Pitch, client.Character.qweasd.Yaw, client.Character.qweasd.Roll, client.Character.MovementOptions);

                    }
                    PrevTick = Tick;
                }
            });
        }

        public void JoinSector(Client client)
        {
            clients.Add(client);

            //foreach(Client c in clients)
            //{
            int index = client.index;
            GameProtocol.GetProtocol().SetTimeOrigin(index);
            GameProtocol.GetProtocol().SendWhoIsPlayer(index, SpaceEntityType.Player, (uint)index, (uint)index, Server.GetClientByIndex(index).Character.WorldCardGUID);
            GameProtocol.GetProtocol().SyncMove(index, SpaceEntityType.Player, (uint)index, client.Character.MovementFrame.position);
            //}
        }

        public void LeaveSector(Client client)
        {
            clients.Remove(client);
        }

        public string serverSectorName
        {
            get
            {
                switch (Name)
                {
                    case "Alpha Ceti":
                        return "sector0";
                    default:
                        return "sector1982";
                }
            }
        }
    }
}
