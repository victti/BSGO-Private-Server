using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server._3dAlgorithm
{
    internal interface IMovementController
    {
        float CurrentSpeed
        {
            get;
        }

        Vector3 CurrentStrafingSpeed
        {
            get;
        }

        float MarchSpeed
        {
            get;
        }

        Gear Gear
        {
            get;
        }

        MovementFrame GetTickFrame(Tick tick);

        bool Move(double time);

        void Advance(Tick tick);

        void PostAdvance();

        void AddManeuver(Maneuver newManeuver);

        void AddSyncFrame(Tick tick, MovementFrame frame);
    }
}
