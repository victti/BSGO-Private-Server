using System;
using System.Collections.Generic;
using BSGO_Server.Database.Entities;

namespace BSGO_Server
{
    internal class Character
    {
        private readonly int index;
        public string Name { get; set; }
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
        public Dictionary<AvatarItem, string> Items { get; set; } = new Dictionary<AvatarItem, string>();
        public uint WorldCardGUID { get; set; } = 22131177;

        public byte Qweasd { get; set; }
        public byte ShipMode { get; set; }
        public float ShipSpeed { get; set; }

        public Character(int index)
        {
            this.index = index;
            gameLocation = GameLocation.Starter;

            GUICard ownerGUIDCard = new GUICard((uint)index, CardView.GUI, "", 0, "", 0, "", "GUI/Slots/" + ((GUICard)Catalogue.FetchCard(WorldCardGUID, CardView.GUI)).Key, "", new string[0]);
            OwnerCard ownerCard = new OwnerCard((uint)index, CardView.Owner, false, 0, 1);
            Catalogue.AddCard(ownerGUIDCard);
            Catalogue.AddCard(ownerCard);

            Characters character = Database.Database.GetCharacterById(Server.GetClientByIndex(index).PlayerId.ToString());
            if (character is null)
                return;

            Name = character.Name;
            Faction = (Faction)character.Faction;
            gameLocation = (GameLocation)character.GameLocation;

            foreach (KeyValuePair<string, string> item in character.AvatarItems)
                Items.Add((AvatarItem)Enum.Parse(typeof(AvatarItem), item.Key), item.Value);
            

        }

        // Returns the Transition Scene Type based on where you are/were.
        public TransSceneType GetTransSceneType()
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
