using System.Drawing;
using System.Numerics;

namespace BSGO_Server
{
    internal class LightDesc : IProtocolWrite
    {
        public Quaternion Rotation { get; set; }
        public Color Color { get; set; }
        public float Intensity { get; set; }

        public LightDesc(Quaternion rotation, Color color, float intensity)
        {
            Rotation = rotation;
            Color = color;
            Intensity = intensity;
        }

        public void Write(BgoProtocolWriter w)
        {
            w.Write(Rotation);
            w.Write(Color);
            w.Write(Intensity);
        }
    }
}
