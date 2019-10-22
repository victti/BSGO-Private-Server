using System.Drawing;

namespace BSGO_Server
{
    internal class JGlobalFog : IProtocolWrite
    {
        public bool Enabled { get; set; }
        public Color Color { get; set; } //= new Color(0.427f, 0.534f, 0.538f, 0.314f);
        public float Density { get; set; } = 0.0015f;
        public float StartDistance { get; set; }

        public JGlobalFog(bool enabled, Color color, float density, float startDistance)
        {
            Enabled = enabled;
            Color = color;
            Density = density;
            StartDistance = startDistance;
        }

        public void Write(BgoProtocolWriter w)
        {
            w.Write(Enabled);
            w.Write(Color);
            w.Write(Density);
            w.Write(StartDistance);
        }
    }
}
