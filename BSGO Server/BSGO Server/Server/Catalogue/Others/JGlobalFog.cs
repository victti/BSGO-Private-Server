using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BSGO_Server
{
    class JGlobalFog : IProtocolWrite
    {
        public bool Enabled;
        public Color Color; //= new Color(0.427f, 0.534f, 0.538f, 0.314f);
        public float Density = 0.0015f;
        public float StartDistance;

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
