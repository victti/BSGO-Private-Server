using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server._3dAlgorithm
{
    internal class MovementOptions : IProtocolWrite
    {
        public Gear gear;

        public float speed;

        public float maxRegularLinSpeed = 60f;

        public float acceleration = 10f;

        public float inertiaCompensation = 10f;

        public float pitchAcceleration;

        public float pitchMaxSpeed;

        public float yawAcceleration;

        public float yawMaxSpeed;

        public float rollAcceleration;

        public float rollMaxSpeed;

        public float strafeAcceleration = 20f;

        public float strafeMaxSpeed = 20f;

        public float minYawSpeed;

        public float maxPitch;

        public float maxRoll;

        public float pitchFading;

        public float yawFading;

        public float rollFading;

        public Euler3 maxTurnAcceleration
        {
            get
            {
                return new Euler3(pitchAcceleration, yawAcceleration, rollAcceleration);
            }
        }

        public Euler3 maxTurnSpeed
        {
            get
            {
                return new Euler3(pitchMaxSpeed, yawMaxSpeed, rollMaxSpeed);
            }
        }

        public Euler3 MinEulerSpeed(Euler3 euler3)
        {
            return new Euler3(MinPitchSpeed(euler3.pitch), MinYawSpeed(euler3.roll, euler3.pitch), MinRollSpeed(euler3.roll));
        }

        public Euler3 MaxEulerSpeed(Euler3 euler3)
        {
            return new Euler3(MaxPitchSpeed(euler3.pitch), MaxYawSpeed(euler3.roll, euler3.pitch), MaxRollSpeed(euler3.roll));
        }

        public float MinYawSpeed(float roll, float pitch)
        {
            float num = 0f - Mathf.Clamp(Algorithm3D.NormalizeAngle(roll) / maxRoll, -1f, 1f);
            float num2 = Mathf.Lerp(0f - yawMaxSpeed, 0f - minYawSpeed, (num + 1f) / 2f);
            return num2 * (1f + Mathf.Abs(Algorithm3D.NormalizeAngle(pitch)) * 1f / 90f);
        }

        public float MaxYawSpeed(float roll, float pitch)
        {
            float num = 0f - Mathf.Clamp(Algorithm3D.NormalizeAngle(roll) / maxRoll, -1f, 1f);
            float num2 = Mathf.Lerp(minYawSpeed, yawMaxSpeed, (num + 1f) / 2f);
            return num2 * (1f + Mathf.Abs(Algorithm3D.NormalizeAngle(pitch)) * 1f / 90f);
        }

        public float MinPitchSpeed(float pitch)
        {
            float num = (0f - maxPitch) * 0.7f;
            if (pitch < num)
            {
                return Mathf.Lerp(0f - pitchMaxSpeed, 0f, Mathf.Clamp01((num - pitch) / (maxPitch * 0.3f)));
            }
            return 0f - pitchMaxSpeed;
        }

        public float MaxPitchSpeed(float pitch)
        {
            float num = maxPitch * 0.7f;
            if (pitch > num)
            {
                return Mathf.Lerp(pitchMaxSpeed, 0f, Mathf.Clamp01((0f - num + pitch) / (maxPitch * 0.3f)));
            }
            return pitchMaxSpeed;
        }

        public float MinRollSpeed(float roll)
        {
            float num = (0f - maxRoll) * 0.5f;
            return (!(roll < num)) ? (0f - rollMaxSpeed) : Mathf.Lerp(0f - rollMaxSpeed, 0f, Mathf.Clamp01((num - roll) / (maxRoll * 0.5f)));
        }

        public float MaxRollSpeed(float roll)
        {
            float num = maxRoll * 0.5f;
            return (!(roll > num)) ? rollMaxSpeed : Mathf.Lerp(rollMaxSpeed, 0f, Mathf.Clamp01((0f - num + roll) / (maxRoll * 0.5f)));
        }

        public void Write(BgoProtocolWriter pw)
        {
            pw.Write((byte)gear);
            pw.Write(speed);
            pw.Write(acceleration);
            pw.Write(inertiaCompensation);
            pw.Write(pitchAcceleration);
            pw.Write(pitchMaxSpeed);
            pw.Write(yawAcceleration);
            pw.Write(yawMaxSpeed);
            pw.Write(rollAcceleration);
            pw.Write(rollMaxSpeed);
            pw.Write(strafeAcceleration);
            pw.Write(strafeMaxSpeed);
        }

        public void ApplyCard(MovementCard card)
        {
            minYawSpeed = card.minYawSpeed * yawMaxSpeed;
            maxPitch = card.maxPitch;
            maxRoll = card.maxRoll;
            pitchFading = card.pitchFading;
            yawFading = card.yawFading;
            rollFading = card.rollFading;
        }

        public override string ToString()
        {
            return string.Format("Gear: {0}, Speed: {1}, MaxRegularLinSpeed: {2}, Acceleration: {3}, InteriaCompensation: {4}, PitchAcceleration: {5}, PitchMaxSpeed: {6}, YawAcceleration: {7}, YawMaxSpeed: {8}, RollAcceleration: {9}, RollMaxSpeed: {10}, StrafeAcceleration: {11}, StrafeMaxSpeed: {12}, MinYawSpeed: {13}, MaxPitch: {14}, MaxRoll: {15}, PitchFading: {16}, YawFading: {17}, RollFading: {18}", gear, speed, maxRegularLinSpeed, acceleration, inertiaCompensation, pitchAcceleration, pitchMaxSpeed, yawAcceleration, yawMaxSpeed, rollAcceleration, rollMaxSpeed, strafeAcceleration, strafeMaxSpeed, minYawSpeed, maxPitch, maxRoll, pitchFading, yawFading, rollFading);
        }
    }
}
