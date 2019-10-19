using System.Drawing;
using System.Numerics;

namespace BSGO_Server
{
    internal class BackgroundDesc : IProtocolWrite
    {
        public string PrefabName { get; set; }
        public Quaternion Rotation { get; set; }
        public Color Color { get; set; }
        public Vector3 Position { get; set; }

        public BackgroundDesc(string prefabName, Quaternion rotation, Color color, Vector3 position)
        {
            PrefabName = prefabName;
            Rotation = rotation;
            Color = color;
            Position = position;
        }

        public void Write(BgoProtocolWriter w)
        {
            w.Write(PrefabName);
            w.Write(Rotation);
            w.Write(Color);
            w.Write(Position);
        }
    }
}
