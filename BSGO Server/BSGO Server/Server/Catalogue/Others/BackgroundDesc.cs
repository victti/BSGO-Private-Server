using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace BSGO_Server
{
    class BackgroundDesc : IProtocolWrite
    {
        public string prefabName;
        public Quaternion rotation;
        public Color color;
        public Vector3 position;

        public BackgroundDesc(string prefabName, Quaternion rotation, Color color, Vector3 position)
        {
            this.prefabName = prefabName;
            this.rotation = rotation;
            this.color = color;
            this.position = position;
        }

        public void Write(BgoProtocolWriter w)
        {
            w.Write(prefabName);
            w.Write(rotation);
            w.Write(color);
            w.Write(position);
        }
    }
}
