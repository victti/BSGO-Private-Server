using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    internal class RoomProtocol : Protocol
    {
        public enum Request : ushort
        {
            Talk = 0,
            NpcMarks = 2,
            EnterDoor = 4,
            Quit = 5,
            Enter = 6
        }

        public enum Reply : ushort
        {
            Talk = 1,
            NpcMarks = 3
        }

        public RoomProtocol()
    : base(ProtocolID.Room)
        {
        }

        public static RoomProtocol GetProtocol()
        {
            return ProtocolManager.GetProtocol(ProtocolID.Room) as RoomProtocol;
        }

        public override void ParseMessage(int index, BgoProtocolReader br)
        {
            ushort msgType = br.ReadUInt16();

            switch ((Request)msgType)
            {
                case Request.Quit:
                    SendQuit(index);
                    break;
                default:
                    Log.Add(LogSeverity.ERROR, string.Format("Unknown msgType \"{0}\" on {1}Protocol.", (Request)msgType, protocolID));
                    break;
            }
        }

        private void SendQuit(int index)
        {
            Server.GetClientByIndex(index).Character.GameLocation = GameLocation.Space;
        }
    }
}
