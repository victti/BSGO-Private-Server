using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    class SceneProtocol : Protocol
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
            ushort msgType = (ushort)br.ReadUInt16();

            switch ((Request)msgType)
            {
                case Request.QuitLogin:
                    SendLoadNextScene(index);
                    break;
                default:
                    Log.Add(LogSeverity.ERROR, string.Format("Unknown msgType \"{0}\" on {1}Protocol.", (Request)msgType, protocolID));
                    break;
            }
        }

        // This is the case that determines where the player is going to be. It will send the
        // TransSceneType and GameLocation.
        // It should check if the character does exist or not. If he doesn't, then we should go to the
        // character creation (Starter). There are multiple locations to send such as Space, Room, Story etc.
        // In this case, we have a fake database, in order to keep track of things and set them the way we want.
        private void SendLoadNextScene(int index)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.LoadNextScene);

            Character charIndex = Server.GetClientByIndex(index).Character;

            buffer.Write((byte)charIndex.getTransSceneType());
            buffer.Write((byte)charIndex.GameLocation);

            switch (charIndex.GameLocation)
            {
                case GameLocation.Starter:
                    buffer.Write((uint)1); //ColonialBonusGUID
                    buffer.Write((uint)2); //CylonBonusGUID
                    break;
                case GameLocation.Room:

                    break;
                case GameLocation.Space:

                    break;
            }

            SendMessageToUser(index, buffer);
        }
    }
}
