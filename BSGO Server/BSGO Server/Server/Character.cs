using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    class Character
    {
        private int index;
        public string name;
        public int characterId;
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
        public uint WorldCardGUID = 100;

        public Character(int index,  int characterId, GameLocation gameLocation)
        {
            this.index = index;
            this.characterId = characterId;
            this.gameLocation = gameLocation;
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
