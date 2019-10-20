using System;
namespace BSGO_Server._3dAlgorithm
{
    public struct Euler3
    {
        public float Pitch { get; set; }

        public float Yaw { get; set; }

        public float Roll { get; set; }

        public static Euler3 Zero => new Euler3(0f, 0f, 0f);

        public static Euler3 Identity => GetRotation(Quaternion.Identity);

        public Quaternion Rotation => Quaternion.Euler(Pitch, Yaw, Roll);

        public Vector3 Direction => Rotation * Vector3.Forward;

        public Euler3(float pitch, float yaw, float roll = 0f)
        {
            Pitch = pitch;
            Yaw = yaw;
            Roll = roll;
        }

        public Euler3 Normalized(bool forceStraight)
        {
            float num = NormAngle(Pitch);
            float num2 = Yaw;
            float num3 = Roll;
            if (forceStraight && Mathf.Abs(num) > 90.0)
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

        public static Euler3 GetDirection(Vector3 direction)
        {
            float num = Mathf.Atan2(direction.X, direction.Z) * 57.29578f;
            float num2 = (0f - Mathf.Atan2(direction.Y, Mathf.Sqrt(direction.X * direction.X + direction.Z * direction.Z))) * 57.29578f;
            return new Euler3(num2, num, 0f);
        }

        public static Euler3 GetRotation(Quaternion quat)
        {
            float num = quat.X * quat.X;
            float num2 = quat.Y * quat.Y;
            float num3 = quat.Z * quat.Z;
            float num4 = quat.W * quat.W;
            float num5 = num + num2 + num3 + num4;
            float num6 = (0f - quat.Z) * quat.Y + quat.X * quat.W;
            float num7;
            float num8;
            float num9;
            if (num6 > 0.499999 * num5)
            {
                num7 = (float)Math.PI / 2f;
                num8 = 2f * Mathf.Atan2(0f - quat.Z, quat.W);
                num9 = 0f;
            }
            else if (num6 < -0.499999 * num5)
            {
                num7 = -(float)Math.PI / 2f;
                num8 = -2f * Mathf.Atan2(0f - quat.Z, quat.W);
                num9 = 0f;
            }
            else
            {
                num7 = Mathf.Asin(2f * num6 / num5);
                num8 = Mathf.Atan2(2f * quat.Y * quat.W + 2f * quat.Z * quat.X, 0f - num - num2 + num3 + num4);
                num9 = Mathf.Atan2(2f * quat.Z * quat.W + 2f * quat.Y * quat.X, 0f - num + num2 - num3 + num4);
            }
            return new Euler3(num7, num8, num9) * 57.29578f;
        }

        public void Clamp(Euler3 from, Euler3 to)
        {
            Pitch = Mathf.Clamp(Pitch, from.Pitch, to.Pitch);
            Yaw = Mathf.Clamp(Yaw, from.Yaw, to.Yaw);
            Roll = Mathf.Clamp(Roll, from.Roll, to.Roll);
        }

        public void ClampMax(Euler3 max)
        {
            Pitch = Mathf.Min(Pitch, max.Pitch);
            Yaw = Mathf.Min(Yaw, max.Yaw);
            Roll = Mathf.Min(Roll, max.Roll);
        }

        public void ClampMin(Euler3 min)
        {
            Pitch = Mathf.Max(Pitch, min.Pitch);
            Yaw = Mathf.Max(Yaw, min.Yaw);
            Roll = Mathf.Max(Roll, min.Roll);
        }

        public static Euler3 Scale(Euler3 a, Euler3 b)
        {
            return new Euler3(a.Pitch * b.Pitch, a.Yaw * b.Yaw, a.Roll * b.Roll);
        }

        public override string ToString()
        {
            return string.Format("({0},{1},{2})", Pitch, Yaw, Roll);
        }

        public static Euler3 RotateOverTime(Euler3 start, Euler3 changePerSecond, float dt)
        {
            Vector3 vector = changePerSecond.ComponentsToVector3();
            Quaternion lhs = Quaternion.AngleAxis();
            return GetRotation(lhs * start.Rotation);
        }

        public static Euler3 RotateOverTimeLocal(Euler3 start, Euler3 changePerSecond, float dt)
        {
            Quaternion rhs = Quaternion.Slerp(Quaternion.Identity, changePerSecond.Rotation, dt);
            return GetRotation(start.Rotation * rhs);
        }

        public static Quaternion RotateOverTime(Quaternion start, Quaternion changePerSecond, float dt)
        {
            Quaternion lhs = Quaternion.Slerp(Quaternion.Identity, changePerSecond, dt);
            return lhs * start;
        }

        public static Quaternion RotateOverTimeLocal(Quaternion start, Quaternion changePerSecond, float dt)
        {
            Quaternion rhs = Quaternion.Slerp(Quaternion.Identity, changePerSecond, dt);
            return start * rhs;
        }

        public Vector3 ComponentsToVector3()
        {
            return new Vector3(Pitch, Yaw, Roll);
        }

        public void ComponentsFromVector3(Vector3 input)
        {
            Pitch = input.X;
            Yaw = input.Y;
            Roll = input.Z;
        }

        public override int GetHashCode() =>
            GetHashCode();


        public override bool Equals(object obj) =>
            this == (Euler3)obj;


        public static bool operator ==(Euler3 a, Euler3 b)
        {
            return a.Pitch == b.Pitch && a.Yaw == b.Yaw && a.Roll == b.Roll;
        }

        public static bool operator !=(Euler3 a, Euler3 b)
        {
            return a.Pitch != b.Pitch || a.Yaw != b.Yaw || a.Roll != b.Roll;
        }

        public static Euler3 operator +(Euler3 a, Euler3 b)
        {
            return new Euler3(a.Pitch + b.Pitch, a.Yaw + b.Yaw, a.Roll + b.Roll);
        }

        public static Euler3 operator -(Euler3 a, Euler3 b)
        {
            return new Euler3(a.Pitch - b.Pitch, a.Yaw - b.Yaw, a.Roll - b.Roll);
        }

        public static Euler3 operator -(Euler3 a)
        {
            return new Euler3(0f - a.Pitch, 0f - a.Yaw, 0f - a.Roll);
        }

        public static Euler3 operator *(Euler3 a, float b)
        {
            return new Euler3(a.Pitch * b, a.Yaw * b, a.Roll * b);
        }

        public static Euler3 operator /(Euler3 a, float b)
        {
            return new Euler3(a.Pitch / b, a.Yaw / b, a.Roll / b);
        }
    }
}
