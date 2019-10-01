using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace BSGO_Server
{
    class LightDesc : IProtocolWrite
    {
        public Quaternion rotation;
        public Color color;
        public float intensity;

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
