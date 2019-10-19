using System.Drawing;

namespace BSGO_Server
{
    internal class SectorCard : Card
    {
        public float Width { get; set; }
        public float Height { get; set; }
        public float Length { get; set; }
        public uint RegulationCardGUID { get; set; }
        public Color AmbientColor { get; set; }
        public Color FogColor { get; set; }
        public int FogDensity { get; set; }
        public Color DustColor { get; set; }
        public int DustDensity { get; set; }
        public BackgroundDesc NebulaDesc { get; set; }
        public BackgroundDesc StarsDesc { get; set; }
        public BackgroundDesc StarsMultDesc { get; set; }
        public BackgroundDesc StarsVarianceDesc { get; set; }
        public MovingNebulaDesc[] MovingNebulaDescs { get; set; }
        public LightDesc[] LightDescs { get; set; }
        public SunDesc[] SunDescs { get; set; }
        public JGlobalFog GlobalFogDesc { get; set; }
        public JCameraFx CameraFxDesc { get; set; }
        public string[] RequiredAssets { get; set; }

        public SectorCard(uint cardGUID, CardView cardView, float width, float height, float length, uint regulationCardGUID, Color ambientColor, Color fogColor, int fogDensity, Color dustColor, int dustDensity, BackgroundDesc nebulaDesc, BackgroundDesc starsDesc, BackgroundDesc starsMultDesc, BackgroundDesc starsVarianceDesc, MovingNebulaDesc[] movingNebulaDescs, LightDesc[] lightDescs, SunDesc[] sunDescs, JGlobalFog globalFogDesc, JCameraFx cameraFxDesc, string[] requiredAssets)
            : base(cardGUID, cardView)
        {
            Width = width;
            Height = height;
            Length = length;
            RegulationCardGUID = regulationCardGUID;
            AmbientColor = ambientColor;
            FogColor = fogColor;
            FogDensity = fogDensity;
            DustColor = dustColor;
            DustDensity = dustDensity;
            NebulaDesc = nebulaDesc;
            StarsDesc = starsDesc;
            StarsMultDesc = starsMultDesc;
            StarsVarianceDesc = starsVarianceDesc;
            MovingNebulaDescs = movingNebulaDescs;
            LightDescs = lightDescs;
            SunDescs = sunDescs;
            GlobalFogDesc = globalFogDesc;
            CameraFxDesc = cameraFxDesc;
            RequiredAssets = requiredAssets;
        }

        public override void Write(BgoProtocolWriter w)
        {
            base.Write(w);
            w.Write(Width);
            w.Write(Height);
            w.Write(Length);
            w.Write(RegulationCardGUID);
            w.Write(AmbientColor);
            w.Write(FogColor);
            w.Write(FogDensity);
            w.Write(DustColor);
            w.Write(DustDensity);
            NebulaDesc.Write(w);
            StarsDesc.Write(w);
            StarsMultDesc.Write(w);
            StarsVarianceDesc.Write(w);
            int num = MovingNebulaDescs.Length;
            w.Write((ushort)num);
            for(int i = 0; i < num; i++)
                MovingNebulaDescs[i].Write(w);
            
            int num2 = LightDescs.Length;
            w.Write((ushort)num2);
            for (int j = 0; j < num2; j++)
                LightDescs[j].Write(w);
            
            int num3 = SunDescs.Length;
            w.Write((ushort)num3);
            for (int k = 0; k < num3; k++)
                SunDescs[k].Write(w);
            
            GlobalFogDesc.Write(w);
            CameraFxDesc.Write(w);
            w.Write(RequiredAssets);
        }
    }
}
