namespace BSGO_Server
{
    internal class CameraCard : Card
    {
        public float DefaultZoom { get; set; } = 1f;
        public float MinZoom { get; set; } = 10f;
        public float MaxZoom { get; set; } = 20f;
        public float SoftTrembleSpeed { get; set; } = 1f;
        public float HardTrembleSpeed { get; set; } = 1f;

        public CameraCard(uint cardGUID, CardView cardView, float defaultZoom, float minZoom, float maxZoom, float softTrembleSpeed, float hardTrembleSpeed)
            : base(cardGUID, cardView)
        {
            DefaultZoom = defaultZoom;
            MinZoom = minZoom;
            MaxZoom = maxZoom;
            SoftTrembleSpeed = softTrembleSpeed;
            HardTrembleSpeed = hardTrembleSpeed;
        }

        public override void Write(BgoProtocolWriter w)
        {
            base.Write(w);
            w.Write(DefaultZoom);
            w.Write(MaxZoom);
            w.Write(MinZoom);
            w.Write(SoftTrembleSpeed);
            w.Write(HardTrembleSpeed);
        }
    }
}
