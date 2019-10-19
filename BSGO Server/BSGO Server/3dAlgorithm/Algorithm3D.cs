namespace BSGO_Server._3dAlgorithm
{
    public static class Algorithm3D
    {
        public const int defaultLayerMask = 1;

        public const int ignoreReycastLayerMask = 4;

        public static Vector3 DistanceToLineSection(Vector3 from, Vector3 lineBegin, Vector3 lineEnd)
        {
            Vector3 vector = lineEnd - lineBegin;
            Vector3 lhs = from - lineBegin;
            float num = Vector3.Dot(lhs, vector);
            if (num <= 0f)
            {
                return lineBegin - from;
            }
            float num2 = Vector3.Dot(vector, vector);
            if (num2 <= num)
            {
                return lineEnd - from;
            }
            float d = num / num2;
            Vector3 a = lineBegin + d * vector;
            return a - from;
        }

        public static Vector3 CubicHermiteSpline(Vector3 p0, Vector3 m0, Vector3 p1, Vector3 m1, float t)
        {
            float num = t * t;
            float num2 = num * t;
            return (2f * num2 - 3f * num + 1f) * p0 + (num2 - 2f * num + t) * m0 + (-2f * num2 + 3f * num) * p1 + (num2 - num) * m1;
        }

        public static Vector3 CubicBezierCurves(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            float num = t * t;
            float d = num * t;
            float num2 = 1f - t;
            float num3 = num2 * num2;
            float d2 = num3 * num2;
            return d2 * p0 + 3f * num3 * t * p1 + 3f * num2 * num * p2 + d * p3;
        }

        public static Vector3 QuadraticBézierCurves(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            float d = t * t;
            float num = 1f - t;
            float d2 = num * num;
            return d2 * p0 + 2f * num * t * p1 + d * p2;
        }

        public static Vector3 CubicBezierCurvesTangent(Vector3 point0, Vector3 m0, Vector3 point1, Vector3 m1, float t)
        {
            Vector3 p = point0 + m0;
            Vector3 p2 = point1 - m1;
            return CubicBezierCurves(point0, p, p2, point1, t);
        }

        public static Vector3 Clamp(Vector3 value, Vector3 min, Vector3 max)
        {
            Vector3 result = default(Vector3);
            result.x = Mathf.Clamp(value.x, min.x, max.x);
            result.y = Mathf.Clamp(value.y, min.y, max.y);
            result.z = Mathf.Clamp(value.z, min.z, max.z);
            return result;
        }

        public static bool IsNaN(Quaternion q)
        {
            return float.IsNaN(q.W) || float.IsNaN(q.X) || float.IsNaN(q.Y) || float.IsNaN(q.Z);
        }

        public static bool IsInfinity(Quaternion q)
        {
            return float.IsInfinity(q.W) || float.IsInfinity(q.X) || float.IsInfinity(q.Y) || float.IsInfinity(q.Z);
        }

        //public static bool Raycast(GameObject gameObject, Vector3 origin, Vector3 direction, float distance, out RaycastHit raycastHit)
        //{
        //    int layer = gameObject.layer;
        //    gameObject.layer = 4;
        //    bool result = Physics.Raycast(origin, direction.normalized, out raycastHit, distance, 1);
        //    gameObject.layer = layer;
        //    return result;
        //}

        //public static bool Linecast(GameObject gameObject, Vector3 start, Vector3 end, out RaycastHit raycastHit)
        //{
        //    int layer = gameObject.layer;
        //    gameObject.layer = 4;
        //    bool result = Physics.Linecast(start, end, out raycastHit, 1);
        //    gameObject.layer = layer;
        //    return result;
        //}

        public static Vector3 GetTrueDirection(Vector3 direction)
        {
            Vector3 vector = direction.Normalized;
            if (Vector3.Angle(vector, Vector3.Up) < 40f)
            {
                Vector3 from = vector;
                from.y = 0f;
                from.Normalize();
                vector = Vector3.Slerp(from, Vector3.Up, 5f / 9f);
            }
            else if (Vector3.Angle(vector, Vector3.Down) < 40f)
            {
                Vector3 from2 = vector;
                from2.y = 0f;
                from2.Normalize();
                vector = Vector3.Slerp(from2, Vector3.Down, 5f / 9f);
            }
            return vector.Normalized;
        }

        public static Quaternion RotateQuaternion(Quaternion q1, Quaternion q2, float rotationAngle)
        {
            float num = Quaternion.Angle(q1, q2);
            if (Mathf.Approximately(num, 0f) || num <= rotationAngle)
            {
                return q2;
            }
            return Quaternion.Slerp(q1, q2, rotationAngle / num);
        }

        public static Quaternion MirrorQuaternion(Quaternion q)
        {
            float angle = 0f;
            Vector3 axis = Vector3.Zero;
            q.ToAngleAxis(out angle, out axis);
            axis.x = 0f - axis.x;
            return Quaternion.AngleAxis(0f - angle, axis);
        }

        public static Vector3 Distance(Vector3 s1p0, Vector3 s1p1, Vector3 s2p0, Vector3 s2p1)
        {
            Vector3 vector = s1p1 - s1p0;
            Vector3 vector2 = s2p1 - s2p0;
            Vector3 vector3 = s1p0 - s2p0;
            float num = Vector3.Dot(vector, vector);
            float num2 = Vector3.Dot(vector, vector2);
            float num3 = Vector3.Dot(vector2, vector2);
            float num4 = Vector3.Dot(vector, vector3);
            float num5 = Vector3.Dot(vector2, vector3);
            float num6 = num * num3 - num2 * num2;
            float num7 = num6;
            float num8 = num6;
            float num9;
            float num10;
            if (num6 < Mathf.Epsilon)
            {
                num9 = 0f;
                num7 = 1f;
                num10 = num5;
                num8 = num3;
            }
            else
            {
                num9 = num2 * num5 - num3 * num4;
                num10 = num * num5 - num2 * num4;
                if (num9 < 0f)
                {
                    num9 = 0f;
                    num10 = num5;
                    num8 = num3;
                }
                else if (num9 > num7)
                {
                    num9 = num7;
                    num10 = num5 + num2;
                    num8 = num3;
                }
            }
            if (num10 < 0f)
            {
                num10 = 0f;
                if (0f - num4 < 0f)
                {
                    num9 = 0f;
                }
                else if (0f - num4 > num)
                {
                    num9 = num7;
                }
                else
                {
                    num9 = 0f - num4;
                    num7 = num;
                }
            }
            else if (num10 > num8)
            {
                num10 = num8;
                if (0f - num4 + num2 < 0f)
                {
                    num9 = 0f;
                }
                else if (0f - num4 + num2 > num)
                {
                    num9 = num7;
                }
                else
                {
                    num9 = 0f - num4 + num2;
                    num7 = num;
                }
            }
            float d = (!(Mathf.Abs(num9) < Mathf.Epsilon)) ? (num9 / num7) : 0f;
            float d2 = (!(Mathf.Abs(num10) < Mathf.Epsilon)) ? (num10 / num8) : 0f;
            return vector3 + d * vector - d2 * vector2;
        }

        public static void Distance(Vector3 s0, Vector3 s1, Vector3 s2, Vector3 s3, out Vector3 p0, out Vector3 p1)
        {
            Vector3 a = s1 - s0;
            Vector3 b = s3 - s2;
            Vector3 rhs = a - b;
            Vector3 lhs = s2 - s0;
            float sqrMagnitude = rhs.SqrMagnitude;
            float value = 0f;
            if (!Mathf.Approximately(sqrMagnitude, 0f))
            {
                value = Vector3.Dot(lhs, rhs) / sqrMagnitude;
            }
            value = Mathf.Clamp01(value);
            p0 = s0 + (s1 - s0) * value;
            p1 = s2 + (s3 - s2) * value;
        }

        public static Vector3 NormalizeEuler(Vector3 v)
        {
            return new Vector3(NormalizeAngle(v.x), NormalizeAngle(v.y), NormalizeAngle(v.z));
        }

        public static Vector3 NormalizeEuler(Vector3 v, Vector3 nearest)
        {
            return new Vector3(NormalizeAngle(v.x, nearest.x), NormalizeAngle(v.y, nearest.y), NormalizeAngle(v.z, nearest.z));
        }

        public static float NormalizeAngle(float angle)
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

        public static float NormalizeAngle(float angle, float nearest)
        {
            while (angle - nearest < 0f)
            {
                angle += 360f;
            }
            while (angle - nearest > 180f)
            {
                angle -= 360f;
            }
            return angle;
        }
    }
}
