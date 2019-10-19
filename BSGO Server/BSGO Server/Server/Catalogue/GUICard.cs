namespace BSGO_Server
{
    internal class GUICard : Card
    {
        public string Key { get; set; } = string.Empty;

        public byte Level { get; set; }

        private readonly string[] args;

        public string GUIIcon { get; set; }

        public string GUITexturePath { get; set; } = string.Empty;

        public string GUIAtlasTexturePath { get; set; } = string.Empty;

        public string GUIAvatarSlotTexturePath { get; set; } = string.Empty;

        public ushort FrameIndex { get; set; }

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
            this.args = args;
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
            w.Write(args);
        }
    }
}
