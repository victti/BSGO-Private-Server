using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server._3dAlgorithm
{
    public struct Euler3
    {
        public float pitch;

        public float yaw;

        public float roll;

        public static Euler3 zero
        {
            get
            {
                return new Euler3(0f, 0f, 0f);
            }
        }

        public static Euler3 identity
        {
            get
            {
                return Rotation(Quaternion.identity);
            }
        }

        public Quaternion rotation
        {
            get
            {
                return Quaternion.Euler(pitch, yaw, roll);
            }
        }

        public Vector3 direction
        {
            get
            {
                return rotation * Vector3.forward;
            }
        }

        public Euler3(float pitch, float yaw)
        {
            this.pitch = pitch;
            this.yaw = yaw;
            roll = 0f;
        }

        public Euler3(float pitch, float yaw, float roll)
        {
            this.pitch = pitch;
            this.yaw = yaw;
            this.roll = roll;
        }

        public Euler3 Normalized(bool forceStraight)
        {
            float num = NormAngle(pitch);
            float num2 = yaw;
            float num3 = roll;
            if (forceStraight && (double)Mathf.Abs(num) > 90.0)
            {
                num = NormAngle(179.99f - num);
                num2 += 179.99f;
                num3 += 179.99f;
            }
            return new Euler3(num, NormAngle(num2), NormAngle(num3));
        }

        private static float NormAngle(float angle)
        {
            while (angle <= -180f)
            {
                angle += 360f;
            }
            while (angle > 180f)
            {
                angle -= 360f;
            }
            return angle;
        }

        public static Euler3 Direction(Vector3 direction)
        {
            float num = Mathf.Atan2(direction.x, direction.z) * 57.29578f;
            float num2 = (0f - Mathf.Atan2(direction.y, Mathf.Sqrt(direction.x * direction.x + direction.z * direction.z))) * 57.29578f;
            return new Euler3(num2, num, 0f);
        }

        public static Euler3 Rotation(Quaternion quat)
        {
            float num = quat.x * quat.x;
            float num2 = quat.y * quat.y;
            float num3 = quat.z * quat.z;
            float num4 = quat.w * quat.w;
            float num5 = num + num2 + num3 + num4;
            float num6 = (0f - quat.z) * quat.y + quat.x * quat.w;
            float num7;
            float num8;
            float num9;
            if ((double)num6 > 0.499999 * (double)num5)
            {
                num7 = (float)Math.PI / 2f;
                num8 = 2f * Mathf.Atan2(0f - quat.z, quat.w);
                num9 = 0f;
            }
            else if ((double)num6 < -0.499999 * (double)num5)
            {
                num7 = -(float)Math.PI / 2f;
                num8 = -2f * Mathf.Atan2(0f - quat.z, quat.w);
                num9 = 0f;
            }
            else
            {
                num7 = Mathf.Asin(2f * num6 / num5);
                num8 = Mathf.Atan2(2f * quat.y * quat.w + 2f * quat.z * quat.x, 0f - num - num2 + num3 + num4);
                num9 = Mathf.Atan2(2f * quat.z * quat.w + 2f * quat.y * quat.x, 0f - num + num2 - num3 + num4);
            }
            return new Euler3(num7, num8, num9) * 57.29578f;
        }

        public void Clamp(Euler3 from, Euler3 to)
        {
            pitch = Mathf.Clamp(pitch, from.pitch, to.pitch);
            yaw = Mathf.Clamp(yaw, from.yaw, to.yaw);
            roll = Mathf.Clamp(roll, from.roll, to.roll);
        }

        public void ClampMax(Euler3 max)
        {
            pitch = Mathf.Min(pitch, max.pitch);
            yaw = Mathf.Min(yaw, max.yaw);
            roll = Mathf.Min(roll, max.roll);
        }

        public void ClampMin(Euler3 min)
        {
            pitch = Mathf.Max(pitch, min.pitch);
            yaw = Mathf.Max(yaw, min.yaw);
            roll = Mathf.Max(roll, min.roll);
        }

        public static Euler3 Scale(Euler3 a, Euler3 b)
        {
            return new Euler3(a.pitch * b.pitch, a.yaw * b.yaw, a.roll * b.roll);
        }

        public override string ToString()
        {
            return string.Format("({0},{1},{2})", pitch, yaw, roll);
        }

        public static Euler3 RotateOverTime(Euler3 start, Euler3 changePerSecond, float dt)
        {
            Vector3 vector = changePerSecond.ComponentsToVector3();
            Quaternion lhs = Quaternion.AngleAxis(vector.magnitude * dt, vector.normalized);
            return Rotation(lhs * start.rotation);
        }

        public static Euler3 RotateOverTimeLocal(Euler3 start, Euler3 changePerSecond, float dt)
        {
            Quaternion rhs = Quaternion.Slerp(Quaternion.identity, changePerSecond.rotation, dt);
            return Rotation(start.rotation * rhs);
        }

        public static Quaternion RotateOverTime(Quaternion start, Quaternion changePerSecond, float dt)
        {
            Quaternion lhs = Quaternion.Slerp(Quaternion.identity, changePerSecond, dt);
            return lhs * start;
        }

        public static Quaternion RotateOverTimeLocal(Quaternion start, Quaternion changePerSecond, float dt)
        {
            Quaternion rhs = Quaternion.Slerp(Quaternion.identity, changePerSecond, dt);
            return start * rhs;
        }

        public Vector3 ComponentsToVector3()
        {
            return new Vector3(pitch, yaw, roll);
        }

        public void ComponentsFromVector3(Vector3 input)
        {
            pitch = input.x;
            yaw = input.y;
            roll = input.z;
        }

        public override int GetHashCode()
        {
            return ((ValueType)this).GetHashCode();
        }

        public override bool Equals(object o)
        {
            return this == (Euler3)o;
        }

        public static bool operator ==(Euler3 a, Euler3 b)
        {
            return a.pitch == b.pitch && a.yaw == b.yaw && a.roll == b.roll;
        }

        public static bool operator !=(Euler3 a, Euler3 b)
        {
            return a.pitch != b.pitch || a.yaw != b.yaw || a.roll != b.roll;
        }

        public static Euler3 operator +(Euler3 a, Euler3 b)
        {
            return new Euler3(a.pitch + b.pitch, a.yaw + b.yaw, a.roll + b.roll);
        }

        public static Euler3 operator -(Euler3 a, Euler3 b)
        {
            return new Euler3(a.pitch - b.pitch, a.yaw - b.yaw, a.roll - b.roll);
        }

        public static Euler3 operator -(Euler3 a)
        {
            return new Euler3(0f - a.pitch, 0f - a.yaw, 0f - a.roll);
        }

        public static Euler3 operator *(Euler3 a, float b)
        {
            return new Euler3(a.pitch * b, a.yaw * b, a.roll * b);
        }

        public static Euler3 operator /(Euler3 a, float b)
        {
            return new Euler3(a.pitch / b, a.yaw / b, a.roll / b);
        }
    }
}
