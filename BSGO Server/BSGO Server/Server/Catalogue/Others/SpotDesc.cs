using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace BSGO_Server
{
    class SpotDesc : IProtocolWrite
    {
        public ushort ObjectPointServerHash;

        public string ObjectPointName;

        public SpotType Type;

        public Vector3 LocalPosition;

        public Quaternion LocalRotation;

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
