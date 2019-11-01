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
        public double SectorTime 
        { 
            get
            {
                return DateTime.UtcNow.ToUniversalTime().Subtract(CreatedTime).TotalSeconds;
            } 
        }
        public Loop loop = new Loop();
        public Tick Tick = new Tick(0);
        public List<Client> clients = new List<Client>();

        public Sector(string Name, uint sectorId, uint sectorGuid, Color ambientColor, Color fogColor, Color dustColor, BackgroundDesc backgroundDesc, BackgroundDesc starsDesc, BackgroundDesc starsMult, BackgroundDesc starsVariance, MovingNebulaDesc[] movingNebulas, LightDesc[] lightDescs, SunDesc[] sunDescs, JGlobalFog jGlobalFog, JCameraFx jCameraFx)
        {
            this.sectorId = sectorId;
            this.sectorGuid = sectorGuid;
            this.Name = Name;
            CreatedTime = DateTime.UtcNow.ToUniversalTime();
            Tick.Reset(SectorTime);
            loop.OnUpdated = Update;
            loop.Initialize();

            SectorCard sector = new SectorCard(sectorGuid, CardView.Sector, 1000, 1000, 1000, sectorGuid, ambientColor, fogColor, 12, dustColor, 12, backgroundDesc, starsDesc, starsMult, starsVariance, movingNebulas, lightDescs, sunDescs, jGlobalFog, jCameraFx, new string[0]);
            GUICard sectorGUI = new GUICard(sectorGuid, CardView.GUI, serverSectorName, 0, "", 0, "", "", "", new string[0]);
            RegulationCard sectorReg = new RegulationCard(sectorGuid, CardView.Regulation, new ConsumableEffectType[0], new Dictionary<uint, HashSet<ShipAbilitySide>>(), new Dictionary<uint, HashSet<ShipAbilityTarget>>(), TargetBracketMode.Default, true);

            Catalogue.AddCard(sector);
            Catalogue.AddCard(sectorGUI);
            Catalogue.AddCard(sectorReg);
        }

        public void Update(float dt)
        {                                                
            Tick.Update(SectorTime);

            Tick tick = Tick.Last + 1;

            while (tick <= Tick.Current)
            {
                foreach (Client value in clients)
                {
                    if (value.Character.ManeuverController != null)
                        value.Character.ManeuverController.Advance(tick);
                }
                foreach (Client value in clients)
                {
                    if (value.Character.ManeuverController != null)
                        value.Character.ManeuverController.PostAdvance();
                }
                tick = ++tick;
            }

            foreach (Client client in clients)
            {
                if (client.Character.ManeuverController != null)
                    client.Character.ManeuverController.Move(SectorTime);
            }
        }

        public void JoinSector(Client client)
        {
            clients.Add(client);
            client.Character.ManeuverController = new ManeuverController(client.index, (MovementCard)Catalogue.FetchCard(client.Character.WorldCardGUID, CardView.Movement));
            client.Character.ManeuverController.AddManeuver(new TurnManeuver(ManeuverType.Turn, 0, new QWEASD(0), client.Character.MovementOptions));
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
