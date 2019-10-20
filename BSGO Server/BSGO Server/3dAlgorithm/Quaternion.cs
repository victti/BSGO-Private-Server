using System;

namespace BSGO_Server._3dAlgorithm
{
    public struct Quaternion
    {
        public const float kEpsilon = 1E-06f;
        private const float radToDeg = (float)(180.0 / Math.PI);
        private const float degToRad = (float)(Math.PI / 180.0);

        public Vector3 Xyz
        {
            get => new Vector3(X, Y, Z);
            set
            {
                X = value.X;
                Y = value.Y;
                Z = value.Z;
            }
        }

        /// <summary>
        ///   <para>X component of the Quaternion. Don't modify this directly unless you know quaternions inside out.</para>
        /// </summary>
        public float X { get; set; }

        /// <summary>
        ///   <para>Y component of the Quaternion. Don't modify this directly unless you know quaternions inside out.</para>
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        ///   <para>Z component of the Quaternion. Don't modify this directly unless you know quaternions inside out.</para>
        /// </summary>
        public float Z { get; set; }

        /// <summary>
        ///   <para>W component of the Quaternion. Don't modify this directly unless you know quaternions inside out.</para>
        /// </summary>
        public float W { get; set; }

        public float this[int index]
        {
            get => index switch
            {
                0 => X,
                1 => Y,
                2 => Z,
                3 => W,
                _ => throw new IndexOutOfRangeException("Invalid Quaternion index!"),
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
                    case 3:
                        W = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Quaternion index!");
                }
            }
        }

        /// <summary>
        /// Gets the length (magnitude) of the quaternion.
        /// </summary>
        public float Length => (float)Math.Sqrt(X * X + Y * Y + Z * Z + W * W);

        /// <summary>
        ///   <para>The identity rotation (RO).</para>
        /// </summary>
        public static Quaternion Identity => new Quaternion(0f, 0f, 0f, 1f);

        /// <summary>
        ///   NEEDS FIX
        ///   <para>Returns the euler angle representation of the rotation.</para>
        /// </summary>
        /*
        public Vector3 EulerAngles
        {
            get
            {
                return new Vector3();
                //return Internal_ToEulerRad(this) * 57.29578f;
            }
            // no value param being used
            set
            {
                this = new Quaternion(); 
                //this = Internal_FromEulerRad(value * ((float)Math.PI / 180f));
            }
            
        }
        */

        /// <summary>
        ///   <para>Constructs new Quaternion with given x,y,z,w components.</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="w"></param>
        public Quaternion(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
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
            X = new_x;
            Y = new_y;
            Z = new_z;
            W = new_w;
        }

        /// <summary>
        ///   <para>The dot product between two rotations.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static float Dot(Quaternion a, Quaternion b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z + a.W * b.W;

        /// <summary>
        ///   NEEDS FIX
        ///   <para>Creates a rotation which rotates /angle/ degrees around /axis/.</para>
        /// </summary>
        // args: float angle, Vector3 axis
        public static Quaternion AngleAxis() => new Quaternion();

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
            Quaternion quaternion = new Quaternion();
            float num = t;
            float num4 = (((q1.X * q2.X) + (q1.Y * q2.Y)) + (q1.Z * q2.Z)) + (q1.W * q2.W);
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
            quaternion.X = (num3 * q1.X) + (num2 * q2.X);
            quaternion.Y = (num3 * q1.Y) + (num2 * q2.Y);
            quaternion.Z = (num3 * q1.Z) + (num2 * q2.Z);
            quaternion.W = (num3 * q1.W) + (num2 * q2.W);
            return quaternion;
        }

        public void ToAngleAxis(out float angle, out Vector3 axis)
        {
            ToAxisAngleRad(this, out axis, out angle);
            angle *= radToDeg;
        }

        private static void ToAxisAngleRad(Quaternion q, out Vector3 axis, out float angle)
        {
            if (Math.Abs(q.W) > 1.0f)
                q.Normalize();
            angle = 2.0f * (float)Math.Acos(q.W); // angle
            float den = (float)Math.Sqrt(1.0 - q.W * q.W);
            if (den > 0.0001f)
            {
                axis = q.Xyz / den;
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
            float scale = 1.0f / Length;
            Xyz *= scale;
            W *= scale;
        }

        /// <summary>
        ///   <para>Returns a rotation that rotates z degrees around the z axis, x degrees around the x axis, and y degrees around the y axis (in that order).</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public static Quaternion Euler(float x, float y, float z) =>
            FromEulerRad(new Vector3(x, y, z) * degToRad);
        
        /// <summary>
        ///   <para>Returns a rotation that rotates z degrees around the z axis, x degrees around the x axis, and y degrees around the y axis (in that order).</para>
        /// </summary>
        /// <param name="euler"></param>
        public static Quaternion Euler(Vector3 euler) => 
            FromEulerRad(euler * degToRad);
        

        private static Quaternion FromEulerRad(Vector3 euler)
        {
            var yaw = euler.X;
            var pitch = euler.Y;
            var roll = euler.Z;
            float rollOver2 = roll * 0.5f;
            float sinRollOver2 = (float)Math.Sin(rollOver2);
            float cosRollOver2 = (float)Math.Cos(rollOver2);
            float pitchOver2 = pitch * 0.5f;
            float sinPitchOver2 = (float)Math.Sin(pitchOver2);
            float cosPitchOver2 = (float)Math.Cos(pitchOver2);
            float yawOver2 = yaw * 0.5f;
            float sinYawOver2 = (float)Math.Sin(yawOver2);
            float cosYawOver2 = (float)Math.Cos(yawOver2);
            Quaternion result = new Quaternion
            {
                X = cosYawOver2 * cosPitchOver2 * cosRollOver2 + sinYawOver2 * sinPitchOver2 * sinRollOver2,
                Y = cosYawOver2 * cosPitchOver2 * sinRollOver2 - sinYawOver2 * sinPitchOver2 * cosRollOver2,
                Z = cosYawOver2 * sinPitchOver2 * cosRollOver2 + sinYawOver2 * cosPitchOver2 * sinRollOver2,
                W = sinYawOver2 * cosPitchOver2 * cosRollOver2 - cosYawOver2 * sinPitchOver2 * sinRollOver2
            };
            return result;

        }

        public static Quaternion operator *(Quaternion lhs, Quaternion rhs) =>
            new Quaternion(lhs.W * rhs.X + lhs.X * rhs.W + lhs.Y * rhs.Z - lhs.Z * rhs.Y, lhs.W * rhs.Y + lhs.Y * rhs.W + lhs.Z * rhs.X - lhs.X * rhs.Z, lhs.W * rhs.Z + lhs.Z * rhs.W + lhs.X * rhs.Y - lhs.Y * rhs.X, lhs.W * rhs.W - lhs.X * rhs.X - lhs.Y * rhs.Y - lhs.Z * rhs.Z);

        public static Vector3 operator *(Quaternion rotation, Vector3 point)
        {
            float num = rotation.X * 2f;
            float num2 = rotation.Y * 2f;
            float num3 = rotation.Z * 2f;
            float num4 = rotation.X * num;
            float num5 = rotation.Y * num2;
            float num6 = rotation.Z * num3;
            float num7 = rotation.X * num2;
            float num8 = rotation.X * num3;
            float num9 = rotation.Y * num3;
            float num10 = rotation.W * num;
            float num11 = rotation.W * num2;
            float num12 = rotation.W * num3;
            Vector3 result = default;
            result.X = (1f - (num5 + num6)) * point.X + (num7 - num12) * point.Y + (num8 + num11) * point.Z;
            result.Y = (num7 + num12) * point.X + (1f - (num4 + num6)) * point.Y + (num9 - num10) * point.Z;
            result.Z = (num8 - num11) * point.X + (num9 + num10) * point.Y + (1f - (num4 + num5)) * point.Z;
            return result;
        }

        public static bool operator ==(Quaternion lhs, Quaternion rhs) => Dot(lhs, rhs) > 0.999999f;

        public static bool operator !=(Quaternion lhs, Quaternion rhs) => Dot(lhs, rhs) <= 0.999999f;

        /// <summary>
        ///   <para>Returns a nicely formatted string of the Quaternion.</para>
        /// </summary>
        public override string ToString() => string.Format("({0:F1}, {1:F1}, {2:F1}, {3:F1})", X, Y, Z, W);
    }
}
