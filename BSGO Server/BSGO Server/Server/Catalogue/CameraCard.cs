using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    class CameraCard : Card
    {
        public float DefaultZoom = 1f;
        public float MinZoom = 10f;
        public float MaxZoom = 20f;
        public float SoftTrembleSpeed = 1f;
        public float HardTrembleSpeed = 1f;

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
