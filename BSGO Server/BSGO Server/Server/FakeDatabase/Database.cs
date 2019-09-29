using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    class Database
    {
        public static GameLocation GetLastGameLocation()
        {
            return GameLocation.Starter;

            return GameLocation.Unknown;
        }
    }
}
