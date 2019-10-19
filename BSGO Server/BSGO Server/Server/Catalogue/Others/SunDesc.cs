using System.Drawing;
using System.Numerics;

namespace BSGO_Server
{
    internal class SunDesc : IProtocolWrite
    {
        public Color RaysColor { get; set; } = Color.White;
        public Color StreakColor { get; set; } = Color.White;
        public Color GlowColor { get; set; } = Color.Red;
        public Color DiscColor { get; set; } = Color.White;
        public bool OcclusionFade { get; set; } = true;
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
        public Vector3 Scale { get; set; } = new Vector3(1f, 1f, 1f);

        public SunDesc(Color raysColor, Color streakColor, Color glowColor, Color discColor, bool occlusionFade, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            RaysColor = raysColor;
            StreakColor = streakColor;
            GlowColor = glowColor;
            DiscColor = discColor;
            OcclusionFade = occlusionFade;
            Position = position;
            Rotation = rotation;
            Scale = scale;
        }

        public void Write(BgoProtocolWriter w)
        {
            w.Write(RaysColor);
            w.Write(StreakColor);
            w.Write(GlowColor);
            w.Write(DiscColor);
            w.Write(OcclusionFade);
            w.Write(Rotation);
            w.Write(Position);
            w.Write(Scale);
        }
    }
}
