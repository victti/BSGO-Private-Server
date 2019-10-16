using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BSGO_Server
{
    class SectorCard : Card
    {
        public float Width;
        public float Height;
        public float Length;
        public uint RegulationCardGUID;
        public Color AmbientColor;
        public Color FogColor;
        public int FogDensity;
        public Color DustColor;
        public int DustDensity;
        public BackgroundDesc NebulaDesc;
        public BackgroundDesc StarsDesc;
        public BackgroundDesc StarsMultDesc;
        public BackgroundDesc StarsVarianceDesc;
        public MovingNebulaDesc[] MovingNebulaDescs;
        public LightDesc[] LightDescs;
        public SunDesc[] SunDescs;
        public JGlobalFog GlobalFogDesc;
        public JCameraFx CameraFxDesc;
        public string[] RequiredAssets;

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
            {
                MovingNebulaDescs[i].Write(w);
            }
            int num2 = LightDescs.Length;
            w.Write((ushort)num2);
            for (int j = 0; j < num2; j++)
            {
                LightDescs[j].Write(w);
            }
            int num3 = SunDescs.Length;
            w.Write((ushort)num3);
            for (int k = 0; k < num3; k++)
            {
                SunDescs[k].Write(w);
            }
            GlobalFogDesc.Write(w);
            CameraFxDesc.Write(w);
            w.Write(RequiredAssets);
        }
    }
}
