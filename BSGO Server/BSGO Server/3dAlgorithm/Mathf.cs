using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server._3dAlgorithm
{
    public struct Mathf
    {
        public const float PI = (float)Math.PI;

        /// <summary>
        ///   <para>A tiny floating point value (RO).</para>
        /// </summary>
        public static readonly float Epsilon = (!MathfInternal.IsFlushToZeroEnabled) ? MathfInternal.FloatMinDenormal : MathfInternal.FloatMinNormal;

        /// <summary>
        ///   <para>Radians-to-degrees conversion constant (RO).</para>
        /// </summary>
        public const float Rad2Deg = 57.29578f;

        /// <summary>
        ///   <para>Returns the sine of angle /f/ in radians.</para>
        /// </summary>
        /// <param name="f"></param>
        public static float Sin(float f)
        {
            return (float)Math.Sin(f);
        }

        /// <summary>
        ///   <para>Returns the cosine of angle /f/ in radians.</para>
        /// </summary>
        /// <param name="f"></param>
        public static float Cos(float f)
        {
            return (float)Math.Cos(f);
        }

        /// <summary>
        ///   <para>Returns the tangent of angle /f/ in radians.</para>
        /// </summary>
        /// <param name="f"></param>
        public static float Tan(float f)
        {
            return (float)Math.Tan(f);
        }

        /// <summary>
        ///   <para>Returns the arc-sine of /f/ - the angle in radians whose sine is /f/.</para>
        /// </summary>
        /// <param name="f"></param>
        public static float Asin(float f)
        {
            return (float)Math.Asin(f);
        }

        /// <summary>
        ///   <para>Returns the arc-cosine of /f/ - the angle in radians whose cosine is /f/.</para>
        /// </summary>
        /// <param name="f"></param>
        public static float Acos(float f)
        {
            return (float)Math.Acos(f);
        }

        /// <summary>
        ///   <para>Returns the arc-tangent of /f/ - the angle in radians whose tangent is /f/.</para>
        /// </summary>
        /// <param name="f"></param>
        public static float Atan(float f)
        {
            return (float)Math.Atan(f);
        }

        /// <summary>
        ///   <para>Returns the angle in radians whose ::ref::Tan is @@y/x@@.</para>
        /// </summary>
        /// <param name="y"></param>
        /// <param name="x"></param>
        public static float Atan2(float y, float x)
        {
            return (float)Math.Atan2(y, x);
        }

        /// <summary>
        ///   <para>Returns square root of /f/.</para>
        /// </summary>
        /// <param name="f"></param>
        public static float Sqrt(float f)
        {
            return (float)Math.Sqrt(f);
        }

        /// <summary>
        ///   <para>Returns the absolute value of /f/.</para>
        /// </summary>
        /// <param name="f"></param>
        public static float Abs(float f)
        {
            return Math.Abs(f);
        }

        /// <summary>
        ///   <para>Returns the absolute value of /value/.</para>
        /// </summary>
        /// <param name="value"></param>
        public static int Abs(int value)
        {
            return Math.Abs(value);
        }

        /// <summary>
        ///   <para>Returns the smallest of two or more values.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="values"></param>
        public static float Min(float a, float b)
        {
            return (!(a < b)) ? b : a;
        }

        /// <summary>
        ///   <para>Returns the smallest of two or more values.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="values"></param>
        public static float Min(params float[] values)
        {
            int num = values.Length;
            if (num == 0)
            {
                return 0f;
            }
            float num2 = values[0];
            for (int i = 1; i < num; i++)
            {
                if (values[i] < num2)
                {
                    num2 = values[i];
                }
            }
            return num2;
        }

        /// <summary>
        ///   <para>Returns the smallest of two or more values.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="values"></param>
        public static int Min(int a, int b)
        {
            return (a >= b) ? b : a;
        }

        /// <summary>
        ///   <para>Returns the smallest of two or more values.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="values"></param>
        public static int Min(params int[] values)
        {
            int num = values.Length;
            if (num == 0)
            {
                return 0;
            }
            int num2 = values[0];
            for (int i = 1; i < num; i++)
            {
                if (values[i] < num2)
                {
                    num2 = values[i];
                }
            }
            return num2;
        }

        /// <summary>
        ///   <para>Returns largest of two or more values.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="values"></param>
        public static float Max(float a, float b)
        {
            return (!(a > b)) ? b : a;
        }

        /// <summary>
        ///   <para>Returns largest of two or more values.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="values"></param>
        public static float Max(params float[] values)
        {
            int num = values.Length;
            if (num == 0)
            {
                return 0f;
            }
            float num2 = values[0];
            for (int i = 1; i < num; i++)
            {
                if (values[i] > num2)
                {
                    num2 = values[i];
                }
            }
            return num2;
        }

        /// <summary>
        ///   <para>Returns the largest of two or more values.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="values"></param>
        public static int Max(int a, int b)
        {
            return (a <= b) ? b : a;
        }

        /// <summary>
        ///   <para>Returns the largest of two or more values.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="values"></param>
        public static int Max(params int[] values)
        {
            int num = values.Length;
            if (num == 0)
            {
                return 0;
            }
            int num2 = values[0];
            for (int i = 1; i < num; i++)
            {
                if (values[i] > num2)
                {
                    num2 = values[i];
                }
            }
            return num2;
        }

        /// <summary>
        ///   <para>Returns /f/ raised to power /p/.</para>
        /// </summary>
        /// <param name="f"></param>
        /// <param name="p"></param>
        public static float Pow(float f, float p)
        {
            return (float)Math.Pow(f, p);
        }

        /// <summary>
        ///   <para>Returns e raised to the specified power.</para>
        /// </summary>
        /// <param name="power"></param>
        public static float Exp(float power)
        {
            return (float)Math.Exp(power);
        }

        /// <summary>
        ///   <para>Returns the logarithm of a specified number in a specified base.</para>
        /// </summary>
        /// <param name="f"></param>
        /// <param name="p"></param>
        public static float Log(float f, float p)
        {
            return (float)Math.Log(f, p);
        }

        /// <summary>
        ///   <para>Returns the natural (base e) logarithm of a specified number.</para>
        /// </summary>
        /// <param name="f"></param>
        public static float Log(float f)
        {
            return (float)Math.Log(f);
        }

        /// <summary>
        ///   <para>Returns the base 10 logarithm of a specified number.</para>
        /// </summary>
        /// <param name="f"></param>
        public static float Log10(float f)
        {
            return (float)Math.Log10(f);
        }

        /// <summary>
        ///   <para>Returns the smallest integer greater to or equal to /f/.</para>
        /// </summary>
        /// <param name="f"></param>
        public static float Ceil(float f)
        {
            return (float)Math.Ceiling(f);
        }

        /// <summary>
        ///   <para>Returns the largest integer smaller to or equal to /f/.</para>
        /// </summary>
        /// <param name="f"></param>
        public static float Floor(float f)
        {
            return (float)Math.Floor(f);
        }

        /// <summary>
        ///   <para>Returns /f/ rounded to the nearest integer.</para>
        /// </summary>
        /// <param name="f"></param>
        public static float Round(float f)
        {
            return (float)Math.Round(f);
        }

        /// <summary>
        ///   <para>Returns the smallest integer greater to or equal to /f/.</para>
        /// </summary>
        /// <param name="f"></param>
        public static int CeilToInt(float f)
        {
            return (int)Math.Ceiling(f);
        }

        /// <summary>
        ///   <para>Returns the largest integer smaller to or equal to /f/.</para>
        /// </summary>
        /// <param name="f"></param>
        public static int FloorToInt(float f)
        {
            return (int)Math.Floor(f);
        }

        /// <summary>
        ///   <para>Returns /f/ rounded to the nearest integer.</para>
        /// </summary>
        /// <param name="f"></param>
        public static int RoundToInt(float f)
        {
            return (int)Math.Round(f);
        }

        /// <summary>
        ///   <para>Returns the sign of /f/.</para>
        /// </summary>
        /// <param name="f"></param>
        public static float Sign(float f)
        {
            return (!(f >= 0f)) ? (-1f) : 1f;
        }

        /// <summary>
        ///   <para>Clamps a value between a minimum float and maximum float value.</para>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public static float Clamp(float value, float min, float max)
        {
            if (value < min)
            {
                value = min;
            }
            else if (value > max)
            {
                value = max;
            }
            return value;
        }

        /// <summary>
        ///   <para>Clamps value between min and max and returns value.</para>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public static int Clamp(int value, int min, int max)
        {
            if (value < min)
            {
                value = min;
            }
            else if (value > max)
            {
                value = max;
            }
            return value;
        }

        /// <summary>
        ///   <para>Clamps value between 0 and 1 and returns value.</para>
        /// </summary>
        /// <param name="value"></param>
        public static float Clamp01(float value)
        {
            if (value < 0f)
            {
                return 0f;
            }
            if (value > 1f)
            {
                return 1f;
            }
            return value;
        }

        /// <summary>
        ///   <para>Interpolates between /a/ and /b/ by /t/. /t/ is clamped between 0 and 1.</para>
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="t"></param>
        public static float Lerp(float from, float to, float t)
        {
            return from + (to - from) * Clamp01(t);
        }

        /// <summary>
        ///   <para>Same as ::ref::Lerp but makes sure the values interpolate correctly when they wrap around 360 degrees.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        public static float LerpAngle(float a, float b, float t)
        {
            float num = Repeat(b - a, 360f);
            if (num > 180f)
            {
                num -= 360f;
            }
            return a + num * Clamp01(t);
        }

        /// <summary>
        ///   <para>Moves a value /current/ towards /target/.</para>
        /// </summary>
        /// <param name="current">The current value.</param>
        /// <param name="target">The value to move towards.</param>
        /// <param name="maxDelta">The maximum change that should be applied to the value.</param>
        public static float MoveTowards(float current, float target, float maxDelta)
        {
            if (Abs(target - current) <= maxDelta)
            {
                return target;
            }
            return current + Sign(target - current) * maxDelta;
        }

        /// <summary>
        ///   <para>Same as ::ref::MoveTowards but makes sure the values interpolate correctly when they wrap around 360 degrees.</para>
        /// </summary>
        /// <param name="current"></param>
        /// <param name="target"></param>
        /// <param name="maxDelta"></param>
        public static float MoveTowardsAngle(float current, float target, float maxDelta)
        {
            target = current + DeltaAngle(current, target);
            return MoveTowards(current, target, maxDelta);
        }

        /// <summary>
        ///   <para>Interpolates between /min/ and /max/ with smoothing at the limits.</para>
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="t"></param>
        public static float SmoothStep(float from, float to, float t)
        {
            t = Clamp01(t);
            t = -2f * t * t * t + 3f * t * t;
            return to * t + from * (1f - t);
        }

        public static float Gamma(float value, float absmax, float gamma)
        {
            bool flag = false;
            if (value < 0f)
            {
                flag = true;
            }
            float num = Abs(value);
            if (num > absmax)
            {
                return (!flag) ? num : (0f - num);
            }
            float num2 = Pow(num / absmax, gamma) * absmax;
            return (!flag) ? num2 : (0f - num2);
        }

        /// <summary>
        ///   <para>Compares two floating point values if they are similar.</para>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static bool Approximately(float a, float b)
        {
            return Abs(b - a) < Max(1E-06f * Max(Abs(a), Abs(b)), Epsilon * 8f);
        }

        /// <summary>
        ///   <para>Loops the value t, so that it is never larger than length and never smaller than 0.</para>
        /// </summary>
        /// <param name="t"></param>
        /// <param name="length"></param>
        public static float Repeat(float t, float length)
        {
            return t - Floor(t / length) * length;
        }

        /// <summary>
        ///   <para>Calculates the shortest difference between two given angles given in degrees.</para>
        /// </summary>
        /// <param name="current"></param>
        /// <param name="target"></param>
        public static float DeltaAngle(float current, float target)
        {
            float num = Repeat(target - current, 360f);
            if (num > 180f)
            {
                num -= 360f;
            }
            return num;
        }
    }
}
