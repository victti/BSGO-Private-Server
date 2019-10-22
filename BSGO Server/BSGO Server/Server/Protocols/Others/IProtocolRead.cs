using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    internal interface IProtocolRead
    {
        void Read(BgoProtocolReader r);
    }
}
