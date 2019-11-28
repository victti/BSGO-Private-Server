using BSGO_Server._3dAlgorithm;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    internal class PlayerShip
    {
        private int index { get; set; }
        public ShipCard ShipCard { get; set; }
        public uint WorldGuid { get; private set; }
        private ushort hangarId { get; set; }
        public long timeOrigin { get; set; }
        public ObjectStats currentShipStats { get; set; }
        public MovementOptions MovementOptions { get; set; } = new MovementOptions();
        public MovementFrame MovementFrame { get; set; } = new MovementFrame(new Vector3(), new Euler3(), new Vector3(), new Vector3(), new Euler3());
        public uint sectorId { get; set; } = 0;
        public int requestedJumpSectorId { get; set; } = -1;
        public QWEASD qweasd { get; set; } = new QWEASD();
        public Euler3 direction { get; set; }
        public Gear shipGear { get; set; } = Gear.Regular;
        public byte shipMode { get; set; }
        public float shipSpeed { get; set; }
        public bool isVisible { get; set; } = false;
        public bool isSpawned { get; set; } = false;
        public DateTime jumpInTime { get; set; }
        public DateTime ftlTime { get; set; }
        public ManeuverController ManeuverController { get; set; }
        public ushort HangarId
        {
            get
            {
                return hangarId;
            }
            set
            {
                uint WorldGuid = Catalogue.GetShipCardGuidById(value, Server.GetClientByIndex(index).Character.Faction);
                this.WorldGuid = WorldGuid;
                GUICard ownerGUIDCard = new GUICard((uint)index, CardView.GUI, "", 0, "", 0, "", ((GUICard)Catalogue.FetchCard(WorldGuid, CardView.GUI)).GUIAvatarSlotTexturePath, "", new string[0]);
                Catalogue.AddCard(ownerGUIDCard);

                ShipCard = (ShipCard) Catalogue.FetchCard(WorldGuid, CardView.Ship);
                MovementCard movementCardSpaceShip = (MovementCard)Catalogue.FetchCard(WorldGuid, CardView.Movement);
                MovementOptions.gear = Gear.Regular;
                MovementOptions.speed = shipSpeed;
                MovementOptions.acceleration = ShipCard.Stats.Acceleration;
                MovementOptions.inertiaCompensation = ShipCard.Stats.InertiaCompensation;
                MovementOptions.pitchAcceleration = ShipCard.Stats.PitchAcceleration;
                MovementOptions.pitchMaxSpeed = ShipCard.Stats.PitchMaxSpeed;
                MovementOptions.yawAcceleration = ShipCard.Stats.YawAcceleration;
                MovementOptions.yawMaxSpeed = ShipCard.Stats.YawMaxSpeed;
                MovementOptions.rollAcceleration = ShipCard.Stats.RollAcceleration;
                MovementOptions.rollMaxSpeed = ShipCard.Stats.RollMaxSpeed;
                MovementOptions.strafeAcceleration = ShipCard.Stats.StrafeAcceleration;
                MovementOptions.strafeMaxSpeed = ShipCard.Stats.StrafeMaxSpeed;
                MovementOptions.minYawSpeed = movementCardSpaceShip.minYawSpeed;
                MovementOptions.maxPitch = movementCardSpaceShip.maxPitch;
                MovementOptions.maxRoll = movementCardSpaceShip.maxRoll;
                MovementOptions.pitchFading = movementCardSpaceShip.pitchFading;
                MovementOptions.yawFading = movementCardSpaceShip.yawFading;
                MovementOptions.rollFading = movementCardSpaceShip.rollFading;

                hangarId = value;
                CatalogueProtocol.GetProtocol().SendCard(index, CardView.GUI, (uint)index);
            }
        }

        public PlayerShip(int index)
        {
            this.index = index;
        }

        public void Update(float dt)
        {
            if (jumpInTime <= DateTime.Now && !isVisible)
            {
                isVisible = true;
                GameProtocol.GetProtocol().SendChangeVisibility(index, Server.GetObjectId(index), isVisible, 1);
            }
            if (requestedJumpSectorId != -1 && requestedJumpSectorId != sectorId && ftlTime <= DateTime.Now)
            {
                GameProtocol.GetProtocol().SendRemoveMe(index, 7);
                sectorId = (uint)requestedJumpSectorId;
            }
        }
    }
}
