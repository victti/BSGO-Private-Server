using System.Numerics;

namespace BSGO_Server
{
    internal class SpotDesc : IProtocolWrite
    {
        public ushort ObjectPointServerHash { get; set; }

        public string ObjectPointName { get; set; }

        public SpotType Type { get; set; }

        public Vector3 LocalPosition { get; set; }

        public Quaternion LocalRotation { get; set; }

        public SpotDesc(ushort objectPointServerHash, string objectPointName, SpotType type, Vector3 localPosition, Quaternion localRotation)
        {
            ObjectPointServerHash = objectPointServerHash;
            ObjectPointName = objectPointName;
            Type = type;
            LocalPosition = localPosition;
            LocalRotation = localRotation;
        }

        public void Write(BgoProtocolWriter w)
        {
            w.Write(ObjectPointServerHash);
            w.Write(ObjectPointName);
            w.Write((byte)Type);
            w.Write(LocalPosition);
            w.Write(LocalRotation);
        }
    }
}
