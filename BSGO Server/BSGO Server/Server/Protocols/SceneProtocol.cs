using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    internal class SceneProtocol : Protocol
    {
        public enum Request : ushort
        {
            SceneLoaded = 1,
            Disconnect,
            StopDisconnect,
            QuitLogin
        }

        public enum Reply : ushort
        {
            LoadNextScene = 1,
            DisconnectTimer = 2,
            Disconnect = 100
        }

        public SceneProtocol()
            : base(ProtocolID.Scene)
        {
        }

        public static SceneProtocol GetProtocol()
        {
            return ProtocolManager.GetProtocol(ProtocolID.Scene) as SceneProtocol;
        }

        public override void ParseMessage(int index, BgoProtocolReader br)
        {
            ushort msgType = br.ReadUInt16();

            switch ((Request)msgType)
            {
                case Request.QuitLogin:
                    SendLoadNextScene(index);
                    break;
                case Request.SceneLoaded:
                    SceneLoaded(index);
                    break;
                default:
                    Log.Add(LogSeverity.ERROR, string.Format("Unknown msgType \"{0}\" on {1}Protocol.", (Request)msgType, protocolID));
                    break;
            }
        }

        // This is the case that determines where the player is going to be. It will send the
        // TransSceneType and GameLocation.
        // There are multiple locations to send such as Space, Room, Story etc.
        public void SendLoadNextScene(int index)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.LoadNextScene);

            Character charIndex = Server.GetClientByIndex(index).Character;

            buffer.Write((byte)charIndex.getTransSceneType());
            buffer.Write((byte)charIndex.GameLocation);

            switch (charIndex.GameLocation)
            {
                case GameLocation.Starter:
                    buffer.Write((uint)3027); //ColonialBonusGUID
                    buffer.Write((uint)3127); //CylonBonusGUID
                    break;
                case GameLocation.Avatar:
                    buffer.Write((ushort)0); // No idea since the game doesn't use this (it does, but for a loop that does nothing).
                    buffer.Write(false); // No idea since the game doesn't use this.
                    break;
                case GameLocation.Room:

                    break;
                case GameLocation.Space:
                case GameLocation.Story:
                case GameLocation.BattleSpace:
                case GameLocation.Tournament:
                case GameLocation.Tutorial:
                case GameLocation.Teaser:
                    // I'm not sure where to send these. I feel like sending every time the user enters the
                    // Space is wrong.

                    PlayerProtocol.GetProtocol().SendPlayerId(index);
                    PlayerProtocol.GetProtocol().SendName(index);
                    PlayerProtocol.GetProtocol().SendAvatar(index);
                    PlayerProtocol.GetProtocol().SendFaction(index);
                    PlayerProtocol.GetProtocol().SendPlayerShips(index, 100, (uint)22131177);

                    PlayerProtocol.GetProtocol().SetActivePlayerShip(index, 100);

                    // I don't know which values to give so I'm just giving the numbers in order. E.g:
                    // ColonialBonusGUID was 3027 and CylonBonusGUID was 3127. So here we have 1327 and 1427 :) lol
                    buffer.Write(Server.GetClientByIndex(index).Character.sectorId); // sector id
                    buffer.Write(Server.GetSectorById(Server.GetClientByIndex(index).Character.sectorId).sectorGuid); // cardGuid2
                    break;
            }

            SendMessageToUser(index, buffer);
        }

        private void SceneLoaded(int index)
        {
            switch (Server.GetClientByIndex(index).Character.GameLocation)
            {
                // I'm not sure about this one but I did it similar to the server showed on the video on my channel.
                case GameLocation.Space:
                case GameLocation.Story:
                case GameLocation.BattleSpace:
                case GameLocation.Tournament:
                case GameLocation.Tutorial:
                case GameLocation.Teaser:
                    //PlayerProtocol.GetProtocol().SendUnanchor(index, (uint)index);
                    break;
            }
        }
    }
}
