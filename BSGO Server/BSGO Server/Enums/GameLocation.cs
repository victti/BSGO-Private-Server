using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    enum GameLocation : byte
    {
        Unknown,
        Space,
        Room,
        Story,
        Disconnect,
        Arena,
        BattleSpace,
        Tournament,
        Tutorial,
        Teaser,
        Avatar,
        Starter,
        Zone
    }
}
