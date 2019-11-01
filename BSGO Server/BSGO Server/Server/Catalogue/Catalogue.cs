using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using BSGO_Server._3dAlgorithm;

namespace BSGO_Server
{
    internal static class Catalogue
    {
        private static readonly Dictionary<long, Card> cards = new Dictionary<long, Card>();

        // This method will generate all "static" cards that the server is going to send to the client.
        public static void SetupCards()
        {
            RewardCard colonialBonusReward = new RewardCard(3027, CardView.Reward, 0, AugmentActionType.None, "", 0);
            RewardCard cylonBonusReward = new RewardCard(3127, CardView.Reward, 0, AugmentActionType.None, "", 0);

            AddCard(colonialBonusReward);
            AddCard(cylonBonusReward);

            // These two cards shouldn't be static since they are most likely set according to the database.
            // Since we are just debugging this, there's no need to hook it up even with the fake database yet.
            GUICard colonialBonus = new GUICard(colonialBonusReward.CardGUID, CardView.GUI, "bonus_faction_balance_neutral", 0, "", 0, "", "", "", new string[0]);
            GUICard cylonBonus = new GUICard(cylonBonusReward.CardGUID, CardView.GUI, "bonus_faction_balance_neutral", 0, "", 0, "", "", "", new string[0]);

            AddCard(colonialBonus);
            AddCard(cylonBonus);

            AvatarCatalogueCard avatarCatalogue = new AvatarCatalogueCard(109873795, CardView.AvatarCatalogue);

            AddCard(avatarCatalogue);

            Color ambientColor = Color.FromArgb(255, 100, 100, 100);
            Color fogColor = Color.FromArgb(255, 100, 100, 100);
            Color dustColor = Color.FromArgb(255, 100, 100, 100);
            BackgroundDesc backgroundDesc = new BackgroundDesc("nebula1", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0));
            BackgroundDesc starsDesc = new BackgroundDesc("stars", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0));
            BackgroundDesc starsMult = new BackgroundDesc("starsmultiply_mid", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0));
            BackgroundDesc starsVariance = new BackgroundDesc("starsvariances", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0));
            MovingNebulaDesc[] movingNebulas = new MovingNebulaDesc[0];
            LightDesc[] lightDescs = new LightDesc[0];
            SunDesc[] sunDescs = new SunDesc[0];
            JGlobalFog jGlobalFog = new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0);
            JCameraFx jCameraFx = new JCameraFx(false);

            SectorCard sector4 = new SectorCard(1427, CardView.Sector, 1000, 1000, 1000, 1427, ambientColor, fogColor, 12, dustColor, 12, backgroundDesc, starsDesc, starsMult, starsVariance, movingNebulas, lightDescs, sunDescs, jGlobalFog, jCameraFx, new string[0]);
            GUICard sector4GUI = new GUICard(1427, CardView.GUI, "sector4", 0, "", 0, "", "", "", new string[0]);
            RegulationCard sector4Reg = new RegulationCard(1427, CardView.Regulation, new ConsumableEffectType[0], new Dictionary<uint, HashSet<ShipAbilitySide>>(), new Dictionary<uint, HashSet<ShipAbilityTarget>>(), TargetBracketMode.Default, true);

            //AddCard(sector4);
            //AddCard(sector4GUI);
            //AddCard(sector4Reg);

            Dictionary<ObjectStat, float> mk7Stats = new Dictionary<ObjectStat, float>();
            mk7Stats.Add(ObjectStat.MaxHullPoints, 585);
            mk7Stats.Add(ObjectStat.HullRecovery, 4.5f);
            mk7Stats.Add(ObjectStat.ArmorValue, 5);
            mk7Stats.Add(ObjectStat.CriticalDefense, 80);
            mk7Stats.Add(ObjectStat.Avoidance, 510);
            mk7Stats.Add(ObjectStat.TurnSpeed, 50);
            mk7Stats.Add(ObjectStat.TurnAcceleration, 55);
            mk7Stats.Add(ObjectStat.InertiaCompensation, 100);
            mk7Stats.Add(ObjectStat.Acceleration, 12);
            mk7Stats.Add(ObjectStat.Speed, 55);
            mk7Stats.Add(ObjectStat.BoostSpeed, 85);
            mk7Stats.Add(ObjectStat.BoostCost, 0.75f);
            mk7Stats.Add(ObjectStat.FtlRange, 4.5f);
            mk7Stats.Add(ObjectStat.FtlCharge, 15);
            mk7Stats.Add(ObjectStat.FtlCost, 30);
            mk7Stats.Add(ObjectStat.MaxPowerPoints, 150);
            mk7Stats.Add(ObjectStat.PowerRecovery, 5);
            mk7Stats.Add(ObjectStat.FirewallRating, 100);
            mk7Stats.Add(ObjectStat.DradisRange, 2000);
            mk7Stats.Add(ObjectStat.DetectionVisualRadius, 200);

            mk7Stats.Add(ObjectStat.StrafeAcceleration, 145);
            mk7Stats.Add(ObjectStat.StrafeMaxSpeed, 40);
            mk7Stats.Add(ObjectStat.PitchMaxSpeed, 65);
            mk7Stats.Add(ObjectStat.PitchAcceleration, 120);
            mk7Stats.Add(ObjectStat.YawMaxSpeed, 65);
            mk7Stats.Add(ObjectStat.YawAcceleration, 120);
            mk7Stats.Add(ObjectStat.RollMaxSpeed, 135);
            mk7Stats.Add(ObjectStat.RollAcceleration, 120);

            List<ShipSlotCard> mk7ShipSlots = new List<ShipSlotCard>();
            mk7ShipSlots.Add(new ShipSlotCard(2, 0, ShipSlotType.weapon, "", 0));
            mk7ShipSlots.Add(new ShipSlotCard(2, 1, ShipSlotType.weapon, "", 0));
            mk7ShipSlots.Add(new ShipSlotCard(2, 2, ShipSlotType.weapon, "", 0));
            mk7ShipSlots.Add(new ShipSlotCard(2, 3, ShipSlotType.weapon, "", 0));
            mk7ShipSlots.Add(new ShipSlotCard(2, 4, ShipSlotType.hull, "", 0));
            mk7ShipSlots.Add(new ShipSlotCard(2, 5, ShipSlotType.hull, "", 0));
            mk7ShipSlots.Add(new ShipSlotCard(2, 6, ShipSlotType.hull, "", 0));
            mk7ShipSlots.Add(new ShipSlotCard(2, 7, ShipSlotType.computer, "", 0));
            mk7ShipSlots.Add(new ShipSlotCard(2, 8, ShipSlotType.computer, "", 0));
            mk7ShipSlots.Add(new ShipSlotCard(2, 9, ShipSlotType.computer, "", 0));
            mk7ShipSlots.Add(new ShipSlotCard(2, 10, ShipSlotType.engine, "", 0));
            mk7ShipSlots.Add(new ShipSlotCard(2, 11, ShipSlotType.engine, "", 0));
            mk7ShipSlots.Add(new ShipSlotCard(2, 12, ShipSlotType.engine, "", 0));

            List<ShipImmutableSlot> mk7ShipImmSlots = new List<ShipImmutableSlot>();

            ShipCard mk7ShipCard = new ShipCard(22131177, CardView.Ship, 100, 2, 2, 1, 100, 0, 9000, 1, new ShipRole[1] { ShipRole.Assault }, ShipRoleDeprecated.Fighter, "ship_viper_mk7_paperdoll_layouts", mk7ShipSlots, false, new List<uint>(), -1, new ObjectStats(mk7Stats), Faction.Colonial, mk7ShipImmSlots);
            GUICard mk7GuiCard = new GUICard(22131177, CardView.GUI, "vipermk7", 0, "", 0, "", "gui/infojournal/ships/Human11", "", new string[0]);
            ShopItemCard mk7ShopItemCard = new ShopItemCard(22131177, CardView.Price, ShopCategory.Ship, ShopItemType.Ship, 1, new string[0], 0, new Price(), new Price(), new Price(), Faction.Colonial, false);
            CameraCard mk7CameraCard = new CameraCard(22131177, CardView.Camera, 20, 40, 10, 20, 20);
            WorldCard mk7WorldCard = new WorldCard(22131177, CardView.World, "HumanT1Merit", 0, 0, new SpotDesc[0], "", 0, 0, true, true, true);
            ShipLightCard mk7ShipLightCard = new ShipLightCard(22131177, CardView.ShipLight, 100, 1, new ShipRole[1] { ShipRole.Assault }, ShipRoleDeprecated.Fighter);

            MovementCard movementCard = new MovementCard(22131177, CardView.Movement, 6.5f, 360f, 40f, 2f, 2f, 2f);

            AddCard(movementCard);

            AddCard(mk7ShipCard);
            AddCard(mk7GuiCard);
            AddCard(mk7ShopItemCard);
            AddCard(mk7CameraCard);
            AddCard(mk7WorldCard);
            AddCard(mk7ShipLightCard);

            RewardCard idk2 = new RewardCard(49842157, CardView.Reward, 0, AugmentActionType.LootItem, "", 0);
            GlobalCard idk = new GlobalCard(49842157, CardView.Global, 100, 100, 100, 10, idk2, new Dictionary<int, RewardCard>());
            GUICard idk3 = new GUICard(49842157, CardView.GUI, "", (byte)0, "", 0, "", "", "", new string[0]);

            AddCard(idk2);
            AddCard(idk);
            AddCard(idk3);

            GUICard idk4 = new GUICard(130920111, CardView.GUI, "", (byte)0, "", 0, "", "", "", new string[0]);

            AddCard(idk4);

            // Provided by bubisnew
            Dictionary<uint, MapStarDesc> mapStar = new Dictionary<uint, MapStarDesc>();
            mapStar.Add(0, new MapStarDesc(0, new System.Numerics.Vector2(-302.0f, 233.8f), 0, Faction.Neutral, 0, 0, 163231265, false, false, false, false));
            mapStar.Add(1, new MapStarDesc(1, new System.Numerics.Vector2(-218.1f, 217.0f), 0, Faction.Neutral, 2, 255, 163231266, false, false, false, false));
            mapStar.Add(2, new MapStarDesc(2, new System.Numerics.Vector2(-157.0f, 241.5f), 1, Faction.Neutral, 5, 5, 163231267, false, false, false, false));
            mapStar.Add(3, new MapStarDesc(3, new System.Numerics.Vector2(59.0f, 230.8f), 1, Faction.Neutral, 8, 8, 163231268, false, false, false, false));
            mapStar.Add(4, new MapStarDesc(4, new System.Numerics.Vector2(173.7f, 198.9f), 1, Faction.Neutral, 5, 5, 163231269, false, false, false, false));
            mapStar.Add(5, new MapStarDesc(5, new System.Numerics.Vector2(254.9f, 211.0f), 1, Faction.Neutral, 255, 2, 163231270, false, false, false, false));
            mapStar.Add(6, new MapStarDesc(6, new System.Numerics.Vector2(317.0f, 209.0f), 0, Faction.Neutral, 0, 0, 163231271, false, false, false, false));
            mapStar.Add(7, new MapStarDesc(7, new System.Numerics.Vector2(-354.5f, 160.8f), 0, Faction.Neutral, 3, 255, 163231272, false, false, false, false));
            mapStar.Add(8, new MapStarDesc(8, new System.Numerics.Vector2(-271.1f, 159.9f), 1, Faction.Neutral, 4, 255, 163231273, false, false, false, false));
            mapStar.Add(9, new MapStarDesc(9, new System.Numerics.Vector2(-40.0f, 231.4f), 1, Faction.Neutral, 8, 8, 163231274, false, false, false, false));
            mapStar.Add(10, new MapStarDesc(10, new System.Numerics.Vector2(8.1f, 192.3f), 0, Faction.Neutral, 13, 13, 195781329, false, false, false, false));
            mapStar.Add(11, new MapStarDesc(11, new System.Numerics.Vector2(99.6f, 174.0f), 0, Faction.Neutral, 7, 7, 195781330, false, false, false, false));
            mapStar.Add(12, new MapStarDesc(12, new System.Numerics.Vector2(293.2f, 154.0f), 0, Faction.Neutral, 255, 4, 195781331, false, false, false, false));
            mapStar.Add(13, new MapStarDesc(13, new System.Numerics.Vector2(360.8f, 132.7f), 1, Faction.Neutral, 255, 3, 195781332, false, false, false, false));
            mapStar.Add(14, new MapStarDesc(14, new System.Numerics.Vector2(-104.1f, 210.4f), 1, Faction.Neutral, 6, 6, 195781333, false, false, false, false));
            mapStar.Add(15, new MapStarDesc(15, new System.Numerics.Vector2(-299.0f, 94.8f), 0, Faction.Neutral, 9, 9, 195781334, false, false, false, false));
            mapStar.Add(16, new MapStarDesc(16, new System.Numerics.Vector2(-80.9f, 153.3f), 1, Faction.Neutral, 12, 12, 195781335, false, false, false, false));
            mapStar.Add(17, new MapStarDesc(17, new System.Numerics.Vector2(-126.9f, 103.8f), 1, Faction.Neutral, 15, 15, 195781336, false, false, false, false));
            mapStar.Add(18, new MapStarDesc(18, new System.Numerics.Vector2(134.0f, 124.1f), 1, Faction.Neutral, 12, 12, 195781337, false, false, false, false));
            mapStar.Add(19, new MapStarDesc(19, new System.Numerics.Vector2(265.0f, 84.4f), 1, Faction.Neutral, 9, 9, 195781338, false, false, false, false));
            mapStar.Add(20, new MapStarDesc(20, new System.Numerics.Vector2(218.2f, 150.0f), 1, Faction.Neutral, 6, 6, 1957811313, false, false, false, false));
            mapStar.Add(21, new MapStarDesc(21, new System.Numerics.Vector2(-379.8f, 92.2f), 1, Faction.Neutral, 10, 10, 1957811314, false, false, false, false));
            mapStar.Add(22, new MapStarDesc(22, new System.Numerics.Vector2(-194.1f, 149.5f), 0, Faction.Neutral, 11, 11, 1957811315, false, false, false, false));
            mapStar.Add(23, new MapStarDesc(23, new System.Numerics.Vector2(-222.3f, 69.7f), 0, Faction.Neutral, 16, 16, 1957811316, false, false, false, false));
            mapStar.Add(24, new MapStarDesc(24, new System.Numerics.Vector2(6.6f, 133.8f), 0, Faction.Neutral, 19, 19, 1957811317, false, false, false, false));
            mapStar.Add(25, new MapStarDesc(25, new System.Numerics.Vector2(134.0f, 34.8f), 0, Faction.Neutral, 16, 16, 1957811318, false, false, false, false));
            mapStar.Add(26, new MapStarDesc(26, new System.Numerics.Vector2(197.0f, 64.6f), 0, Faction.Neutral, 13, 13, 1957811319, false, false, false, false));
            mapStar.Add(27, new MapStarDesc(27, new System.Numerics.Vector2(344.8f, 75.7f), 0, Faction.Neutral, 10, 10, 1957811320, false, false, false, false));
            mapStar.Add(28, new MapStarDesc(28, new System.Numerics.Vector2(-298.3f, 28.9f), 0, Faction.Neutral, 14, 14, 1957811321, false, false, false, false));
            mapStar.Add(29, new MapStarDesc(29, new System.Numerics.Vector2(-298.0f, -51.8f), 1, Faction.Neutral, 17, 17, 1957811322, false, false, false, false));
            mapStar.Add(30, new MapStarDesc(30, new System.Numerics.Vector2(-195.0f, 4.2f), 1, Faction.Neutral, 20, 20, 195781361, false, false, false, false));
            mapStar.Add(31, new MapStarDesc(31, new System.Numerics.Vector2(74.0f, 54.6f), 1, Faction.Neutral, 20, 20, 195781362, false, false, false, false));
            mapStar.Add(32, new MapStarDesc(32, new System.Numerics.Vector2(169.3f, -14.0f), 1, Faction.Neutral, 20, 20, 195781363, false, false, false, false));
            mapStar.Add(33, new MapStarDesc(33, new System.Numerics.Vector2(306.4f, -46.8f), 0, Faction.Neutral, 17, 17, 195781364, false, false, false, false));
            mapStar.Add(34, new MapStarDesc(34, new System.Numerics.Vector2(268.2f, 13.9f), 0, Faction.Neutral, 14, 14, 195781365, false, false, false, false));
            mapStar.Add(35, new MapStarDesc(35, new System.Numerics.Vector2(-243.1f, -108.1f), 1, Faction.Neutral, 18, 18, 195781366, false, false, false, false));
            mapStar.Add(36, new MapStarDesc(36, new System.Numerics.Vector2(-200.5f, -54.5f), 0, Faction.Neutral, 20, 20, 195781367, false, false, false, false));
            mapStar.Add(37, new MapStarDesc(37, new System.Numerics.Vector2(-69.5f, 58.8f), 0, Faction.Neutral, 20, 20, 195781368, false, false, false, false));
            mapStar.Add(38, new MapStarDesc(38, new System.Numerics.Vector2(64.9f, -106.4f), 1, Faction.Neutral, 20, 20, 195781369, false, false, false, false));
            mapStar.Add(39, new MapStarDesc(39, new System.Numerics.Vector2(96.6f, -43.9f), 0, Faction.Neutral, 20, 20, 195781370, false, false, false, false));
            mapStar.Add(40, new MapStarDesc(40, new System.Numerics.Vector2(156.7f, -82.9f), 1, Faction.Neutral, 20, 20, 195781345, false, false, false, false));
            mapStar.Add(41, new MapStarDesc(41, new System.Numerics.Vector2(243.4f, -89.1f), 0, Faction.Neutral, 18, 18, 195781346, false, false, false, false));
            mapStar.Add(42, new MapStarDesc(42, new System.Numerics.Vector2(-120.4f, -142.8f), 1, Faction.Neutral, 20, 20, 195781347, false, false, false, false));
            mapStar.Add(43, new MapStarDesc(43, new System.Numerics.Vector2(-113.5f, 6.2f), 1, Faction.Neutral, 20, 20, 195781348, false, false, false, false));
            mapStar.Add(44, new MapStarDesc(44, new System.Numerics.Vector2(-133.2f, -66.6f), 1, Faction.Neutral, 20, 20, 195781349, false, false, false, false));
            mapStar.Add(45, new MapStarDesc(45, new System.Numerics.Vector2(-68.2f, -201.8f), 0, Faction.Neutral, 20, 20, 195781350, false, false, false, false));
            mapStar.Add(46, new MapStarDesc(46, new System.Numerics.Vector2(7.3f, -165.2f), 1, Faction.Neutral, 20, 20, 195781351, false, false, false, false));
            mapStar.Add(47, new MapStarDesc(47, new System.Numerics.Vector2(90.8f, -186.2f), 1, Faction.Neutral, 20, 20, 195781352, false, false, false, false));
            mapStar.Add(48, new MapStarDesc(48, new System.Numerics.Vector2(176.2f, -159.8f), 1, Faction.Neutral, 20, 20, 195781353, false, false, false, false));
            mapStar.Add(49, new MapStarDesc(49, new System.Numerics.Vector2(-364.2f, 4.7f), 0, Faction.Neutral, 0, 0, 195781354, false, false, false, false));
            mapStar.Add(50, new MapStarDesc(50, new System.Numerics.Vector2(342.4f, 5.5f), 0, Faction.Neutral, 0, 0, 195781137, false, false, false, false));
            mapStar.Add(51, new MapStarDesc(51, new System.Numerics.Vector2(-194.0f, -173.0f), 0, Faction.Neutral, 20, 20, 195781138, false, false, false, false));
            mapStar.Add(52, new MapStarDesc(52, new System.Numerics.Vector2(249.0f, -164.0f), 0, Faction.Neutral, 20, 20, 195781139, false, false, false, false));
            mapStar.Add(53, new MapStarDesc(110, new System.Numerics.Vector2(-354.0f, 214.0f), 0, Faction.Neutral, 6, 255, 179711473, false, false, false, false));
            mapStar.Add(54, new MapStarDesc(111, new System.Numerics.Vector2(364.0f, 184.0f), 1, Faction.Neutral, 255, 6, 179711474, false, false, false, false));
            mapStar.Add(55, new MapStarDesc(112, new System.Numerics.Vector2(-46.0f, 109.0f), 0, Faction.Neutral, 115, 115, 179711475, false, false, false, false));
            mapStar.Add(56, new MapStarDesc(113, new System.Numerics.Vector2(6.0f, 37.0f), 0, Faction.Neutral, 120, 120, 179711476, false, false, false, false));
            mapStar.Add(57, new MapStarDesc(114, new System.Numerics.Vector2(60.0f, 106.0f), 0, Faction.Neutral, 115, 115, 179711477, false, false, false, false));

            GalaxyMapCard galaxyMapCard = new GalaxyMapCard(150576033, CardView.GalaxyMap, mapStar, new int[0], 0);

            AddCard(galaxyMapCard);

            StickerListCard stickerListCard = new StickerListCard(166885587, CardView.StickerList);

            AddCard(stickerListCard);

            GUICard ownerGUIDCard = new GUICard((uint)10, CardView.GUI, "", 0, "", 0, "", "GUI/Slots/vipermk7", "", new string[0]);
            OwnerCard ownerCard = new OwnerCard((uint)10, CardView.Owner, false, 0, 1);
            AddCard(ownerGUIDCard);
            AddCard(ownerCard);

            ShipCard mk7ShipCard2 = new ShipCard(22131178, CardView.Ship, 100, 2, 2, 1, 100, 0, 9000, 1, new ShipRole[1] { ShipRole.Assault }, ShipRoleDeprecated.Fighter, "ship_viper_mk7_paperdoll_layouts", new List<ShipSlotCard>(), false, new List<uint>(), -1, new ObjectStats(mk7Stats), Faction.Colonial, new List<ShipImmutableSlot>());
            GUICard mk7GuiCard2 = new GUICard(22131178, CardView.GUI, "vipermk7", 0, "", 0, "", "gui/infojournal/ships/Human11", "", new string[0]);
            ShopItemCard mk7ShopItemCard2 = new ShopItemCard(22131178, CardView.Price, ShopCategory.Ship, ShopItemType.Ship, 1, new string[0], 0, new Price(), new Price(), new Price(), Faction.Colonial, false);
            CameraCard mk7CameraCard2 = new CameraCard(22131178, CardView.Camera, 20, 40, 10, 20, 20);
            WorldCard mk7WorldCard2 = new WorldCard(22131178, CardView.World, "HumanT1Merit", 0, 0, new SpotDesc[0], "", 0, 0, true, true, true);
            ShipLightCard mk7ShipLightCard2 = new ShipLightCard(22131178, CardView.ShipLight, 100, 1, new ShipRole[1] { ShipRole.Assault }, ShipRoleDeprecated.Fighter);

            MovementCard movementCard2 = new MovementCard(22131178, CardView.Movement, 0.1f, 360f, 80f, 2f, 2f, 2f);

            AddCard(movementCard2);

            AddCard(mk7ShipCard2);
            AddCard(mk7GuiCard2);
            AddCard(mk7ShopItemCard2);
            AddCard(mk7CameraCard2);
            AddCard(mk7WorldCard2);
            AddCard(mk7ShipLightCard2);
        }

        // All cards should be requested using this method. It will return either null or the card.
        public static Card FetchCard(uint cardGUID, CardView cardView)
        {
            if (cardGUID == 0)
                return null;

            Log.Add(LogSeverity.WARNING, string.Format("Received a card request: CardGUID={0}, CardView={1}", cardGUID, cardView));

            long key = GenerateKey(cardGUID, cardView);
            if (!cards.TryGetValue(key, out Card value))
            {
                Log.Add(LogSeverity.ERROR, string.Format("The {0}CardView({1}) isn't on the cards dictionary.", cardView, cardGUID));
            }
            return value;
        }

        public static void AddCard(Card card)
        {
            long key = GenerateKey(card.CardGUID, card.CardView);
            cards[key] = card;
        }

        // Method from the game itself.
        private static long GenerateKey(uint cardGUID, CardView cardView)
        {
            long num = (int)cardView;
            num <<= 32;
            return num + cardGUID;
        }
    }
}
