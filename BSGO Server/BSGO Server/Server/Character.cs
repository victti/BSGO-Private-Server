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
        public IDictionary<UserSetting, object> settings = new Dictionary<UserSetting, object>();
        public List<string> controlKeys = new List<string>();
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
        public uint partyId { get; set; } = 0;
        public PlayerShip PlayerShip { get; set; }

        public Character(int index)
        {
            this.index = index;
            PlayerShip = new PlayerShip(index);
            gameLocation = GameLocation.Starter;

            GUICard ownerGUIDCard = new GUICard((uint)index, CardView.GUI, "", 0, "", 0, "", ((GUICard)Catalogue.FetchCard(22131170u, CardView.GUI)).GUIAvatarSlotTexturePath, "", new string[0]);
            OwnerCard ownerCard = new OwnerCard((uint)index, CardView.Owner, false, 0, 1);
            Catalogue.AddCard(ownerGUIDCard);
            Catalogue.AddCard(ownerCard);

            Characters character = Database.Database.GetCharacterById(Server.GetClientByIndex(index).playerId.ToString());
            if (character is null)
                return;

            name = character.Name;
            Faction = (Faction)character.Faction;
            gameLocation = (GameLocation)character.GameLocation;
            PlayerShip.sectorId = (uint)character.SectorId;

            foreach (KeyValuePair<string, string> item in character.AvatarItems)
            {
                items.Add((AvatarItem)Enum.Parse(typeof(AvatarItem), item.Key), item.Value);
            }
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
