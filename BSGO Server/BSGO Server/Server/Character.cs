using System;
using System.Collections.Generic;
using System.Text;
using BSGO_Server.Database;
using BSGO_Server.Database.Entities;

namespace BSGO_Server
{
    class Character
    {
        private int index;
        public string name;
        public Faction Faction;

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
        public Dictionary<AvatarItem, string> items = new Dictionary<AvatarItem, string>();
        public uint WorldCardGUID = 22131177;

        public Character(int index)
        {
            this.index = index;
            this.gameLocation = GameLocation.Starter;

            GUICard ownerGUIDCard = new GUICard((uint)index, CardView.GUI, "", 0, "", 0, "", "", "", new string[0]);
            OwnerCard ownerCard = new OwnerCard((uint)index, CardView.Owner, false, 0, 1);
            Catalogue.AddCard(ownerGUIDCard);
            Catalogue.AddCard(ownerCard);

            Characters character = Database.Database.GetCharacterById(Server.GetClientByIndex(index).playerId.ToString());
            if (character == null)
                return;

            this.name = character.Name;
            this.Faction = (Faction)character.Faction;
            this.gameLocation = (GameLocation)character.GameLocation;

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
                    else if (lastGameLocation == GameLocation.Starter || lastGameLocation == GameLocation.Space)
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
