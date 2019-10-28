using System;
using System.Collections.Generic;
using BSGO_Server._3dAlgorithm;
using BSGO_Server.Database.Entities;

namespace BSGO_Server
{
    class Character
    {
        private readonly int index;
        public string name { get; set; }
        public Faction Faction { get; set; }

        public GameLocation GameLocation {
            get
            {
                return gameLocation;
            }
            set
            {
                lastGameLocation = gameLocation;
                gameLocation = value;
                SceneProtocol.GetProtocol().SendLoadNextScene(index);
            }
        }
        private GameLocation gameLocation;
        private GameLocation lastGameLocation = GameLocation.Unknown;
        public Dictionary<AvatarItem, string> items { get; set; } = new Dictionary<AvatarItem, string>();
        public uint WorldCardGUID { get; set; } = 22131177;

        public MovementOptions MovementOptions = new MovementOptions();
        public MovementFrame MovementFrame { get; set; } = new MovementFrame(new Vector3(), new Euler3(), new Vector3(), new Vector3(), new Euler3());
        public uint sectorId { get; set; } = 0;
        public QWEASD qweasd { get; set; } = new QWEASD();
        public byte shipMode { get; set; }
        public float shipSpeed { get; set; }

        public Character(int index)
        {
            this.index = index;
            gameLocation = GameLocation.Starter;

            GUICard ownerGUIDCard = new GUICard((uint)index, CardView.GUI, "", 0, "", 0, "", "GUI/Slots/" + ((GUICard)Catalogue.FetchCard(WorldCardGUID, CardView.GUI)).Key, "", new string[0]);
            OwnerCard ownerCard = new OwnerCard((uint)index, CardView.Owner, false, 0, 1);
            Catalogue.AddCard(ownerGUIDCard);
            Catalogue.AddCard(ownerCard);

            Characters character = Database.Database.GetCharacterById(Server.GetClientByIndex(index).playerId.ToString());
            if (character is null)
                return;

            name = character.Name;
            Faction = (Faction)character.Faction;
            gameLocation = (GameLocation)character.GameLocation;

            foreach (KeyValuePair<string, string> item in character.AvatarItems)
            {
                items.Add((AvatarItem)Enum.Parse(typeof(AvatarItem), item.Key), item.Value);
            }

            ShipCard spaceShip = (ShipCard)Catalogue.FetchCard(WorldCardGUID, CardView.Ship);
            MovementCard movementCardSpaceShip = (MovementCard)Catalogue.FetchCard(WorldCardGUID, CardView.Movement);
            MovementOptions.gear = Gear.Regular;
            MovementOptions.speed = shipSpeed;
            MovementOptions.acceleration = spaceShip.Stats.Acceleration;
            MovementOptions.inertiaCompensation = spaceShip.Stats.InertiaCompensation;
            MovementOptions.pitchAcceleration = spaceShip.Stats.PitchAcceleration;
            MovementOptions.pitchMaxSpeed = spaceShip.Stats.PitchMaxSpeed;
            MovementOptions.yawAcceleration = spaceShip.Stats.YawAcceleration;
            MovementOptions.yawMaxSpeed = spaceShip.Stats.YawMaxSpeed;
            MovementOptions.rollAcceleration = spaceShip.Stats.RollAcceleration;
            MovementOptions.rollMaxSpeed = spaceShip.Stats.RollMaxSpeed;
            MovementOptions.strafeAcceleration = spaceShip.Stats.StrafeAcceleration;
            MovementOptions.strafeMaxSpeed = spaceShip.Stats.StrafeMaxSpeed;
            MovementOptions.minYawSpeed = movementCardSpaceShip.minYawSpeed;
            MovementOptions.maxPitch = movementCardSpaceShip.maxPitch;
            MovementOptions.maxRoll = movementCardSpaceShip.maxRoll;
            MovementOptions.pitchFading = movementCardSpaceShip.pitchFading;
            MovementOptions.yawFading = movementCardSpaceShip.yawFading;
            MovementOptions.rollFading = movementCardSpaceShip.rollFading;
        }

        // Returns the Transition Scene Type based on where you are/were.
        public TransSceneType getTransSceneType()
        {
            switch (gameLocation)
            {
                case GameLocation.Space:
                    if (lastGameLocation == GameLocation.Room)
                        return TransSceneType.Undock;
                    else if (lastGameLocation == GameLocation.Starter || lastGameLocation == GameLocation.Space || lastGameLocation == GameLocation.Unknown)
                        return TransSceneType.Ftl;
                    break;
                case GameLocation.Starter:
                    return TransSceneType.Teaser;
                default:
                    return TransSceneType.None;
            }
            return TransSceneType.None;
        }
    }
}
