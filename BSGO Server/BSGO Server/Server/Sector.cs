using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BSGO_Server
{
    class Sector
    {
        public uint sectorId { get; private set; }
        public string Name { get; private set; }
        public DateTime CreatedTime { get; private set; }

        public List<Client> clients = new List<Client>();

        public Sector(string Name, uint sectorGuid, Color ambientColor, Color fogColor, Color dustColor, BackgroundDesc backgroundDesc, BackgroundDesc starsDesc, BackgroundDesc starsMult, BackgroundDesc starsVariance, MovingNebulaDesc[] movingNebulas, LightDesc[] lightDescs, SunDesc[] sunDescs, JGlobalFog jGlobalFog, JCameraFx jCameraFx)
        {
            this.sectorId = sectorGuid;
            this.Name = Name;
            CreatedTime = DateTime.UtcNow.ToUniversalTime();

            SectorCard sector = new SectorCard(sectorGuid, CardView.Sector, 1000, 1000, 1000, sectorGuid, ambientColor, fogColor, 12, dustColor, 12, backgroundDesc, starsDesc, starsMult, starsVariance, movingNebulas, lightDescs, sunDescs, jGlobalFog, jCameraFx, new string[0]);
            GUICard sectorGUI = new GUICard(sectorGuid, CardView.GUI, serverSectorName, 0, "", 0, "", "", "", new string[0]);
            RegulationCard sectorReg = new RegulationCard(sectorGuid, CardView.Regulation, new ConsumableEffectType[0], new Dictionary<uint, HashSet<ShipAbilitySide>>(), new Dictionary<uint, HashSet<ShipAbilityTarget>>(), TargetBracketMode.Default, true);

            Catalogue.AddCard(sector);
            Catalogue.AddCard(sectorGUI);
            Catalogue.AddCard(sectorReg);
        }

        public void JoinSector(Client client)
        {
            clients.Add(client);

            //foreach(Client c in clients)
            //{
                int index = client.index;
                Task.Run(()=> { 
                    GameProtocol.GetProtocol().SendWhoIsPlayer(index, SpaceEntityType.Player, (uint)index, (uint)index, Server.GetClientByIndex(index).Character.WorldCardGUID);
                    GameProtocol.GetProtocol().SyncMove(index, SpaceEntityType.Player, (uint)index, new Vector3(0, 0f, 0f));
                });
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
