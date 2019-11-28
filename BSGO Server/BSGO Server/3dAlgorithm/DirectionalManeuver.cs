using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server._3dAlgorithm
{
    internal class DirectionalManeuver : Maneuver
    {
        protected Euler3 direction;

        protected bool slide;

        public DirectionalManeuver() { }

        public DirectionalManeuver(ManeuverType maneuverType, int startTick, Euler3 direction, MovementOptions movementOptions)
        {
            base.ManeuverType = maneuverType;
            this.startTick = new Tick(startTick);
            this.direction = direction;
            options = movementOptions;
        }

        public override string ToString()
        {
            return string.Format("DirectionalManeuver: direction={0}", direction);
        }

        public override MovementFrame NextFrame(Tick tick, MovementFrame prevFrame)
        {
            return MoveToDirection(prevFrame, direction);
        }

        public void Read(ManeuverType maneuverType, int startTick, Euler3 direction, MovementOptions movementOptions)
        {
            base.ManeuverType = maneuverType;
            this.startTick = new Tick(startTick);
            this.direction = direction;
            options = movementOptions;
        }
    }
}
