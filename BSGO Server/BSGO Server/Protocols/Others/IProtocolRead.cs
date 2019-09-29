using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    interface IProtocolRead
    {
        void Read(BgoProtocolReader r);
    }
}
