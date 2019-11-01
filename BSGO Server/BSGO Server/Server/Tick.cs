using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    internal class Tick : IComparable<Tick>, IProtocolWrite, IProtocolRead
    {
        private const double tickTime = 0.1;

        private const double ticksInSec = 10.0;

        public const float Delta = 0.1f;

        private bool isLastValid;

        private Tick last;

        private Tick current;

        private Tick next;

        private bool isNewTick;

        public int value;

        public double Time
        {
            get
            {
                return (double)value * 0.1;
            }
        }

        public Tick Current
        {
            get
            {
                return current;
            }
        }

        public Tick Next
        {
            get
            {
                return next;
            }
        }

        public Tick Last
        {
            get
            {
                return last;
            }
        }

        public Tick(int value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return string.Format("tick{0}", value);
        }

        //public void Nearest(double time, out Tick previousTick, out Tick nextTick)
        //{
        //    double d = time * 10.0;
        //    previousTick.value = (int)Math.Floor(d);
        //    nextTick.value = previousTick.value + 1;
        //}

        public static Tick Previous(double time)
        {
            return new Tick((int)Math.Floor(time * 10.0));
        }

        public bool IsNewTick()
        {
            return isNewTick;
        }

        public void Init()
        {
        }

        public void Update(double currentSectorTime)
        {
            if (isLastValid)
            {
                last = current;
                current = Previous(currentSectorTime);
                if (current < last)
                {
                    current = last;
                }
                isNewTick = (current != last);
                next = current + 1;
            }
            else
            {
                current = Previous(currentSectorTime);
                last = current - 1;
                next = current + 1;
                isNewTick = true;
                isLastValid = true;
                //Log.Add("WARNING: ticks reset " + Last + " current ");
            }
        }

        public void Reset(double currentSectorTime)
        {
            isLastValid = false;
            Update(currentSectorTime);
        }

        public void Write(BgoProtocolWriter pw)
        {
            pw.Write(value);
        }

        public void Read(BgoProtocolReader pr)
        {
            value = pr.ReadInt32();
        }

        public override int GetHashCode()
        {
            return value;
        }

        public override bool Equals(object obj)
        {
            if (obj is Tick)
            {
                int num = value;
                Tick tick = (Tick)obj;
                return num == tick.value;
            }
            return false;
        }

        public int CompareTo(Tick other)
        {
            if (value < other.value)
            {
                return -1;
            }
            if (value > other.value)
            {
                return 1;
            }
            return 0;
        }

        public static bool operator ==(Tick a, Tick b)
        {
            return a.value == b.value;
        }

        public static bool operator !=(Tick a, Tick b)
        {
            return a.value != b.value;
        }

        public static bool operator <(Tick a, Tick b)
        {
            return a.value < b.value;
        }

        public static bool operator <=(Tick a, Tick b)
        {
            return a.value <= b.value;
        }

        public static bool operator >=(Tick a, Tick b)
        {
            return a.value >= b.value;
        }

        public static bool operator >(Tick a, Tick b)
        {
            return a.value > b.value;
        }

        public static int operator -(Tick a, Tick b)
        {
            return a.value - b.value;
        }

        public static Tick operator +(Tick a, int b)
        {
            return new Tick(a.value + b);
        }

        public static Tick operator -(Tick a, int b)
        {
            return new Tick(a.value - b);
        }

        public static Tick operator ++(Tick a)
        {
            a.value++;
            return a;
        }

        public static Tick operator -(Tick a)
        {
            a.value--;
            return a;
        }
    }
}
