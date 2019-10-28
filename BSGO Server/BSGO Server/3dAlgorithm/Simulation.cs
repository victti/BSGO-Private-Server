using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server._3dAlgorithm
{
    internal static class Simulation
    {
        [Flags]
        private enum OutCode
        {
            INSIDE = 0x0,
            LEFT = 0x1,
            RIGHT = 0x2,
            BOTTOM = 0x4,
            TOP = 0x8
        }

        public static MovementFrame MoveToDirection(MovementFrame prevFrame, Euler3 targetEuler3, MovementOptions options)
        {
            Euler3 euler = prevFrame.euler3;
            Euler3 to = options.MaxEulerSpeed(euler);
            Euler3 from = options.MinEulerSpeed(euler);
            Euler3 euler2 = (targetEuler3 - euler).Normalized(false);
            float num = Mathf.Abs(2f * euler2.pitch / options.pitchAcceleration);
            float num2 = Mathf.Abs(2f * euler2.yaw / options.yawAcceleration);
            float num3 = 0f;
            if (euler2.yaw > Mathf.Epsilon)
            {
                num3 = options.yawAcceleration * num2;
            }
            else if (euler2.yaw < 0f - Mathf.Epsilon)
            {
                num3 = (0f - options.yawAcceleration) * num2;
            }
            float num4 = 0f;
            if (euler2.pitch > Mathf.Epsilon)
            {
                num4 = options.pitchAcceleration * num;
            }
            else if (euler2.pitch < 0f - Mathf.Epsilon)
            {
                num4 = (0f - options.pitchAcceleration) * num;
            }
            Euler3 zero = Euler3.zero;
            zero.yaw = (num3 - prevFrame.Euler3Speed.yaw) / 0.1f;
            zero.pitch = (num4 - prevFrame.Euler3Speed.pitch) / 0.1f;
            float num5 = Mathf.Clamp((0f - euler2.yaw) * options.maxRoll / 135f, 0f - options.maxRoll, options.maxRoll);
            float num6 = Algorithm3D.NormalizeAngle(num5 - euler.roll);
            zero.roll = options.rollAcceleration * (num6 / options.maxRoll - prevFrame.Euler3Speed.roll * options.rollFading / options.rollMaxSpeed);
            zero.Clamp(-options.maxTurnAcceleration, options.maxTurnAcceleration);
            Euler3 euler3Speed = prevFrame.Euler3Speed + zero * 0.1f;
            euler3Speed.Clamp(from, to);
            Vector3 linearSpeed = AdvanceLinearSpeed(prevFrame, options);
            Vector3 strafeSpeed = AdvanceStrafingSpeed(prevFrame, options, 0f, 0f);
            Euler3 nextEuler = prevFrame.NextEuler3;
            MovementFrame result = new MovementFrame(prevFrame.nextPosition, nextEuler, linearSpeed, strafeSpeed, euler3Speed);
            Quaternion rotationDelta = nextEuler.rotation * Quaternion.Inverse(prevFrame.rotation) * Quaternion.identity;
            //result.ActiveThrusterEffects = DetermineThrusterEffects(rotationDelta, 0f, 0f);
            result.mode = 0;
            return result;
        }

        public static MovementFrame WASD(MovementFrame prevFrame, int pitch, int yaw, int roll, MovementOptions options)
        {
            Euler3 euler = prevFrame.euler3;
            Euler3 to = options.MaxEulerSpeed(euler);
            Euler3 from = options.MinEulerSpeed(euler);
            float num = 0f;
            if (yaw > 0)
            {
                num = 0f - options.maxRoll;
            }
            if (yaw < 0)
            {
                num = options.maxRoll;
            }
            float num2 = Algorithm3D.NormalizeAngle(num - euler.roll);
            Euler3 zero = Euler3.zero;
            zero.roll = options.rollAcceleration * (num2 / options.maxRoll - prevFrame.Euler3Speed.roll * options.rollFading / options.rollMaxSpeed);
            if (yaw != 0)
            {
                zero.yaw = (float)yaw * options.yawAcceleration;
            }
            else if (prevFrame.Euler3Speed.yaw > Mathf.Epsilon)
            {
                zero.yaw = (0f - options.yawAcceleration) * options.yawFading;
                from.yaw = 0f;
            }
            else if (prevFrame.Euler3Speed.yaw < 0f - Mathf.Epsilon)
            {
                zero.yaw = options.yawAcceleration * options.yawFading;
                to.yaw = 0f;
            }
            if (pitch != 0)
            {
                zero.pitch = (float)pitch * options.pitchAcceleration;
            }
            else if (prevFrame.Euler3Speed.pitch > Mathf.Epsilon)
            {
                zero.pitch = (0f - options.pitchAcceleration) * options.pitchFading;
                from.pitch = 0f;
            }
            else if (prevFrame.Euler3Speed.pitch < 0f - Mathf.Epsilon)
            {
                zero.pitch = options.pitchAcceleration * options.pitchFading;
                to.pitch = 0f;
            }
            Euler3 euler3Speed = prevFrame.Euler3Speed + zero * 0.1f;
            euler3Speed.Clamp(from, to);
            Vector3 linearSpeed = AdvanceLinearSpeed(prevFrame, options);
            Vector3 strafeSpeed = AdvanceStrafingSpeed(prevFrame, options, 0f, 0f);
            Euler3 nextEuler = prevFrame.NextEuler3;
            MovementFrame result = new MovementFrame(prevFrame.nextPosition, nextEuler, linearSpeed, strafeSpeed, euler3Speed);
            Quaternion rotationDelta = nextEuler.rotation * Quaternion.Inverse(prevFrame.rotation) * Quaternion.identity;
            //result.ActiveThrusterEffects = DetermineThrusterEffects(rotationDelta, 0f, 0f);
            if (pitch != 0)
            {
                result.mode = 1;
            }
            return result;
        }

        private static Vector3 ScalePitchYawSoThatOneOfThemHasLengthOneAndSetZToZero(Vector3 source)
        {
            float f = Mathf.Max(Mathf.Abs(source.x), Mathf.Abs(source.y));
            f = Mathf.Abs(f);
            if (Math.Abs(f) < 0.001f)
            {
                return source;
            }
            source.x *= 1f / f;
            source.y *= 1f / f;
            source.z = 0f;
            return source;
        }

        public static MovementFrame TurnToDirectionStrikes(MovementFrame prevFrame, Euler3 targetEuler3, float roll, float strafeX, float strafeY, MovementOptions options)
        {
            Vector3 vector = options.maxTurnAcceleration.ComponentsToVector3();
            Vector3 a = FindDamping(vector, options.maxTurnSpeed.ComponentsToVector3());
            float d = 3f;
            Vector3 vector2 = options.maxTurnSpeed.ComponentsToVector3();
            Vector3 nextPosition = prevFrame.nextPosition;
            Euler3 nextEuler = prevFrame.NextEuler3;
            Quaternion rotation = nextEuler.rotation;
            Vector3 point = targetEuler3.rotation * Vector3.forward;
            roll = Mathf.Clamp(roll, -1f, 1f);
            Vector3 linearSpeed = AdvanceLinearSpeed(prevFrame, options);
            Quaternion rotation2 = Quaternion.Inverse(rotation);
            Vector3 toDirection = rotation2 * point;
            Vector3 vector3 = rotation2 * prevFrame.Euler3Speed.ComponentsToVector3();
            Vector3 eulerAngles = Quaternion.FromToRotation(Vector3.forward, toDirection).eulerAngles;
            eulerAngles = KeepVectorComponentsWithinPlusMinus180(eulerAngles);
            Vector3 a2 = FindVelocityForRotationDelta(eulerAngles, a * d);
            Vector3 scale = Vector3.one - a * 0.1f;
            Vector3 vector4 = vector3;
            vector4.Scale(scale);
            Vector3 scale2 = Vector3.one - a * d * 0.1f;
            Vector3 vector5 = vector3;
            vector5.Scale(scale2);
            bool flag = false;
            bool flag2 = false;
            if (Mathf.Abs(vector4.x) > Mathf.Abs(a2.x))
            {
                scale.x = Mathf.Max(scale2.x, a2.x / vector3.x);
                flag = true;
            }
            if (Mathf.Abs(vector4.y) > Mathf.Abs(a2.y))
            {
                scale.y = Mathf.Max(scale2.y, a2.y / vector3.y);
                flag2 = true;
            }
            vector3.Scale(scale);
            Vector3 a3 = ScalePitchYawSoThatOneOfThemHasLengthOneAndSetZToZero(eulerAngles);
            Vector3 scale3 = vector;
            a3.Scale(scale3);
            Vector3 vector6 = a2 - vector3;
            if (Mathf.Abs(a3.x) > Mathf.Abs(vector6.x))
            {
                a3.x = vector6.x;
            }
            if (Mathf.Abs(a3.y) > Mathf.Abs(vector6.y))
            {
                a3.y = vector6.y;
            }
            if (flag)
            {
                a3.x = 0f;
            }
            if (flag2)
            {
                a3.y = 0f;
            }
            float num = vector.z * roll;
            vector3.z += num * 0.1f;
            Vector3 val = vector3 + a3 * 0.1f;
            val = Clamp(val, -vector2, vector2);
            Vector3 point2 = val;
            Vector3 vector7 = rotation * point2;
            Euler3 euler3Speed = new Euler3(vector7.x, vector7.y, vector7.z);
            Vector3 strafeSpeed = AdvanceStrafingSpeed(prevFrame, options, 0f, 0f);
            MovementFrame result = new MovementFrame(nextPosition, nextEuler, linearSpeed, strafeSpeed, euler3Speed);
            result.mode = 2;
            Quaternion rotationDelta = rotation * Quaternion.Inverse(prevFrame.rotation) * Quaternion.identity;
            //result.ActiveThrusterEffects = DetermineThrusterEffects(rotationDelta, strafeX, strafeY);
            return result;
        }

        private static void ClampVectorComponents(ref Vector3 vec, int min, int max)
        {
            vec.x = Mathf.Clamp(vec.x, min, max);
            vec.y = Mathf.Clamp(vec.y, min, max);
            vec.z = Mathf.Clamp(vec.z, min, max);
        }

        private static void ClampVectorComponents(ref Vector3 vec, Vector3 minVector, Vector3 maxVector)
        {
            vec.x = Mathf.Clamp(vec.x, minVector.x, maxVector.x);
            vec.y = Mathf.Clamp(vec.y, minVector.y, maxVector.y);
            vec.z = Mathf.Clamp(vec.z, minVector.z, maxVector.z);
        }

        public static MovementFrame TurnByPitchYawStrikes(MovementFrame prevFrame, Vector3 pitchYawRollFactor, Vector3 strafeDirection, float strafeMagnitude, MovementOptions options)
        {
            Vector3 a = options.maxTurnAcceleration.ComponentsToVector3();
            Vector3 scale = options.maxTurnSpeed.ComponentsToVector3();
            Vector3 nextPosition = prevFrame.nextPosition;
            Euler3 nextEuler = prevFrame.NextEuler3;
            Quaternion rotation = nextEuler.rotation;
            ClampVectorComponents(ref pitchYawRollFactor, -1, 1);
            Vector3 linearSpeed = AdvanceLinearSpeed(prevFrame, options);
            float strafeX = strafeDirection.x * strafeMagnitude;
            float strafeY = strafeDirection.y * strafeMagnitude;
            Vector3 strafeSpeed = AdvanceStrafingSpeed(prevFrame, options, strafeX, strafeY);
            Quaternion rotation2 = Quaternion.Inverse(rotation);
            Vector3 vector = rotation2 * prevFrame.Euler3Speed.ComponentsToVector3();
            Vector3 a2 = pitchYawRollFactor;
            a2.Scale(scale);
            Vector3 vec = a2 - vector;
            Vector3 vector2 = a * 0.1f;
            ClampVectorComponents(ref vec, -vector2, vector2);
            Vector3 vector3 = vector;
            vector3 += vec;
            if (Mathf.Abs(pitchYawRollFactor.z) < 0.01f)
            {
                vector3.z = GetNewSpeed(vector3.z, 0f, options.rollAcceleration);
            }
            Vector3 vector4 = rotation * vector3;
            MovementFrame result = new MovementFrame(euler3Speed: new Euler3(vector4.x, vector4.y, vector4.z), position: nextPosition, euler3: nextEuler, linearSpeed: linearSpeed, strafeSpeed: strafeSpeed);
            result.mode = 2;
            Quaternion rotationDelta = rotation * Quaternion.Inverse(prevFrame.rotation) * Quaternion.identity;
            //result.ActiveThrusterEffects = DetermineThrusterEffects(rotationDelta, strafeX, strafeY);
            return result;
        }

        private static bool ShipIsStrafing(float strafeX, float strafeY)
        {
            return strafeX * strafeX + strafeY * strafeY > 0.01f;
        }

        //private static List<ThrusterEffect> DetermineThrusterEffects(Quaternion rotationDelta, float strafeX, float strafeY)
        //{
        //    List<ThrusterEffect> list = new List<ThrusterEffect>();
        //    if (ShipIsStrafing(strafeX, strafeY))
        //    {
        //        ThrusterEffectStrafing thrusterEffectStrafing = default(ThrusterEffectStrafing);
        //        thrusterEffectStrafing.ThrusterDirectionLocal = new Vector3(0f - strafeX, 0f - strafeY, 0f);
        //        ThrusterEffect item = thrusterEffectStrafing;
        //        list.Add(item);
        //    }
        //    list.Add(new ThrusterEffectTurning
        //    {
        //        ShipRotationDelta = rotationDelta
        //    });
        //    return list;
        //}

        private static Vector3 FindVelocityForRotationDelta(Vector3 localDeltaAngles, float damping)
        {
            return FindVelocityForRotationDelta(localDeltaAngles, damping * Vector3.one);
        }

        private static Vector3 FindVelocityForRotationDelta(Vector3 localDeltaAngles, Vector3 damping)
        {
            float angle;
            Vector3 axis;
            Quaternion.Euler(localDeltaAngles).ToAngleAxis(out angle, out axis);
            return new Vector3(damping.x * axis.x, damping.y * axis.y, damping.z * axis.z) * angle;
        }

        private static Vector3 FindVelocityForRotationDelta(Quaternion currentRotation, Vector3 globalDeltaAngles, float damping)
        {
            Vector3 localDeltaAngles = Quaternion.Inverse(currentRotation) * globalDeltaAngles;
            return FindVelocityForRotationDelta(localDeltaAngles, damping);
        }

        private static Vector3 FindVelocityForRotationDelta(Quaternion currentRotation, Vector3 globalDeltaAngles, Vector3 damping)
        {
            Vector3 localDeltaAngles = Quaternion.Inverse(currentRotation) * globalDeltaAngles;
            return FindVelocityForRotationDelta(localDeltaAngles, damping);
        }

        private static Vector3 KeepVectorComponentsWithinPlusMinus180(Vector3 source)
        {
            source.x %= 360f;
            source.y %= 360f;
            source.z %= 360f;
            source.x = ((!(source.x > 180f)) ? source.x : (source.x - 360f));
            source.x = ((!(source.x < -180f)) ? source.x : (source.x + 360f));
            source.y = ((!(source.y > 180f)) ? source.y : (source.y - 360f));
            source.y = ((!(source.y < -180f)) ? source.y : (source.y + 360f));
            source.z = ((!(source.z > 180f)) ? source.z : (source.z - 360f));
            source.z = ((!(source.z < -180f)) ? source.z : (source.z + 360f));
            return source;
        }

        private static Vector3 FindFinalRotationAngles(Quaternion currentRotation, Vector3 angularVelocity, float damping)
        {
            return (currentRotation * Quaternion.AngleAxis(angularVelocity.magnitude / damping, angularVelocity.normalized)).eulerAngles;
        }

        private static Vector3 FindFinalRotationAngles(Quaternion currentRotation, Vector3 angularVelocity, Vector3 damping)
        {
            Vector3 normalized = angularVelocity.normalized;
            float magnitude = new Vector3(angularVelocity.x / damping.x, angularVelocity.y / damping.y, angularVelocity.z / damping.z).magnitude;
            normalized = new Vector3(normalized.x / damping.x, normalized.y / damping.y, normalized.z / damping.z).normalized;
            Vector3 eulerAngles = Quaternion.AngleAxis(magnitude, normalized).eulerAngles;
            return currentRotation * eulerAngles;
        }

        private static Vector3 FindMaximumVelocity(Vector3 acceleration, float damping)
        {
            return FindMaximumVelocity(acceleration, Vector3.one * damping);
        }

        private static Vector3 FindMaximumVelocity(Vector3 acceleration, Vector3 damping)
        {
            Vector3 scale = new Vector3(1f / damping.x, 1f / damping.y, 1f / damping.z);
            acceleration.Scale(scale);
            return acceleration;
        }

        private static Vector3 FindDamping(Vector3 acceleration, Vector3 maximumVelocity)
        {
            return new Vector3(acceleration.x / maximumVelocity.x, acceleration.y / maximumVelocity.y, acceleration.z / maximumVelocity.z);
        }

        private static float FindDamping(float acceleration, float maximumVelocity)
        {
            return acceleration / maximumVelocity;
        }

        private static Vector3 findShortestAnglesAbsolute(Vector3 eulerRotation)
        {
            return new Vector3((!(eulerRotation.x > 180f)) ? eulerRotation.x : Mathf.Abs(eulerRotation.x - 360f), (!(eulerRotation.y > 180f)) ? eulerRotation.y : Mathf.Abs(eulerRotation.y - 360f), (!(eulerRotation.z > 180f)) ? eulerRotation.z : Mathf.Abs(eulerRotation.z - 360f));
        }

        public static MovementFrame MoveToDirectionWithoutRoll(MovementFrame prevFrame, Euler3 targetEuler3, MovementOptions options)
        {
            Quaternion rotation = prevFrame.Euler3Speed.rotation;
            Quaternion q = Quaternion.FromToRotation(prevFrame.euler3.direction, targetEuler3.direction);
            Quaternion lhs = ScaleWithMax(q, 2f, options.yawMaxSpeed);
            Quaternion q2 = lhs * Quaternion.Inverse(rotation);
            Quaternion to = ScaleWithMax(q2, 8f, options.yawAcceleration);
            Quaternion changePerSecond = Quaternion.RotateTowards(Quaternion.identity, to, options.yawAcceleration);
            Quaternion to2 = Euler3.RotateOverTime(rotation, changePerSecond, 0.1f);
            Quaternion quat = Quaternion.RotateTowards(Quaternion.identity, to2, options.yawMaxSpeed);
            Euler3 euler3Speed = Euler3.Rotation(quat);
            Vector3 linearSpeed = AdvanceLinearSpeed(prevFrame, options);
            Vector3 strafeSpeed = AdvanceStrafingSpeed(prevFrame, options, 0f, 0f);
            Euler3 nextEuler = prevFrame.NextEuler3;
            MovementFrame result = new MovementFrame(prevFrame.nextPosition, nextEuler, linearSpeed, strafeSpeed, euler3Speed);
            result.mode = 2;
            Quaternion rotationDelta = nextEuler.rotation * Quaternion.Inverse(prevFrame.rotation) * Quaternion.identity;
            //result.ActiveThrusterEffects = DetermineThrusterEffects(rotationDelta, 0f, 0f);
            return result;
        }

        public static Quaternion ScaleWithMax(Quaternion q, float scale, float max)
        {
            if (q.w < 0f)
            {
                q = new Quaternion(0f - q.x, 0f - q.y, 0f - q.z, 0f - q.w);
            }
            float angle;
            Vector3 axis;
            q.ToAngleAxis(out angle, out axis);
            float angle2 = Mathf.Clamp(angle * scale, 0f, max);
            return Quaternion.AngleAxis(angle2, axis);
        }

        public static Quaternion RotateOverTimeFromReferenceFrame(Quaternion rotateThis, Quaternion byThis, Quaternion asIfMeasuredFromThis, float dt)
        {
            Quaternion start = Quaternion.Inverse(asIfMeasuredFromThis) * rotateThis;
            Quaternion rhs = Euler3.RotateOverTime(start, byThis, dt);
            return asIfMeasuredFromThis * rhs;
        }

        public static float SlowingThrust(float v, float maxAccelThisFrame)
        {
            float num = (v < 0f) ? 1 : (-1);
            return num * Mathf.Min(Mathf.Abs(v), Mathf.Abs(maxAccelThisFrame));
        }

        private static Vector3 Clamp(Vector3 val, Vector3 min, Vector3 max)
        {
            return Vector3.Min(Vector3.Max(val, min), max);
        }

        public static Vector3 ClampToRotatedBox(Vector3 v, Vector3 halfSideLengths, Quaternion boxOrientation)
        {
            Vector3 val = Quaternion.Inverse(boxOrientation) * v;
            Vector3 point = Clamp(val, -halfSideLengths, halfSideLengths);
            return boxOrientation * point;
        }

        public static Vector3 ClipAtPitchYawBorderOfRotatedBox(Vector3 v, Vector3 halfSideLengths, Quaternion boxOrientation)
        {
            Vector3 val = Quaternion.Inverse(boxOrientation) * v;
            Vector3 point = ClipEulerAgainstMaxPitchYaw(val, -halfSideLengths, halfSideLengths);
            return boxOrientation * point;
        }

        public static MovementFrame QWEASD(MovementFrame prevFrame, float pitch, float yaw, float roll, MovementOptions options)
        {
            Vector3 vector = new Vector3(pitch, yaw, roll);
            Vector3 b = (options.maxTurnAcceleration * 0.1f).ComponentsToVector3();
            vector = Vector3.Scale(vector, b);
            Vector3 vector2 = prevFrame.Euler3Speed.ComponentsToVector3();
            if (true)
            {
                Vector3 vector3 = Quaternion.Inverse(prevFrame.rotation) * vector2;
                if (vector.x == 0f)
                {
                    vector.x = SlowingThrust(vector3.x, b.x);
                }
                if (vector.y == 0f)
                {
                    vector.y = SlowingThrust(vector3.y, b.y);
                }
                if (vector.z == 0f)
                {
                    vector.z = SlowingThrust(vector3.z, b.z);
                }
            }
            Vector3 b2 = prevFrame.rotation * vector;
            Vector3 v = vector2 + b2;
            vector2 = ClampToRotatedBox(v, options.maxTurnSpeed.ComponentsToVector3(), prevFrame.rotation);
            Euler3 euler3Speed = default(Euler3);
            euler3Speed.ComponentsFromVector3(vector2);
            Vector3 linearSpeed = AdvanceLinearSpeed(prevFrame, options);
            Vector3 strafeSpeed = AdvanceStrafingSpeed(prevFrame, options, 0f, 0f);
            Euler3 nextEuler = prevFrame.NextEuler3;
            MovementFrame result = new MovementFrame(prevFrame.nextPosition, nextEuler, linearSpeed, strafeSpeed, euler3Speed);
            result.mode = 2;
            Quaternion rotationDelta = nextEuler.rotation * Quaternion.Inverse(prevFrame.rotation) * Quaternion.identity;
            //result.ActiveThrusterEffects = DetermineThrusterEffects(rotationDelta, 0f, 0f);
            return result;
        }

        public static Vector3 AdvanceLinearSpeed(MovementFrame prevFrame, MovementOptions options)
        {
            Vector3 linearSpeed = prevFrame.linearSpeed;
            Vector3 result = linearSpeed;
            if (options.gear != Gear.RCS)
            {
                Vector3 direction = prevFrame.euler3.direction;
                direction.z *= -1;
                float num = Vector3.Dot(direction, linearSpeed);
                Vector3 b = direction * num;
                Vector3 vector = linearSpeed - b;
                float magnitude = vector.magnitude;
                Vector3 a = vector.normalized * GetNewSpeed(magnitude, 0f, options.inertiaCompensation);
                Vector3 b2 = direction * GetNewSpeed(num, options.speed, options.acceleration);
                result = a + b2;
            }
            return result;
        }

        public static Vector3 AdvanceStrafingSpeed(MovementFrame prevFrame, MovementOptions options, float strafeX, float strafeY)
        {
            Vector3 strafeSpeed = prevFrame.strafeSpeed;
            strafeX = Mathf.Clamp(strafeX, -1f, 1f);
            strafeY = Mathf.Clamp(strafeY, -1f, 1f);
            Vector3 point = Quaternion.Inverse(prevFrame.rotation) * strafeSpeed;
            float num = strafeX * options.strafeMaxSpeed;
            float num2 = strafeY * options.strafeMaxSpeed;
            point.x = GetNewSpeed(point.x, num, (float)((!(point.x * num < -1f)) ? 1 : 2) * options.strafeAcceleration);
            point.y = GetNewSpeed(point.y, num2, (float)((!(point.y * num2 < -1f)) ? 1 : 2) * options.strafeAcceleration);
            point.z = GetNewSpeed(point.z, 0f, options.strafeAcceleration);
            return prevFrame.rotation * point;
        }

        public static float GetNewSpeed(float current, float target, float acceleration)
        {
            float num = acceleration * 0.1f;
            float num2 = current - target;
            if (Mathf.Abs(num2) < num)
            {
                return target;
            }
            if (num2 > 0f)
            {
                return current - num;
            }
            return current + num;
        }

        private static bool IsPowerOfTwoOrZero(OutCode x)
        {
            return (x & (x - 1)) == 0;
        }

        private static OutCode ComputeOutCode(Vector3 euler, Vector3 min, Vector3 max)
        {
            OutCode outCode = OutCode.INSIDE;
            if (euler.x < min.x)
            {
                outCode |= OutCode.LEFT;
            }
            else if (euler.x > max.x)
            {
                outCode |= OutCode.RIGHT;
            }
            if (euler.y < min.y)
            {
                outCode |= OutCode.BOTTOM;
            }
            else if (euler.y > max.y)
            {
                outCode |= OutCode.TOP;
            }
            if (!IsPowerOfTwoOrZero(outCode))
            {
                float num = euler.y / euler.x;
                switch (outCode)
                {
                    case OutCode.LEFT | OutCode.TOP:
                        {
                            float num4 = (0f - max.y) / max.x;
                            outCode = ((num > num4) ? OutCode.LEFT : OutCode.TOP);
                            break;
                        }
                    case OutCode.RIGHT | OutCode.TOP:
                        {
                            float num3 = max.y / max.x;
                            outCode = ((!(num > num3)) ? OutCode.RIGHT : OutCode.TOP);
                            break;
                        }
                    case OutCode.LEFT | OutCode.BOTTOM:
                        {
                            float num5 = max.y / max.x;
                            outCode = ((!(num > num5)) ? OutCode.LEFT : OutCode.BOTTOM);
                            break;
                        }
                    case OutCode.RIGHT | OutCode.BOTTOM:
                        {
                            float num2 = (0f - max.y) / max.x;
                            outCode = ((!(num > num2)) ? OutCode.BOTTOM : OutCode.RIGHT);
                            break;
                        }
                }
            }
            return outCode;
        }

        private static Vector3 ClipEulerAgainstMaxPitchYaw(Vector3 val, Vector3 min, Vector3 max)
        {
            OutCode outCode = ComputeOutCode(val, min, max);
            if ((OutCode.INSIDE | outCode) != 0)
            {
                OutCode outCode2 = outCode;
                if ((outCode2 & OutCode.TOP) != 0)
                {
                    val.x = val.x * max.y / val.y;
                    val.y = max.y;
                }
                else if ((outCode2 & OutCode.BOTTOM) != 0)
                {
                    val.x = val.x * min.y / val.y;
                    val.y = min.y;
                }
                else if ((outCode2 & OutCode.RIGHT) != 0)
                {
                    val.y = val.y * max.x / val.x;
                    val.x = max.x;
                }
                else if ((outCode2 & OutCode.LEFT) != 0)
                {
                    val.y = val.y * min.x / val.x;
                    val.x = min.x;
                }
            }
            val.z = Mathf.Clamp(val.z, min.z, max.z);
            return val;
        }
    }
}
