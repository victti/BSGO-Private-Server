using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server._3dAlgorithm
{
    internal struct MovementFrame : IProtocolWrite
    {
        public Vector3 position;

        public Euler3 euler3;

        public Vector3 linearSpeed;

        public Vector3 strafeSpeed;

        public Euler3 Euler3Speed;

        public int mode;

        public bool valid;

        private static MovementFrame invalid = InvalidFrame();

        public Quaternion rotation
        {
            get
            {
                return euler3.rotation;
            }
            set
            {
                euler3 = Euler3.Rotation(value);
            }
        }

        public Vector3 lookDirection
        {
            get
            {
                return euler3.direction;
            }
        }

        public Vector3 nextPosition
        {
            get
            {
                return position + (linearSpeed + strafeSpeed) * 0.1f;
            }
        }

        public Euler3 NextEuler3
        {
            get
            {
                if (mode == 0)
                {
                    return (euler3 + Euler3Speed * 0.1f).Normalized(true);
                }
                if (mode == 1)
                {
                    return (euler3 + Euler3Speed * 0.1f).Normalized(false);
                }
                if (mode == 2)
                {
                    return Euler3.RotateOverTime(euler3, Euler3Speed, 0.1f);
                }
                if (mode == 3)
                {
                    return Euler3.RotateOverTimeLocal(euler3, Euler3Speed, 0.1f);
                }
                //Debug.LogError("MovementFrame.nextEuler: unknown mode " + mode);
                return Euler3.zero;
            }
        }

        public static MovementFrame Invalid
        {
            get
            {
                return invalid;
            }
        }

        public MovementFrame(Vector3 position, Euler3 euler3, Vector3 linearSpeed, Vector3 strafeSpeed, Euler3 euler3Speed)
        {
            this.position = position;
            this.euler3 = euler3;
            this.linearSpeed = linearSpeed;
            this.strafeSpeed = strafeSpeed;
            Euler3Speed = euler3Speed;
            mode = 0;
            valid = true;
        }

        public Vector3 GetFuturePosition(float t)
        {
            return position + (linearSpeed + strafeSpeed) * t;
        }

        public Quaternion GetFutureRotation(float t)
        {
            if (mode == 2)
            {
                return Euler3.RotateOverTime(euler3, Euler3Speed, t).rotation;
            }
            if (mode == 3)
            {
                return Euler3.RotateOverTimeLocal(euler3, Euler3Speed, t).rotation;
            }
            return (euler3 + Euler3Speed * t).rotation;
        }

        public override string ToString()
        {
            if (valid)
            {
                return string.Format("MovementFrame: position = {0}, euler = {1}, linearSpeed = {2}, eulerSpeed = {3}", position, euler3, linearSpeed, Euler3Speed);
            }
            return "MovemantFrame.Invalid";
        }

        private static MovementFrame InvalidFrame()
        {
            MovementFrame result = default(MovementFrame);
            result.position = Vector3.zero;
            result.linearSpeed = Vector3.zero;
            result.Euler3Speed = Euler3.zero;
            result.euler3 = Euler3.identity;
            result.mode = 0;
            result.valid = false;
            return result;
        }

        public void Write(BgoProtocolWriter pw)
        {
            pw.Write(position);
            pw.Write(euler3);
            pw.Write(linearSpeed);
            pw.Write(strafeSpeed);
            pw.Write(Euler3Speed);
            pw.Write((byte)mode);
        }
    }
}
