using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server._3dAlgorithm
{
    internal class QWEASD
    {
        private int bitmask;

        private bool inputChanged;

        public int Bitmask
        {
            get
            {
                return bitmask;
            }
            set
            {
                bitmask = value;
            }
        }

        public bool InputChanged
        {
            get
            {
                return inputChanged;
            }
        }

        public int Pitch
        {
            get
            {
                return Bit(0) - Bit(2);
            }
        }

        public int Yaw
        {
            get
            {
                return Bit(3) - Bit(1);
            }
        }

        public int Roll
        {
            get
            {
                return Bit(4) - Bit(5);
            }
        }

        public QWEASD()
        {
            bitmask = 0;
        }

        public QWEASD(int bitmask)
        {
            this.bitmask = bitmask;
        }

        public void ResetKeyStates()
        {
            bitmask = 0;
        }

        public void Flush()
        {
            inputChanged = false;
        }

        public System.Numerics.Vector2 DeriveDirectionVectorFromPressedKeys()
        {
            Vector3 zero = Vector3.zero;
            zero += Bit(0) * Vector3.up;
            zero += Bit(1) * Vector3.left;
            zero += Bit(2) * Vector3.down;
            zero += Bit(3) * Vector3.right;
            return new System.Numerics.Vector2(zero.x, zero.y);
        }

        public void SetBit(int bit, bool bActive)
        {
            if (bActive)
            {
                bitmask = (Bitmask | bit);
            }
            else
            {
                bitmask = (Bitmask & (63 - bit));
            }
        }

        public bool IsAnyKeyPressed()
        {
            return Bitmask > 0;
        }

        private int Bit(int n)
        {
            return (Bitmask & (1 << n)) >> n;
        }
    }
}
