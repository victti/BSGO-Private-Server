using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server._3dAlgorithm
{
    internal abstract class Maneuver : IComparable<Maneuver>, IProtocolRead
    {
        protected Tick startTick;

        protected MovementOptions options = new MovementOptions();

        protected byte isExclusive = 1;

        public ManeuverType ManeuverType
        {
            get;
            set;
        }

        public MovementCard Card
        {
            set
            {
                options.ApplyCard(value);
            }
        }

        public byte IsExclusive
        {
            get
            {
                return isExclusive;
            }
        }

        public Maneuver()
        {
        }

        public Maneuver(Tick startTick)
        {
            this.startTick = startTick;
        }

        public Tick GetStartTick()
        {
            if (startTick is null)
                startTick = new Tick(0);
            return startTick;
        }

        public Gear GetGear()
        {
            return options.gear;
        }

        public float GetMarchSpeed()
        {
            return options.speed;
        }

        public abstract MovementFrame NextFrame(Tick tick, MovementFrame prevFrame);

        public virtual void Read(BgoProtocolReader pr)
        {
            ManeuverType = (ManeuverType)pr.ReadByte();
        }

        public int CompareTo(Maneuver other)
        {
            int num = startTick.CompareTo(other.startTick);
            if (num == 0)
            {
                if ((int)ManeuverType < (int)other.ManeuverType)
                {
                    num = -1;
                }
                else if ((int)ManeuverType > (int)other.ManeuverType)
                {
                    num = 1;
                }
                else if (ManeuverType == other.ManeuverType)
                {
                    num = 0;
                }
            }
            return num;
        }

        protected MovementFrame MoveToDirection(MovementFrame prevFrame, Euler3 direction)
        {
            if (!prevFrame.valid)
            {
                return MovementFrame.Invalid;
            }
            return Simulation.MoveToDirection(prevFrame, direction, options);
        }

        protected MovementFrame Drift(MovementFrame prevFrame)
        {
            if (!prevFrame.valid)
            {
                return MovementFrame.Invalid;
            }
            return Simulation.WASD(prevFrame, 0, 0, 0, options);
        }
    }
}
