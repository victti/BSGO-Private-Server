using System.Drawing;
using BSGO_Server._3dAlgorithm;

namespace BSGO_Server
{
    class MovingNebulaDesc : IProtocolWrite
    {
        public string modelName { get; set; } = "movingnebula";
        public string matSuffix { get; set; } = "1";
        public Color color { get; set; } = Color.White;
        public System.Numerics.Vector2 textureOffset { get; set; } = new System.Numerics.Vector2(0f, 0f);
        public System.Numerics.Vector2 textureScale { get; set; } = new System.Numerics.Vector2(1f, 1f);
        public Vector3 position { get; set; }
        public Quaternion rotation { get; set; }
        public Vector3 scale { get; set; } = new Vector3(1f, 1f, 1f);

        public MovingNebulaDesc(string modelName, string matSuffix, Color color, System.Numerics.Vector2 textureOffset, System.Numerics.Vector2 textureScale, Vector3 position, Quaternion rotation, Vector3 scale)
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
