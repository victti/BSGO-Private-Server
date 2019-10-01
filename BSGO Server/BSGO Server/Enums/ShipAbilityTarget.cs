using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    enum ShipAbilityTarget
    {
        Asteroid = 1,
        Ship = 2,
        Any = 4,
        Missile = 8,
        Planetoid = 0x10,
        Mine = 0x20,
        JumpTargetTransponder = 0x40,
        Comet = 0x80
    }

}
