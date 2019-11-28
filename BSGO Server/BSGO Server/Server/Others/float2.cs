using BSGO_Server._3dAlgorithm;
using System.Globalization;

namespace BSGO_Server
{
    internal struct float2
    {
        public float x;

        public float y;

        public float magnitude
        {
            get
            {
                return Mathf.Sqrt(x * x + y * y);
            }
        }

        public float sqrMagnitude
        {
            get
            {
                return x * x + y * y;
            }
        }

        public float2 normalized
        {
            get
            {
                float2 result = this;
                result.Normalize();
                return result;
            }
        }

        public static float2 up
        {
            get
            {
                return new float2(0f, -1f);
            }
        }

        public static float2 zero
        {
            get
            {
                return new float2(0f, 0f);
            }
        }

        public float2(float _x, float _y)
        {
            x = _x;
            y = _y;
        }

        public void Normalize()
        {
            float magnitude = this.magnitude;
            if (magnitude != 0f)
            {
                x /= magnitude;
                y /= magnitude;
            }
        }

        public override string ToString()
        {
            NumberFormatInfo numberFormatInfo = new NumberFormatInfo();
            numberFormatInfo.NumberDecimalSeparator = ".";
            return string.Format(numberFormatInfo, "x:{0};y:{1}", x, y);
        }

        public float Angle(float2 rhs)
        {
            return Angle(this, rhs);
        }

        public float Cross(float2 rhs)
        {
            return Cross(this, rhs);
        }

        public static float Angle(float2 lhs, float2 rhs)
        {
            return Vector3.Angle(lhs.ToV3XZ(), rhs.ToV3XZ());
        }

        public static float Dot(float2 lhs, float2 rhs)
        {
            return lhs.x * rhs.x + lhs.y * rhs.y;
        }

        public static float Cross(float2 lhs, float2 rhs)
        {
            return lhs.x * rhs.y - lhs.y * rhs.x;
        }

        public Vector3 ToV3XZ()
        {
            return new Vector3(x, 0f, y);
        }

        public System.Numerics.Vector2 ToV2()
        {
            return ToV2(this);
        }

        public static Vector3 ToV3XZ(float2 vec)
        {
            return new Vector3(vec.x, 0f, vec.y);
        }

        public static float2 FromV3XZ(Vector3 vec)
        {
            return new float2(vec.x, vec.z);
        }

        public static System.Numerics.Vector2 ToV2(float2 vec)
        {
            return new System.Numerics.Vector2(vec.x, vec.y);
        }

        public static float2 FromV2(System.Numerics.Vector2 vec)
        {
            return new float2(vec.X, vec.Y);
        }

        public static float2 operator *(float2 lhs, float rhs)
        {
            return new float2(lhs.x * rhs, lhs.y * rhs);
        }

        public static float2 operator *(float lhs, float2 rhs)
        {
            return new float2(lhs * rhs.x, lhs * rhs.y);
        }

        public static float2 operator /(float2 lhs, float rhs)
        {
            return new float2(lhs.x / rhs, lhs.y / rhs);
        }

        public static float2 operator +(float2 lhs, float2 rhs)
        {
            return new float2(lhs.x + rhs.x, lhs.y + rhs.y);
        }

        public static float2 operator -(float2 lhs, float2 rhs)
        {
            return new float2(lhs.x - rhs.x, lhs.y - rhs.y);
        }

        public static float2 operator -(float2 lhs)
        {
            return new float2(0f - lhs.x, 0f - lhs.y);
        }
    }
}
