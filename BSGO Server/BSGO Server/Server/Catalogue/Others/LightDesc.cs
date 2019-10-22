using System.Drawing;
using System.Numerics;

namespace BSGO_Server
{
    internal class LightDesc : IProtocolWrite
    {
        public Quaternion rotation { get; set; }
        public Color color { get; set; }
        public float intensity { get; set; }

        public LightDesc(Quaternion rotation, Color color, float intensity)
        {
            this.rotation = rotation;
            this.color = color;
            this.intensity = intensity;
        }

        public void Write(BgoProtocolWriter w)
        {
            w.Write(rotation);
            w.Write(color);
            w.Write(intensity);
        }
    }
}
