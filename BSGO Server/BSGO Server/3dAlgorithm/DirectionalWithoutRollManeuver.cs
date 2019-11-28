using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server._3dAlgorithm
{
    internal class DirectionalWithoutRollManeuver : Maneuver
    {
        protected Euler3 direction;

        protected bool slide;

        public DirectionalWithoutRollManeuver() { }

        public DirectionalWithoutRollManeuver(ManeuverType maneuverType, int startTick, Euler3 direction, MovementOptions movementOptions)
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
            return Simulation.MoveToDirectionWithoutRoll(prevFrame, direction, options);
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
