using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace BSGO_Server
{
    class MovingNebulaDesc : IProtocolWrite
    {
        public string modelName = "movingnebula";
        public string matSuffix = "1";
        public Color color = Color.White;
        public Vector2 textureOffset = new Vector2(0f, 0f);
        public Vector2 textureScale = new Vector2(1f, 1f);
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale = new Vector3(1f, 1f, 1f);

        public MovingNebulaDesc(string modelName, string matSuffix, Color color, Vector2 textureOffset, Vector2 textureScale, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            this.modelName = modelName;
            this.matSuffix = matSuffix;
            this.color = color;
            this.textureOffset = textureOffset;
            this.textureScale = textureScale;
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
        }

        public void Write(BgoProtocolWriter w)
        {
            w.Write(matSuffix);
            w.Write(modelName);
            w.Write(rotation);
            w.Write(position);
            w.Write(scale);
            w.Write(textureOffset);
            w.Write(textureScale);
            w.Write(color);
        }
    }
}
