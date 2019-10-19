namespace BSGO_Server
{
    internal class MovementCard : Card
    {
        public float MinYawSpeed { get; set; } = 3f;

        public float MaxPitch { get; set; } = 55f;

        public float MaxRoll { get; set; } = 50f;

        public float PitchFading { get; set; } = 0.3f;

        public float YawFading { get; set; } = 0.3f;

        public float RollFading { get; set; } = 0.6f;

        public MovementCard(uint cardGUID, CardView cardView)
            : base(cardGUID, cardView) { }

        public MovementCard(uint cardGUID, CardView cardView, float minYawSpeed, float maxPitch, float maxRoll, float pitchFading, float yawFading, float rollFading)
            : base(cardGUID, cardView)
        {
            MinYawSpeed = minYawSpeed;
            MaxPitch = maxPitch;
            MaxRoll = maxRoll;
            PitchFading = pitchFading;
            YawFading = yawFading;
            RollFading = rollFading;
        }

        public override void Write(BgoProtocolWriter w)
        {
            base.Write(w);
            w.Write(MinYawSpeed);
            w.Write(MaxPitch);
            w.Write(MaxRoll);
            w.Write(PitchFading);
            w.Write(YawFading);
            w.Write(RollFading);
        }
    }
}
