using System.Collections.Generic;

namespace BSGO_Server
{
    internal class ObjectStats : IProtocolWrite
    {
        public float Accuracy => ObjStats[ObjectStat.Accuracy];

        public float MaxHullPoints => ObjStats[ObjectStat.MaxHullPoints];

        public float MaxPowerPoints => ObjStats[ObjectStat.MaxPowerPoints];

        public float HullRecovery => ObjStats[ObjectStat.HullRecovery];

        public float PowerRecovery => ObjStats[ObjectStat.PowerRecovery];

        public float Avoidance => ObjStats[ObjectStat.Avoidance];

        public float FirewallRating => ObjStats[ObjectStat.FirewallRating];

        public float ArmorValue => ObjStats[ObjectStat.ArmorValue];

        public float CriticalDefense => ObjStats[ObjectStat.CriticalDefense];

        public float DamageHigh => ObjStats[ObjectStat.DamageHigh];

        public float DamageLow => ObjStats[ObjectStat.DamageLow];

        public float DrainHigh => ObjStats[ObjectStat.DrainHigh];

        public float DrainLow => ObjStats[ObjectStat.DrainLow];

        public float DPS => (DamageHigh + DamageLow) / 2f / Cooldown;

        public float PenetrationStrength => ObjStats[ObjectStat.PenetrationStrength];

        public float ArmorPiercing => ObjStats[ObjectStat.ArmorPiercing];

        public float CriticalOffense => ObjStats[ObjectStat.CriticalOffense];

        public float Acceleration => ObjStats[ObjectStat.Acceleration];

        public float MaxSpeed => ObjStats[ObjectStat.Speed];

        public float BoostSpeed => ObjStats[ObjectStat.BoostSpeed];

        public float BoostCost => ObjStats[ObjectStat.BoostCost];

        public float TurnAcceleration => (ObjStats[ObjectStat.PitchAcceleration] + ObjStats[ObjectStat.YawAcceleration]) / 2f;

        public float TurnSpeed => (ObjStats[ObjectStat.PitchMaxSpeed] + ObjStats[ObjectStat.YawMaxSpeed]) / 2f;

        public float PitchMaxSpeed => ObjStats[ObjectStat.PitchMaxSpeed];

        public float YawMaxSpeed => ObjStats[ObjectStat.YawMaxSpeed];

        public float RollMaxSpeed => ObjStats[ObjectStat.RollMaxSpeed];

        public float StrafeMaxSpeed => ObjStats[ObjectStat.StrafeMaxSpeed];

        public float PitchAcceleration => ObjStats[ObjectStat.PitchAcceleration];

        public float YawAcceleration => ObjStats[ObjectStat.YawAcceleration];

        public float RollAcceleration => ObjStats[ObjectStat.RollAcceleration];

        public float StrafeAcceleration => ObjStats[ObjectStat.StrafeAcceleration];

        public float InertiaCompensation => ObjStats[ObjectStat.InertiaCompensation];

        public float LifeTime => ObjStats[ObjectStat.LifeTime];

        public int MaxCargo => (int)ObjStats[ObjectStat.CargoHoldVolume];

        public float FTLRange => ObjStats[ObjectStat.FtlRange];

        public float FTLCharge => ObjStats[ObjectStat.FtlCharge];

        public float FTLCooldown => ObjStats[ObjectStat.FtlCooldown];

        public float FTLCost => ObjStats[ObjectStat.FtlCost];

        public float OptimalRange => ObjStats[ObjectStat.OptimalRange];

        public float MaxRange => ObjStats[ObjectStat.MaxRange];

        public float MinRange => ObjStats[ObjectStat.MinRange];

        public float Angle => ObjStats[ObjectStat.Angle];

        public float Cooldown => ObjStats[ObjectStat.Cooldown];

        public float DamageMining => ObjStats[ObjectStat.DamageMining];

        public float PowerPointCost => ObjStats[ObjectStat.PowerPointCost];

        public float PowerPointCostPerSecond => ObjStats[ObjectStat.PpCostPerSec];

        public float FlareRange => ObjStats[ObjectStat.FlareRange];

        public float HullPointRestore => ObjStats[ObjectStat.HullPointRestore];

        public float PowerPointRestore => ObjStats[ObjectStat.PowerPointRestore];

        public float DetectionInnerRadius => ObjStats[ObjectStat.DetectionInnerRadius];

        public float DetectionOuterRadius => ObjStats[ObjectStat.DetectionOuterRadius];

        public float DetectionVisualRadius => ObjStats[ObjectStat.DetectionVisualRadius];

        public Dictionary<ObjectStat, float> ObjStats;

        public ObjectStats(Dictionary<ObjectStat, float> objStats)
        {
            ObjStats = objStats;
        }

        public void Write(BgoProtocolWriter w)
        {
            int num = ObjStats.Count;
            w.Write((ushort)num);
            foreach (KeyValuePair<ObjectStat, float> pair in ObjStats)
            {
                w.Write((ushort)pair.Key);
                w.Write(pair.Value);
            }
        }
    }
}
