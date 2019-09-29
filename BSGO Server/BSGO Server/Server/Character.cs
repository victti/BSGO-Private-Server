using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    class Character
    {
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
            }
        }
        private GameLocation gameLocation;
        private GameLocation lastGameLocation = GameLocation.Unknown;

        public Character(int characterId, GameLocation gameLocation)
        {
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
