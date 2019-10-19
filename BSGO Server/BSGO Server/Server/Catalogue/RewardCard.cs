namespace BSGO_Server
{
    internal class RewardCard : Card
    {
        public int Experience { get; set; }
        //public List<ShipItem> Items { get; set; }= new List<ShipItems>();
        public AugmentActionType Action { get; set; }
        public string Package { get; set; } = string.Empty;
        public uint ItemGroup { get; set; } = 101;
        public uint PackagedCubits { get; set; }

        public RewardCard(uint cardGUID, CardView cardView, int experience, AugmentActionType action, string package, uint packagedCubits)
            : base(cardGUID, cardView)
        {
            Experience = experience;
            Action = action;
            Package = package;
            PackagedCubits = packagedCubits;
        }

        public override void Write(BgoProtocolWriter w)
        {
            base.Write(w);
            w.Write(Experience);

            // Since the list isn't added yet, we just send an empty list.
            w.Write((ushort)0);

            w.Write((byte)Action);
            w.Write(PackagedCubits);
            w.Write(Package);
            w.Write(ItemGroup);

            // The game also wants items based on your faction. Since we don't have that, also send an
            // empty list.
            w.Write((ushort)0); // Colonial
            w.Write((ushort)0); // Cylon
        }
    }
}
