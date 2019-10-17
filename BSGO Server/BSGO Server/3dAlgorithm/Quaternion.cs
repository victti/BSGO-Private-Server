using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server._3dAlgorithm
{
    public struct Quaternion
    {
        public const float kEpsilon = 1E-06f;
        const float radToDeg = (float)(180.0 / Math.PI);
        const float degToRad = (float)(Math.PI / 180.0);

        public Vector3 xyz
        {
            set
            {
                x = value.x;
                y = value.y;
                z = value.z;
            }
            get
            {
                return new Vector3(x, y, z);
            }
        }

        /// <summary>
        ///   <para>X component of the Quaternion. Don't modify this directly unless you know quaternions inside out.</para>
        /// </summary>
        public float x;

        /// <summary>
        ///   <para>Y component of the Quaternion. Don't modify this directly unless you know quaternions inside out.</para>
        /// </summary>
        public float y;

        /// <summary>
        ///   <para>Z component of the Quaternion. Don't modify this directly unless you know quaternions inside out.</para>
        /// </summary>
        public float z;

        /// <summary>
        ///   <para>W component of the Quaternion. Don't modify this directly unless you know quaternions inside out.</para>
        /// </summary>
        public float w;

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
                    case 3:
                        return w;
                    default:
                        throw new IndexOutOfRangeException("Invalid Quaternion index!");
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
                    case 3:
                        w = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Quaternion index!");
                }
            }
        }

        /// <summary>
        /// Gets the length (magnitude) of the quaternion.
        /// </summary>
        /// <seealso cref="LengthSquared"/>
        public float Length
        {
            get
            {
                return (float)Math.Sqrt(x * x + y * y + z * z + w * w);
            }
        }

        /// <summary>
        ///   <para>The identity rotation (RO).</para>
        /// </summary>
        public static Quaternion identity
        {
            get
            {
                return new Quaternion(0f, 0f, 0f, 1f);
            }
        }

        /// <summary>
        ///   <para>Returns the euler angle representation of the rotation.</para>
        /// </summary>
        public Vector3 eulerAngles
        {
            get
            {
                return new Vector3();
                //return Internal_ToEulerRad(this) * 57.29578f;
            }
            set
            {
                this = new Quaternion();
                //this = Internal_FromEulerRad(value * ((float)Math.PI / 180f));
            }
        }

        /// <summary>
        ///   <para>Constructs new Quaternion with given x,y,z,w components.</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="w"></param>
        public Quaternion(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        /// <summary>
        ///   <para>Set x, y, z and w components of an existing Quaternion.</para>
        /// </summary>
        /// <param name="new_x"></param>
        /// <param name="new_y"></param>
        /// <param name="new_z"></param>
        /// <param name="new_w"></param>
        public void Set(float new_x, float new_y, float new_z, float new_w)
        {
            x = new_x;
            y = new_y;
            z = new_z;
            w = new_w;
        }

        /// <summary>
        ///   <para>The dot product between two rotations.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static float Dot(Quaternion a, Quaternion b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        }

        /// <summary>
        ///   <para>Creates a rotation which rotates /angle/ degrees around /axis/.</para>
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="axis"></param>
        public static Quaternion AngleAxis(float angle, Vector3 axis)
        {
            return new Quaternion();
        }

        /// <summary>
        ///   <para>Returns the angle in degrees between two rotations /a/ and /b/.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static float Angle(Quaternion a, Quaternion b)
        {
            float f = Dot(a, b);
            return Mathf.Acos(Mathf.Min(Mathf.Abs(f), 1f)) * 2f * 57.29578f;
        }

        public static Quaternion Slerp(Quaternion q1, Quaternion q2, float t)
        {
            float num2;
            float num3;
            Quaternion quaternion;
            float num = t;
            float num4 = (((q1.x * q2.x) + (q1.y * q2.y)) + (q1.z * q2.z)) + (q1.w * q2.w);
            bool flag = false;
            if (num4 < 0f)
            {
                flag = true;
                num4 = -num4;
            }
            if (num4 > 0.999999f)
            {
                num3 = 1f - num;
                num2 = flag ? -num : num;
            }
            else
            {
                float num5 = MathF.Acos(num4);
                float num6 = (1.0f / MathF.Sin(num5));
                num3 = (MathF.Sin(((1f - num) * num5))) * num6;
                num2 = flag ? ((-MathF.Sin((num * num5))) * num6) : ((MathF.Sin((num * num5))) * num6);
            }
            quaternion.x = (num3 * q1.x) + (num2 * q2.x);
            quaternion.y = (num3 * q1.y) + (num2 * q2.y);
            quaternion.z = (num3 * q1.z) + (num2 * q2.z);
            quaternion.w = (num3 * q1.w) + (num2 * q2.w);
            return quaternion;
        }

        public void ToAngleAxis(out float angle, out Vector3 axis)
        {
            Quaternion.ToAxisAngleRad(this, out axis, out angle);
            angle *= radToDeg;
        }

        private static void ToAxisAngleRad(Quaternion q, out Vector3 axis, out float angle)
        {
            if (Math.Abs(q.w) > 1.0f)
                q.Normalize();
            angle = 2.0f * (float)Math.Acos(q.w); // angle
            float den = (float)Math.Sqrt(1.0 - q.w * q.w);
            if (den > 0.0001f)
            {
                axis = q.xyz / den;
            }
            else
            {
                // This occurs when the angle is zero. 
                // Not a problem: just set an arbitrary normalized axis.
                axis = new Vector3(1, 0, 0);
            }
        }

        /// <summary>
        /// Scales the MyQuaternion to unit length.
        /// </summary>
        public void Normalize()
        {
            float scale = 1.0f / this.Length;
            xyz *= scale;
            w *= scale;
        }

        /// <summary>
        ///   <para>Returns a rotation that rotates z degrees around the z axis, x degrees around the x axis, and y degrees around the y axis (in that order).</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public static Quaternion Euler(float x, float y, float z)
        {
            return FromEulerRad(new Vector3((float)x, (float)y, (float)z) * degToRad);
        }
        /// <summary>
        ///   <para>Returns a rotation that rotates z degrees around the z axis, x degrees around the x axis, and y degrees around the y axis (in that order).</para>
        /// </summary>
        /// <param name="euler"></param>
        public static Quaternion Euler(Vector3 euler)
        {
            return FromEulerRad(euler * degToRad);
        }

        private static Quaternion FromEulerRad(Vector3 euler)
        {
            var yaw = euler.x;
            var pitch = euler.y;
            var roll = euler.z;
            float rollOver2 = roll * 0.5f;
            float sinRollOver2 = (float)System.Math.Sin((float)rollOver2);
            float cosRollOver2 = (float)System.Math.Cos((float)rollOver2);
            float pitchOver2 = pitch * 0.5f;
            float sinPitchOver2 = (float)System.Math.Sin((float)pitchOver2);
            float cosPitchOver2 = (float)System.Math.Cos((float)pitchOver2);
            float yawOver2 = yaw * 0.5f;
            float sinYawOver2 = (float)System.Math.Sin((float)yawOver2);
            float cosYawOver2 = (float)System.Math.Cos((float)yawOver2);
            Quaternion result;
            result.x = cosYawOver2 * cosPitchOver2 * cosRollOver2 + sinYawOver2 * sinPitchOver2 * sinRollOver2;
            result.y = cosYawOver2 * cosPitchOver2 * sinRollOver2 - sinYawOver2 * sinPitchOver2 * cosRollOver2;
            result.z = cosYawOver2 * sinPitchOver2 * cosRollOver2 + sinYawOver2 * cosPitchOver2 * sinRollOver2;
            result.w = sinYawOver2 * cosPitchOver2 * cosRollOver2 - cosYawOver2 * sinPitchOver2 * sinRollOver2;
            return result;

        }

        public static Quaternion operator *(Quaternion lhs, Quaternion rhs)
        {
            return new Quaternion(lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y, lhs.w * rhs.y + lhs.y * rhs.w + lhs.z * rhs.x - lhs.x * rhs.z, lhs.w * rhs.z + lhs.z * rhs.w + lhs.x * rhs.y - lhs.y * rhs.x, lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z);
        }

        public static Vector3 operator *(Quaternion rotation, Vector3 point)
        {
            float num = rotation.x * 2f;
            float num2 = rotation.y * 2f;
            float num3 = rotation.z * 2f;
            float num4 = rotation.x * num;
            float num5 = rotation.y * num2;
            float num6 = rotation.z * num3;
            float num7 = rotation.x * num2;
            float num8 = rotation.x * num3;
            float num9 = rotation.y * num3;
            float num10 = rotation.w * num;
            float num11 = rotation.w * num2;
            float num12 = rotation.w * num3;
            Vector3 result = default(Vector3);
            result.x = (1f - (num5 + num6)) * point.x + (num7 - num12) * point.y + (num8 + num11) * point.z;
            result.y = (num7 + num12) * point.x + (1f - (num4 + num6)) * point.y + (num9 - num10) * point.z;
            result.z = (num8 - num11) * point.x + (num9 + num10) * point.y + (1f - (num4 + num5)) * point.z;
            return result;
        }

        public static bool operator ==(Quaternion lhs, Quaternion rhs)
        {
            return Dot(lhs, rhs) > 0.999999f;
        }

        public static bool operator !=(Quaternion lhs, Quaternion rhs)
        {
            return Dot(lhs, rhs) <= 0.999999f;
        }

        /// <summary>
        ///   <para>Returns a nicely formatted string of the Quaternion.</para>
        /// </summary>
        /// <param name="format"></param>
        public override string ToString()
        {
            return string.Format("({0:F1}, {1:F1}, {2:F1}, {3:F1})", x, y, z, w);
        }
    }
}
