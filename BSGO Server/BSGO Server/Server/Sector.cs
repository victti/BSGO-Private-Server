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

            SectorCard sector = new SectorCard(sectorGuid, CardView.Sector, 25000, 25000, 25000, sectorGuid, ambientColor, fogColor, 12, dustColor, 12, backgroundDesc, starsDesc, starsMult, starsVariance, movingNebulas, lightDescs, sunDescs, jGlobalFog, jCameraFx, new string[0]);
            GUICard sectorGUI = new GUICard(sectorGuid, CardView.GUI, serverSectorName, 0, "", 0, "", "", "", new string[0]);
            RegulationCard sectorReg = new RegulationCard(sectorGuid, CardView.Regulation, new ConsumableEffectType[0], new Dictionary<uint, HashSet<ShipAbilitySide>>(), new Dictionary<uint, HashSet<ShipAbilityTarget>>(), TargetBracketMode.Default, true);
            RoomCard sectorRoom = new RoomCard(sectorGuid, CardView.Room);

            Catalogue.AddCard(sector);
            Catalogue.AddCard(sectorGUI);
            Catalogue.AddCard(sectorReg);
            Catalogue.AddCard(sectorRoom);
        }

        public void Update(float dt)
        {
            Tick.Update(SectorTime);

            Tick tick = Tick.Last + 1;

            List<Client> _clients = clients;
            if (Tick.IsNewTick())
            {
                while (tick <= Tick.Current)
                {
                    foreach (Client value in _clients)
                    {
                        if (value.Character.PlayerShip.ManeuverController != null)
                            value.Character.PlayerShip.ManeuverController.Advance(tick);
                    }
                    foreach (Client value in _clients)
                    {
                        if (value.Character.PlayerShip.ManeuverController != null)
                            value.Character.PlayerShip.ManeuverController.PostAdvance();
                    }
                    tick = ++tick;
                }
            }

            foreach (Client value in _clients)
            {
                if (value.Character.PlayerShip.ManeuverController != null)
                    value.Character.PlayerShip.ManeuverController.Move(SectorTime);
            }
        }

        public void JoinSector(Client client)
        {
            clients.Add(client);
            client.Character.PlayerShip.currentShipStats = ((ShipCard)Catalogue.FetchCard(client.Character.PlayerShip.WorldGuid, CardView.Ship)).Stats;
            client.Character.PlayerShip.ManeuverController = new ManeuverController(client.index, (MovementCard)Catalogue.FetchCard(client.Character.PlayerShip.WorldGuid, CardView.Movement));
            client.Character.PlayerShip.ManeuverController.AddManeuver(new RestManeuver(ManeuverType.Rest, Tick.Current.value, new Vector3(0,0,0), new Euler3(0,0,0)));

            int index = client.index;
            GameProtocol.GetProtocol().SetTimeOrigin(index);
            GameProtocol.GetProtocol().SendWhoIsPlayerToSector(index, SpaceEntityType.Player, Server.GetObjectId(index), (uint)index, Server.GetClientByIndex(index).Character.PlayerShip.WorldGuid);

            List <Client> _clients = clients;
            Parallel.ForEach(_clients, (c) =>
            {
                if (c.index != index)
                {
                    GameProtocol.GetProtocol().SendWhoIsPlayerToPlayer(index, c.index, SpaceEntityType.Player, Server.GetObjectId(c.index), (uint)c.index, Server.GetClientByIndex(c.index).Character.PlayerShip.WorldGuid);
                }
            });
        }

        public void LeaveSector(Client client, RemovingCause removingCause)
        {
            clients.Remove(client);

            switch (removingCause)
            {
                case RemovingCause.JustRemoved:
                case RemovingCause.Dock:
                case RemovingCause.JumpOut:
                    GameProtocol.GetProtocol().SendObjectLeft(client.index, 1, new SpaceEntityType[1] { SpaceEntityType.Player }, new uint[1] { Server.GetObjectId(client.index) }, new RemovingCause[1] { removingCause });
                    break;
                default:
                    Log.Add(LogSeverity.ERROR, "Uknown removal cause on Leave Sector of the client " + client.Character.name);
                    break;
            }
        }

        public void SetOutpost(Faction faction, int Points)
        {
            GameProtocol.GetProtocol().SpawnOutpost(sectorId, faction);
        }

        public string serverSectorName
        {
            get
            {
                return "sector" + sectorId;
            }
        }
    }
}
