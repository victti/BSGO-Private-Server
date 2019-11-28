using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server._3dAlgorithm
{
    class RestManeuver : Maneuver
    {
        protected Vector3 position;

        protected Euler3 euler3;

        public override string ToString()
        {
            return string.Format("RestManeuver: position={0}, euler={1}", position, euler3);
        }

        public RestManeuver(ManeuverType maneuverType, int startTick, Vector3 position, Euler3 euler3)
        {
            base.ManeuverType = maneuverType;
            this.startTick = new Tick(startTick);
            this.position = position;
            this.euler3 = euler3;
        }

        public override MovementFrame NextFrame(Tick tick, MovementFrame prevFrame)
        {
            return new MovementFrame(position, euler3, Vector3.zero, Vector3.zero, Euler3.zero);
        }

        public void Read(ManeuverType maneuverType, int startTick, Vector3 position, Euler3 euler3)
        {
            base.ManeuverType = maneuverType;
            this.startTick = new Tick(startTick);
            this.position = position;
            this.euler3 = euler3;
        }
    }
}
