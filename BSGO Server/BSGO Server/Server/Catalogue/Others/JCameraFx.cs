using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    class JCameraFx : IProtocolWrite
    {
        public bool ForceDisableBloom;

        public JCameraFx(bool forceDisableBloom)
        {
            ForceDisableBloom = forceDisableBloom;
        }

        public void Write(BgoProtocolWriter w)
        {
            w.Write(ForceDisableBloom);
        }
    }
}
