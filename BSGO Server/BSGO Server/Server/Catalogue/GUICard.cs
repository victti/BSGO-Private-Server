using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    class GUICard : Card
    {
        public string Key = string.Empty;

        public byte Level;

        private string[] Args;

        public string GUIIcon;

        public string GUITexturePath = string.Empty;

        public string GUIAtlasTexturePath = string.Empty;

        public string GUIAvatarSlotTexturePath = string.Empty;

        public ushort FrameIndex;

        public GUICard(uint cardGUID, CardView cardView, string key, byte level, string GUIAtlasTexturePath, ushort frameIndex, string GUIIcon, string GUIAvatarSlotTexturePath, string GUITexturePath, string[] args)
            : base(cardGUID, cardView)
        {
            Key = key;
            Level = level;
            this.GUIAtlasTexturePath = GUIAtlasTexturePath;
            FrameIndex = frameIndex;
            this.GUIIcon = GUIIcon;
            this.GUIAvatarSlotTexturePath = GUIAvatarSlotTexturePath;
            this.GUITexturePath = GUITexturePath;
            Args = args;
        }

        public override void Write(BgoProtocolWriter w)
        {
            base.Write(w);
            w.Write(Key);
            w.Write(Level);
            w.Write(GUIAtlasTexturePath);
            w.Write(FrameIndex);
            w.Write(GUIIcon);
            w.Write(GUIAvatarSlotTexturePath);
            w.Write(GUITexturePath);
            w.Write(Args);
        }
    }
}
