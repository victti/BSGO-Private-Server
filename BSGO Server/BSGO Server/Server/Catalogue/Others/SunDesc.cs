using System.Drawing;
using BSGO_Server._3dAlgorithm;

namespace BSGO_Server
{
    class SunDesc : IProtocolWrite
    {
        public Color raysColor { get; set; } = Color.White;
        public Color streakColor { get; set; } = Color.White;
        public Color glowColor { get; set; } = Color.Red;
        public Color discColor { get; set; } = Color.White;
        public bool occlusionFade { get; set; } = true;
        public Vector3 position { get; set; }
        public Quaternion rotation { get; set; }
        public Vector3 scale { get; set; } = new Vector3(1f, 1f, 1f);

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
