using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    class ShipLightCard : Card
    {
        public uint ShipObjectKey;

        public byte Tier;

        public ShipRole[] ShipRoles;

        public ShipRoleDeprecated ShipRoleDeprecated;

        public ShipLightCard(uint cardGUID, CardView cardView, uint shipObjectKey, byte tier, ShipRole[] shipRoles, ShipRoleDeprecated shipRoleDeprecated)
            : base(cardGUID, cardView)
        {
            ShipObjectKey = shipObjectKey;
            Tier = tier;
            ShipRoles = shipRoles;
            ShipRoleDeprecated = shipRoleDeprecated;
        }

        public override void Write(BgoProtocolWriter w)
        {
            base.Write(w);
            w.Write(ShipObjectKey);
            w.Write(Tier);
            int num = ShipRoles.Length;
            w.Write(num);
            for (int i = 0; i < num; i++)
            {
                w.Write((byte)ShipRoles[i]);
            }
            w.Write((byte)ShipRoleDeprecated);
        }
    }
}
