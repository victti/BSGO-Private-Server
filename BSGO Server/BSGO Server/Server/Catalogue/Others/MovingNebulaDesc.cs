using System.Drawing;
using System.Numerics;

namespace BSGO_Server
{
    internal class MovingNebulaDesc : IProtocolWrite
    {
        public string ModelName { get; set; } = "movingnebula";
        public string MatSuffix { get; set; } = "1";
        public Color Color { get; set; } = Color.White;
        public Vector2 TextureOffset { get; set; } = new Vector2(0f, 0f);
        public Vector2 TextureScale { get; set; } = new Vector2(1f, 1f);
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
        public Vector3 Scale { get; set; } = new Vector3(1f, 1f, 1f);

        public MovingNebulaDesc(string modelName, string matSuffix, Color color, Vector2 textureOffset, Vector2 textureScale, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            ModelName = modelName;
            MatSuffix = matSuffix;
            Color = color;
            TextureOffset = textureOffset;
            TextureScale = textureScale;
            Position = position;
            Rotation = rotation;
            Scale = scale;
        }

        public void Write(BgoProtocolWriter w)
        {
            w.Write(MatSuffix);
            w.Write(ModelName);
            w.Write(Rotation);
            w.Write(Position);
            w.Write(Scale);
            w.Write(TextureOffset);
            w.Write(TextureScale);
            w.Write(Color);
        }
    }
}
