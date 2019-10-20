using System;

namespace BSGO_Server._3dAlgorithm
{
    public struct Vector3 
    {
        public const float kEpsilon = 1E-05f;

        /// <summary>
        ///   <para>X component of the vector.</para>
        /// </summary>
        public float X { get; set; }

        /// <summary>
        ///   <para>Y component of the vector.</para>
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        ///   <para>Z component of the vector.</para>
        /// </summary>
        public float Z { get; set; }

        public float this[int index]
        {
            get => index switch
            {
                0 => X,
                1 => Y,
                2 => Z,
                _ => throw new IndexOutOfRangeException("Invalid Vector3 index!"),
            };
            set
            {
                switch (index)
                {
                    case 0:
                        X = value;
                        break;
                    case 1:
                        Y = value;
                        break;
                    case 2:
                        Z = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector3 index!");
                }
            }
        }

        /// <summary>
        ///   <para>Returns this vector with a ::ref::magnitude of 1 (RO).</para>
        /// </summary>
        public Vector3 Normalized => Normalize(this);

        /// <summary>
        ///   <para>Returns the length of this vector (RO).</para>
        /// </summary>
        public float Magnitude => MathF.Sqrt(X * X + Y * Y + Z * Z);

        /// <summary>
        ///   <para>Returns the squared length of this vector (RO).</para>
        /// </summary>
        public float SqrMagnitude => X * X + Y * Y + Z * Z;

        /// <summary>
        ///   <para>Shorthand for writing @@Vector3(0, 0, 0)@@.</para>
        /// </summary>
        public static Vector3 Zero => new Vector3(0f, 0f, 0f);

        /// <summary>
        ///   <para>Shorthand for writing @@Vector3(1, 1, 1)@@.</para>
        /// </summary>
        public static Vector3 One => new Vector3(1f, 1f, 1f);

        /// <summary>
        ///   <para>Shorthand for writing @@Vector3(0, 0, 1)@@.</para>
        /// </summary>
        public static Vector3 Forward => new Vector3(0f, 0f, 1f);

        /// <summary>
        ///   <para>Shorthand for writing @@Vector3(0, 0, -1)@@.</para>
        /// </summary>
        public static Vector3 Back => new Vector3(0f, 0f, -1f);

        /// <summary>
        ///   <para>Shorthand for writing @@Vector3(0, 1, 0)@@.</para>
        /// </summary>
        public static Vector3 Up => new Vector3(0f, 1f, 0f);

        /// <summary>
        ///   <para>Shorthand for writing @@Vector3(0, -1, 0)@@.</para>
        /// </summary>
        public static Vector3 Down => new Vector3(0f, -1f, 0f);

        /// <summary>
        ///   <para>Shorthand for writing @@Vector3(-1, 0, 0)@@.</para>
        /// </summary>
        public static Vector3 Left => new Vector3(-1f, 0f, 0f);

        /// <summary>
        ///   <para>Shorthand for writing @@Vector3(1, 0, 0)@@.</para>
        /// </summary>
        public static Vector3 Right => new Vector3(1f, 0f, 0f);

        /// <summary>
        ///   <para>Creates a new vector with given x, y, z components. (Z default is 0)</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Vector3(float x, float y, float z = 0)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        ///   <para>Linearly interpolates between two vectors.</para>
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="t"></param>
        public static Vector3 Lerp(Vector3 from, Vector3 to, float t)
        {
            t = Math.Clamp(t, 0, 1);
            return new Vector3(from.X + (to.X - from.X) * t, from.Y + (to.Y - from.Y) * t, from.Z + (to.Z - from.Z) * t);
        }

        /// <summary>
        ///   <para>Spherically interpolates between two vectors.</para>
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="t"></param>
        public static Vector3 Slerp(Vector3 from, Vector3 to, float t)
        {
            float dot = Dot(from, to);
            Math.Clamp(dot, -1.0f, 1.0f);
            float theta = MathF.Acos(dot) * t;
            Vector3 RelativeVec = to - from * dot;
            RelativeVec.Normalize();
            return (from * MathF.Cos(theta)) + (RelativeVec * MathF.Sin(theta));
        }

        /// <summary>
        ///   <para></para>
        /// </summary>
        /// <param name="value"></param>
        public static Vector3 Normalize(Vector3 value)
        {
            float num = GetMagnitude(value);
            if (num > 1E-05f)
            {
                return value / num;
            }
            return Zero;
        }

        /// <summary>
        ///   <para>Makes this vector have a ::ref::magnitude of 1.</para>
        /// </summary>
        public void Normalize()
        {
            float num = GetMagnitude(this);
            if (num > 1E-05f)
            {
                this /= num;
            }
            else
            {
                this = Zero;
            }
        }

        /// <summary>
        ///   <para>Dot Product of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static float Dot(Vector3 lhs, Vector3 rhs)
        {
            return lhs.X * rhs.X + lhs.Y * rhs.Y + lhs.Z * rhs.Z;
        }

        /// <summary>
        ///   <para>Returns the angle in degrees between /from/ and /to/.</para>
        /// </summary>
        /// <param name="from">The angle extends round from this vector.</param>
        /// <param name="to">The angle extends round to this vector.</param>
        public static float Angle(Vector3 from, Vector3 to) => MathF.Acos(Math.Clamp(Dot(from.Normalized, to.Normalized), -1f, 1f)) * 57.29578f;

        public static float GetMagnitude(Vector3 a) => MathF.Sqrt(a.X * a.X + a.Y * a.Y + a.Z * a.Z);

        public static float GetSqrMagnitude(Vector3 a) => a.X * a.X + a.Y * a.Y + a.Z * a.Z;

        public static Vector3 operator +(Vector3 a, Vector3 b) => new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

        public static Vector3 operator -(Vector3 a, Vector3 b) => new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

        public static Vector3 operator -(Vector3 a) => new Vector3(0f - a.X, 0f - a.Y, 0f - a.Z);

        public static Vector3 operator *(Vector3 a, float d) => new Vector3(a.X * d, a.Y * d, a.Z * d);

        public static Vector3 operator *(float d, Vector3 a) => new Vector3(a.X * d, a.Y * d, a.Z * d);

        public static Vector3 operator /(Vector3 a, float d) => new Vector3(a.X / d, a.Y / d, a.Z / d);

        public static bool operator ==(Vector3 lhs, Vector3 rhs) => GetSqrMagnitude(lhs - rhs) < 9.99999944E-11f;

        public static bool operator !=(Vector3 lhs, Vector3 rhs) => GetSqrMagnitude(lhs - rhs) >= 9.99999944E-11f;

        /// <summary>
        ///   <para>Returns a nicely formatted string for this vector.</para>
        /// </summary>
        public override string ToString() => string.Format("({0:F1}, {1:F1}, {2:F1})", X, Y, Z);
    }
}
