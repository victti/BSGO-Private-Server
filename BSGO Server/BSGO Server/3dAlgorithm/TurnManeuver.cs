using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server._3dAlgorithm
{
    internal class TurnManeuver : Maneuver
    {
        protected QWEASD qweasd;

        public TurnManeuver() { }

        public TurnManeuver(ManeuverType maneuverType, int startTick, QWEASD qweasd, MovementOptions movementOptions)
        {
            base.ManeuverType = maneuverType;
            this.startTick = new Tick(startTick);
            this.qweasd = qweasd;
            options = movementOptions;
        }

        public override string ToString()
        {
            return string.Format("TurnManeuver: pitch={0}, yaw={1}, roll{2}", qweasd.Pitch, qweasd.Yaw, qweasd.Roll);
        }

        public override MovementFrame NextFrame(Tick tick, MovementFrame prevFrame)
        {
            if (!prevFrame.valid)
            {
                return MovementFrame.Invalid;
            }
            if (qweasd == null)
                qweasd = new QWEASD();
            return Simulation.WASD(prevFrame, qweasd.Pitch, qweasd.Yaw, qweasd.Roll, options);
        }

        public void Read(ManeuverType maneuverType, int startTick, QWEASD qweasd, MovementOptions movementOptions)
        {
            base.ManeuverType = maneuverType;
            this.startTick = new Tick(startTick);
            this.qweasd = qweasd;
            options = movementOptions;
        }
    }
}
