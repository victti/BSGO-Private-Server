using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    enum ShipAbilitySide : byte
    {
        Self = 1,
        Any = 2,
        Neutral = 4,
        Friend = 8,
        Enemy = 0x10
    }
}
