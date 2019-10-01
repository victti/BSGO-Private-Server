using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    enum ShopCategory : byte
    {
        None,
        Resource,
        Augment,
        Consumable,
        System,
        StarterKit,
        Ship,
        Unknown
    }
}
