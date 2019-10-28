using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server._3dAlgorithm
{
    public struct Vector3 
    {
        public const float kEpsilon = 1E-05f;

        /// <summary>
        ///   <para>X component of the vector.</para>
        /// </summary>
        public float x;

        /// <summary>
        ///   <para>Y component of the vector.</para>
        /// </summary>
        public float y;

        /// <summary>
        ///   <para>Z component of the vector.</para>
        /// </summary>
        public float z;

        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return x;
                    case 1:
                        return y;
                    case 2:
                        return z;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector3 index!");
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        x = value;
                        break;
                    case 1:
                        y = value;
                        break;
                    case 2:
                        z = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector3 index!");
                }
            }
        }

        /// <summary>
        ///   <para>Returns this vector with a ::ref::magnitude of 1 (RO).</para>
        /// </summary>
        public Vector3 normalized
        {
            get
            {
                return Normalize(this);
            }
        }

        /// <summary>
        ///   <para>Returns the length of this vector (RO).</para>
        /// </summary>
        public float magnitude
        {
            get
            {
                return MathF.Sqrt(x * x + y * y + z * z);
            }
        }

        /// <summary>
        ///   <para>Returns the squared length of this vector (RO).</para>
        /// </summary>
        public float sqrMagnitude
        {
            get
            {
                return x * x + y * y + z * z;
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing @@Vector3(0, 0, 0)@@.</para>
        /// </summary>
        public static Vector3 zero
        {
            get
            {
                return new Vector3(0f, 0f, 0f);
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing @@Vector3(1, 1, 1)@@.</para>
        /// </summary>
        public static Vector3 one
        {
            get
            {
                return new Vector3(1f, 1f, 1f);
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing @@Vector3(0, 0, 1)@@.</para>
        /// </summary>
        public static Vector3 forward
        {
            get
            {
                return new Vector3(0f, 0f, 1f);
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing @@Vector3(0, 0, -1)@@.</para>
        /// </summary>
        public static Vector3 back
        {
            get
            {
                return new Vector3(0f, 0f, -1f);
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing @@Vector3(0, 1, 0)@@.</para>
        /// </summary>
        public static Vector3 up
        {
            get
            {
                return new Vector3(0f, 1f, 0f);
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing @@Vector3(0, -1, 0)@@.</para>
        /// </summary>
        public static Vector3 down
        {
            get
            {
                return new Vector3(0f, -1f, 0f);
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing @@Vector3(-1, 0, 0)@@.</para>
        /// </summary>
        public static Vector3 left
        {
            get
            {
                return new Vector3(-1f, 0f, 0f);
            }
        }

        /// <summary>
        ///   <para>Shorthand for writing @@Vector3(1, 0, 0)@@.</para>
        /// </summary>
        public static Vector3 right
        {
            get
            {
                return new Vector3(1f, 0f, 0f);
            }
        }

        /// <summary>
        ///   <para>Creates a new vector with given x, y, z components.</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        ///   <para>Creates a new vector with given x, y components and sets /z/ to zero.</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Vector3(float x, float y)
        {
            this.x = x;
            this.y = y;
            z = 0f;
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
            return new Vector3(from.x + (to.x - from.x) * t, from.y + (to.y - from.y) * t, from.z + (to.z - from.z) * t);
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
            return ((from * MathF.Cos(theta)) + (RelativeVec * MathF.Sin(theta)));
        }

        /// <summary>
        ///   <para></para>
        /// </summary>
        /// <param name="value"></param>
        public static Vector3 Normalize(Vector3 value)
        {
            float num = Magnitude(value);
            if (num > 1E-05f)
            {
                return value / num;
            }
            return zero;
        }

        /// <summary>
        ///   <para>Makes this vector have a ::ref::magnitude of 1.</para>
        /// </summary>
        public void Normalize()
        {
            float num = Magnitude(this);
            if (num > 1E-05f)
            {
                this /= num;
            }
            else
            {
                this = zero;
            }
        }

        /// <summary>
        ///   <para>Dot Product of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static float Dot(Vector3 lhs, Vector3 rhs)
        {
            return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
        }

        /// <summary>
        ///   <para>Multiplies every component of this vector by the same component of /scale/.</para>
        /// </summary>
        /// <param name="scale"></param>
        public void Scale(Vector3 scale)
        {
            x *= scale.x;
            y *= scale.y;
            z *= scale.z;
        }

        /// <summary>
        ///   <para>Multiplies two vectors component-wise.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static Vector3 Scale(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        /// <summary>
        ///   <para>Returns the angle in degrees between /from/ and /to/.</para>
        /// </summary>
        /// <param name="from">The angle extends round from this vector.</param>
        /// <param name="to">The angle extends round to this vector.</param>
        public static float Angle(Vector3 from, Vector3 to)
        {
            return MathF.Acos(Math.Clamp(Dot(from.normalized, to.normalized), -1f, 1f)) * 57.29578f;
        }

        /// <summary>
        ///   <para>Returns a vector that is made from the smallest components of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static Vector3 Min(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z));
        }

        /// <summary>
        ///   <para>Returns a vector that is made from the largest components of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static Vector3 Max(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z));
        }

        /// <summary>
        ///   <para>Cross Product of two vectors.</para>
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static Vector3 Cross(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(lhs.y * rhs.z - lhs.z * rhs.y, lhs.z * rhs.x - lhs.x * rhs.z, lhs.x * rhs.y - lhs.y * rhs.x);
        }

        public static float Magnitude(Vector3 a)
        {
            return MathF.Sqrt(a.x * a.x + a.y * a.y + a.z * a.z);
        }

        public static float SqrMagnitude(Vector3 a)
        {
            return a.x * a.x + a.y * a.y + a.z * a.z;
        }

        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static Vector3 operator -(Vector3 a)
        {
            return new Vector3(0f - a.x, 0f - a.y, 0f - a.z);
        }

        public static Vector3 operator *(Vector3 a, float d)
        {
            return new Vector3(a.x * d, a.y * d, a.z * d);
        }

        public static Vector3 operator *(float d, Vector3 a)
        {
            return new Vector3(a.x * d, a.y * d, a.z * d);
        }

        public static Vector3 operator /(Vector3 a, float d)
        {
            return new Vector3(a.x / d, a.y / d, a.z / d);
        }

        public static bool operator ==(Vector3 lhs, Vector3 rhs)
        {
            return SqrMagnitude(lhs - rhs) < 9.99999944E-11f;
        }

        public static bool operator !=(Vector3 lhs, Vector3 rhs)
        {
            return SqrMagnitude(lhs - rhs) >= 9.99999944E-11f;
        }

        /// <summary>
        ///   <para>Returns a nicely formatted string for this vector.</para>
        /// </summary>
        /// <param name="format"></param>
        public override string ToString()
        {
            return string.Format("({0:F1}, {1:F1}, {2:F1})", x, y, z);
        }
    }
}
