using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    class ObjectStats : IProtocolWrite
    {
        public float Accuracy
        {
            get
            {
                return ObjStats[ObjectStat.Accuracy];
            }
        }

        public float MaxHullPoints
        {
            get
            {
                return ObjStats[ObjectStat.MaxHullPoints];
            }
        }

        public float MaxPowerPoints
        {
            get
            {
                return ObjStats[ObjectStat.MaxPowerPoints];
            }
        }

        public float HullRecovery
        {
            get
            {
                return ObjStats[ObjectStat.HullRecovery];
            }
        }

        public float PowerRecovery
        {
            get
            {
                return ObjStats[ObjectStat.PowerRecovery];
            }
        }

        public float Avoidance
        {
            get
            {
                return ObjStats[ObjectStat.Avoidance];
            }
        }

        public float FirewallRating
        {
            get
            {
                return ObjStats[ObjectStat.FirewallRating];
            }
        }

        public float ArmorValue
        {
            get
            {
                return ObjStats[ObjectStat.ArmorValue];
            }
        }

        public float CriticalDefense
        {
            get
            {
                return ObjStats[ObjectStat.CriticalDefense];
            }
        }

        public float DamageHigh
        {
            get
            {
                return ObjStats[ObjectStat.DamageHigh];
            }
        }

        public float DamageLow
        {
            get
            {
                return ObjStats[ObjectStat.DamageLow];
            }
        }

        public float DrainHigh
        {
            get
            {
                return ObjStats[ObjectStat.DrainHigh];
            }
        }

        public float DrainLow
        {
            get
            {
                return ObjStats[ObjectStat.DrainLow];
            }
        }

        public float DPS
        {
            get
            {
                return (DamageHigh + DamageLow) / 2f / Cooldown;
            }
        }

        public float PenetrationStrength
        {
            get
            {
                return ObjStats[ObjectStat.PenetrationStrength];
            }
        }

        public float ArmorPiercing
        {
            get
            {
                return ObjStats[ObjectStat.ArmorPiercing];
            }
        }

        public float CriticalOffense
        {
            get
            {
                return ObjStats[ObjectStat.CriticalOffense];
            }
        }

        public float Acceleration
        {
            get
            {
                return ObjStats[ObjectStat.Acceleration];
            }
        }

        public float MaxSpeed
        {
            get
            {
                return ObjStats[ObjectStat.Speed];
            }
        }

        public float BoostSpeed
        {
            get
            {
                return ObjStats[ObjectStat.BoostSpeed];
            }
        }

        public float BoostCost
        {
            get
            {
                return ObjStats[ObjectStat.BoostCost];
            }
        }

        public float TurnAcceleration
        {
            get
            {
                return (ObjStats[ObjectStat.PitchAcceleration] + ObjStats[ObjectStat.YawAcceleration]) / 2f;
            }
        }

        public float TurnSpeed
        {
            get
            {
                return (ObjStats[ObjectStat.PitchMaxSpeed] + ObjStats[ObjectStat.YawMaxSpeed]) / 2f;
            }
        }

        public float PitchMaxSpeed
        {
            get
            {
                return ObjStats[ObjectStat.PitchMaxSpeed];
            }
        }

        public float YawMaxSpeed
        {
            get
            {
                return ObjStats[ObjectStat.YawMaxSpeed];
            }
        }

        public float RollMaxSpeed
        {
            get
            {
                return ObjStats[ObjectStat.RollMaxSpeed];
            }
        }

        public float StrafeMaxSpeed
        {
            get
            {
                return ObjStats[ObjectStat.StrafeMaxSpeed];
            }
        }

        public float PitchAcceleration
        {
            get
            {
                return ObjStats[ObjectStat.PitchAcceleration];
            }
        }

        public float YawAcceleration
        {
            get
            {
                return ObjStats[ObjectStat.YawAcceleration];
            }
        }

        public float RollAcceleration
        {
            get
            {
                return ObjStats[ObjectStat.RollAcceleration];
            }
        }

        public float StrafeAcceleration
        {
            get
            {
                return ObjStats[ObjectStat.StrafeAcceleration];
            }
        }

        public float InertiaCompensation
        {
            get
            {
                return ObjStats[ObjectStat.InertiaCompensation];
            }
        }

        public float LifeTime
        {
            get
            {
                return ObjStats[ObjectStat.LifeTime];
            }
        }

        public int MaxCargo
        {
            get
            {
                return (int)ObjStats[ObjectStat.CargoHoldVolume];
            }
        }

        public float FTLRange
        {
            get
            {
                return ObjStats[ObjectStat.FtlRange];
            }
        }

        public float FTLCharge
        {
            get
            {
                return ObjStats[ObjectStat.FtlCharge];
            }
        }

        public float FTLCooldown
        {
            get
            {
                return ObjStats[ObjectStat.FtlCooldown];
            }
        }

        public float FTLCost
        {
            get
            {
                return ObjStats[ObjectStat.FtlCost];
            }
        }

        public float OptimalRange
        {
            get
            {
                return ObjStats[ObjectStat.OptimalRange];
            }
        }

        public float MaxRange
        {
            get
            {
                return ObjStats[ObjectStat.MaxRange];
            }
        }

        public float MinRange
        {
            get
            {
                return ObjStats[ObjectStat.MinRange];
            }
        }

        public float Angle
        {
            get
            {
                return ObjStats[ObjectStat.Angle];
            }
        }

        public float Cooldown
        {
            get
            {
                return ObjStats[ObjectStat.Cooldown];
            }
        }

        public float DamageMining
        {
            get
            {
                return ObjStats[ObjectStat.DamageMining];
            }
        }

        public float PowerPointCost
        {
            get
            {
                return ObjStats[ObjectStat.PowerPointCost];
            }
        }

        public float PowerPointCostPerSecond
        {
            get
            {
                return ObjStats[ObjectStat.PpCostPerSec];
            }
        }

        public float FlareRange
        {
            get
            {
                return ObjStats[ObjectStat.FlareRange];
            }
        }

        public float HullPointRestore
        {
            get
            {
                return ObjStats[ObjectStat.HullPointRestore];
            }
        }

        public float PowerPointRestore
        {
            get
            {
                return ObjStats[ObjectStat.PowerPointRestore];
            }
        }

        public float DetectionInnerRadius
        {
            get
            {
                return ObjStats[ObjectStat.DetectionInnerRadius];
            }
        }

        public float DetectionOuterRadius
        {
            get
            {
                return ObjStats[ObjectStat.DetectionOuterRadius];
            }
        }

        public float DetectionVisualRadius
        {
            get
            {
                return ObjStats[ObjectStat.DetectionVisualRadius];
            }
        }

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
