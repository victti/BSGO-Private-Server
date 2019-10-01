using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace BSGO_Server
{
    class SunDesc : IProtocolWrite
    {
        public Color raysColor = Color.White;
        public Color streakColor = Color.White;
        public Color glowColor = Color.Red;
        public Color discColor = Color.White;
        public bool occlusionFade = true;
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale = new Vector3(1f, 1f, 1f);

        public SunDesc(Color raysColor, Color streakColor, Color glowColor, Color discColor, bool occlusionFade, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            this.raysColor = raysColor;
            this.streakColor = streakColor;
            this.glowColor = glowColor;
            this.discColor = discColor;
            this.occlusionFade = occlusionFade;
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
        }

        public void Write(BgoProtocolWriter w)
        {
            w.Write(raysColor);
            w.Write(streakColor);
            w.Write(glowColor);
            w.Write(discColor);
            w.Write(occlusionFade);
            w.Write(rotation);
            w.Write(position);
            w.Write(scale);
        }
    }
}
