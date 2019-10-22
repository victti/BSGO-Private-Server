namespace BSGO_Server
{
    internal class ShipLightCard : Card
    {
        public uint ShipObjectKey { get; set; }

        public byte Tier { get; set; }

        public ShipRole[] ShipRoles { get; set; }

        public ShipRoleDeprecated ShipRoleDeprecated { get; set; }

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
            w.Write((ushort)num);
            for (int i = 0; i < num; i++)
            {
                w.Write((byte)ShipRoles[i]);
            }
            w.Write((byte)ShipRoleDeprecated);
        }
    }
}
