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

            RewardCard idk2 = new RewardCard(49842157, CardView.Reward, 0, AugmentActionType.LootItem, "", 0);
            RewardCard idk4 = new RewardCard(49842158, CardView.Reward, 0, AugmentActionType.LootItem, "", 0);
            RewardCard idk5 = new RewardCard(49842159, CardView.Reward, 0, AugmentActionType.LootItem, "", 0);
            RewardCard idk6 = new RewardCard(49842160, CardView.Reward, 0, AugmentActionType.LootItem, "", 0);
            Dictionary<int, RewardCard> specialFriendBonus = new Dictionary<int, RewardCard>();
            specialFriendBonus.Add(5, idk4);
            specialFriendBonus.Add(10, idk5);
            specialFriendBonus.Add(25, idk6);
            GUICard idk3 = new GUICard(49842157, CardView.GUI, "", (byte)0, "", 0, "", "", "", new string[0]);
            GlobalCard idk = new GlobalCard(49842157, CardView.Global, 100, 100, 100, 10, idk2, specialFriendBonus);

            AddCard(idk);
            AddCard(idk2);
            AddCard(idk3);
            AddCard(idk4);
            AddCard(idk5);
            AddCard(idk6);

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

            ShipConsumableCard ShipConsumableCard_7253_1 = new ShipConsumableCard(264733124, CardView.ShipConsumable, 7253, new ObjectStats(new Dictionary<ObjectStat, float>() { }), new ObjectStats(new Dictionary<ObjectStat, float>() { }), 0, AugmentActionType.None, false, false, false, 1, new ConsumableAttribute[] { new ConsumableAttribute("standard") }, ConsumableEffectType.None);
            AddCard(ShipConsumableCard_7253_1);

            GUICard resource_tylium_GuiCard_0 = new GUICard(215278030, CardView.GUI, "resource_tylium", 0, "GUI/Inventory/items_atlas", 6, "GUI/Common/thylium", "", "", new string[0] { });
            AddCard(resource_tylium_GuiCard_0);
            ShipConsumableCard ShipConsumableCard_7253 = new ShipConsumableCard(215278030, CardView.ShipConsumable, 7253, new ObjectStats(new Dictionary<ObjectStat, float>() { }), new ObjectStats(new Dictionary<ObjectStat, float>() { }), 0, AugmentActionType.None, false, false, false, 1, new ConsumableAttribute[] { new ConsumableAttribute("standard") }, ConsumableEffectType.None);
            AddCard(ShipConsumableCard_7253);
            ShopItemCard PriceCard_Resource_Resource_0 = new ShopItemCard(215278030, CardView.Price, ShopCategory.Resource, ShopItemType.Resource, 0, new string[1] { "standard", }, 10, new Price(new Dictionary<ShipConsumableCard, float>() { { ShipConsumableCard_7253_1, 0.1f }, }), new Price(new Dictionary<ShipConsumableCard, float>() { }), new Price(new Dictionary<ShipConsumableCard, float>() { }), Faction.Neutral, false);
            AddCard(PriceCard_Resource_Resource_0);

            GUICard resource_cubits_GuiCard_0 = new GUICard(264733124, CardView.GUI, "resource_cubits", 0, "GUI/Inventory/items_atlas", 25, "GUI/Common/cubits", "", "", new string[0] { });
            AddCard(resource_cubits_GuiCard_0);
            ShopItemCard PriceCard_Resource_Resource_0_1 = new ShopItemCard(264733124, CardView.Price, ShopCategory.Resource, ShopItemType.Resource, 0, new string[1] { "standard", }, 1, new Price(new Dictionary<ShipConsumableCard, float>() { { ShipConsumableCard_7253, 10f }, }), new Price(new Dictionary<ShipConsumableCard, float>() { }), new Price(new Dictionary<ShipConsumableCard, float>() { }), Faction.Neutral, false);
            AddCard(PriceCard_Resource_Resource_0_1);

            GUICard resource_titanium_GuiCard_0 = new GUICard(207047790, CardView.GUI, "resource_titanium", 0, "GUI/Inventory/items_atlas", 4, "GUI/Common/titanium", "", "", new string[0] { });
            AddCard(resource_titanium_GuiCard_0);
            ShipConsumableCard ShipConsumableCard_7253_2 = new ShipConsumableCard(207047790, CardView.ShipConsumable, 7253, new ObjectStats(new Dictionary<ObjectStat, float>() { }), new ObjectStats(new Dictionary<ObjectStat, float>() { }), 0, AugmentActionType.None, false, false, false, 1, new ConsumableAttribute[] { new ConsumableAttribute("standard") }, ConsumableEffectType.None);
            AddCard(ShipConsumableCard_7253_2);
            ShopItemCard PriceCard_Resource_Resource_0_2 = new ShopItemCard(207047790, CardView.Price, ShopCategory.Resource, ShopItemType.Resource, 0, new string[1] { "standard", }, 30, new Price(new Dictionary<ShipConsumableCard, float>() { { ShipConsumableCard_7253_1, 0.1f }, }), new Price(new Dictionary<ShipConsumableCard, float>() { }), new Price(new Dictionary<ShipConsumableCard, float>() { { ShipConsumableCard_7253, 0.5f }, }), Faction.Neutral, true);
            AddCard(PriceCard_Resource_Resource_0_2);

            GUICard resource_water_GuiCard_0 = new GUICard(130762195, CardView.GUI, "resource_water", 0, "GUI/Inventory/items_atlas", 5, "GUI/Common/water", "", "", new string[0] { });
            AddCard(resource_water_GuiCard_0);
            ShipConsumableCard ShipConsumableCard_7253_3 = new ShipConsumableCard(130762195, CardView.ShipConsumable, 7253, new ObjectStats(new Dictionary<ObjectStat, float>() { }), new ObjectStats(new Dictionary<ObjectStat, float>() { }), 0, AugmentActionType.None, false, false, false, 1, new ConsumableAttribute[] { new ConsumableAttribute("standard") }, ConsumableEffectType.None);
            AddCard(ShipConsumableCard_7253_3);
            ShopItemCard PriceCard_Resource_Resource_0_3 = new ShopItemCard(130762195, CardView.Price, ShopCategory.Resource, ShopItemType.Resource, 0, new string[1] { "standard", }, 40, new Price(new Dictionary<ShipConsumableCard, float>() { { ShipConsumableCard_7253, 10f }, }), new Price(new Dictionary<ShipConsumableCard, float>() { }), new Price(new Dictionary<ShipConsumableCard, float>() { }), Faction.Neutral, false);
            AddCard(PriceCard_Resource_Resource_0_3);

            GUICard resource_token_GuiCard_0 = new GUICard(130920111, CardView.GUI, "resource_token", 0, "GUI/Inventory/items_atlas", 109, "GUI/Common/token", "", "", new string[0] { });
            AddCard(resource_token_GuiCard_0);
            ShipConsumableCard ShipConsumableCard_7253_4 = new ShipConsumableCard(130920111, CardView.ShipConsumable, 7253, new ObjectStats(new Dictionary<ObjectStat, float>() { }), new ObjectStats(new Dictionary<ObjectStat, float>() { }), 0, AugmentActionType.None, false, false, false, 1, new ConsumableAttribute[] { new ConsumableAttribute("standard") }, ConsumableEffectType.None);
            AddCard(ShipConsumableCard_7253_4);
            ShopItemCard PriceCard_Resource_Resource_0_4 = new ShopItemCard(130920111, CardView.Price, ShopCategory.Resource, ShopItemType.Resource, 0, new string[1] { "standard", }, 20, new Price(new Dictionary<ShipConsumableCard, float>() { }), new Price(new Dictionary<ShipConsumableCard, float>() { }), new Price(new Dictionary<ShipConsumableCard, float>() { }), Faction.Neutral, false);
            AddCard(PriceCard_Resource_Resource_0_4);

            ShipCard card28 = new ShipCard(997u, CardView.Ship, 1u, 2, 2, 1, 0, 0, 1000f, 4, new ShipRole[1]{ ShipRole.Destroyer }, ShipRoleDeprecated.Defender, "", new List<ShipSlotCard>(), false, new List<uint>(), -1, new ObjectStats(new Dictionary<ObjectStat, float>()), Faction.Colonial, new List<ShipImmutableSlot>());
            GUICard card29 = new GUICard(997u, CardView.GUI, "outpost_human", 0, "", 0, "", "GUI/Slots/human_outpost", "", new string[0]);
            WorldCard card30 = new WorldCard(997u, CardView.World, "humanoutpost", 0, 0f, new SpotDesc[0], "gui/map/map_objects", 18, 20, true, true, true);
            ShipLightCard card31 = new ShipLightCard(997u, CardView.ShipLight, 100u, 4, new ShipRole[1]{ ShipRole.Destroyer }, ShipRoleDeprecated.Defender);
            OwnerCard card32 = new OwnerCard(997u, CardView.Owner, true, 750f, 1);
            MovementCard card33 = new MovementCard(997u, CardView.Movement, 6.5f, 360f, 40f, 2f, 2f, 2f);
            AddCard(card28);
            AddCard(card29);
            AddCard(card30);
            AddCard(card31);
            AddCard(card32);
            AddCard(card33);

            ShipCard card34 = new ShipCard(998u, CardView.Ship, 1u, 2, 2, 1, 0, 0, 1000f, 4, new ShipRole[1] { ShipRole.Destroyer }, ShipRoleDeprecated.Defender, "", new List<ShipSlotCard>(), false, new List<uint>(), -1, new ObjectStats(new Dictionary<ObjectStat, float>()), Faction.Cylon, new List<ShipImmutableSlot>());
            GUICard card35 = new GUICard(998u, CardView.GUI, "outpost_cylon", 0, "", 0, "", "GUI/Slots/outpost_cylon", "", new string[0]);
            WorldCard card36 = new WorldCard(998u, CardView.World, "cylonoutpost", 0, 0f, new SpotDesc[0], "gui/map/map_objects", 15, 19, true, true, true);
            ShipLightCard card37 = new ShipLightCard(998u, CardView.ShipLight, 100u, 4, new ShipRole[1] { ShipRole.Destroyer }, ShipRoleDeprecated.Defender);
            OwnerCard card38 = new OwnerCard(998u, CardView.Owner, true, 750f, 1);
            MovementCard card39 = new MovementCard(998u, CardView.Movement, 6.5f, 360f, 40f, 2f, 2f, 2f);
            AddCard(card34);
            AddCard(card35);
            AddCard(card36);
            AddCard(card37);
            AddCard(card38);
            AddCard(card39);

            CreateShipCards();
            CreateTitleAndDutyCards();
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

        public static Card FetchCard(int index, uint cardGUID, CardView cardView)
        {
            if (cardGUID == 0)
                return null;

            Log.Add(LogSeverity.WARNING, string.Format("Received a card request: CardGUID={0}, CardView={1}", cardGUID, cardView));

            if (cardView == CardView.World && isSectorGuid(cardGUID))
            {
                Faction faction = Server.GetClientByIndex(index).Character.Faction;
                if (faction == Faction.Colonial)
                    return new WorldCard(cardGUID, CardView.World, "hangar_human", 0, 0, new SpotDesc[0], "", 0, 0, false, false, false);
                else if (faction == Faction.Cylon)
                    return new WorldCard(cardGUID, CardView.World, "hangar_cylon", 0, 0, new SpotDesc[0], "", 0, 0, false, false, false);
            }

            long key = GenerateKey(cardGUID, cardView);
            if (!cards.TryGetValue(key, out Card value))
            {
                Log.Add(LogSeverity.WARNING, string.Format("The {0}CardView({1}) isn't on the static cards dictionary.", cardView, cardGUID));

                if (!Server.GetClientByIndex(index).cards.TryGetValue(key, out Card value2))
                {
                    Log.Add(LogSeverity.WARNING, string.Format("The {0}CardView({1}) isn't on the player cards dictionary.", cardView, cardGUID));
                }
                return value2;
            }
            return value;
        }

        private static bool isSectorGuid(uint cardGUID)
        {
            List<Sector> sectors = Server.Sectors;
            foreach(Sector sector in sectors)
            {
                if (sector.sectorGuid == cardGUID)
                    return true;
            }

            return false;
        }

        public static uint GetShipCardGuidById(ushort shipId, Faction faction)
        {
            foreach(KeyValuePair<long, Card> card in cards)
            {
                if (card.Value.GetType() == typeof(ShipCard))
                {
                    ShipCard currShip = ((ShipCard)card.Value);
                    if (currShip.HangarID == shipId && currShip.Faction == faction)
                    {
                        return ((ShipCard)card.Value).CardGUID;
                    }
                }
            }
            return 0u;
        }

        public static uint GetSectorGuidById(uint sectorId)
        {
            GalaxyMapCard gmc = (GalaxyMapCard)FetchCard(150576033u, CardView.GalaxyMap);

            foreach(KeyValuePair<uint, MapStarDesc> sector in gmc.Stars)
            {
                if (sector.Value.Id == sectorId)
                    return sector.Value.SectorGUID;
            }
            return 0u;
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

        private static void CreateTitleAndDutyCards()
        {
            GUICard title_uranium_asteroid_GuiCard_1 = new GUICard(1350412208, CardView.GUI, "title_uranium_asteroid", 1, "", 0, "", "", "", new string[0] { });
            AddCard(title_uranium_asteroid_GuiCard_1);
            GUICard title_viper_mk7_GuiCard_0 = new GUICard(1694113194, CardView.GUI, "title_viper_mk7", 0, "", 0, "", "", "", new string[0] { });
            AddCard(title_viper_mk7_GuiCard_0);
            GUICard title_brimir_GuiCard_0 = new GUICard(738029365, CardView.GUI, "title_brimir", 0, "", 0, "", "", "", new string[0] { });
            AddCard(title_brimir_GuiCard_0);
            GUICard title_gungnir_GuiCard_1 = new GUICard(951692974, CardView.GUI, "title_gungnir", 1, "", 0, "", "", "", new string[0] { });
            AddCard(title_gungnir_GuiCard_1);
            GUICard title_jotunn_GuiCard_0 = new GUICard(2443893377, CardView.GUI, "title_jotunn", 0, "", 0, "", "", "", new string[0] { });
            AddCard(title_jotunn_GuiCard_0);
            GUICard title_aesir_GuiCard_0 = new GUICard(300579749, CardView.GUI, "title_aesir", 0, "", 0, "", "", "", new string[0] { });
            AddCard(title_aesir_GuiCard_0);
            GUICard title_avenger_GuiCard_0 = new GUICard(1841958757, CardView.GUI, "title_avenger", 0, "", 0, "", "", "", new string[0] { });
            AddCard(title_avenger_GuiCard_0);
            GUICard title_maul_GuiCard_1 = new GUICard(4223258904, CardView.GUI, "title_maul", 1, "", 0, "", "", "", new string[0] { });
            AddCard(title_maul_GuiCard_1);
            GUICard title_berserker_GuiCard_0 = new GUICard(4250542725, CardView.GUI, "title_berserker", 0, "", 0, "", "", "", new string[0] { });
            AddCard(title_berserker_GuiCard_0);
            GUICard title_rhino_GuiCard_1 = new GUICard(735014715, CardView.GUI, "title_rhino", 1, "", 0, "", "", "", new string[0] { });
            AddCard(title_rhino_GuiCard_1);
            GUICard title_raptor_GuiCard_2 = new GUICard(575928935, CardView.GUI, "title_raptor", 2, "", 0, "", "", "", new string[0] { });
            AddCard(title_raptor_GuiCard_2);
            GUICard title_viper_mk3_GuiCard_1 = new GUICard(321839791, CardView.GUI, "title_viper_mk3", 1, "", 0, "", "", "", new string[0] { });
            AddCard(title_viper_mk3_GuiCard_1);
            GUICard title_viper_GuiCard_2 = new GUICard(2551431799, CardView.GUI, "title_viper", 2, "", 0, "", "", "", new string[0] { });
            AddCard(title_viper_GuiCard_2);
            GUICard title_halberd_GuiCard_1 = new GUICard(3303216816, CardView.GUI, "title_halberd", 1, "", 0, "", "", "", new string[0] { });
            AddCard(title_halberd_GuiCard_1);
            GUICard title_explorer_cylon_GuiCard_5 = new GUICard(2819064734, CardView.GUI, "title_explorer_cylon", 5, "", 0, "", "", "", new string[0] { });
            AddCard(title_explorer_cylon_GuiCard_5);
            GUICard title_debris_cylon_GuiCard_2 = new GUICard(3295707779, CardView.GUI, "title_debris_cylon", 2, "", 0, "", "", "", new string[0] { });
            AddCard(title_debris_cylon_GuiCard_2);
            GUICard title_scan_cylon_GuiCard_3 = new GUICard(3861811756, CardView.GUI, "title_scan_cylon", 3, "", 0, "", "", "", new string[0] { });
            AddCard(title_scan_cylon_GuiCard_3);
            GUICard title_titanium_cylon_GuiCard_2 = new GUICard(2397216915, CardView.GUI, "title_titanium_cylon", 2, "", 0, "", "", "", new string[0] { });
            AddCard(title_titanium_cylon_GuiCard_2);
            GUICard title_tylium_cylon_GuiCard_1 = new GUICard(2487413466, CardView.GUI, "title_tylium_cylon", 1, "", 0, "", "", "", new string[0] { });
            AddCard(title_tylium_cylon_GuiCard_1);
            GUICard title_stationary_cylon_GuiCard_2 = new GUICard(480742803, CardView.GUI, "title_stationary_cylon", 2, "", 0, "", "", "", new string[0] { });
            AddCard(title_stationary_cylon_GuiCard_2);
            GUICard title_drone_cylon_GuiCard_3 = new GUICard(535039964, CardView.GUI, "title_drone_cylon", 3, "", 0, "", "", "", new string[0] { });
            AddCard(title_drone_cylon_GuiCard_3);
            GUICard title_pvp_cylon_GuiCard_1 = new GUICard(1327503642, CardView.GUI, "title_pvp_cylon", 1, "", 0, "", "", "", new string[0] { });
            AddCard(title_pvp_cylon_GuiCard_1);
            GUICard title_pve_cylon_GuiCard_3 = new GUICard(3520129340, CardView.GUI, "title_pve_cylon", 3, "", 0, "", "", "", new string[0] { });
            AddCard(title_pve_cylon_GuiCard_3);
            GUICard title_uranium_asteroid_GuiCard_2 = new GUICard(1732136377, CardView.GUI, "title_uranium_asteroid", 2, "", 0, "", "", "", new string[0] { });
            AddCard(title_uranium_asteroid_GuiCard_2);
            GUICard title_viper_mk7_GuiCard_1 = new GUICard(2075837363, CardView.GUI, "title_viper_mk7", 1, "", 0, "", "", "", new string[0] { });
            AddCard(title_viper_mk7_GuiCard_1);
            GUICard title_brimir_GuiCard_1 = new GUICard(1119753534, CardView.GUI, "title_brimir", 1, "", 0, "", "", "", new string[0] { });
            AddCard(title_brimir_GuiCard_1);
            GUICard title_gungnir_GuiCard_2 = new GUICard(1333417143, CardView.GUI, "title_gungnir", 2, "", 0, "", "", "", new string[0] { });
            AddCard(title_gungnir_GuiCard_2);
            GUICard title_jotunn_GuiCard_1 = new GUICard(2825617546, CardView.GUI, "title_jotunn", 1, "", 0, "", "", "", new string[0] { });
            AddCard(title_jotunn_GuiCard_1);
            GUICard title_aesir_GuiCard_1 = new GUICard(682303918, CardView.GUI, "title_aesir", 1, "", 0, "", "", "", new string[0] { });
            AddCard(title_aesir_GuiCard_1);
            GUICard title_avenger_GuiCard_1 = new GUICard(2223682926, CardView.GUI, "title_avenger", 1, "", 0, "", "", "", new string[0] { });
            AddCard(title_avenger_GuiCard_1);
            GUICard title_maul_GuiCard_2 = new GUICard(310015777, CardView.GUI, "title_maul", 2, "", 0, "", "", "", new string[0] { });
            AddCard(title_maul_GuiCard_2);
            GUICard title_berserker_GuiCard_1 = new GUICard(337299598, CardView.GUI, "title_berserker", 1, "", 0, "", "", "", new string[0] { });
            AddCard(title_berserker_GuiCard_1);
            GUICard title_rhino_GuiCard_2 = new GUICard(1116738884, CardView.GUI, "title_rhino", 2, "", 0, "", "", "", new string[0] { });
            AddCard(title_rhino_GuiCard_2);
            GUICard title_raptor_GuiCard_3 = new GUICard(957653104, CardView.GUI, "title_raptor", 3, "", 0, "", "", "", new string[0] { });
            AddCard(title_raptor_GuiCard_3);
            GUICard title_viper_mk3_GuiCard_2 = new GUICard(703563960, CardView.GUI, "title_viper_mk3", 2, "", 0, "", "", "", new string[0] { });
            AddCard(title_viper_mk3_GuiCard_2);
            GUICard title_viper_GuiCard_3 = new GUICard(2933155968, CardView.GUI, "title_viper", 3, "", 0, "", "", "", new string[0] { });
            AddCard(title_viper_GuiCard_3);
            GUICard title_halberd_GuiCard_2 = new GUICard(3684940985, CardView.GUI, "title_halberd", 2, "", 0, "", "", "", new string[0] { });
            AddCard(title_halberd_GuiCard_2);
            GUICard title_debris_cylon_GuiCard_3 = new GUICard(3677431948, CardView.GUI, "title_debris_cylon", 3, "", 0, "", "", "", new string[0] { });
            AddCard(title_debris_cylon_GuiCard_3);
            GUICard title_scan_cylon_GuiCard_4 = new GUICard(4243535925, CardView.GUI, "title_scan_cylon", 4, "", 0, "", "", "", new string[0] { });
            AddCard(title_scan_cylon_GuiCard_4);
            GUICard title_titanium_cylon_GuiCard_3 = new GUICard(2778941084, CardView.GUI, "title_titanium_cylon", 3, "", 0, "", "", "", new string[0] { });
            AddCard(title_titanium_cylon_GuiCard_3);
            GUICard title_tylium_cylon_GuiCard_2 = new GUICard(2869137635, CardView.GUI, "title_tylium_cylon", 2, "", 0, "", "", "", new string[0] { });
            AddCard(title_tylium_cylon_GuiCard_2);
            GUICard title_stationary_cylon_GuiCard_3 = new GUICard(862466972, CardView.GUI, "title_stationary_cylon", 3, "", 0, "", "", "", new string[0] { });
            AddCard(title_stationary_cylon_GuiCard_3);
            GUICard title_drone_cylon_GuiCard_4 = new GUICard(916764133, CardView.GUI, "title_drone_cylon", 4, "", 0, "", "", "", new string[0] { });
            AddCard(title_drone_cylon_GuiCard_4);
            GUICard title_pvp_cylon_GuiCard_2 = new GUICard(1709227811, CardView.GUI, "title_pvp_cylon", 2, "", 0, "", "", "", new string[0] { });
            AddCard(title_pvp_cylon_GuiCard_2);
            GUICard title_pve_cylon_GuiCard_4 = new GUICard(3901853509, CardView.GUI, "title_pve_cylon", 4, "", 0, "", "", "", new string[0] { });
            AddCard(title_pve_cylon_GuiCard_4);
            GUICard title_uranium_asteroid_GuiCard_3 = new GUICard(2113860546, CardView.GUI, "title_uranium_asteroid", 3, "", 0, "", "", "", new string[0] { });
            AddCard(title_uranium_asteroid_GuiCard_3);
            GUICard title_viper_mk7_GuiCard_2 = new GUICard(2457561532, CardView.GUI, "title_viper_mk7", 2, "", 0, "", "", "", new string[0] { });
            AddCard(title_viper_mk7_GuiCard_2);
            GUICard title_brimir_GuiCard_2 = new GUICard(1501477703, CardView.GUI, "title_brimir", 2, "", 0, "", "", "", new string[0] { });
            AddCard(title_brimir_GuiCard_2);
            GUICard title_gungnir_GuiCard_3 = new GUICard(1715141312, CardView.GUI, "title_gungnir", 3, "", 0, "", "", "", new string[0] { });
            AddCard(title_gungnir_GuiCard_3);
            GUICard title_jotunn_GuiCard_2 = new GUICard(3207341715, CardView.GUI, "title_jotunn", 2, "", 0, "", "", "", new string[0] { });
            AddCard(title_jotunn_GuiCard_2);
            GUICard title_aesir_GuiCard_2 = new GUICard(1064028087, CardView.GUI, "title_aesir", 2, "", 0, "", "", "", new string[0] { });
            AddCard(title_aesir_GuiCard_2);
            GUICard title_avenger_GuiCard_2 = new GUICard(2605407095, CardView.GUI, "title_avenger", 2, "", 0, "", "", "", new string[0] { });
            AddCard(title_avenger_GuiCard_2);
            GUICard title_maul_GuiCard_3 = new GUICard(691739946, CardView.GUI, "title_maul", 3, "", 0, "", "", "", new string[0] { });
            AddCard(title_maul_GuiCard_3);
            GUICard title_berserker_GuiCard_2 = new GUICard(719023767, CardView.GUI, "title_berserker", 2, "", 0, "", "", "", new string[0] { });
            AddCard(title_berserker_GuiCard_2);
            GUICard title_rhino_GuiCard_3 = new GUICard(1498463053, CardView.GUI, "title_rhino", 3, "", 0, "", "", "", new string[0] { });
            AddCard(title_rhino_GuiCard_3);
            GUICard title_raptor_GuiCard_4 = new GUICard(1339377273, CardView.GUI, "title_raptor", 4, "", 0, "", "", "", new string[0] { });
            AddCard(title_raptor_GuiCard_4);
            GUICard title_viper_mk3_GuiCard_3 = new GUICard(1085288129, CardView.GUI, "title_viper_mk3", 3, "", 0, "", "", "", new string[0] { });
            AddCard(title_viper_mk3_GuiCard_3);
            GUICard title_viper_GuiCard_4 = new GUICard(3314880137, CardView.GUI, "title_viper", 4, "", 0, "", "", "", new string[0] { });
            AddCard(title_viper_GuiCard_4);
            GUICard title_halberd_GuiCard_3 = new GUICard(4066665154, CardView.GUI, "title_halberd", 3, "", 0, "", "", "", new string[0] { });
            AddCard(title_halberd_GuiCard_3);
            GUICard title_debris_cylon_GuiCard_4 = new GUICard(4059156117, CardView.GUI, "title_debris_cylon", 4, "", 0, "", "", "", new string[0] { });
            AddCard(title_debris_cylon_GuiCard_4);
            GUICard title_scan_cylon_GuiCard_5 = new GUICard(330292798, CardView.GUI, "title_scan_cylon", 5, "", 0, "", "", "", new string[0] { });
            AddCard(title_scan_cylon_GuiCard_5);
            GUICard title_titanium_cylon_GuiCard_4 = new GUICard(3160665253, CardView.GUI, "title_titanium_cylon", 4, "", 0, "", "", "", new string[0] { });
            AddCard(title_titanium_cylon_GuiCard_4);
            GUICard title_tylium_cylon_GuiCard_3 = new GUICard(3250861804, CardView.GUI, "title_tylium_cylon", 3, "", 0, "", "", "", new string[0] { });
            AddCard(title_tylium_cylon_GuiCard_3);
            GUICard title_stationary_cylon_GuiCard_4 = new GUICard(1244191141, CardView.GUI, "title_stationary_cylon", 4, "", 0, "", "", "", new string[0] { });
            AddCard(title_stationary_cylon_GuiCard_4);
            GUICard title_drone_cylon_GuiCard_5 = new GUICard(1298488302, CardView.GUI, "title_drone_cylon", 5, "", 0, "", "", "", new string[0] { });
            AddCard(title_drone_cylon_GuiCard_5);
            GUICard title_pvp_cylon_GuiCard_3 = new GUICard(2090951980, CardView.GUI, "title_pvp_cylon", 3, "", 0, "", "", "", new string[0] { });
            AddCard(title_pvp_cylon_GuiCard_3);
            GUICard title_pve_cylon_GuiCard_5 = new GUICard(4283577678, CardView.GUI, "title_pve_cylon", 5, "", 0, "", "", "", new string[0] { });
            AddCard(title_pve_cylon_GuiCard_5);
            GUICard title_uranium_asteroid_GuiCard_4 = new GUICard(2495584715, CardView.GUI, "title_uranium_asteroid", 4, "", 0, "", "", "", new string[0] { });
            AddCard(title_uranium_asteroid_GuiCard_4);
            GUICard title_viper_mk7_GuiCard_3 = new GUICard(2839285701, CardView.GUI, "title_viper_mk7", 3, "", 0, "", "", "", new string[0] { });
            AddCard(title_viper_mk7_GuiCard_3);
            GUICard title_brimir_GuiCard_3 = new GUICard(1883201872, CardView.GUI, "title_brimir", 3, "", 0, "", "", "", new string[0] { });
            AddCard(title_brimir_GuiCard_3);
            GUICard title_gungnir_GuiCard_4 = new GUICard(2096865481, CardView.GUI, "title_gungnir", 4, "", 0, "", "", "", new string[0] { });
            AddCard(title_gungnir_GuiCard_4);
            GUICard title_jotunn_GuiCard_3 = new GUICard(3589065884, CardView.GUI, "title_jotunn", 3, "", 0, "", "", "", new string[0] { });
            AddCard(title_jotunn_GuiCard_3);
            GUICard title_aesir_GuiCard_3 = new GUICard(1445752256, CardView.GUI, "title_aesir", 3, "", 0, "", "", "", new string[0] { });
            AddCard(title_aesir_GuiCard_3);
            GUICard title_avenger_GuiCard_3 = new GUICard(2987131264, CardView.GUI, "title_avenger", 3, "", 0, "", "", "", new string[0] { });
            AddCard(title_avenger_GuiCard_3);
            GUICard title_maul_GuiCard_4 = new GUICard(1073464115, CardView.GUI, "title_maul", 4, "", 0, "", "", "", new string[0] { });
            AddCard(title_maul_GuiCard_4);
            GUICard title_berserker_GuiCard_3 = new GUICard(1100747936, CardView.GUI, "title_berserker", 3, "", 0, "", "", "", new string[0] { });
            AddCard(title_berserker_GuiCard_3);
            GUICard title_rhino_GuiCard_4 = new GUICard(1880187222, CardView.GUI, "title_rhino", 4, "", 0, "", "", "", new string[0] { });
            AddCard(title_rhino_GuiCard_4);
            GUICard title_raptor_GuiCard_5 = new GUICard(1721101442, CardView.GUI, "title_raptor", 5, "", 0, "", "", "", new string[0] { });
            AddCard(title_raptor_GuiCard_5);
            GUICard title_viper_mk3_GuiCard_4 = new GUICard(1467012298, CardView.GUI, "title_viper_mk3", 4, "", 0, "", "", "", new string[0] { });
            AddCard(title_viper_mk3_GuiCard_4);
            GUICard title_viper_GuiCard_5 = new GUICard(3696604306, CardView.GUI, "title_viper", 5, "", 0, "", "", "", new string[0] { });
            AddCard(title_viper_GuiCard_5);
            GUICard title_halberd_GuiCard_4 = new GUICard(153422027, CardView.GUI, "title_halberd", 4, "", 0, "", "", "", new string[0] { });
            AddCard(title_halberd_GuiCard_4);
            GUICard title_debris_cylon_GuiCard_5 = new GUICard(145912990, CardView.GUI, "title_debris_cylon", 5, "", 0, "", "", "", new string[0] { });
            AddCard(title_debris_cylon_GuiCard_5);
            GUICard title_titanium_cylon_GuiCard_5 = new GUICard(3542389422, CardView.GUI, "title_titanium_cylon", 5, "", 0, "", "", "", new string[0] { });
            AddCard(title_titanium_cylon_GuiCard_5);
            GUICard title_tylium_cylon_GuiCard_4 = new GUICard(3632585973, CardView.GUI, "title_tylium_cylon", 4, "", 0, "", "", "", new string[0] { });
            AddCard(title_tylium_cylon_GuiCard_4);
            GUICard title_stationary_cylon_GuiCard_5 = new GUICard(1625915310, CardView.GUI, "title_stationary_cylon", 5, "", 0, "", "", "", new string[0] { });
            AddCard(title_stationary_cylon_GuiCard_5);
            GUICard title_pvp_cylon_GuiCard_4 = new GUICard(2472676149, CardView.GUI, "title_pvp_cylon", 4, "", 0, "", "", "", new string[0] { });
            AddCard(title_pvp_cylon_GuiCard_4);
            GUICard title_uranium_asteroid_GuiCard_5 = new GUICard(2877308884, CardView.GUI, "title_uranium_asteroid", 5, "", 0, "", "", "", new string[0] { });
            AddCard(title_uranium_asteroid_GuiCard_5);
            GUICard title_viper_mk7_GuiCard_4 = new GUICard(3221009870, CardView.GUI, "title_viper_mk7", 4, "", 0, "", "", "", new string[0] { });
            AddCard(title_viper_mk7_GuiCard_4);
            GUICard title_brimir_GuiCard_4 = new GUICard(2264926041, CardView.GUI, "title_brimir", 4, "", 0, "", "", "", new string[0] { });
            AddCard(title_brimir_GuiCard_4);
            GUICard title_gungnir_GuiCard_5 = new GUICard(2478589650, CardView.GUI, "title_gungnir", 5, "", 0, "", "", "", new string[0] { });
            AddCard(title_gungnir_GuiCard_5);
            GUICard title_jotunn_GuiCard_4 = new GUICard(3970790053, CardView.GUI, "title_jotunn", 4, "", 0, "", "", "", new string[0] { });
            AddCard(title_jotunn_GuiCard_4);
            GUICard title_aesir_GuiCard_4 = new GUICard(1827476425, CardView.GUI, "title_aesir", 4, "", 0, "", "", "", new string[0] { });
            AddCard(title_aesir_GuiCard_4);
            GUICard title_avenger_GuiCard_4 = new GUICard(3368855433, CardView.GUI, "title_avenger", 4, "", 0, "", "", "", new string[0] { });
            AddCard(title_avenger_GuiCard_4);
            GUICard title_maul_GuiCard_5 = new GUICard(1455188284, CardView.GUI, "title_maul", 5, "", 0, "", "", "", new string[0] { });
            AddCard(title_maul_GuiCard_5);
            GUICard title_berserker_GuiCard_4 = new GUICard(1482472105, CardView.GUI, "title_berserker", 4, "", 0, "", "", "", new string[0] { });
            AddCard(title_berserker_GuiCard_4);
            GUICard title_rhino_GuiCard_5 = new GUICard(2261911391, CardView.GUI, "title_rhino", 5, "", 0, "", "", "", new string[0] { });
            AddCard(title_rhino_GuiCard_5);
            GUICard title_viper_mk3_GuiCard_5 = new GUICard(1848736467, CardView.GUI, "title_viper_mk3", 5, "", 0, "", "", "", new string[0] { });
            AddCard(title_viper_mk3_GuiCard_5);
            GUICard title_halberd_GuiCard_5 = new GUICard(535146196, CardView.GUI, "title_halberd", 5, "", 0, "", "", "", new string[0] { });
            AddCard(title_halberd_GuiCard_5);
            GUICard title_tylium_cylon_GuiCard_5 = new GUICard(4014310142, CardView.GUI, "title_tylium_cylon", 5, "", 0, "", "", "", new string[0] { });
            AddCard(title_tylium_cylon_GuiCard_5);
            GUICard title_pvp_cylon_GuiCard_5 = new GUICard(2854400318, CardView.GUI, "title_pvp_cylon", 5, "", 0, "", "", "", new string[0] { });
            AddCard(title_pvp_cylon_GuiCard_5);
            GUICard title_viper_mk7_GuiCard_5 = new GUICard(3602734039, CardView.GUI, "title_viper_mk7", 5, "", 0, "", "", "", new string[0] { });
            AddCard(title_viper_mk7_GuiCard_5);
            GUICard title_brimir_GuiCard_5 = new GUICard(2646650210, CardView.GUI, "title_brimir", 5, "", 0, "", "", "", new string[0] { });
            AddCard(title_brimir_GuiCard_5);
            GUICard title_jotunn_GuiCard_5 = new GUICard(57546926, CardView.GUI, "title_jotunn", 5, "", 0, "", "", "", new string[0] { });
            AddCard(title_jotunn_GuiCard_5);
            GUICard title_aesir_GuiCard_5 = new GUICard(2209200594, CardView.GUI, "title_aesir", 5, "", 0, "", "", "", new string[0] { });
            AddCard(title_aesir_GuiCard_5);
            GUICard title_avenger_GuiCard_5 = new GUICard(3750579602, CardView.GUI, "title_avenger", 5, "", 0, "", "", "", new string[0] { });
            AddCard(title_avenger_GuiCard_5);
            GUICard title_berserker_GuiCard_5 = new GUICard(1864196274, CardView.GUI, "title_berserker", 5, "", 0, "", "", "", new string[0] { });
            AddCard(title_berserker_GuiCard_5);

            GUICard duty_uranium_asteroid_GuiCard_1 = new GUICard(2884276912, CardView.GUI, "duty_uranium_asteroid", 1, "", 0, "", "", "", new string[0] { });
            AddCard(duty_uranium_asteroid_GuiCard_1);
            GUICard duty_viper_mk7_GuiCard_0 = new GUICard(3631064602, CardView.GUI, "duty_viper_mk7", 0, "", 0, "", "", "", new string[0] { });
            AddCard(duty_viper_mk7_GuiCard_0);
            GUICard duty_brimir_GuiCard_0 = new GUICard(411842165, CardView.GUI, "duty_brimir", 0, "", 0, "", "", "", new string[0] { });
            AddCard(duty_brimir_GuiCard_0);
            GUICard duty_gungnir_GuiCard_1 = new GUICard(877373150, CardView.GUI, "duty_gungnir", 1, "", 0, "", "", "", new string[0] { });
            AddCard(duty_gungnir_GuiCard_1);
            GUICard duty_jotunn_GuiCard_0 = new GUICard(323350849, CardView.GUI, "duty_jotunn", 0, "", 0, "", "", "", new string[0] { });
            AddCard(duty_jotunn_GuiCard_0);
            GUICard duty_aesir_GuiCard_0 = new GUICard(642749237, CardView.GUI, "duty_aesir", 0, "", 0, "", "", "", new string[0] { });
            AddCard(duty_aesir_GuiCard_0);
            GUICard duty_avenger_GuiCard_0 = new GUICard(1662453525, CardView.GUI, "duty_avenger", 0, "", 0, "", "", "", new string[0] { });
            AddCard(duty_avenger_GuiCard_0);
            GUICard duty_maul_GuiCard_1 = new GUICard(1873016968, CardView.GUI, "duty_maul", 1, "", 0, "", "", "", new string[0] { });
            AddCard(duty_maul_GuiCard_1);
            GUICard duty_berserker_GuiCard_0 = new GUICard(2281196597, CardView.GUI, "duty_berserker", 0, "", 0, "", "", "", new string[0] { });
            AddCard(duty_berserker_GuiCard_0);
            GUICard duty_rhino_GuiCard_1 = new GUICard(1408844491, CardView.GUI, "duty_rhino", 1, "", 0, "", "", "", new string[0] { });
            AddCard(duty_rhino_GuiCard_1);
            GUICard duty_raptor_GuiCard_2 = new GUICard(2377323303, CardView.GUI, "duty_raptor", 2, "", 0, "", "", "", new string[0] { });
            AddCard(duty_raptor_GuiCard_2);
            GUICard duty_viper_mk3_GuiCard_1 = new GUICard(2258791199, CardView.GUI, "duty_viper_mk3", 1, "", 0, "", "", "", new string[0] { });
            AddCard(duty_viper_mk3_GuiCard_1);
            GUICard duty_viper_GuiCard_2 = new GUICard(1268650759, CardView.GUI, "duty_viper", 2, "", 0, "", "", "", new string[0] { });
            AddCard(duty_viper_GuiCard_2);
            GUICard duty_halberd_GuiCard_1 = new GUICard(59322080, CardView.GUI, "duty_halberd", 1, "", 0, "", "", "", new string[0] { });
            AddCard(duty_halberd_GuiCard_1);
            GUICard duty_explorer_cylon_GuiCard_5 = new GUICard(4282113518, CardView.GUI, "duty_explorer_cylon", 5, "", 0, "", "", "", new string[0] { });
            AddCard(duty_explorer_cylon_GuiCard_5);
            GUICard duty_debris_cylon_GuiCard_2 = new GUICard(1440120643, CardView.GUI, "duty_debris_cylon", 2, "", 0, "", "", "", new string[0] { });
            AddCard(duty_debris_cylon_GuiCard_2);
            GUICard duty_scan_cylon_GuiCard_3 = new GUICard(1291811644, CardView.GUI, "duty_scan_cylon", 3, "", 0, "", "", "", new string[0] { });
            AddCard(duty_scan_cylon_GuiCard_3);
            GUICard duty_titanium_cylon_GuiCard_2 = new GUICard(2896850115, CardView.GUI, "duty_titanium_cylon", 2, "", 0, "", "", "", new string[0] { });
            AddCard(duty_titanium_cylon_GuiCard_2);
            GUICard duty_tylium_cylon_GuiCard_1 = new GUICard(2200949658, CardView.GUI, "duty_tylium_cylon", 1, "", 0, "", "", "", new string[0] { });
            AddCard(duty_tylium_cylon_GuiCard_1);
            GUICard duty_stationary_cylon_GuiCard_2 = new GUICard(3607566419, CardView.GUI, "duty_stationary_cylon", 2, "", 0, "", "", "", new string[0] { });
            AddCard(duty_stationary_cylon_GuiCard_2);
            GUICard duty_drone_cylon_GuiCard_3 = new GUICard(1287173964, CardView.GUI, "duty_drone_cylon", 3, "", 0, "", "", "", new string[0] { });
            AddCard(duty_drone_cylon_GuiCard_3);
            GUICard duty_pvp_cylon_GuiCard_1 = new GUICard(3999754698, CardView.GUI, "duty_pvp_cylon", 1, "", 0, "", "", "", new string[0] { });
            AddCard(duty_pvp_cylon_GuiCard_1);
            GUICard duty_pve_cylon_GuiCard_3 = new GUICard(3065590220, CardView.GUI, "duty_pve_cylon", 3, "", 0, "", "", "", new string[0] { });
            AddCard(duty_pve_cylon_GuiCard_3);

            DutyCard DutyCard_1_1 = new DutyCard(2884276912, CardView.Duty, 1, 5, 3266001081, 93906565, 1, 50, 1350412208);
            AddCard(DutyCard_1_1);
            DutyCard DutyCard_0_2 = new DutyCard(3631064602, CardView.Duty, 0, 5, 4012788771, 178117637, 5, 50, 1694113194);
            AddCard(DutyCard_0_2);
            DutyCard DutyCard_0_3 = new DutyCard(411842165, CardView.Duty, 0, 5, 793566334, 219954581, 2, 50, 738029365);
            AddCard(DutyCard_0_3);
            DutyCard DutyCard_1_4 = new DutyCard(877373150, CardView.Duty, 1, 5, 1259097319, 50025957, 2, 50, 951692974);
            AddCard(DutyCard_1_4);
            DutyCard DutyCard_0_5 = new DutyCard(323350849, CardView.Duty, 0, 5, 705075018, 77889557, 2, 50, 2443893377);
            AddCard(DutyCard_0_5);
            DutyCard DutyCard_0_6 = new DutyCard(642749237, CardView.Duty, 0, 5, 1024473406, 64371525, 2, 50, 300579749);
            AddCard(DutyCard_0_6);
            DutyCard DutyCard_0_7 = new DutyCard(1662453525, CardView.Duty, 0, 5, 2044177694, 173249509, 3, 50, 1841958757);
            AddCard(DutyCard_0_7);
            DutyCard DutyCard_1_8 = new DutyCard(1873016968, CardView.Duty, 1, 5, 2254741137, 53014821, 3, 50, 4223258904);
            AddCard(DutyCard_1_8);
            DutyCard DutyCard_0_9 = new DutyCard(2281196597, CardView.Duty, 0, 5, 2662920766, 182923925, 3, 50, 4250542725);
            AddCard(DutyCard_0_9);
            DutyCard DutyCard_1_10 = new DutyCard(1408844491, CardView.Duty, 1, 5, 1790568660, 250643893, 5, 50, 735014715);
            AddCard(DutyCard_1_10);
            DutyCard DutyCard_2_11 = new DutyCard(2377323303, CardView.Duty, 2, 5, 2759047472, 241523877, 15, 100, 575928935);
            AddCard(DutyCard_2_11);
            DutyCard DutyCard_1_12 = new DutyCard(2258791199, CardView.Duty, 1, 5, 2640515368, 237166309, 5, 50, 321839791);
            AddCard(DutyCard_1_12);
            DutyCard DutyCard_2_13 = new DutyCard(1268650759, CardView.Duty, 2, 5, 1650374928, 152762549, 15, 100, 2551431799);
            AddCard(DutyCard_2_13);
            DutyCard DutyCard_1_14 = new DutyCard(59322080, CardView.Duty, 1, 5, 441046249, 143972261, 3, 50, 3303216816);
            AddCard(DutyCard_1_14);
            DutyCard DutyCard_5_15 = new DutyCard(4282113518, CardView.Duty, 5, 5, 0, 130082917, 45, 1000, 2819064734);
            AddCard(DutyCard_5_15);
            DutyCard DutyCard_2_16 = new DutyCard(1440120643, CardView.Duty, 2, 5, 1821844812, 11402165, 15, 100, 3295707779);
            AddCard(DutyCard_2_16);
            DutyCard DutyCard_3_17 = new DutyCard(1291811644, CardView.Duty, 3, 5, 1673535813, 10577685, 100, 250, 3861811756);
            AddCard(DutyCard_3_17);
            DutyCard DutyCard_2_18 = new DutyCard(2896850115, CardView.Duty, 2, 5, 3278574284, 67875749, 3000, 100, 2397216915);
            AddCard(DutyCard_2_18);
            DutyCard DutyCard_1_19 = new DutyCard(2200949658, CardView.Duty, 1, 5, 2582673827, 111400053, 2500, 50, 2487413466);
            AddCard(DutyCard_1_19);
            DutyCard DutyCard_2_20 = new DutyCard(3607566419, CardView.Duty, 2, 5, 3989290588, 205553269, 10, 100, 480742803);
            AddCard(DutyCard_2_20);
            DutyCard DutyCard_3_21 = new DutyCard(1287173964, CardView.Duty, 3, 5, 1668898133, 260734533, 50, 250, 535039964);
            AddCard(DutyCard_3_21);
            DutyCard DutyCard_1_22 = new DutyCard(3999754698, CardView.Duty, 1, 5, 86511571, 102330325, 5, 50, 1327503642);
            AddCard(DutyCard_1_22);
            DutyCard DutyCard_3_23 = new DutyCard(3065590220, CardView.Duty, 3, 5, 3447314389, 102329861, 100, 250, 3520129340);
            AddCard(DutyCard_3_23);
            CounterCard CounterCard_dynamic_mission_hard_completed_24 = new CounterCard(208428933, CardView.Counter, "dynamic_mission_hard_completed");
            AddCard(CounterCard_dynamic_mission_hard_completed_24);
            CounterCard CounterCard_dynamic_mission_hard_won_25 = new CounterCard(138322223, CardView.Counter, "dynamic_mission_hard_won");
            AddCard(CounterCard_dynamic_mission_hard_won_25);
            CounterCard CounterCard_freighters_killed_26 = new CounterCard(160002101, CardView.Counter, "freighters_killed");
            AddCard(CounterCard_freighters_killed_26);
            CounterCard CounterCard_opposite_faction_killed_27 = new CounterCard(164239893, CardView.Counter, "opposite_faction_killed");
            AddCard(CounterCard_opposite_faction_killed_27);
            CounterCard CounterCard_mining_ships_killed_28 = new CounterCard(142881253, CardView.Counter, "mining_ships_killed");
            AddCard(CounterCard_mining_ships_killed_28);
            CounterCard CounterCard_debris_looted_29 = new CounterCard(11402165, CardView.Counter, "debris_looted");
            AddCard(CounterCard_debris_looted_29);
            CounterCard CounterCard_asteroids_scanned_30 = new CounterCard(10577685, CardView.Counter, "asteroids_scanned");
            AddCard(CounterCard_asteroids_scanned_30);
            CounterCard CounterCard_pvp_killed_31 = new CounterCard(102330325, CardView.Counter, "pvp_killed");
            AddCard(CounterCard_pvp_killed_31);
            CounterCard CounterCard_number_of_deaths_since_payment_popup_32 = new CounterCard(228630769, CardView.Counter, "number_of_deaths_since_payment_popup");
            AddCard(CounterCard_number_of_deaths_since_payment_popup_32);
            CounterCard CounterCard_arena_points_1x1_t1_33 = new CounterCard(224365282, CardView.Counter, "arena_points_1x1_t1");
            AddCard(CounterCard_arena_points_1x1_t1_33);
            CounterCard CounterCard_arena_34 = new CounterCard(6851650, CardView.Counter, "arena");
            AddCard(CounterCard_arena_34);
            CounterCard CounterCard_arena_points_1x1_t2_35 = new CounterCard(224365283, CardView.Counter, "arena_points_1x1_t2");
            AddCard(CounterCard_arena_points_1x1_t2_35);
            CounterCard CounterCard_hack_dradis_stats_differ_36 = new CounterCard(241522355, CardView.Counter, "hack_dradis_stats_differ");
            AddCard(CounterCard_hack_dradis_stats_differ_36);
            CounterCard CounterCard_received_recruit_bonus_37 = new CounterCard(250285108, CardView.Counter, "received_recruit_bonus");
            AddCard(CounterCard_received_recruit_bonus_37);
            CounterCard CounterCard_total_deaths_38 = new CounterCard(35463892, CardView.Counter, "total_deaths");
            AddCard(CounterCard_total_deaths_38);
            CounterCard CounterCard_pvp_deaths_39 = new CounterCard(95221652, CardView.Counter, "pvp_deaths");
            AddCard(CounterCard_pvp_deaths_39);
            CounterCard CounterCard_payment_offers_40 = new CounterCard(224333716, CardView.Counter, "payment_offers");
            AddCard(CounterCard_payment_offers_40);
            CounterCard CounterCard_story_missions_41 = new CounterCard(197138420, CardView.Counter, "story_missions");
            AddCard(CounterCard_story_missions_41);
            CounterCard CounterCard_dynamic_mission_freighter_easy_success_42 = new CounterCard(68409156, CardView.Counter, "dynamic_mission_freighter_easy_success");
            AddCard(CounterCard_dynamic_mission_freighter_easy_success_42);
            CounterCard CounterCard_dynamic_mission_freighter_hard_success_43 = new CounterCard(71561796, CardView.Counter, "dynamic_mission_freighter_hard_success");
            AddCard(CounterCard_dynamic_mission_freighter_hard_success_43);
            CounterCard CounterCard_pve_deaths_44 = new CounterCard(95221316, CardView.Counter, "pve_deaths");
            AddCard(CounterCard_pve_deaths_44);
            CounterCard CounterCard_dynamic_mission_freighter_medium_success_45 = new CounterCard(65917668, CardView.Counter, "dynamic_mission_freighter_medium_success");
            AddCard(CounterCard_dynamic_mission_freighter_medium_success_45);
            CounterCard CounterCard_arena_points_1x1_t3_46 = new CounterCard(224365284, CardView.Counter, "arena_points_1x1_t3");
            AddCard(CounterCard_arena_points_1x1_t3_46);
            CounterCard CounterCard_death_payment_popup_rules_47 = new CounterCard(70669140, CardView.Counter, "death_payment_popup_rules");
            AddCard(CounterCard_death_payment_popup_rules_47);
            CounterCard CounterCard_brimirs_killed_48 = new CounterCard(219954581, CardView.Counter, "brimirs_killed");
            AddCard(CounterCard_brimirs_killed_48);
            CounterCard CounterCard_berserkers_killed_49 = new CounterCard(182923925, CardView.Counter, "berserkers_killed");
            AddCard(CounterCard_berserkers_killed_49);
            CounterCard CounterCard_vipers_killed_50 = new CounterCard(152762549, CardView.Counter, "vipers_killed");
            AddCard(CounterCard_vipers_killed_50);
            CounterCard CounterCard_rhinos_killed_51 = new CounterCard(250643893, CardView.Counter, "rhinos_killed");
            AddCard(CounterCard_rhinos_killed_51);
            CounterCard CounterCard_tylium_mined_52 = new CounterCard(111400053, CardView.Counter, "tylium_mined");
            AddCard(CounterCard_tylium_mined_52);
            CounterCard CounterCard_asteroids_mined_53 = new CounterCard(61526949, CardView.Counter, "asteroids_mined");
            AddCard(CounterCard_asteroids_mined_53);
            CounterCard CounterCard_titanium_mined_54 = new CounterCard(67875749, CardView.Counter, "titanium_mined");
            AddCard(CounterCard_titanium_mined_54);
            CounterCard CounterCard_stationaries_killed_55 = new CounterCard(205553269, CardView.Counter, "stationaries_killed");
            AddCard(CounterCard_stationaries_killed_55);
            CounterCard CounterCard_damage_dealt_56 = new CounterCard(233996773, CardView.Counter, "damage_dealt");
            AddCard(CounterCard_damage_dealt_56);
            CounterCard CounterCard_drones_killed_57 = new CounterCard(260734533, CardView.Counter, "drones_killed");
            AddCard(CounterCard_drones_killed_57);
            CounterCard CounterCard_enemies_killed_58 = new CounterCard(48097509, CardView.Counter, "enemies_killed");
            AddCard(CounterCard_enemies_killed_58);
            CounterCard CounterCard_pve_killed_59 = new CounterCard(102329861, CardView.Counter, "pve_killed");
            AddCard(CounterCard_pve_killed_59);
            CounterCard CounterCard_ancients_killed_60 = new CounterCard(233054565, CardView.Counter, "ancients_killed");
            AddCard(CounterCard_ancients_killed_60);
            CounterCard CounterCard_tylium_burned_61 = new CounterCard(161454293, CardView.Counter, "tylium_burned");
            AddCard(CounterCard_tylium_burned_61);
            CounterCard CounterCard_wof_played_62 = new CounterCard(108358261, CardView.Counter, "wof_played");
            AddCard(CounterCard_wof_played_62);
            CounterCard CounterCard_sectors_visited_63 = new CounterCard(130082917, CardView.Counter, "sectors_visited");
            AddCard(CounterCard_sectors_visited_63);
            CounterCard CounterCard_missions_completed_64 = new CounterCard(126566101, CardView.Counter, "missions_completed");
            AddCard(CounterCard_missions_completed_64);
            CounterCard CounterCard_time_played_65 = new CounterCard(103386117, CardView.Counter, "time_played");
            AddCard(CounterCard_time_played_65);
            CounterCard CounterCard_story_missions_unsubmitted_66 = new CounterCard(229439477, CardView.Counter, "story_missions_unsubmitted");
            AddCard(CounterCard_story_missions_unsubmitted_66);
            CounterCard CounterCard_viper_mk7_killed_67 = new CounterCard(178117637, CardView.Counter, "viper_mk7_killed");
            AddCard(CounterCard_viper_mk7_killed_67);
            CounterCard CounterCard_viper_mk3s_killed_68 = new CounterCard(237166309, CardView.Counter, "viper_mk3s_killed");
            AddCard(CounterCard_viper_mk3s_killed_68);
            CounterCard CounterCard_raptors_killed_69 = new CounterCard(241523877, CardView.Counter, "raptors_killed");
            AddCard(CounterCard_raptors_killed_69);
            CounterCard CounterCard_dynamic_mission_easy_completed_70 = new CounterCard(210329397, CardView.Counter, "dynamic_mission_easy_completed");
            AddCard(CounterCard_dynamic_mission_easy_completed_70);
            CounterCard CounterCard_mauls_killed_71 = new CounterCard(53014821, CardView.Counter, "mauls_killed");
            AddCard(CounterCard_mauls_killed_71);
            CounterCard CounterCard_dynamic_mission_medium_completed_72 = new CounterCard(16726325, CardView.Counter, "dynamic_mission_medium_completed");
            AddCard(CounterCard_dynamic_mission_medium_completed_72);
            CounterCard CounterCard_halberds_killed_73 = new CounterCard(143972261, CardView.Counter, "halberds_killed");
            AddCard(CounterCard_halberds_killed_73);
            CounterCard CounterCard_jotunns_killed_74 = new CounterCard(77889557, CardView.Counter, "jotunns_killed");
            AddCard(CounterCard_jotunns_killed_74);
            CounterCard CounterCard_cubits_bought_75 = new CounterCard(256882181, CardView.Counter, "cubits_bought");
            AddCard(CounterCard_cubits_bought_75);
            CounterCard CounterCard_uranium_asteroids_killed_76 = new CounterCard(93906565, CardView.Counter, "uranium_asteroids_killed");
            AddCard(CounterCard_uranium_asteroids_killed_76);
            CounterCard CounterCard_avengers_killed_77 = new CounterCard(173249509, CardView.Counter, "avengers_killed");
            AddCard(CounterCard_avengers_killed_77);
            CounterCard CounterCard_gungnirs_killed_78 = new CounterCard(50025957, CardView.Counter, "gungnirs_killed");
            AddCard(CounterCard_gungnirs_killed_78);
            CounterCard CounterCard_aesirs_killed_79 = new CounterCard(64371525, CardView.Counter, "aesirs_killed");
            AddCard(CounterCard_aesirs_killed_79);
            CounterCard CounterCard_damage_done_80 = new CounterCard(232732470, CardView.Counter, "damage_done");
            AddCard(CounterCard_damage_done_80);
            CounterCard CounterCard_select_hax_81 = new CounterCard(196068009, CardView.Counter, "select_hax");
            AddCard(CounterCard_select_hax_81);
            CounterCard CounterCard_tokens_gained_daily_82 = new CounterCard(149700234, CardView.Counter, "tokens_gained_daily");
            AddCard(CounterCard_tokens_gained_daily_82);
            CounterCard CounterCard_last_user_interaction_spam_check_83 = new CounterCard(221726988, CardView.Counter, "last_user_interaction_spam_check");
            AddCard(CounterCard_last_user_interaction_spam_check_83);
            CounterCard CounterCard_patrol_84 = new CounterCard(124303709, CardView.Counter, "patrol");
            AddCard(CounterCard_patrol_84);
            CounterCard CounterCard_daily_login_85 = new CounterCard(256499151, CardView.Counter, "daily_login");
            AddCard(CounterCard_daily_login_85);
            CounterCard CounterCard_dynamic_mission_easy_won_86 = new CounterCard(136028543, CardView.Counter, "dynamic_mission_easy_won");
            AddCard(CounterCard_dynamic_mission_easy_won_86);
            CounterCard CounterCard_dynamic_mission_medium_won_87 = new CounterCard(76400511, CardView.Counter, "dynamic_mission_medium_won");
            AddCard(CounterCard_dynamic_mission_medium_won_87);
            TitleCard TitleCard_1_88 = new TitleCard(1350412208, CardView.Title, 1, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_1_88);
            DutyCard DutyCard_2_89 = new DutyCard(3266001081, CardView.Duty, 2, 5, 3647725250, 93906565, 10, 100, 1732136377);
            AddCard(DutyCard_2_89);
            TitleCard TitleCard_0_90 = new TitleCard(1694113194, CardView.Title, 0, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_0_90);
            DutyCard DutyCard_1_91 = new DutyCard(4012788771, CardView.Duty, 1, 5, 99545644, 178117637, 5, 50, 2075837363);
            AddCard(DutyCard_1_91);
            TitleCard TitleCard_0_92 = new TitleCard(738029365, CardView.Title, 0, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_0_92);
            DutyCard DutyCard_1_93 = new DutyCard(793566334, CardView.Duty, 1, 5, 1175290503, 219954581, 2, 50, 1119753534);
            AddCard(DutyCard_1_93);
            TitleCard TitleCard_1_94 = new TitleCard(951692974, CardView.Title, 1, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_1_94);
            DutyCard DutyCard_2_95 = new DutyCard(1259097319, CardView.Duty, 2, 5, 1640821488, 50025957, 5, 100, 1333417143);
            AddCard(DutyCard_2_95);
            TitleCard TitleCard_0_96 = new TitleCard(2443893377, CardView.Title, 0, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_0_96);
            DutyCard DutyCard_1_97 = new DutyCard(705075018, CardView.Duty, 1, 5, 1086799187, 77889557, 2, 50, 2825617546);
            AddCard(DutyCard_1_97);
            TitleCard TitleCard_0_98 = new TitleCard(300579749, CardView.Title, 0, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_0_98);
            DutyCard DutyCard_1_99 = new DutyCard(1024473406, CardView.Duty, 1, 5, 1406197575, 64371525, 2, 50, 682303918);
            AddCard(DutyCard_1_99);
            TitleCard TitleCard_0_100 = new TitleCard(1841958757, CardView.Title, 0, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_0_100);
            DutyCard DutyCard_1_101 = new DutyCard(2044177694, CardView.Duty, 1, 5, 2425901863, 173249509, 3, 50, 2223682926);
            AddCard(DutyCard_1_101);
            TitleCard TitleCard_1_102 = new TitleCard(4223258904, CardView.Title, 1, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_1_102);
            DutyCard DutyCard_2_103 = new DutyCard(2254741137, CardView.Duty, 2, 5, 2636465306, 53014821, 10, 100, 310015777);
            AddCard(DutyCard_2_103);
            TitleCard TitleCard_0_104 = new TitleCard(4250542725, CardView.Title, 0, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_0_104);
            DutyCard DutyCard_1_105 = new DutyCard(2662920766, CardView.Duty, 1, 5, 3044644935, 182923925, 3, 50, 337299598);
            AddCard(DutyCard_1_105);
            TitleCard TitleCard_1_106 = new TitleCard(735014715, CardView.Title, 1, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_1_106);
            DutyCard DutyCard_2_107 = new DutyCard(1790568660, CardView.Duty, 2, 5, 2172292829, 250643893, 15, 100, 1116738884);
            AddCard(DutyCard_2_107);
            TitleCard TitleCard_2_108 = new TitleCard(575928935, CardView.Title, 2, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_2_108);
            DutyCard DutyCard_3_109 = new DutyCard(2759047472, CardView.Duty, 3, 5, 3140771641, 241523877, 50, 250, 957653104);
            AddCard(DutyCard_3_109);
            TitleCard TitleCard_1_110 = new TitleCard(321839791, CardView.Title, 1, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_1_110);
            DutyCard DutyCard_2_111 = new DutyCard(2640515368, CardView.Duty, 2, 5, 3022239537, 237166309, 15, 100, 703563960);
            AddCard(DutyCard_2_111);
            TitleCard TitleCard_2_112 = new TitleCard(2551431799, CardView.Title, 2, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_2_112);
            DutyCard DutyCard_3_113 = new DutyCard(1650374928, CardView.Duty, 3, 5, 2032099097, 152762549, 50, 250, 2933155968);
            AddCard(DutyCard_3_113);
            TitleCard TitleCard_1_114 = new TitleCard(3303216816, CardView.Title, 1, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_1_114);
            DutyCard DutyCard_2_115 = new DutyCard(441046249, CardView.Duty, 2, 5, 822770418, 143972261, 10, 100, 3684940985);
            AddCard(DutyCard_2_115);
            TitleCard TitleCard_5_116 = new TitleCard(2819064734, CardView.Title, 5, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_5_116);
            TitleCard TitleCard_2_117 = new TitleCard(3295707779, CardView.Title, 2, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_2_117);
            DutyCard DutyCard_3_118 = new DutyCard(1821844812, CardView.Duty, 3, 5, 2203568981, 11402165, 50, 250, 3677431948);
            AddCard(DutyCard_3_118);
            TitleCard TitleCard_3_119 = new TitleCard(3861811756, CardView.Title, 3, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_3_119);
            DutyCard DutyCard_4_120 = new DutyCard(1673535813, CardView.Duty, 4, 5, 2055259982, 10577685, 300, 500, 4243535925);
            AddCard(DutyCard_4_120);
            TitleCard TitleCard_2_121 = new TitleCard(2397216915, CardView.Title, 2, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_2_121);
            DutyCard DutyCard_3_122 = new DutyCard(3278574284, CardView.Duty, 3, 5, 3660298453, 67875749, 10000, 250, 2778941084);
            AddCard(DutyCard_3_122);
            TitleCard TitleCard_1_123 = new TitleCard(2487413466, CardView.Title, 1, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_1_123);
            DutyCard DutyCard_2_124 = new DutyCard(2582673827, CardView.Duty, 2, 5, 2964397996, 111400053, 7500, 100, 2869137635);
            AddCard(DutyCard_2_124);
            TitleCard TitleCard_2_125 = new TitleCard(480742803, CardView.Title, 2, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_2_125);
            DutyCard DutyCard_3_126 = new DutyCard(3989290588, CardView.Duty, 3, 5, 76047461, 205553269, 30, 250, 862466972);
            AddCard(DutyCard_3_126);
            TitleCard TitleCard_3_127 = new TitleCard(535039964, CardView.Title, 3, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_3_127);
            DutyCard DutyCard_4_128 = new DutyCard(1668898133, CardView.Duty, 4, 5, 2050622302, 260734533, 150, 500, 916764133);
            AddCard(DutyCard_4_128);
            TitleCard TitleCard_1_129 = new TitleCard(1327503642, CardView.Title, 1, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_1_129);
            DutyCard DutyCard_2_130 = new DutyCard(86511571, CardView.Duty, 2, 5, 468235740, 102330325, 15, 100, 1709227811);
            AddCard(DutyCard_2_130);
            TitleCard TitleCard_3_131 = new TitleCard(3520129340, CardView.Title, 3, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_3_131);
            DutyCard DutyCard_4_132 = new DutyCard(3447314389, CardView.Duty, 4, 5, 3829038558, 102329861, 300, 500, 3901853509);
            AddCard(DutyCard_4_132);
            TitleCard TitleCard_2_133 = new TitleCard(1732136377, CardView.Title, 2, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_2_133);
            DutyCard DutyCard_3_134 = new DutyCard(3647725250, CardView.Duty, 3, 5, 4029449419, 93906565, 50, 250, 2113860546);
            AddCard(DutyCard_3_134);
            TitleCard TitleCard_1_135 = new TitleCard(2075837363, CardView.Title, 1, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_1_135);
            DutyCard DutyCard_2_136 = new DutyCard(99545644, CardView.Duty, 2, 5, 481269813, 178117637, 15, 100, 2457561532);
            AddCard(DutyCard_2_136);
            TitleCard TitleCard_1_137 = new TitleCard(1119753534, CardView.Title, 1, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_1_137);
            DutyCard DutyCard_2_138 = new DutyCard(1175290503, CardView.Duty, 2, 5, 1557014672, 219954581, 5, 100, 1501477703);
            AddCard(DutyCard_2_138);
            TitleCard TitleCard_2_139 = new TitleCard(1333417143, CardView.Title, 2, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_2_139);
            DutyCard DutyCard_3_140 = new DutyCard(1640821488, CardView.Duty, 3, 5, 2022545657, 50025957, 15, 250, 1715141312);
            AddCard(DutyCard_3_140);
            TitleCard TitleCard_1_141 = new TitleCard(2825617546, CardView.Title, 1, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_1_141);
            DutyCard DutyCard_2_142 = new DutyCard(1086799187, CardView.Duty, 2, 5, 1468523356, 77889557, 5, 100, 3207341715);
            AddCard(DutyCard_2_142);
            TitleCard TitleCard_1_143 = new TitleCard(682303918, CardView.Title, 1, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_1_143);
            DutyCard DutyCard_2_144 = new DutyCard(1406197575, CardView.Duty, 2, 5, 1787921744, 64371525, 5, 100, 1064028087);
            AddCard(DutyCard_2_144);
            TitleCard TitleCard_1_145 = new TitleCard(2223682926, CardView.Title, 1, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_1_145);
            DutyCard DutyCard_2_146 = new DutyCard(2425901863, CardView.Duty, 2, 5, 2807626032, 173249509, 10, 100, 2605407095);
            AddCard(DutyCard_2_146);
            TitleCard TitleCard_2_147 = new TitleCard(310015777, CardView.Title, 2, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_2_147);
            DutyCard DutyCard_3_148 = new DutyCard(2636465306, CardView.Duty, 3, 5, 3018189475, 53014821, 30, 250, 691739946);
            AddCard(DutyCard_3_148);
            TitleCard TitleCard_1_149 = new TitleCard(337299598, CardView.Title, 1, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_1_149);
            DutyCard DutyCard_2_150 = new DutyCard(3044644935, CardView.Duty, 2, 5, 3426369104, 182923925, 10, 100, 719023767);
            AddCard(DutyCard_2_150);
            TitleCard TitleCard_2_151 = new TitleCard(1116738884, CardView.Title, 2, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_2_151);
            DutyCard DutyCard_3_152 = new DutyCard(2172292829, CardView.Duty, 3, 5, 2554016998, 250643893, 50, 250, 1498463053);
            AddCard(DutyCard_3_152);
            TitleCard TitleCard_3_153 = new TitleCard(957653104, CardView.Title, 3, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_3_153);
            DutyCard DutyCard_4_154 = new DutyCard(3140771641, CardView.Duty, 4, 5, 3522495810, 241523877, 150, 500, 1339377273);
            AddCard(DutyCard_4_154);
            TitleCard TitleCard_2_155 = new TitleCard(703563960, CardView.Title, 2, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_2_155);
            DutyCard DutyCard_3_156 = new DutyCard(3022239537, CardView.Duty, 3, 5, 3403963706, 237166309, 50, 250, 1085288129);
            AddCard(DutyCard_3_156);
            TitleCard TitleCard_3_157 = new TitleCard(2933155968, CardView.Title, 3, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_3_157);
            DutyCard DutyCard_4_158 = new DutyCard(2032099097, CardView.Duty, 4, 5, 2413823266, 152762549, 150, 500, 3314880137);
            AddCard(DutyCard_4_158);
            TitleCard TitleCard_2_159 = new TitleCard(3684940985, CardView.Title, 2, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_2_159);
            DutyCard DutyCard_3_160 = new DutyCard(822770418, CardView.Duty, 3, 5, 1204494587, 143972261, 30, 250, 4066665154);
            AddCard(DutyCard_3_160);
            TitleCard TitleCard_3_161 = new TitleCard(3677431948, CardView.Title, 3, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_3_161);
            DutyCard DutyCard_4_162 = new DutyCard(2203568981, CardView.Duty, 4, 5, 2585293150, 11402165, 150, 500, 4059156117);
            AddCard(DutyCard_4_162);
            TitleCard TitleCard_4_163 = new TitleCard(4243535925, CardView.Title, 4, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_4_163);
            DutyCard DutyCard_5_164 = new DutyCard(2055259982, CardView.Duty, 5, 5, 0, 10577685, 1000, 1000, 330292798);
            AddCard(DutyCard_5_164);
            TitleCard TitleCard_3_165 = new TitleCard(2778941084, CardView.Title, 3, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_3_165);
            DutyCard DutyCard_4_166 = new DutyCard(3660298453, CardView.Duty, 4, 5, 4042022622, 67875749, 30000, 500, 3160665253);
            AddCard(DutyCard_4_166);
            TitleCard TitleCard_2_167 = new TitleCard(2869137635, CardView.Title, 2, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_2_167);
            DutyCard DutyCard_3_168 = new DutyCard(2964397996, CardView.Duty, 3, 5, 3346122165, 111400053, 25000, 250, 3250861804);
            AddCard(DutyCard_3_168);
            TitleCard TitleCard_3_169 = new TitleCard(862466972, CardView.Title, 3, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_3_169);
            DutyCard DutyCard_4_170 = new DutyCard(76047461, CardView.Duty, 4, 5, 457771630, 205553269, 100, 500, 1244191141);
            AddCard(DutyCard_4_170);
            TitleCard TitleCard_4_171 = new TitleCard(916764133, CardView.Title, 4, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_4_171);
            DutyCard DutyCard_5_172 = new DutyCard(2050622302, CardView.Duty, 5, 5, 0, 260734533, 500, 1000, 1298488302);
            AddCard(DutyCard_5_172);
            TitleCard TitleCard_2_173 = new TitleCard(1709227811, CardView.Title, 2, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_2_173);
            DutyCard DutyCard_3_174 = new DutyCard(468235740, CardView.Duty, 3, 5, 849959909, 102330325, 50, 250, 2090951980);
            AddCard(DutyCard_3_174);
            TitleCard TitleCard_4_175 = new TitleCard(3901853509, CardView.Title, 4, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_4_175);
            DutyCard DutyCard_5_176 = new DutyCard(3829038558, CardView.Duty, 5, 5, 0, 102329861, 1000, 1000, 4283577678);
            AddCard(DutyCard_5_176);
            TitleCard TitleCard_3_177 = new TitleCard(2113860546, CardView.Title, 3, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_3_177);
            DutyCard DutyCard_4_178 = new DutyCard(4029449419, CardView.Duty, 4, 5, 116206292, 93906565, 100, 500, 2495584715);
            AddCard(DutyCard_4_178);
            TitleCard TitleCard_2_179 = new TitleCard(2457561532, CardView.Title, 2, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_2_179);
            DutyCard DutyCard_3_180 = new DutyCard(481269813, CardView.Duty, 3, 5, 862993982, 178117637, 50, 250, 2839285701);
            AddCard(DutyCard_3_180);
            TitleCard TitleCard_2_181 = new TitleCard(1501477703, CardView.Title, 2, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_2_181);
            DutyCard DutyCard_3_182 = new DutyCard(1557014672, CardView.Duty, 3, 5, 1938738841, 219954581, 15, 250, 1883201872);
            AddCard(DutyCard_3_182);
            TitleCard TitleCard_3_183 = new TitleCard(1715141312, CardView.Title, 3, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_3_183);
            DutyCard DutyCard_4_184 = new DutyCard(2022545657, CardView.Duty, 4, 5, 2404269826, 50025957, 50, 500, 2096865481);
            AddCard(DutyCard_4_184);
            TitleCard TitleCard_2_185 = new TitleCard(3207341715, CardView.Title, 2, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_2_185);
            DutyCard DutyCard_3_186 = new DutyCard(1468523356, CardView.Duty, 3, 5, 1850247525, 77889557, 15, 250, 3589065884);
            AddCard(DutyCard_3_186);
            TitleCard TitleCard_2_187 = new TitleCard(1064028087, CardView.Title, 2, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_2_187);
            DutyCard DutyCard_3_188 = new DutyCard(1787921744, CardView.Duty, 3, 5, 2169645913, 64371525, 15, 250, 1445752256);
            AddCard(DutyCard_3_188);
            TitleCard TitleCard_2_189 = new TitleCard(2605407095, CardView.Title, 2, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_2_189);
            DutyCard DutyCard_3_190 = new DutyCard(2807626032, CardView.Duty, 3, 5, 3189350201, 173249509, 30, 250, 2987131264);
            AddCard(DutyCard_3_190);
            TitleCard TitleCard_3_191 = new TitleCard(691739946, CardView.Title, 3, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_3_191);
            DutyCard DutyCard_4_192 = new DutyCard(3018189475, CardView.Duty, 4, 5, 3399913644, 53014821, 100, 500, 1073464115);
            AddCard(DutyCard_4_192);
            TitleCard TitleCard_2_193 = new TitleCard(719023767, CardView.Title, 2, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_2_193);
            DutyCard DutyCard_3_194 = new DutyCard(3426369104, CardView.Duty, 3, 5, 3808093273, 182923925, 30, 250, 1100747936);
            AddCard(DutyCard_3_194);
            TitleCard TitleCard_3_195 = new TitleCard(1498463053, CardView.Title, 3, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_3_195);
            DutyCard DutyCard_4_196 = new DutyCard(2554016998, CardView.Duty, 4, 5, 2935741167, 250643893, 150, 500, 1880187222);
            AddCard(DutyCard_4_196);
            TitleCard TitleCard_4_197 = new TitleCard(1339377273, CardView.Title, 4, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_4_197);
            DutyCard DutyCard_5_198 = new DutyCard(3522495810, CardView.Duty, 5, 5, 0, 241523877, 500, 1000, 1721101442);
            AddCard(DutyCard_5_198);
            TitleCard TitleCard_3_199 = new TitleCard(1085288129, CardView.Title, 3, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_3_199);
            DutyCard DutyCard_4_200 = new DutyCard(3403963706, CardView.Duty, 4, 5, 3785687875, 237166309, 150, 500, 1467012298);
            AddCard(DutyCard_4_200);
            TitleCard TitleCard_4_201 = new TitleCard(3314880137, CardView.Title, 4, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_4_201);
            DutyCard DutyCard_5_202 = new DutyCard(2413823266, CardView.Duty, 5, 5, 0, 152762549, 500, 1000, 3696604306);
            AddCard(DutyCard_5_202);
            TitleCard TitleCard_3_203 = new TitleCard(4066665154, CardView.Title, 3, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_3_203);
            DutyCard DutyCard_4_204 = new DutyCard(1204494587, CardView.Duty, 4, 5, 1586218756, 143972261, 100, 500, 153422027);
            AddCard(DutyCard_4_204);
            TitleCard TitleCard_4_205 = new TitleCard(4059156117, CardView.Title, 4, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_4_205);
            DutyCard DutyCard_5_206 = new DutyCard(2585293150, CardView.Duty, 5, 5, 0, 11402165, 500, 1000, 145912990);
            AddCard(DutyCard_5_206);
            TitleCard TitleCard_5_207 = new TitleCard(330292798, CardView.Title, 5, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_5_207);
            TitleCard TitleCard_4_208 = new TitleCard(3160665253, CardView.Title, 4, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_4_208);
            DutyCard DutyCard_5_209 = new DutyCard(4042022622, CardView.Duty, 5, 5, 0, 67875749, 100000, 1000, 3542389422);
            AddCard(DutyCard_5_209);
            TitleCard TitleCard_3_210 = new TitleCard(3250861804, CardView.Title, 3, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_3_210);
            DutyCard DutyCard_4_211 = new DutyCard(3346122165, CardView.Duty, 4, 5, 3727846334, 111400053, 75000, 500, 3632585973);
            AddCard(DutyCard_4_211);
            TitleCard TitleCard_4_212 = new TitleCard(1244191141, CardView.Title, 4, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_4_212);
            DutyCard DutyCard_5_213 = new DutyCard(457771630, CardView.Duty, 5, 5, 0, 205553269, 250, 1000, 1625915310);
            AddCard(DutyCard_5_213);
            TitleCard TitleCard_5_214 = new TitleCard(1298488302, CardView.Title, 5, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_5_214);
            TitleCard TitleCard_3_215 = new TitleCard(2090951980, CardView.Title, 3, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_3_215);
            DutyCard DutyCard_4_216 = new DutyCard(849959909, CardView.Duty, 4, 5, 1231684078, 102330325, 150, 500, 2472676149);
            AddCard(DutyCard_4_216);
            TitleCard TitleCard_5_217 = new TitleCard(4283577678, CardView.Title, 5, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_5_217);
            TitleCard TitleCard_4_218 = new TitleCard(2495584715, CardView.Title, 4, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_4_218);
            DutyCard DutyCard_5_219 = new DutyCard(116206292, CardView.Duty, 5, 5, 0, 93906565, 500, 1000, 2877308884);
            AddCard(DutyCard_5_219);
            TitleCard TitleCard_3_220 = new TitleCard(2839285701, CardView.Title, 3, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_3_220);
            DutyCard DutyCard_4_221 = new DutyCard(862993982, CardView.Duty, 4, 5, 1244718151, 178117637, 150, 500, 3221009870);
            AddCard(DutyCard_4_221);
            TitleCard TitleCard_3_222 = new TitleCard(1883201872, CardView.Title, 3, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_3_222);
            DutyCard DutyCard_4_223 = new DutyCard(1938738841, CardView.Duty, 4, 5, 2320463010, 219954581, 50, 500, 2264926041);
            AddCard(DutyCard_4_223);
            TitleCard TitleCard_4_224 = new TitleCard(2096865481, CardView.Title, 4, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_4_224);
            DutyCard DutyCard_5_225 = new DutyCard(2404269826, CardView.Duty, 5, 5, 0, 50025957, 150, 1000, 2478589650);
            AddCard(DutyCard_5_225);
            TitleCard TitleCard_3_226 = new TitleCard(3589065884, CardView.Title, 3, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_3_226);
            DutyCard DutyCard_4_227 = new DutyCard(1850247525, CardView.Duty, 4, 5, 2231971694, 77889557, 50, 500, 3970790053);
            AddCard(DutyCard_4_227);
            TitleCard TitleCard_3_228 = new TitleCard(1445752256, CardView.Title, 3, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_3_228);
            DutyCard DutyCard_4_229 = new DutyCard(2169645913, CardView.Duty, 4, 5, 2551370082, 64371525, 50, 500, 1827476425);
            AddCard(DutyCard_4_229);
            TitleCard TitleCard_3_230 = new TitleCard(2987131264, CardView.Title, 3, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_3_230);
            DutyCard DutyCard_4_231 = new DutyCard(3189350201, CardView.Duty, 4, 5, 3571074370, 173249509, 100, 500, 3368855433);
            AddCard(DutyCard_4_231);
            TitleCard TitleCard_4_232 = new TitleCard(1073464115, CardView.Title, 4, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_4_232);
            DutyCard DutyCard_5_233 = new DutyCard(3399913644, CardView.Duty, 5, 5, 0, 53014821, 250, 1000, 1455188284);
            AddCard(DutyCard_5_233);
            TitleCard TitleCard_3_234 = new TitleCard(1100747936, CardView.Title, 3, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_3_234);
            DutyCard DutyCard_4_235 = new DutyCard(3808093273, CardView.Duty, 4, 5, 4189817442, 182923925, 100, 500, 1482472105);
            AddCard(DutyCard_4_235);
            TitleCard TitleCard_4_236 = new TitleCard(1880187222, CardView.Title, 4, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_4_236);
            DutyCard DutyCard_5_237 = new DutyCard(2935741167, CardView.Duty, 5, 5, 0, 250643893, 500, 1000, 2261911391);
            AddCard(DutyCard_5_237);
            TitleCard TitleCard_5_238 = new TitleCard(1721101442, CardView.Title, 5, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_5_238);
            TitleCard TitleCard_4_239 = new TitleCard(1467012298, CardView.Title, 4, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_4_239);
            DutyCard DutyCard_5_240 = new DutyCard(3785687875, CardView.Duty, 5, 5, 0, 237166309, 500, 1000, 1848736467);
            AddCard(DutyCard_5_240);
            TitleCard TitleCard_5_241 = new TitleCard(3696604306, CardView.Title, 5, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_5_241);
            TitleCard TitleCard_4_242 = new TitleCard(153422027, CardView.Title, 4, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_4_242);
            DutyCard DutyCard_5_243 = new DutyCard(1586218756, CardView.Duty, 5, 5, 0, 143972261, 250, 1000, 535146196);
            AddCard(DutyCard_5_243);
            TitleCard TitleCard_5_244 = new TitleCard(145912990, CardView.Title, 5, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_5_244);
            TitleCard TitleCard_5_245 = new TitleCard(3542389422, CardView.Title, 5, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_5_245);
            TitleCard TitleCard_4_246 = new TitleCard(3632585973, CardView.Title, 4, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_4_246);
            DutyCard DutyCard_5_247 = new DutyCard(3727846334, CardView.Duty, 5, 5, 0, 111400053, 250000, 1000, 4014310142);
            AddCard(DutyCard_5_247);
            TitleCard TitleCard_5_248 = new TitleCard(1625915310, CardView.Title, 5, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_5_248);
            TitleCard TitleCard_4_249 = new TitleCard(2472676149, CardView.Title, 4, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_4_249);
            DutyCard DutyCard_5_250 = new DutyCard(1231684078, CardView.Duty, 5, 5, 0, 102330325, 500, 1000, 2854400318);
            AddCard(DutyCard_5_250);
            TitleCard TitleCard_5_251 = new TitleCard(2877308884, CardView.Title, 5, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_5_251);
            TitleCard TitleCard_4_252 = new TitleCard(3221009870, CardView.Title, 4, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_4_252);
            DutyCard DutyCard_5_253 = new DutyCard(1244718151, CardView.Duty, 5, 5, 0, 178117637, 500, 1000, 3602734039);
            AddCard(DutyCard_5_253);
            TitleCard TitleCard_4_254 = new TitleCard(2264926041, CardView.Title, 4, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_4_254);
            DutyCard DutyCard_5_255 = new DutyCard(2320463010, CardView.Duty, 5, 5, 0, 219954581, 150, 1000, 2646650210);
            AddCard(DutyCard_5_255);
            TitleCard TitleCard_5_256 = new TitleCard(2478589650, CardView.Title, 5, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_5_256);
            TitleCard TitleCard_4_257 = new TitleCard(3970790053, CardView.Title, 4, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_4_257);
            DutyCard DutyCard_5_258 = new DutyCard(2231971694, CardView.Duty, 5, 5, 0, 77889557, 150, 1000, 57546926);
            AddCard(DutyCard_5_258);
            TitleCard TitleCard_4_259 = new TitleCard(1827476425, CardView.Title, 4, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_4_259);
            DutyCard DutyCard_5_260 = new DutyCard(2551370082, CardView.Duty, 5, 5, 0, 64371525, 150, 1000, 2209200594);
            AddCard(DutyCard_5_260);
            TitleCard TitleCard_4_261 = new TitleCard(3368855433, CardView.Title, 4, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_4_261);
            DutyCard DutyCard_5_262 = new DutyCard(3571074370, CardView.Duty, 5, 5, 0, 173249509, 250, 1000, 3750579602);
            AddCard(DutyCard_5_262);
            TitleCard TitleCard_5_263 = new TitleCard(1455188284, CardView.Title, 5, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_5_263);
            TitleCard TitleCard_4_264 = new TitleCard(1482472105, CardView.Title, 4, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_4_264);
            DutyCard DutyCard_5_265 = new DutyCard(4189817442, CardView.Duty, 5, 5, 0, 182923925, 250, 1000, 1864196274);
            AddCard(DutyCard_5_265);
            TitleCard TitleCard_5_266 = new TitleCard(2261911391, CardView.Title, 5, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_5_266);
            TitleCard TitleCard_5_267 = new TitleCard(1848736467, CardView.Title, 5, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_5_267);
            TitleCard TitleCard_5_268 = new TitleCard(535146196, CardView.Title, 5, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_5_268);
            TitleCard TitleCard_5_269 = new TitleCard(4014310142, CardView.Title, 5, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_5_269);
            TitleCard TitleCard_5_270 = new TitleCard(2854400318, CardView.Title, 5, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_5_270);
            TitleCard TitleCard_5_271 = new TitleCard(3602734039, CardView.Title, 5, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_5_271);
            TitleCard TitleCard_5_272 = new TitleCard(2646650210, CardView.Title, 5, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_5_272);
            TitleCard TitleCard_5_273 = new TitleCard(57546926, CardView.Title, 5, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_5_273);
            TitleCard TitleCard_5_274 = new TitleCard(2209200594, CardView.Title, 5, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_5_274);
            TitleCard TitleCard_5_275 = new TitleCard(3750579602, CardView.Title, 5, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_5_275);
            TitleCard TitleCard_5_276 = new TitleCard(1864196274, CardView.Title, 5, new ObjectStats(new Dictionary<ObjectStat, float>()), new ObjectStats(new Dictionary<ObjectStat, float>()));
            AddCard(TitleCard_5_276);

        }

        private static void CreateShipCards()
        {
            // Human

            // Mk2

            // 2016
            List<ShipSlotCard> mk2ShipSlots = new List<ShipSlotCard>();
            mk2ShipSlots.Add(new ShipSlotCard(1, 0, ShipSlotType.weapon, "bullet01", 49813));
            mk2ShipSlots.Add(new ShipSlotCard(1, 1, ShipSlotType.weapon, "bullet02", 50321));
            mk2ShipSlots.Add(new ShipSlotCard(1, 2, ShipSlotType.weapon, "bullet03", 19778));
            mk2ShipSlots.Add(new ShipSlotCard(1, 3, ShipSlotType.hull, "", 44673));
            mk2ShipSlots.Add(new ShipSlotCard(1, 4, ShipSlotType.hull, "", 44673));
            mk2ShipSlots.Add(new ShipSlotCard(1, 5, ShipSlotType.computer, "", 44673));
            mk2ShipSlots.Add(new ShipSlotCard(1, 6, ShipSlotType.computer, "", 44673));
            mk2ShipSlots.Add(new ShipSlotCard(1, 7, ShipSlotType.engine, "", 44673));
            mk2ShipSlots.Add(new ShipSlotCard(1, 8, ShipSlotType.engine, "", 44673));
            mk2ShipSlots.Add(new ShipSlotCard(1, 9, ShipSlotType.engine, "", 44673));
            mk2ShipSlots.Add(new ShipSlotCard(1, 10, ShipSlotType.engine, "", 44673));
            mk2ShipSlots.Add(new ShipSlotCard(1, 11, ShipSlotType.engine, "", 44673));
            mk2ShipSlots.Add(new ShipSlotCard(2, 12, ShipSlotType.weapon, "elitebullet04", 27288));
            mk2ShipSlots.Add(new ShipSlotCard(1, 13, ShipSlotType.ship_paint, "", 44673));
            mk2ShipSlots.Add(new ShipSlotCard(1, 14, ShipSlotType.avionics, "", 44673));

            // 2016
            Dictionary<ObjectStat, float> viperStats = new Dictionary<ObjectStat, float>();
            viperStats.Add(ObjectStat.Acceleration, 13.5f);
            viperStats.Add(ObjectStat.AccelerationMultiplierOnBoost, 1.5f);
            viperStats.Add(ObjectStat.ArmorValue, 5f);
            viperStats.Add(ObjectStat.Avoidance, 510f);
            viperStats.Add(ObjectStat.AvoidanceFading, 0.75f);
            viperStats.Add(ObjectStat.BoostCost, 0.5f);
            viperStats.Add(ObjectStat.BoostSpeed, 90f);
            viperStats.Add(ObjectStat.CriticalDefense, 80f);
            viperStats.Add(ObjectStat.FtlCharge, 15f);
            viperStats.Add(ObjectStat.FtlCooldown, 35f);
            viperStats.Add(ObjectStat.FtlCost, 1f);
            viperStats.Add(ObjectStat.FtlRange, 90f);
            viperStats.Add(ObjectStat.FirewallRating, 100f);
            viperStats.Add(ObjectStat.MaxHullPoints, 450f);
            viperStats.Add(ObjectStat.HullRecovery, 2.5f);
            viperStats.Add(ObjectStat.MaxPowerPoints, 100f);
            viperStats.Add(ObjectStat.PowerRecovery, 5f);
            viperStats.Add(ObjectStat.PenetrationStrength, 100f);
            viperStats.Add(ObjectStat.Speed, 55f);
            viperStats.Add(ObjectStat.InertiaCompensation, 175f);
            viperStats.Add(ObjectStat.PitchAcceleration, 55f);
            viperStats.Add(ObjectStat.PitchMaxSpeed, 52f);
            viperStats.Add(ObjectStat.YawAcceleration, 55f);
            viperStats.Add(ObjectStat.YawMaxSpeed, 52f);
            viperStats.Add(ObjectStat.RollAcceleration, 748f);
            viperStats.Add(ObjectStat.RollMaxSpeed, 182f);
            viperStats.Add(ObjectStat.StrafeAcceleration, 0f);
            viperStats.Add(ObjectStat.StrafeMaxSpeed, 0f);
            viperStats.Add(ObjectStat.DetectionInnerRadius, 1000f);
            viperStats.Add(ObjectStat.DetectionOuterRadius, 2000f);
            viperStats.Add(ObjectStat.DetectionVisualRadius, 200f);

            ShipCard mk2ShipCard = new ShipCard(22131170, CardView.Ship, 90, 1, 2, 1, 1, 0, 9000, 1, new ShipRole[1] { ShipRole.Assault }, ShipRoleDeprecated.Fighter, "ship_viper_paperdoll_layouts", mk2ShipSlots, false, new List<uint>(), -1, new ObjectStats(new Dictionary<ObjectStat, float>()), Faction.Colonial, new List<ShipImmutableSlot>());
            GUICard mk2GuiCard = new GUICard(22131170, CardView.GUI, "ship_viper", 1, "GUI/AbilityToolbar/abilities_atlas", 0, "", "GUI/Slots/humant1fighter", "", new string[0]);
            ShopItemCard mk2ShopItemCard = new ShopItemCard(22131170, CardView.Price, ShopCategory.Ship, ShopItemType.Ship, 1, new string[0], 0, new Price(), new Price(new Dictionary<ShipConsumableCard, float>() { { (ShipConsumableCard)FetchCard(264733124, CardView.ShipConsumable), 30000f } }), new Price(), Faction.Colonial, false);
            WorldCard mk2WorldCard = new WorldCard(22131170, CardView.World, "HumanT1Fighter", 0, 6, new SpotDesc[0], "gui/map/map_objects", 0, 20, true, true, false);

            AddCard(mk2ShipCard);
            AddCard(mk2GuiCard);
            AddCard(mk2ShopItemCard);
            AddCard(mk2WorldCard);

            // Raptor
            ShipCard raptorShipCard = new ShipCard(22131172, CardView.Ship, 92, 2, 2, 1, 4, 0, 5000, 1, new ShipRole[1] { ShipRole.Command }, ShipRoleDeprecated.Command, "ship_raptor_paperdoll_layouts", new List<ShipSlotCard>(), false, new List<uint>(), -1, new ObjectStats(new Dictionary<ObjectStat, float>()), Faction.Colonial, new List<ShipImmutableSlot>());
            GUICard raptorGuiCard = new GUICard(22131172, CardView.GUI, "ship_raptor", 2, "GUI/AbilityToolbar/abilities_atlas", 0, "", "GUI/Slots/humant1command", "", new string[0]);
            ShopItemCard raptorShopItemCard = new ShopItemCard(22131172, CardView.Price, ShopCategory.Ship, ShopItemType.Ship, 1, new string[0], 1, new Price(), new Price(), new Price(), Faction.Colonial, false);
            WorldCard raptorWorldCard = new WorldCard(22131172, CardView.World, "HumanT1Command", 0, 6, new SpotDesc[0], "gui/map/map_objects", 0, 20, true, true, false);

            AddCard(raptorShipCard);
            AddCard(raptorGuiCard);
            AddCard(raptorShopItemCard);
            AddCard(raptorWorldCard);

            // Raptor FR
            ShipCard raptorFrShipCard = new ShipCard(22131200, CardView.Ship, 120, 1, 1, 1, 41, 0, 10000, 1, new ShipRole[1] { ShipRole.Command }, ShipRoleDeprecated.Command, "ship_raptor_recon_paperdoll_layouts", new List<ShipSlotCard>(), false, new List<uint>() { 4 }, 4, new ObjectStats(new Dictionary<ObjectStat, float>()), Faction.Colonial, new List<ShipImmutableSlot>());
            GUICard raptorFrGuiCard = new GUICard(22131200, CardView.GUI, "ship_raptor_recon", 1, "GUI/AbilityToolbar/abilities_atlas", 0, "", "GUI/Slots/humant1command_scout", "", new string[0]);
            ShopItemCard raptorFrShopItemCard = new ShopItemCard(22131200, CardView.Price, ShopCategory.Ship, ShopItemType.Ship, 1, new string[0], 1, new Price(), new Price(), new Price(), Faction.Colonial, false);
            WorldCard raptorFrWorldCard = new WorldCard(22131200, CardView.World, "HumanT1Scout", 0, 6, new SpotDesc[0], "gui/map/map_objects", 0, 20, true, true, false);

            AddCard(raptorFrShipCard);
            AddCard(raptorFrGuiCard);
            AddCard(raptorFrShopItemCard);
            AddCard(raptorFrWorldCard);

            // Rhino
            ShipCard rhinoShipCard = new ShipCard(22131174, CardView.Ship, 94, 1, 2, 1, 7, 0, 9000, 1, new ShipRole[1] { ShipRole.Bomber }, ShipRoleDeprecated.Defender, "ship_rhino_paperdoll_layouts", new List<ShipSlotCard>(), false, new List<uint>(), -1, new ObjectStats(new Dictionary<ObjectStat, float>()), Faction.Colonial, new List<ShipImmutableSlot>());
            GUICard rhinoGuiCard = new GUICard(22131174, CardView.GUI, "ship_rhino", 1, "GUI/AbilityToolbar/abilities_atlas", 0, "", "GUI/Slots/humant1defender", "", new string[0]);
            ShopItemCard rhinoShopItemCard = new ShopItemCard(22131174, CardView.Price, ShopCategory.Ship, ShopItemType.Ship, 1, new string[0], 3, new Price(), new Price(), new Price(), Faction.Colonial, false);
            WorldCard rhinoWorldCard = new WorldCard(22131174, CardView.World, "HumanT1Defender", 0, 6, new SpotDesc[0], "gui/map/map_objects", 0, 20, true, true, false);

            AddCard(rhinoShipCard);
            AddCard(rhinoGuiCard);
            AddCard(rhinoShopItemCard);
            AddCard(rhinoWorldCard);

            // Mk3
            ShipCard mk3ShipCard = new ShipCard(22131176, CardView.Ship, 96, 1, 2, 1, 16, 0, 9000, 1, new ShipRole[1] { ShipRole.Bomber }, ShipRoleDeprecated.Defender, "ship_viper_mk3_paperdoll_layouts", new List<ShipSlotCard>(), false, new List<uint>(), -1, new ObjectStats(new Dictionary<ObjectStat, float>()), Faction.Colonial, new List<ShipImmutableSlot>());
            GUICard mk3GuiCard = new GUICard(22131176, CardView.GUI, "ship_viper_mk3", 1, "GUI/AbilityToolbar/abilities_atlas", 0, "", "GUI/Slots/humant1multi2", "", new string[0]);
            ShopItemCard mk3ShopItemCard = new ShopItemCard(22131176, CardView.Price, ShopCategory.Ship, ShopItemType.Ship, 1, new string[0], 2, new Price(), new Price(), new Price(), Faction.Colonial, false);
            WorldCard mk3WorldCard = new WorldCard(22131176, CardView.World, "HumanT1Multi2", 0, 6, new SpotDesc[0], "gui/map/map_objects", 0, 20, true, true, false);

            AddCard(mk3ShipCard);
            AddCard(mk3GuiCard);
            AddCard(mk3ShopItemCard);
            AddCard(mk3WorldCard);

            // Raven
            Dictionary<ObjectStat, float> ravenStats = new Dictionary<ObjectStat, float>();
            ravenStats.Add(ObjectStat.Acceleration, 13);
            ravenStats.Add(ObjectStat.AccelerationMultiplierOnBoost, 1.25f);
            ravenStats.Add(ObjectStat.ArmorValue, 0);
            ravenStats.Add(ObjectStat.Avoidance, 520);
            ravenStats.Add(ObjectStat.AvoidanceFading, 0.75f);
            ravenStats.Add(ObjectStat.BoostCost, 0.5f);
            ravenStats.Add(ObjectStat.BoostSpeed, 90);
            ravenStats.Add(ObjectStat.CriticalDefense, 60);
            ravenStats.Add(ObjectStat.FtlCharge, 15);
            ravenStats.Add(ObjectStat.FtlCost, 1);
            ravenStats.Add(ObjectStat.FtlRange, 105f);
            ravenStats.Add(ObjectStat.FirewallRating, 100);
            ravenStats.Add(ObjectStat.MaxHullPoints, 350);
            ravenStats.Add(ObjectStat.HullRecovery, 3f);
            ravenStats.Add(ObjectStat.MaxPowerPoints, 120);
            ravenStats.Add(ObjectStat.PowerRecovery, 3f);
            ravenStats.Add(ObjectStat.PenetrationStrength, 100);
            ravenStats.Add(ObjectStat.Speed, 70);
            ravenStats.Add(ObjectStat.InertiaCompensation, 175);
            ravenStats.Add(ObjectStat.PitchAcceleration, 75);
            ravenStats.Add(ObjectStat.PitchMaxSpeed, 52);
            ravenStats.Add(ObjectStat.YawAcceleration, 75);
            ravenStats.Add(ObjectStat.YawMaxSpeed, 50);
            ravenStats.Add(ObjectStat.RollAcceleration, 750);
            ravenStats.Add(ObjectStat.RollMaxSpeed, 90);
            ravenStats.Add(ObjectStat.StrafeAcceleration, 80);
            ravenStats.Add(ObjectStat.StrafeMaxSpeed, 30);

            ravenStats.Add(ObjectStat.DetectionVisualRadius, 225);
            ravenStats.Add(ObjectStat.DetectionInnerRadius, 1000);
            ravenStats.Add(ObjectStat.DetectionOuterRadius, 2250);

            List<ShipSlotCard>ravenShipSlots = new List<ShipSlotCard>();
            ravenShipSlots.Add(new ShipSlotCard(1, 0, ShipSlotType.gun, "bullet01", 49813));
            ravenShipSlots.Add(new ShipSlotCard(1, 2, ShipSlotType.gun, "bullet03", 19778));
            ravenShipSlots.Add(new ShipSlotCard(1, 3, ShipSlotType.hull, "", 44673));
            ravenShipSlots.Add(new ShipSlotCard(1, 4, ShipSlotType.hull, "", 44673));
            ravenShipSlots.Add(new ShipSlotCard(1, 5, ShipSlotType.computer, "", 44673));
            ravenShipSlots.Add(new ShipSlotCard(1, 10, ShipSlotType.engine, "", 44673));
            ravenShipSlots.Add(new ShipSlotCard(1, 11, ShipSlotType.engine, "", 44673));
            ravenShipSlots.Add(new ShipSlotCard(1, 13, ShipSlotType.ship_paint, "", 44673));
            ravenShipSlots.Add(new ShipSlotCard(1, 14, ShipSlotType.avionics, "", 44673));
            //ravenShipSlots.Add(new ShipSlotCard(2, 8, ShipSlotType.computer, "", 44673));
            ravenShipSlots.Add(new ShipSlotCard(1, 16, ShipSlotType.role, "", 44673));
            //ravenShipSlots.Add(new ShipSlotCard(2, 1, ShipSlotType.engine, "", 44673));
            ravenShipSlots.Add(new ShipSlotCard(1, 6, ShipSlotType.launcher, "bullet02", 50321));

            ShipCard ravenShipCard = new ShipCard(22131178, CardView.Ship, 98, 1, 2, 1, 17, 0, 9000, 1, new ShipRole[1] { ShipRole.Stealth }, ShipRoleDeprecated.Stealth, "ship_colonial_strike_stealth_paperdoll_layouts", ravenShipSlots, false, new List<uint>(), -1, new ObjectStats(ravenStats), Faction.Colonial, new List<ShipImmutableSlot>());
            GUICard ravenGuiCard = new GUICard(22131178, CardView.GUI, "ship_colonial_strike_stealth", 1, "GUI/AbilityToolbar/abilities_atlas", 0, "", "GUI/Slots/humant1stealth", "", new string[0]);
            ShopItemCard ravenShopItemCard = new ShopItemCard(22131178, CardView.Price, ShopCategory.Ship, ShopItemType.Ship, 1, new string[0], 5, new Price(new Dictionary<ShipConsumableCard, float>() { { (ShipConsumableCard)FetchCard(130920111, CardView.ShipConsumable), 30000f } }), new Price(new Dictionary<ShipConsumableCard, float>() { { (ShipConsumableCard)FetchCard(130920111, CardView.ShipConsumable), 45000f } }), new Price(), Faction.Colonial, false);
            CameraCard ravenCameraCard = new CameraCard(22131178, CardView.Camera, 20, 40, 10, 20, 20);
            WorldCard ravenWorldCard = new WorldCard(22131178, CardView.World, "HumanT1Stealth", 0, 6, new SpotDesc[0], "gui/map/map_objects", 0, 20, true, true, false);
            ShipLightCard ravenShipLightCard = new ShipLightCard(22131178, CardView.ShipLight, 98, 1, new ShipRole[1] { ShipRole.Stealth }, ShipRoleDeprecated.Stealth);
            MovementCard ravenMovementCard = new MovementCard(22131178, CardView.Movement, 6.5f, 360f, 40f, 2f, 2f, 2f);

            AddCard(ravenShipCard);
            AddCard(ravenGuiCard);
            AddCard(ravenShopItemCard);
            AddCard(ravenCameraCard);
            AddCard(ravenWorldCard);
            AddCard(ravenShipLightCard);
            AddCard(ravenMovementCard);

            // Advanced Mk7
            Dictionary<ObjectStat, float> advMk7Stats = new Dictionary<ObjectStat, float>();
            advMk7Stats.Add(ObjectStat.MaxHullPoints, 585);
            advMk7Stats.Add(ObjectStat.HullRecovery, 4.5f);
            advMk7Stats.Add(ObjectStat.ArmorValue, 5);
            advMk7Stats.Add(ObjectStat.CriticalDefense, 80);
            advMk7Stats.Add(ObjectStat.Avoidance, 510);
            advMk7Stats.Add(ObjectStat.TurnSpeed, 50);
            advMk7Stats.Add(ObjectStat.TurnAcceleration, 55);
            advMk7Stats.Add(ObjectStat.InertiaCompensation, 100);
            advMk7Stats.Add(ObjectStat.Acceleration, 12);
            advMk7Stats.Add(ObjectStat.Speed, 55);
            advMk7Stats.Add(ObjectStat.BoostSpeed, 85);
            advMk7Stats.Add(ObjectStat.BoostCost, 0.75f);
            advMk7Stats.Add(ObjectStat.FtlRange, 90f); //4.5f * 20
            advMk7Stats.Add(ObjectStat.FtlCharge, 15);
            advMk7Stats.Add(ObjectStat.FtlCost, 30);
            advMk7Stats.Add(ObjectStat.MaxPowerPoints, 150);
            advMk7Stats.Add(ObjectStat.PowerRecovery, 5);
            advMk7Stats.Add(ObjectStat.FirewallRating, 100);
            advMk7Stats.Add(ObjectStat.DradisRange, 2000);
            advMk7Stats.Add(ObjectStat.DetectionVisualRadius, 200);
            advMk7Stats.Add(ObjectStat.DetectionInnerRadius, 1000);
            advMk7Stats.Add(ObjectStat.DetectionOuterRadius, 2000);

            advMk7Stats.Add(ObjectStat.StrafeAcceleration, 145);
            advMk7Stats.Add(ObjectStat.StrafeMaxSpeed, 40);
            advMk7Stats.Add(ObjectStat.PitchMaxSpeed, 65);
            advMk7Stats.Add(ObjectStat.PitchAcceleration, 120);
            advMk7Stats.Add(ObjectStat.YawMaxSpeed, 65);
            advMk7Stats.Add(ObjectStat.YawAcceleration, 120);
            advMk7Stats.Add(ObjectStat.RollMaxSpeed, 135);
            advMk7Stats.Add(ObjectStat.RollAcceleration, 120);

            List<ShipSlotCard> advMk7ShipSlots = new List<ShipSlotCard>();
            advMk7ShipSlots.Add(new ShipSlotCard(2, 0, ShipSlotType.weapon, "", 0));
            advMk7ShipSlots.Add(new ShipSlotCard(2, 1, ShipSlotType.weapon, "", 0));
            advMk7ShipSlots.Add(new ShipSlotCard(2, 2, ShipSlotType.weapon, "", 0));
            advMk7ShipSlots.Add(new ShipSlotCard(2, 3, ShipSlotType.weapon, "", 0));
            advMk7ShipSlots.Add(new ShipSlotCard(2, 4, ShipSlotType.weapon, "", 0));
            advMk7ShipSlots.Add(new ShipSlotCard(2, 5, ShipSlotType.hull, "", 0));
            advMk7ShipSlots.Add(new ShipSlotCard(2, 6, ShipSlotType.computer, "", 0));
            advMk7ShipSlots.Add(new ShipSlotCard(2, 7, ShipSlotType.hull, "", 0));
            advMk7ShipSlots.Add(new ShipSlotCard(2, 8, ShipSlotType.hull, "", 0));
            advMk7ShipSlots.Add(new ShipSlotCard(2, 9, ShipSlotType.engine, "", 0));
            advMk7ShipSlots.Add(new ShipSlotCard(2, 10, ShipSlotType.engine, "", 0));
            advMk7ShipSlots.Add(new ShipSlotCard(2, 11, ShipSlotType.engine, "", 0));
            advMk7ShipSlots.Add(new ShipSlotCard(2, 12, ShipSlotType.computer, "", 0));
            advMk7ShipSlots.Add(new ShipSlotCard(2, 13, ShipSlotType.computer, "", 0));
            advMk7ShipSlots.Add(new ShipSlotCard(2, 14, ShipSlotType.ship_paint, "", 0));
            advMk7ShipSlots.Add(new ShipSlotCard(2, 15, ShipSlotType.avionics, "", 0));

            List<ShipImmutableSlot> advMk7ShipImmSlots = new List<ShipImmutableSlot>();

            ShipCard advMk7ShipCard = new ShipCard(22131180, CardView.Ship, 100, 2, 2, 1, 11, 0, 9000, 1, new ShipRole[1] { ShipRole.Assault }, ShipRoleDeprecated.Fighter, "ship_viper_mk7_paperdoll_layouts", advMk7ShipSlots, false, new List<uint>(), -1, new ObjectStats(advMk7Stats), Faction.Colonial, advMk7ShipImmSlots);
            GUICard advMk7GuiCard = new GUICard(22131180, CardView.GUI, "ship_viper_mk7", 2, "GUI/AbilityToolbar/abilities_atlas", 10, "", "GUI/Slots/vipermk7", "", new string[0]);
            ShopItemCard advMk7ShopItemCard = new ShopItemCard(22131180, CardView.Price, ShopCategory.Ship, ShopItemType.Ship, 2, new string[0], 4, new Price(), new Price(), new Price(), Faction.Colonial, false);
            CameraCard advMk7CameraCard = new CameraCard(22131180, CardView.Camera, 20, 40, 10, 20, 20);
            WorldCard advMk7WorldCard = new WorldCard(22131180, CardView.World, "HumanT1Merit", 0, 6, new SpotDesc[0], "gui/map/map_objects", 0, 20, true, true, false);
            ShipLightCard advMk7ShipLightCard = new ShipLightCard(22131180, CardView.ShipLight, 100, 1, new ShipRole[1] { ShipRole.Assault }, ShipRoleDeprecated.Fighter);

            MovementCard advMk7MovementCard = new MovementCard(22131180, CardView.Movement, 6.5f, 360f, 40f, 2f, 2f, 2f);

            AddCard(advMk7ShipCard);
            AddCard(advMk7GuiCard);
            AddCard(advMk7ShopItemCard);
            AddCard(advMk7CameraCard);
            AddCard(advMk7WorldCard);
            AddCard(advMk7ShipLightCard);
            AddCard(advMk7MovementCard);

            // Maul
            ShipCard maulShipCard = new ShipCard(22131182, CardView.Ship, 102, 1, 2, 10, 8, 0, 9000, 2, new ShipRole[1] { ShipRole.Bomber }, ShipRoleDeprecated.Defender, "ship_dominator_paperdoll_layouts", new List<ShipSlotCard>(), false, new List<uint>(), -1, new ObjectStats(new Dictionary<ObjectStat, float>()), Faction.Colonial, new List<ShipImmutableSlot>());
            GUICard maulGuiCard = new GUICard(22131182, CardView.GUI, "ship_dominator", 1, "GUI/AbilityToolbar/abilities_atlas", 0, "", "GUI/Slots/humant2defender", "", new string[0]);
            ShopItemCard maulShopItemCard = new ShopItemCard(22131182, CardView.Price, ShopCategory.Ship, ShopItemType.Ship, 1, new string[0], 8, new Price(), new Price(), new Price(), Faction.Colonial, false);
            WorldCard maulWorldCard = new WorldCard(22131182, CardView.World, "HumanT2Defender", 0, 6, new SpotDesc[0], "gui/map/map_objects", 3, 20, true, true, false);

            AddCard(maulShipCard);
            AddCard(maulGuiCard);
            AddCard(maulShopItemCard);
            AddCard(maulWorldCard);

            // Foice
            ShipCard foiceShipCard = new ShipCard(22131184, CardView.Ship, 104, 1, 2, 10, 2, 0, 9000, 2, new ShipRole[1] { ShipRole.Fighter }, ShipRoleDeprecated.Fighter, "ship_berserker_paperdoll_layouts", new List<ShipSlotCard>(), false, new List<uint>(), -1, new ObjectStats(new Dictionary<ObjectStat, float>()), Faction.Colonial, new List<ShipImmutableSlot>());
            GUICard foiceGuiCard = new GUICard(22131184, CardView.GUI, "ship_berserker", 1, "GUI/AbilityToolbar/abilities_atlas", 0, "", "GUI/Slots/humant2fighter", "", new string[0]);
            ShopItemCard foiceShopItemCard = new ShopItemCard(22131184, CardView.Price, ShopCategory.Ship, ShopItemType.Ship, 1, new string[0], 6, new Price(), new Price(), new Price(), Faction.Colonial, false);
            WorldCard foiceWorldCard = new WorldCard(22131184, CardView.World, "HumanT2Fighter", 0, 6, new SpotDesc[0], "gui/map/map_objects", 3, 20, true, true, false);

            AddCard(foiceShipCard);
            AddCard(foiceGuiCard);
            AddCard(foiceShopItemCard);
            AddCard(foiceWorldCard);

            // Gladius
            ShipCard gladiusShipCard = new ShipCard(22131186, CardView.Ship, 106, 1, 2, 10, 5, 0, 9000, 2, new ShipRole[1] { ShipRole.Command }, ShipRoleDeprecated.Command, "ship_avenger_paperdoll_layouts", new List<ShipSlotCard>(), false, new List<uint>(), -1, new ObjectStats(new Dictionary<ObjectStat, float>()), Faction.Colonial, new List<ShipImmutableSlot>());
            GUICard gladiusGuiCard = new GUICard(22131186, CardView.GUI, "ship_avenger", 1, "GUI/AbilityToolbar/abilities_atlas", 0, "", "GUI/Slots/humant2command", "", new string[0]);
            ShopItemCard gladiusShopItemCard = new ShopItemCard(22131186, CardView.Price, ShopCategory.Ship, ShopItemType.Ship, 1, new string[0], 7, new Price(), new Price(), new Price(), Faction.Colonial, false);
            WorldCard gladiusWorldCard = new WorldCard(22131186, CardView.World, "HumanT2Command", 0, 6, new SpotDesc[0], "gui/map/map_objects", 3, 20, true, true, false);

            AddCard(gladiusShipCard);
            AddCard(gladiusGuiCard);
            AddCard(gladiusShopItemCard);
            AddCard(gladiusWorldCard);

            // Haldberd
            ShipCard haldberdShipCard = new ShipCard(22131188, CardView.Ship, 108, 1, 2, 10, 13, 0, 9000, 2, new ShipRole[1] { ShipRole.Assault }, ShipRoleDeprecated.Multi, "ship_halberd_paperdoll_layouts", new List<ShipSlotCard>(), false, new List<uint>(), -1, new ObjectStats(new Dictionary<ObjectStat, float>()), Faction.Colonial, new List<ShipImmutableSlot>());
            GUICard haldberdGuiCard = new GUICard(22131188, CardView.GUI, "ship_halberd", 1, "GUI/AbilityToolbar/abilities_atlas", 0, "", "GUI/Slots/humant2merit", "", new string[0]);
            ShopItemCard haldberdShopItemCard = new ShopItemCard(22131188, CardView.Price, ShopCategory.Ship, ShopItemType.Ship, 1, new string[0], 9, new Price(), new Price(), new Price(), Faction.Colonial, false);
            WorldCard haldberdWorldCard = new WorldCard(22131188, CardView.World, "HumanT2Merit", 0, 6, new SpotDesc[0], "gui/map/map_objects", 3, 20, true, true, false);

            AddCard(haldberdShipCard);
            AddCard(haldberdGuiCard);
            AddCard(haldberdShopItemCard);
            AddCard(haldberdWorldCard);

            // Aesir
            ShipCard aesirShipCard = new ShipCard(22131190, CardView.Ship, 110, 1, 2, 20, 3, 0, 9000, 3, new ShipRole[1] { ShipRole.Fighter }, ShipRoleDeprecated.Fighter, "ship_gunstar_paperdoll_layouts", new List<ShipSlotCard>(), false, new List<uint>(), -1, new ObjectStats(new Dictionary<ObjectStat, float>()), Faction.Colonial, new List<ShipImmutableSlot>());
            GUICard aesirGuiCard = new GUICard(22131190, CardView.GUI, "ship_gunstar", 1, "GUI/AbilityToolbar/abilities_atlas", 0, "", "GUI/Slots/humant3fighter", "", new string[0]);
            ShopItemCard aesirShopItemCard = new ShopItemCard(22131190, CardView.Price, ShopCategory.Ship, ShopItemType.Ship, 1, new string[0], 10, new Price(), new Price(), new Price(), Faction.Colonial, false);
            WorldCard aesirWorldCard = new WorldCard(22131190, CardView.World, "HumanT3Fighter", 0, 6, new SpotDesc[0], "gui/map/map_objects", 6, 20, true, true, false);

            AddCard(aesirShipCard);
            AddCard(aesirGuiCard);
            AddCard(aesirShopItemCard);
            AddCard(aesirWorldCard);

            // Vanir
            ShipCard vanirShipCard = new ShipCard(22131192, CardView.Ship, 112, 1, 2, 20, 6, 0, 9000, 3, new ShipRole[1] { ShipRole.Command }, ShipRoleDeprecated.Command, "ship_cruiser_paperdoll_layouts", new List<ShipSlotCard>(), false, new List<uint>(), -1, new ObjectStats(new Dictionary<ObjectStat, float>()), Faction.Colonial, new List<ShipImmutableSlot>());
            GUICard vanirGuiCard = new GUICard(22131192, CardView.GUI, "ship_cruiser", 1, "GUI/AbilityToolbar/abilities_atlas", 0, "", "GUI/Slots/humant3command", "", new string[0]);
            ShopItemCard vanirShopItemCard = new ShopItemCard(22131192, CardView.Price, ShopCategory.Ship, ShopItemType.Ship, 1, new string[0], 11, new Price(), new Price(), new Price(), Faction.Colonial, false);
            WorldCard vanirWorldCard = new WorldCard(22131192, CardView.World, "HumanT3Command", 0, 6, new SpotDesc[0], "gui/map/map_objects", 6, 20, true, true, false);

            AddCard(vanirShipCard);
            AddCard(vanirGuiCard);
            AddCard(vanirShopItemCard);
            AddCard(vanirWorldCard);

            // Jotunn
            ShipCard jotunnShipCard = new ShipCard(22131194, CardView.Ship, 114, 1, 2, 20, 9, 0, 9000, 3, new ShipRole[1] { ShipRole.Destroyer }, ShipRoleDeprecated.Defender, "ship_dreadnought_paperdoll_layouts", new List<ShipSlotCard>(), false, new List<uint>(), -1, new ObjectStats(new Dictionary<ObjectStat, float>()), Faction.Colonial, new List<ShipImmutableSlot>());
            GUICard jotunnGuiCard = new GUICard(22131194, CardView.GUI, "ship_dreadnought", 1, "GUI/AbilityToolbar/abilities_atlas", 0, "", "GUI/Slots/humant3defender", "", new string[0]);
            ShopItemCard jotunnShopItemCard = new ShopItemCard(22131194, CardView.Price, ShopCategory.Ship, ShopItemType.Ship, 1, new string[0], 12, new Price(), new Price(), new Price(), Faction.Colonial, false);
            WorldCard jotunnWorldCard = new WorldCard(22131194, CardView.World, "HumanT3Defender", 0, 6, new SpotDesc[0], "gui/map/map_objects", 6, 20, true, true, false);

            AddCard(jotunnShipCard);
            AddCard(jotunnGuiCard);
            AddCard(jotunnShopItemCard);
            AddCard(jotunnWorldCard);

            // Gungnir
            Dictionary<ObjectStat, float> gungnirStats = new Dictionary<ObjectStat, float>();
            gungnirStats.Add(ObjectStat.MaxHullPoints, 4550);
            gungnirStats.Add(ObjectStat.HullRecovery, 35f);
            gungnirStats.Add(ObjectStat.ArmorValue, 40);
            gungnirStats.Add(ObjectStat.CriticalDefense, 100);
            gungnirStats.Add(ObjectStat.Avoidance, 50);
            gungnirStats.Add(ObjectStat.TurnSpeed, 10);
            gungnirStats.Add(ObjectStat.TurnAcceleration, 10);
            gungnirStats.Add(ObjectStat.InertiaCompensation, 50);
            gungnirStats.Add(ObjectStat.Acceleration, 2);
            gungnirStats.Add(ObjectStat.Speed, 30);
            gungnirStats.Add(ObjectStat.BoostSpeed, 45);
            gungnirStats.Add(ObjectStat.BoostCost, 8.1f);
            gungnirStats.Add(ObjectStat.FtlRange, 200f); //10f * 20
            gungnirStats.Add(ObjectStat.FtlCharge, 25);
            gungnirStats.Add(ObjectStat.FtlCost, 250);
            gungnirStats.Add(ObjectStat.MaxPowerPoints, 750);
            gungnirStats.Add(ObjectStat.PowerRecovery, 25);
            gungnirStats.Add(ObjectStat.FirewallRating, 150);
            gungnirStats.Add(ObjectStat.DradisRange, 3500);
            gungnirStats.Add(ObjectStat.DetectionVisualRadius, 350);
            gungnirStats.Add(ObjectStat.DetectionInnerRadius, 2000);
            gungnirStats.Add(ObjectStat.DetectionOuterRadius, 7000);

            gungnirStats.Add(ObjectStat.StrafeAcceleration, 145);
            gungnirStats.Add(ObjectStat.StrafeMaxSpeed, 40);
            gungnirStats.Add(ObjectStat.PitchMaxSpeed, 10);
            gungnirStats.Add(ObjectStat.PitchAcceleration, 30);
            gungnirStats.Add(ObjectStat.YawMaxSpeed, 10);
            gungnirStats.Add(ObjectStat.YawAcceleration, 30);
            gungnirStats.Add(ObjectStat.RollMaxSpeed, 10);
            gungnirStats.Add(ObjectStat.RollAcceleration, 30);

            ShipCard gungnirShipCard = new ShipCard(22131196, CardView.Ship, 116, 1, 2, 20, 14, 0, 9000, 3, new ShipRole[1] { ShipRole.Command }, ShipRoleDeprecated.Multi, "ship_gungnir_paperdoll_layouts", new List<ShipSlotCard>(), false, new List<uint>(), -1, new ObjectStats(gungnirStats), Faction.Colonial, new List<ShipImmutableSlot>());
            GUICard gungnirGuiCard = new GUICard(22131196, CardView.GUI, "ship_gungnir", 1, "GUI/AbilityToolbar/abilities_atlas", 0, "", "GUI/Slots/humant3merit", "", new string[0]);
            ShopItemCard gungnirShopItemCard = new ShopItemCard(22131196, CardView.Price, ShopCategory.Ship, ShopItemType.Ship, 1, new string[0], 13, new Price(), new Price(), new Price(), Faction.Colonial, false);
            WorldCard gungnirWorldCard = new WorldCard(22131196, CardView.World, "HumanT3Merit", 0, 6, new SpotDesc[0], "gui/map/map_objects", 6, 20, true, true, false);
            ShipLightCard gungnirShipLightCard = new ShipLightCard(22131196, CardView.ShipLight, 116, 3, new ShipRole[1] { ShipRole.Command }, ShipRoleDeprecated.Multi);
            CameraCard gungnirCameraCard = new CameraCard(22131196, CardView.Camera, 520, 540, 510, 520, 20);

            MovementCard gungnirMovementCard = new MovementCard(22131196, CardView.Movement, 6.5f, 40f, 20f, 2f, 2f, 2f);

            AddCard(gungnirShipCard);
            AddCard(gungnirGuiCard);
            AddCard(gungnirShopItemCard);
            AddCard(gungnirWorldCard);
            AddCard(gungnirShipLightCard);
            AddCard(gungnirCameraCard);
            AddCard(gungnirMovementCard);

            // Brimir
            ShipCard brimirShipCard = new ShipCard(22131198, CardView.Ship, 118, 1, 2, 30, 15, 0, 9000, 4, new ShipRole[1] { ShipRole.Carrier }, ShipRoleDeprecated.Carrier, "ship_brimir_paperdoll_layouts", new List<ShipSlotCard>(), false, new List<uint>(), -1, new ObjectStats(new Dictionary<ObjectStat, float>()), Faction.Colonial, new List<ShipImmutableSlot>());
            GUICard brimirGuiCard = new GUICard(22131198, CardView.GUI, "ship_brimir", 1, "GUI/AbilityToolbar/abilities_atlas", 0, "", "GUI/Slots/humant4carrier_brimir", "", new string[0]);
            ShopItemCard brimirShopItemCard = new ShopItemCard(22131198, CardView.Price, ShopCategory.Ship, ShopItemType.Ship, 1, new string[0], 14, new Price(), new Price(), new Price(), Faction.Colonial, false);
            WorldCard brimirWorldCard = new WorldCard(22131198, CardView.World, "HumanT4Carrier", 0, 6, new SpotDesc[0], "", 0, 0, true, true, false);

            AddCard(brimirShipCard);
            AddCard(brimirGuiCard);
            AddCard(brimirShopItemCard);
            AddCard(brimirWorldCard);

            // Add cards
            ShipListCard colShipListCard = new ShipListCard(73551268u);
            colShipListCard.AddShip(mk2ShipCard);
            colShipListCard.AddShip(raptorShipCard);
            colShipListCard.AddShip(rhinoShipCard);
            colShipListCard.AddShip(mk3ShipCard);
            colShipListCard.AddShip(ravenShipCard);
            colShipListCard.AddShip(advMk7ShipCard);
            colShipListCard.AddShip(maulShipCard);
            colShipListCard.AddShip(foiceShipCard);
            colShipListCard.AddShip(gladiusShipCard);
            colShipListCard.AddShip(haldberdShipCard);
            colShipListCard.AddShip(aesirShipCard);
            colShipListCard.AddShip(vanirShipCard);
            colShipListCard.AddShip(jotunnShipCard);
            colShipListCard.AddShip(gungnirShipCard);
            colShipListCard.AddShip(brimirShipCard);
            //colShipListCard.AddShip(raptorFrShipCard);

            AddCard(colShipListCard);

            // Cylon

            // Raider

            // 2016
            List<ShipSlotCard> catalogueRaiderShipSlotCard = new List<ShipSlotCard>();
            catalogueRaiderShipSlotCard.Add(new ShipSlotCard(1, 0, ShipSlotType.weapon, "bullet01", 49813));
            catalogueRaiderShipSlotCard.Add(new ShipSlotCard(1, 1, ShipSlotType.weapon, "bullet02", 50321));
            catalogueRaiderShipSlotCard.Add(new ShipSlotCard(1, 2, ShipSlotType.weapon, "bullet03", 19778));
            catalogueRaiderShipSlotCard.Add(new ShipSlotCard(1, 3, ShipSlotType.engine, "", 44673));
            catalogueRaiderShipSlotCard.Add(new ShipSlotCard(1, 4, ShipSlotType.engine, "", 44673));
            catalogueRaiderShipSlotCard.Add(new ShipSlotCard(1, 5, ShipSlotType.hull, "", 44673));
            catalogueRaiderShipSlotCard.Add(new ShipSlotCard(1, 6, ShipSlotType.hull, "", 44673));
            catalogueRaiderShipSlotCard.Add(new ShipSlotCard(1, 7, ShipSlotType.computer, "", 44673));
            catalogueRaiderShipSlotCard.Add(new ShipSlotCard(1, 8, ShipSlotType.computer, "", 44673));
            catalogueRaiderShipSlotCard.Add(new ShipSlotCard(1, 9, ShipSlotType.engine, "", 44673));
            catalogueRaiderShipSlotCard.Add(new ShipSlotCard(1, 10, ShipSlotType.engine, "", 44673));
            catalogueRaiderShipSlotCard.Add(new ShipSlotCard(1, 13, ShipSlotType.ship_paint, "", 44673));
            catalogueRaiderShipSlotCard.Add(new ShipSlotCard(1, 11, ShipSlotType.engine, "", 44673));
            catalogueRaiderShipSlotCard.Add(new ShipSlotCard(1, 12, ShipSlotType.weapon, "elitebullet04", 27288));
            catalogueRaiderShipSlotCard.Add(new ShipSlotCard(1, 14, ShipSlotType.avionics, "", 44673));

            // 2016
            Dictionary<ObjectStat, float> catalogueRaiderStats = new Dictionary<ObjectStat, float>();
            catalogueRaiderStats.Add(ObjectStat.Acceleration, 13.5f);
            catalogueRaiderStats.Add(ObjectStat.AccelerationMultiplierOnBoost, 1.5f);
            catalogueRaiderStats.Add(ObjectStat.ArmorValue, 5f);
            catalogueRaiderStats.Add(ObjectStat.Avoidance, 510f);
            catalogueRaiderStats.Add(ObjectStat.AvoidanceFading, 0.75f);
            catalogueRaiderStats.Add(ObjectStat.BoostCost, 0.5f);
            catalogueRaiderStats.Add(ObjectStat.BoostSpeed, 90f);
            catalogueRaiderStats.Add(ObjectStat.CriticalDefense, 80f);
            catalogueRaiderStats.Add(ObjectStat.FtlCharge, 15f);
            catalogueRaiderStats.Add(ObjectStat.FtlCooldown, 35f);
            catalogueRaiderStats.Add(ObjectStat.FtlCost, 1f);
            catalogueRaiderStats.Add(ObjectStat.FtlRange, 90f);
            catalogueRaiderStats.Add(ObjectStat.FirewallRating, 100f);
            catalogueRaiderStats.Add(ObjectStat.MaxHullPoints, 450f);
            catalogueRaiderStats.Add(ObjectStat.HullRecovery, 2.5f);
            catalogueRaiderStats.Add(ObjectStat.MaxPowerPoints, 100f);
            catalogueRaiderStats.Add(ObjectStat.PowerRecovery, 5f);
            catalogueRaiderStats.Add(ObjectStat.PenetrationStrength, 100f);
            catalogueRaiderStats.Add(ObjectStat.Speed, 55f);
            catalogueRaiderStats.Add(ObjectStat.InertiaCompensation, 175f);
            catalogueRaiderStats.Add(ObjectStat.PitchAcceleration, 55f);
            catalogueRaiderStats.Add(ObjectStat.PitchMaxSpeed, 52f);
            catalogueRaiderStats.Add(ObjectStat.YawAcceleration, 55f);
            catalogueRaiderStats.Add(ObjectStat.YawMaxSpeed, 52f);
            catalogueRaiderStats.Add(ObjectStat.RollAcceleration, 748f);
            catalogueRaiderStats.Add(ObjectStat.RollMaxSpeed, 182f);
            catalogueRaiderStats.Add(ObjectStat.StrafeAcceleration, 0f);
            catalogueRaiderStats.Add(ObjectStat.StrafeMaxSpeed, 0f);
            catalogueRaiderStats.Add(ObjectStat.DetectionInnerRadius, 1000f);
            catalogueRaiderStats.Add(ObjectStat.DetectionOuterRadius, 2000f);
            catalogueRaiderStats.Add(ObjectStat.DetectionVisualRadius, 200f);

            ShipCard catalogueRaiderShipCard = new ShipCard(117312163, CardView.Ship, 117312163, 0, 2, 1, 1, 0, 9000, 1, new ShipRole[1] { ShipRole.Assault }, ShipRoleDeprecated.Fighter, "ship_raider_paperdoll_layouts", catalogueRaiderShipSlotCard, false, new List<uint>(), -1, new ObjectStats(catalogueRaiderStats), Faction.Cylon, new List<ShipImmutableSlot>());
            GUICard catalogue_ship_raider_GuiCard = new GUICard(117312163, CardView.GUI, "ship_raider", 0, "GUI/AbilityToolbar/abilities_atlas", 10, "", "GUI/Slots/CylonT1Fighter", "", new string[0] { });
            ShopItemCard catalogueRaiderShopItemCard = new ShopItemCard(117312163, CardView.Price, ShopCategory.Ship, ShopItemType.Ship, 1, new string[0], 0, new Price(), new Price(), new Price(), Faction.Cylon, false);
            WorldCard catalogueRaiderWorldCard = new WorldCard(117312163, CardView.World, "CylonT1Fighter", 0, 6, new SpotDesc[0], "gui/map/map_objects", 1, 19, true, true, false);

            AddCard(catalogueRaiderShipCard);
            AddCard(catalogue_ship_raider_GuiCard);
            AddCard(catalogueRaiderShopItemCard);
            AddCard(catalogueRaiderWorldCard);

            // Raider

            // 2016
            List<ShipSlotCard> raiderShipSlotCard = new List<ShipSlotCard>();
            raiderShipSlotCard.Add(new ShipSlotCard(1, 0, ShipSlotType.weapon, "bullet01", 49813));
            raiderShipSlotCard.Add(new ShipSlotCard(1, 1, ShipSlotType.weapon, "bullet02", 50321));
            raiderShipSlotCard.Add(new ShipSlotCard(1, 2, ShipSlotType.weapon, "bullet03", 19778));
            raiderShipSlotCard.Add(new ShipSlotCard(1, 3, ShipSlotType.engine, "", 44673));
            raiderShipSlotCard.Add(new ShipSlotCard(1, 4, ShipSlotType.engine, "", 44673));
            raiderShipSlotCard.Add(new ShipSlotCard(1, 5, ShipSlotType.hull, "", 44673));
            raiderShipSlotCard.Add(new ShipSlotCard(1, 6, ShipSlotType.hull, "", 44673));
            raiderShipSlotCard.Add(new ShipSlotCard(1, 7, ShipSlotType.computer, "", 44673));
            raiderShipSlotCard.Add(new ShipSlotCard(1, 8, ShipSlotType.computer, "", 44673));
            raiderShipSlotCard.Add(new ShipSlotCard(1, 9, ShipSlotType.engine, "", 44673));
            raiderShipSlotCard.Add(new ShipSlotCard(1, 10, ShipSlotType.engine, "", 44673));
            raiderShipSlotCard.Add(new ShipSlotCard(1, 13, ShipSlotType.ship_paint, "", 44673));
            raiderShipSlotCard.Add(new ShipSlotCard(1, 11, ShipSlotType.engine, "", 44673));
            raiderShipSlotCard.Add(new ShipSlotCard(1, 12, ShipSlotType.weapon, "elitebullet04", 27288));
            raiderShipSlotCard.Add(new ShipSlotCard(1, 14, ShipSlotType.avionics, "", 44673));

            // 2016
            Dictionary<ObjectStat, float> raiderStats = new Dictionary<ObjectStat, float>();
            raiderStats.Add(ObjectStat.Acceleration, 13.5f);
            raiderStats.Add(ObjectStat.AccelerationMultiplierOnBoost, 1.5f);
            raiderStats.Add(ObjectStat.ArmorValue, 5f);
            raiderStats.Add(ObjectStat.Avoidance, 510f);
            raiderStats.Add(ObjectStat.AvoidanceFading, 0.75f);
            raiderStats.Add(ObjectStat.BoostCost, 0.5f);
            raiderStats.Add(ObjectStat.BoostSpeed, 90f);
            raiderStats.Add(ObjectStat.CriticalDefense, 80f);
            raiderStats.Add(ObjectStat.FtlCharge, 15f);
            raiderStats.Add(ObjectStat.FtlCooldown, 35f);
            raiderStats.Add(ObjectStat.FtlCost, 1f);
            raiderStats.Add(ObjectStat.FtlRange, 90f);
            raiderStats.Add(ObjectStat.FirewallRating, 100f);
            raiderStats.Add(ObjectStat.MaxHullPoints, 450f);
            raiderStats.Add(ObjectStat.HullRecovery, 2.5f);
            raiderStats.Add(ObjectStat.MaxPowerPoints, 100f);
            raiderStats.Add(ObjectStat.PowerRecovery, 5f);
            raiderStats.Add(ObjectStat.PenetrationStrength, 100f);
            raiderStats.Add(ObjectStat.Speed, 55f);
            raiderStats.Add(ObjectStat.InertiaCompensation, 175f);
            raiderStats.Add(ObjectStat.PitchAcceleration, 55f);
            raiderStats.Add(ObjectStat.PitchMaxSpeed, 52f);
            raiderStats.Add(ObjectStat.YawAcceleration, 55f);
            raiderStats.Add(ObjectStat.YawMaxSpeed, 52f);
            raiderStats.Add(ObjectStat.RollAcceleration, 748f);
            raiderStats.Add(ObjectStat.RollMaxSpeed, 182f);
            raiderStats.Add(ObjectStat.StrafeAcceleration, 0f);
            raiderStats.Add(ObjectStat.StrafeMaxSpeed, 0f);
            raiderStats.Add(ObjectStat.DetectionInnerRadius, 1000f);
            raiderStats.Add(ObjectStat.DetectionOuterRadius, 2000f);
            raiderStats.Add(ObjectStat.DetectionVisualRadius, 200f);

            ShipCard raiderShipCard = new ShipCard(1427261742, CardView.Ship, 117312163, 1, 2, 1, 1, 1808985911, 9000, 1, new ShipRole[1] { ShipRole.Assault }, ShipRoleDeprecated.Fighter, "ship_raider_paperdoll_layouts", raiderShipSlotCard, false, new List<uint>(), -1, new ObjectStats(raiderStats), Faction.Cylon, new List<ShipImmutableSlot>());
			GUICard ship_raider_GuiCard = new GUICard(1427261742, CardView.GUI, "ship_raider", 1, "GUI/AbilityToolbar/abilities_atlas", 10, "", "GUI/Slots/CylonT1Fighter", "", new string[0] {});
			ShopItemCard raiderShopItemCard = new ShopItemCard(1427261742, CardView.Price, ShopCategory.Ship, ShopItemType.Ship, 1, new string[0], 0, new Price(), new Price(), new Price(), Faction.Cylon, false);
            WorldCard raiderWorldCard = new WorldCard(1427261742, CardView.World, "CylonT1Fighter", 0, 6, new SpotDesc[0], "gui/map/map_objects", 1, 19, true, true, false);

            AddCard(raiderShipCard);
            AddCard(ship_raider_GuiCard);
            AddCard(raiderShopItemCard);
            AddCard(raiderWorldCard);			

            // Advanced Raider

            // 2016
            List<ShipSlotCard> advRaiderShipSlotCard = new List<ShipSlotCard>();
            advRaiderShipSlotCard.Add(new ShipSlotCard(1, 0, ShipSlotType.weapon, "bullet01", 49813));
            advRaiderShipSlotCard.Add(new ShipSlotCard(1, 1, ShipSlotType.weapon, "bullet02", 50321));
            advRaiderShipSlotCard.Add(new ShipSlotCard(1, 2, ShipSlotType.weapon, "bullet03", 19778));
            advRaiderShipSlotCard.Add(new ShipSlotCard(1, 3, ShipSlotType.engine, "", 44673));
            advRaiderShipSlotCard.Add(new ShipSlotCard(1, 4, ShipSlotType.engine, "", 44673));
            advRaiderShipSlotCard.Add(new ShipSlotCard(1, 5, ShipSlotType.hull, "", 44673));
            advRaiderShipSlotCard.Add(new ShipSlotCard(1, 6, ShipSlotType.hull, "", 44673));
            advRaiderShipSlotCard.Add(new ShipSlotCard(1, 7, ShipSlotType.computer, "", 44673));
            advRaiderShipSlotCard.Add(new ShipSlotCard(1, 8, ShipSlotType.computer, "", 44673));
            advRaiderShipSlotCard.Add(new ShipSlotCard(1, 9, ShipSlotType.engine, "", 44673));
            advRaiderShipSlotCard.Add(new ShipSlotCard(1, 10, ShipSlotType.engine, "", 44673));
            advRaiderShipSlotCard.Add(new ShipSlotCard(1, 13, ShipSlotType.ship_paint, "", 44673));
            advRaiderShipSlotCard.Add(new ShipSlotCard(1, 11, ShipSlotType.engine, "", 44673));
            advRaiderShipSlotCard.Add(new ShipSlotCard(1, 12, ShipSlotType.weapon, "elitebullet04", 27288));
            advRaiderShipSlotCard.Add(new ShipSlotCard(1, 14, ShipSlotType.avionics, "", 44673));

            // 2016
            Dictionary<ObjectStat, float> advRaiderStats = new Dictionary<ObjectStat, float>();
            advRaiderStats.Add(ObjectStat.Acceleration, 16.2f);
            advRaiderStats.Add(ObjectStat.AccelerationMultiplierOnBoost, 1.75f);
            advRaiderStats.Add(ObjectStat.ArmorValue, 5f);
            advRaiderStats.Add(ObjectStat.Avoidance, 510f);
            advRaiderStats.Add(ObjectStat.AvoidanceFading, 0.75f);
            advRaiderStats.Add(ObjectStat.BoostCost, 0.5f);
            advRaiderStats.Add(ObjectStat.BoostSpeed, 110f);
            advRaiderStats.Add(ObjectStat.CriticalDefense, 80f);
            advRaiderStats.Add(ObjectStat.FtlCharge, 15f);
            advRaiderStats.Add(ObjectStat.FtlCooldown, 35f);
            advRaiderStats.Add(ObjectStat.FtlCost, 1f);
            advRaiderStats.Add(ObjectStat.FtlRange, 90f);
            advRaiderStats.Add(ObjectStat.FirewallRating, 100f);
            advRaiderStats.Add(ObjectStat.MaxHullPoints, 585f);
            advRaiderStats.Add(ObjectStat.HullRecovery, 4.5f);
            advRaiderStats.Add(ObjectStat.MaxPowerPoints, 150f);
            advRaiderStats.Add(ObjectStat.PowerRecovery, 5f);
            advRaiderStats.Add(ObjectStat.PenetrationStrength, 100f);
            advRaiderStats.Add(ObjectStat.Speed, 60f);
            advRaiderStats.Add(ObjectStat.InertiaCompensation, 175f);
            advRaiderStats.Add(ObjectStat.PitchAcceleration, 55f);
            advRaiderStats.Add(ObjectStat.PitchMaxSpeed, 55f);
            advRaiderStats.Add(ObjectStat.YawAcceleration, 55f);
            advRaiderStats.Add(ObjectStat.YawMaxSpeed, 55f);
            advRaiderStats.Add(ObjectStat.RollAcceleration, 748f);
            advRaiderStats.Add(ObjectStat.RollMaxSpeed, 192.5f);
            advRaiderStats.Add(ObjectStat.StrafeAcceleration, 0f);
            advRaiderStats.Add(ObjectStat.StrafeMaxSpeed, 0f);
            advRaiderStats.Add(ObjectStat.DetectionInnerRadius, 1000f);
            advRaiderStats.Add(ObjectStat.DetectionOuterRadius, 2000f);
            advRaiderStats.Add(ObjectStat.DetectionVisualRadius, 200f);

            ShipCard advRaiderShipCard = new ShipCard(1808985911, CardView.Ship, 117312163, 2, 2, 1, 1, 0, 9000, 1, new ShipRole[1] { ShipRole.Assault }, ShipRoleDeprecated.Fighter, "ship_raider_paperdoll_layouts", advRaiderShipSlotCard, false, new List<uint>(), -1, new ObjectStats(advRaiderStats), Faction.Cylon, new List<ShipImmutableSlot>());
            GUICard advRaiderGuiCard = new GUICard(1808985911, CardView.GUI, "ship_raider", 2, "GUI/AbilityToolbar/abilities_atlas", 10, "", "GUI/Slots/cylont1fighter", "", new string[0]);
            ShopItemCard advRaiderShopItemCard = new ShopItemCard(1808985911, CardView.Price, ShopCategory.Ship, ShopItemType.Ship, 1, new string[0], 0, new Price(), new Price(), new Price(), Faction.Cylon, false);
            WorldCard advRaiderWorldCard = new WorldCard(1808985911, CardView.World, "CylonT1Fighter", 0, 6, new SpotDesc[0], "gui/map/map_objects", 1, 19, true, true, false);

            AddCard(advRaiderShipCard);
            AddCard(advRaiderGuiCard);
            AddCard(advRaiderShopItemCard);
            AddCard(advRaiderWorldCard);

            // Heavy Raider
            ShipCard heavyRaiderShipCard = new ShipCard(22131202, CardView.Ship, 92, 2, 2, 1, 4, 0, 5000, 1, new ShipRole[1] { ShipRole.Command }, ShipRoleDeprecated.Command, "ship_heavy_raider_paperdoll_layouts", new List<ShipSlotCard>(), false, new List<uint>(), -1, new ObjectStats(new Dictionary<ObjectStat, float>()), Faction.Cylon, new List<ShipImmutableSlot>());
            GUICard heavyRaiderGuiCard = new GUICard(22131202, CardView.GUI, "ship_heavy_raider", 2, "GUI/AbilityToolbar/abilities_atlas", 0, "", "GUI/Slots/cylon1command", "", new string[0]);
            ShopItemCard heavyRaiderShopItemCard = new ShopItemCard(22131202, CardView.Price, ShopCategory.Ship, ShopItemType.Ship, 1, new string[0], 1, new Price(), new Price(), new Price(), Faction.Cylon, false);
            WorldCard heavyRaiderWorldCard = new WorldCard(22131202, CardView.World, "CylonT1Command", 0, 6, new SpotDesc[0], "gui/map/map_objects", 1, 19, true, true, false);

            AddCard(heavyRaiderShipCard);
            AddCard(heavyRaiderGuiCard);
            AddCard(heavyRaiderShopItemCard);
            AddCard(heavyRaiderWorldCard);

            // Marauder
            ShipCard marauderShipCard = new ShipCard(22131204, CardView.Ship, 94, 1, 2, 1, 7, 0, 9000, 1, new ShipRole[1] { ShipRole.Bomber }, ShipRoleDeprecated.Defender, "ship_scout_paperdoll_layouts", new List<ShipSlotCard>(), false, new List<uint>(), -1, new ObjectStats(new Dictionary<ObjectStat, float>()), Faction.Cylon, new List<ShipImmutableSlot>());
            GUICard marauderGuiCard = new GUICard(22131204, CardView.GUI, "ship_scout", 1, "GUI/AbilityToolbar/abilities_atlas", 0, "", "GUI/Slots/cylont1defender", "", new string[0]);
            ShopItemCard marauderShopItemCard = new ShopItemCard(22131204, CardView.Price, ShopCategory.Ship, ShopItemType.Ship, 1, new string[0], 3, new Price(), new Price(), new Price(), Faction.Cylon, false);
            WorldCard marauderWorldCard = new WorldCard(22131204, CardView.World, "CylonT1Defender", 0, 6, new SpotDesc[0], "gui/map/map_objects", 1, 19, true, true, false);

            AddCard(marauderShipCard);
            AddCard(marauderGuiCard);
            AddCard(marauderShopItemCard);
            AddCard(marauderWorldCard);

            // War Raider MK2
            ShipCard warRaiderMk2ShipCard = new ShipCard(22131206, CardView.Ship, 96, 1, 2, 1, 16, 0, 9000, 1, new ShipRole[1] { ShipRole.Bomber }, ShipRoleDeprecated.Defender, "ship_war_raider_mk2_paperdoll_layouts", new List<ShipSlotCard>(), false, new List<uint>(), -1, new ObjectStats(new Dictionary<ObjectStat, float>()), Faction.Cylon, new List<ShipImmutableSlot>());
            GUICard warRaiderMk2GuiCard = new GUICard(22131206, CardView.GUI, "ship_war_raider_mk2", 1, "GUI/AbilityToolbar/abilities_atlas", 0, "", "GUI/Slots/cylont1multi2", "", new string[0]);
            ShopItemCard warRaiderMk2ShopItemCard = new ShopItemCard(22131206, CardView.Price, ShopCategory.Ship, ShopItemType.Ship, 1, new string[0], 2, new Price(), new Price(), new Price(), Faction.Cylon, false);
            WorldCard warRaiderMk2WorldCard = new WorldCard(22131206, CardView.World, "CylonT1Multi2", 0, 6, new SpotDesc[0], "gui/map/map_objects", 1, 19, true, true, false);

            AddCard(warRaiderMk2ShipCard);
            AddCard(warRaiderMk2GuiCard);
            AddCard(warRaiderMk2ShopItemCard);
            AddCard(warRaiderMk2WorldCard);

            // War Raider
            Dictionary<ObjectStat, float> advWarRaiderStats = new Dictionary<ObjectStat, float>();
            advWarRaiderStats.Add(ObjectStat.MaxHullPoints, 585);
            advWarRaiderStats.Add(ObjectStat.HullRecovery, 4.5f);
            advWarRaiderStats.Add(ObjectStat.ArmorValue, 5);
            advWarRaiderStats.Add(ObjectStat.CriticalDefense, 80);
            advWarRaiderStats.Add(ObjectStat.Avoidance, 510);
            advWarRaiderStats.Add(ObjectStat.TurnSpeed, 50);
            advWarRaiderStats.Add(ObjectStat.TurnAcceleration, 55);
            advWarRaiderStats.Add(ObjectStat.InertiaCompensation, 100);
            advWarRaiderStats.Add(ObjectStat.Acceleration, 12);
            advWarRaiderStats.Add(ObjectStat.Speed, 55);
            advWarRaiderStats.Add(ObjectStat.BoostSpeed, 85);
            advWarRaiderStats.Add(ObjectStat.BoostCost, 0.75f);
            advWarRaiderStats.Add(ObjectStat.FtlRange, 90f); //4.5f * 20
            advWarRaiderStats.Add(ObjectStat.FtlCharge, 15);
            advWarRaiderStats.Add(ObjectStat.FtlCost, 30);
            advWarRaiderStats.Add(ObjectStat.MaxPowerPoints, 150);
            advWarRaiderStats.Add(ObjectStat.PowerRecovery, 5);
            advWarRaiderStats.Add(ObjectStat.FirewallRating, 100);
            advWarRaiderStats.Add(ObjectStat.DradisRange, 2000);
            advWarRaiderStats.Add(ObjectStat.DetectionVisualRadius, 200);
            advWarRaiderStats.Add(ObjectStat.DetectionInnerRadius, 1000);
            advWarRaiderStats.Add(ObjectStat.DetectionOuterRadius, 4000);

            advWarRaiderStats.Add(ObjectStat.StrafeAcceleration, 145);
            advWarRaiderStats.Add(ObjectStat.StrafeMaxSpeed, 40);
            advWarRaiderStats.Add(ObjectStat.PitchMaxSpeed, 65);
            advWarRaiderStats.Add(ObjectStat.PitchAcceleration, 120);
            advWarRaiderStats.Add(ObjectStat.YawMaxSpeed, 65);
            advWarRaiderStats.Add(ObjectStat.YawAcceleration, 120);
            advWarRaiderStats.Add(ObjectStat.RollMaxSpeed, 135);
            advWarRaiderStats.Add(ObjectStat.RollAcceleration, 120);

            List<ShipSlotCard> advWarRaiderShipSlots = new List<ShipSlotCard>();

            List<ShipImmutableSlot> advWarRaiderShipImmSlots = new List<ShipImmutableSlot>();

            ShipCard advWarRaiderShipCard = new ShipCard(22131208, CardView.Ship, 100, 2, 2, 1, 11, 0, 9000, 1, new ShipRole[1] { ShipRole.Assault }, ShipRoleDeprecated.Fighter, "ship_raider_1b_paperdoll_layouts", advWarRaiderShipSlots, false, new List<uint>(), -1, new ObjectStats(advWarRaiderStats), Faction.Cylon, advWarRaiderShipImmSlots);
            GUICard advWarRaiderGuiCard = new GUICard(22131208, CardView.GUI, "ship_raider_1b", 2, "GUI/AbilityToolbar/abilities_atlas", 10, "", "GUI/Slots/warraider", "", new string[0]);
            ShopItemCard advWarRaiderShopItemCard = new ShopItemCard(22131208, CardView.Price, ShopCategory.Ship, ShopItemType.Ship, 2, new string[0], 4, new Price(), new Price(), new Price(), Faction.Cylon, false);
            CameraCard advWarRaiderCameraCard = new CameraCard(22131208, CardView.Camera, 20, 40, 10, 20, 20);
            WorldCard advWarRaiderWorldCard = new WorldCard(22131208, CardView.World, "CylonT1Merit", 0, 6, new SpotDesc[0], "gui/map/map_objects", 1, 19, true, true, false);
            ShipLightCard advWarRaiderShipLightCard = new ShipLightCard(22131208, CardView.ShipLight, 100, 1, new ShipRole[1] { ShipRole.Assault }, ShipRoleDeprecated.Fighter);

            MovementCard advWarRaiderMovementCard = new MovementCard(22131208, CardView.Movement, 6.5f, 360f, 40f, 2f, 2f, 2f);

            AddCard(advWarRaiderShipCard);
            AddCard(advWarRaiderGuiCard);
            AddCard(advWarRaiderShopItemCard);
            AddCard(advWarRaiderCameraCard);
            AddCard(advWarRaiderWorldCard);
            AddCard(advWarRaiderShipLightCard);
            AddCard(advWarRaiderMovementCard);

            // Malefactor
            Dictionary<ObjectStat, float> malefactorStats = new Dictionary<ObjectStat, float>();
            malefactorStats.Add(ObjectStat.MaxHullPoints, 420);
            malefactorStats.Add(ObjectStat.HullRecovery, 3.6f);
            malefactorStats.Add(ObjectStat.ArmorValue, 0);
            malefactorStats.Add(ObjectStat.CriticalDefense, 60);
            malefactorStats.Add(ObjectStat.Avoidance, 520);
            malefactorStats.Add(ObjectStat.TurnSpeed, 53.65f);
            malefactorStats.Add(ObjectStat.TurnAcceleration, 82.5f);
            malefactorStats.Add(ObjectStat.InertiaCompensation, 175);
            malefactorStats.Add(ObjectStat.Acceleration, 15);
            malefactorStats.Add(ObjectStat.Speed, 85);
            malefactorStats.Add(ObjectStat.BoostSpeed, 110);
            malefactorStats.Add(ObjectStat.BoostCost, 0.5f);
            malefactorStats.Add(ObjectStat.FtlRange, 115f); //5.75f * 20
            malefactorStats.Add(ObjectStat.FtlCharge, 15);
            malefactorStats.Add(ObjectStat.FtlCost, 20);
            malefactorStats.Add(ObjectStat.MaxPowerPoints, 150);
            malefactorStats.Add(ObjectStat.PowerRecovery, 3.6f);
            malefactorStats.Add(ObjectStat.FirewallRating, 100);
            malefactorStats.Add(ObjectStat.DradisRange, 2750);
            malefactorStats.Add(ObjectStat.DetectionVisualRadius, 225);
            malefactorStats.Add(ObjectStat.DetectionInnerRadius, 1000);
            malefactorStats.Add(ObjectStat.DetectionOuterRadius, 4000);

            malefactorStats.Add(ObjectStat.StrafeAcceleration, 145);
            malefactorStats.Add(ObjectStat.StrafeMaxSpeed, 40);
            malefactorStats.Add(ObjectStat.PitchMaxSpeed, 65);
            malefactorStats.Add(ObjectStat.PitchAcceleration, 120);
            malefactorStats.Add(ObjectStat.YawMaxSpeed, 65);
            malefactorStats.Add(ObjectStat.YawAcceleration, 120);
            malefactorStats.Add(ObjectStat.RollMaxSpeed, 135);
            malefactorStats.Add(ObjectStat.RollAcceleration, 120);

            ShipCard malefactorShipCard = new ShipCard(22131210, CardView.Ship, 98, 1, 2, 1, 17, 0, 9000, 1, new ShipRole[1] { ShipRole.Stealth }, ShipRoleDeprecated.Stealth, "ship_cylon_strike_stealth_paperdoll_layouts", new List<ShipSlotCard>(), false, new List<uint>(), -1, new ObjectStats(malefactorStats), Faction.Cylon, new List<ShipImmutableSlot>());
            GUICard malefactorGuiCard = new GUICard(22131210, CardView.GUI, "ship_cylon_strike_stealth", 1, "GUI/AbilityToolbar/abilities_atlas", 0, "", "GUI/Slots/cylont1stealth", "", new string[0]);
            ShopItemCard malefactorShopItemCard = new ShopItemCard(22131210, CardView.Price, ShopCategory.Ship, ShopItemType.Ship, 1, new string[0], 5, new Price(), new Price(), new Price(), Faction.Cylon, false);
            CameraCard malefactorCameraCard = new CameraCard(22131210, CardView.Camera, 20, 40, 10, 20, 20);
            WorldCard malefactorWorldCard = new WorldCard(22131210, CardView.World, "CylonT1Stealth", 0, 6, new SpotDesc[0], "gui/map/map_objects", 1, 19, true, true, false);
            ShipLightCard malefactorShipLightCard = new ShipLightCard(22131210, CardView.ShipLight, 98, 1, new ShipRole[1] { ShipRole.Stealth }, ShipRoleDeprecated.Stealth);
            MovementCard malefactorMovementCard = new MovementCard(22131210, CardView.Movement, 6.5f, 360f, 40f, 2f, 2f, 2f);

            AddCard(malefactorShipCard);
            AddCard(malefactorGuiCard);
            AddCard(malefactorShopItemCard);
            AddCard(malefactorCameraCard);
            AddCard(malefactorWorldCard);
            AddCard(malefactorShipLightCard);
            AddCard(malefactorMovementCard);

            // Banshee
            ShipCard bansheeShipCard = new ShipCard(22131212, CardView.Ship, 104, 1, 2, 10, 2, 0, 9000, 2, new ShipRole[1] { ShipRole.Fighter }, ShipRoleDeprecated.Fighter, "ship_wrath_paperdoll_layouts", new List<ShipSlotCard>(), false, new List<uint>(), -1, new ObjectStats(new Dictionary<ObjectStat, float>()), Faction.Cylon, new List<ShipImmutableSlot>());
            GUICard bansheeGuiCard = new GUICard(22131212, CardView.GUI, "ship_wrath", 1, "GUI/AbilityToolbar/abilities_atlas", 0, "", "GUI/Slots/cylont2fighter", "", new string[0]);
            ShopItemCard bansheeShopItemCard = new ShopItemCard(22131212, CardView.Price, ShopCategory.Ship, ShopItemType.Ship, 1, new string[0], 6, new Price(), new Price(), new Price(), Faction.Cylon, false);
            WorldCard bansheeWorldCard = new WorldCard(22131212, CardView.World, "CylonT2Fighter", 0, 6, new SpotDesc[0], "gui/map/map_objects", 4, 19, true, true, false);

            AddCard(bansheeShipCard);
            AddCard(bansheeGuiCard);
            AddCard(bansheeShopItemCard);
            AddCard(bansheeWorldCard);

            // Wraith
            ShipCard wraithShipCard = new ShipCard(22131214, CardView.Ship, 102, 1, 2, 10, 8, 0, 9000, 2, new ShipRole[1] { ShipRole.Bomber }, ShipRoleDeprecated.Defender, "ship_banshee_paperdoll_layouts", new List<ShipSlotCard>(), false, new List<uint>(), -1, new ObjectStats(new Dictionary<ObjectStat, float>()), Faction.Cylon, new List<ShipImmutableSlot>());
            GUICard wraithGuiCard = new GUICard(22131214, CardView.GUI, "ship_banshee", 1, "GUI/AbilityToolbar/abilities_atlas", 0, "", "GUI/Slots/cylont2defender", "", new string[0]);
            ShopItemCard wraithShopItemCard = new ShopItemCard(22131214, CardView.Price, ShopCategory.Ship, ShopItemType.Ship, 1, new string[0], 7, new Price(), new Price(), new Price(), Faction.Cylon, false);
            WorldCard wraithWorldCard = new WorldCard(22131214, CardView.World, "CylonT2Defender", 0, 6, new SpotDesc[0], "gui/map/map_objects", 4, 19, true, true, false);

            AddCard(wraithShipCard);
            AddCard(wraithGuiCard);
            AddCard(wraithShopItemCard);
            AddCard(wraithWorldCard);

            // Spectre
            ShipCard spectreShipCard = new ShipCard(22131216, CardView.Ship, 106, 1, 2, 10, 5, 0, 9000, 2, new ShipRole[1] { ShipRole.Command }, ShipRoleDeprecated.Command, "ship_spectre_paperdoll_layouts", new List<ShipSlotCard>(), false, new List<uint>(), -1, new ObjectStats(new Dictionary<ObjectStat, float>()), Faction.Cylon, new List<ShipImmutableSlot>());
            GUICard spectreGuiCard = new GUICard(22131216, CardView.GUI, "ship_spectre", 1, "GUI/AbilityToolbar/abilities_atlas", 0, "", "GUI/Slots/cylont2command", "", new string[0]);
            ShopItemCard spectreShopItemCard = new ShopItemCard(22131216, CardView.Price, ShopCategory.Ship, ShopItemType.Ship, 1, new string[0], 8, new Price(), new Price(), new Price(), Faction.Cylon, false);
            WorldCard spectreWorldCard = new WorldCard(22131216, CardView.World, "CylonT2Command", 0, 6, new SpotDesc[0], "gui/map/map_objects", 4, 19, true, true, false);

            AddCard(spectreShipCard);
            AddCard(spectreGuiCard);
            AddCard(spectreShopItemCard);
            AddCard(spectreWorldCard);

            // Liche
            ShipCard licheShipCard = new ShipCard(22131218, CardView.Ship, 108, 1, 2, 10, 13, 0, 9000, 2, new ShipRole[1] { ShipRole.Assault }, ShipRoleDeprecated.Multi, "ship_liche_paperdoll_layouts", new List<ShipSlotCard>(), false, new List<uint>(), -1, new ObjectStats(new Dictionary<ObjectStat, float>()), Faction.Cylon, new List<ShipImmutableSlot>());
            GUICard licheGuiCard = new GUICard(22131218, CardView.GUI, "ship_liche", 1, "GUI/AbilityToolbar/abilities_atlas", 0, "", "GUI/Slots/cylont2merit", "", new string[0]);
            ShopItemCard licheShopItemCard = new ShopItemCard(22131218, CardView.Price, ShopCategory.Ship, ShopItemType.Ship, 1, new string[0], 9, new Price(), new Price(), new Price(), Faction.Cylon, false);
            WorldCard licheWorldCard = new WorldCard(22131218, CardView.World, "CylonT2Merit", 0, 6, new SpotDesc[0], "gui/map/map_objects", 4, 19, true, true, false);

            AddCard(licheShipCard);
            AddCard(licheGuiCard);
            AddCard(licheShopItemCard);
            AddCard(licheWorldCard);

            // Fenrir
            ShipCard fenrirShipCard = new ShipCard(22131220, CardView.Ship, 110, 1, 2, 20, 3, 0, 9000, 3, new ShipRole[1] { ShipRole.Fighter }, ShipRoleDeprecated.Fighter, "ship_nova_paperdoll_layouts", new List<ShipSlotCard>(), false, new List<uint>(), -1, new ObjectStats(new Dictionary<ObjectStat, float>()), Faction.Cylon, new List<ShipImmutableSlot>());
            GUICard fenrirGuiCard = new GUICard(22131220, CardView.GUI, "ship_nova", 1, "GUI/AbilityToolbar/abilities_atlas", 0, "", "GUI/Slots/cylont3fighter", "", new string[0]);
            ShopItemCard fenrirShopItemCard = new ShopItemCard(22131220, CardView.Price, ShopCategory.Ship, ShopItemType.Ship, 1, new string[0], 10, new Price(), new Price(), new Price(), Faction.Cylon, false);
            WorldCard fenrirWorldCard = new WorldCard(22131220, CardView.World, "CylonT3Fighter", 0, 6, new SpotDesc[0], "gui/map/map_objects", 7, 19, true, true, false);

            AddCard(fenrirShipCard);
            AddCard(fenrirGuiCard);
            AddCard(fenrirShopItemCard);
            AddCard(fenrirWorldCard);

            // Jormung
            ShipCard jormungShipCard = new ShipCard(22131222, CardView.Ship, 114, 1, 2, 20, 9, 0, 9000, 3, new ShipRole[1] { ShipRole.Destroyer }, ShipRoleDeprecated.Defender, "ship_sentinel_paperdoll_layouts", new List<ShipSlotCard>(), false, new List<uint>(), -1, new ObjectStats(new Dictionary<ObjectStat, float>()), Faction.Cylon, new List<ShipImmutableSlot>());
            GUICard jormungGuiCard = new GUICard(22131222, CardView.GUI, "ship_sentinel", 1, "GUI/AbilityToolbar/abilities_atlas", 0, "", "GUI/Slots/cylont3defender", "", new string[0]);
            ShopItemCard jormungShopItemCard = new ShopItemCard(22131222, CardView.Price, ShopCategory.Ship, ShopItemType.Ship, 1, new string[0], 12, new Price(), new Price(), new Price(), Faction.Cylon, false);
            WorldCard jormungWorldCard = new WorldCard(22131222, CardView.World, "CylonT3Defender", 0, 6, new SpotDesc[0], "gui/map/map_objects", 7, 19, true, true, false);

            AddCard(jormungShipCard);
            AddCard(jormungGuiCard);
            AddCard(jormungShopItemCard);
            AddCard(jormungWorldCard);

            // Hel
            ShipCard helShipCard = new ShipCard(22131224, CardView.Ship, 112, 1, 2, 20, 6, 0, 9000, 3, new ShipRole[1] { ShipRole.Command }, ShipRoleDeprecated.Command, "ship_phantom_paperdoll_layouts", new List<ShipSlotCard>(), false, new List<uint>(), -1, new ObjectStats(new Dictionary<ObjectStat, float>()), Faction.Cylon, new List<ShipImmutableSlot>());
            GUICard helGuiCard = new GUICard(22131224, CardView.GUI, "ship_phantom", 1, "GUI/AbilityToolbar/abilities_atlas", 0, "", "GUI/Slots/cylont3command", "", new string[0]);
            ShopItemCard helShopItemCard = new ShopItemCard(22131224, CardView.Price, ShopCategory.Ship, ShopItemType.Ship, 1, new string[0], 11, new Price(), new Price(), new Price(), Faction.Cylon, false);
            WorldCard helWorldCard = new WorldCard(22131224, CardView.World, "CylonT3Command", 0, 6, new SpotDesc[0], "gui/map/map_objects", 7, 19, true, true, false);

            AddCard(helShipCard);
            AddCard(helGuiCard);
            AddCard(helShopItemCard);
            AddCard(helWorldCard);

            // Nidhogg
            Dictionary<ObjectStat, float> nidhoggStats = new Dictionary<ObjectStat, float>();
            nidhoggStats.Add(ObjectStat.MaxHullPoints, 4550);
            nidhoggStats.Add(ObjectStat.HullRecovery, 35f);
            nidhoggStats.Add(ObjectStat.ArmorValue, 40);
            nidhoggStats.Add(ObjectStat.CriticalDefense, 100);
            nidhoggStats.Add(ObjectStat.Avoidance, 50);
            nidhoggStats.Add(ObjectStat.TurnSpeed, 10);
            nidhoggStats.Add(ObjectStat.TurnAcceleration, 10);
            nidhoggStats.Add(ObjectStat.InertiaCompensation, 50);
            nidhoggStats.Add(ObjectStat.Acceleration, 2);
            nidhoggStats.Add(ObjectStat.Speed, 30);
            nidhoggStats.Add(ObjectStat.BoostSpeed, 45);
            nidhoggStats.Add(ObjectStat.BoostCost, 8.1f);
            nidhoggStats.Add(ObjectStat.FtlRange, 200f); //10f * 20
            nidhoggStats.Add(ObjectStat.FtlCharge, 25);
            nidhoggStats.Add(ObjectStat.FtlCost, 250);
            nidhoggStats.Add(ObjectStat.MaxPowerPoints, 750);
            nidhoggStats.Add(ObjectStat.PowerRecovery, 25);
            nidhoggStats.Add(ObjectStat.FirewallRating, 150);
            nidhoggStats.Add(ObjectStat.DradisRange, 3500);
            nidhoggStats.Add(ObjectStat.DetectionVisualRadius, 350);
            nidhoggStats.Add(ObjectStat.DetectionInnerRadius, 2000);
            nidhoggStats.Add(ObjectStat.DetectionOuterRadius, 7000);

            nidhoggStats.Add(ObjectStat.StrafeAcceleration, 145);
            nidhoggStats.Add(ObjectStat.StrafeMaxSpeed, 40);
            nidhoggStats.Add(ObjectStat.PitchMaxSpeed, 10);
            nidhoggStats.Add(ObjectStat.PitchAcceleration, 30);
            nidhoggStats.Add(ObjectStat.YawMaxSpeed, 10);
            nidhoggStats.Add(ObjectStat.YawAcceleration, 30);
            nidhoggStats.Add(ObjectStat.RollMaxSpeed, 10);
            nidhoggStats.Add(ObjectStat.RollAcceleration, 30);

            ShipCard nidhoggShipCard = new ShipCard(22131226, CardView.Ship, 116, 1, 2, 20, 14, 0, 9000, 3, new ShipRole[1] { ShipRole.Command }, ShipRoleDeprecated.Multi, "ship_nidhogg_paperdoll_layouts", new List<ShipSlotCard>(), false, new List<uint>(), -1, new ObjectStats(nidhoggStats), Faction.Cylon, new List<ShipImmutableSlot>());
            GUICard nidhoggGuiCard = new GUICard(22131226, CardView.GUI, "ship_nidhogg", 1, "GUI/AbilityToolbar/abilities_atlas", 0, "", "GUI/Slots/cylont3merit", "", new string[0]);
            ShopItemCard nidhoggShopItemCard = new ShopItemCard(22131226, CardView.Price, ShopCategory.Ship, ShopItemType.Ship, 1, new string[0], 13, new Price(), new Price(), new Price(), Faction.Cylon, false);
            WorldCard nidhoggWorldCard = new WorldCard(22131226, CardView.World, "CylonT3Merit", 0, 6, new SpotDesc[0], "gui/map/map_objects", 7, 19, true, true, false);
            ShipLightCard nidhoggShipLightCard = new ShipLightCard(22131226, CardView.ShipLight, 116, 3, new ShipRole[1] { ShipRole.Command }, ShipRoleDeprecated.Multi);
            CameraCard nidhoggCameraCard = new CameraCard(22131226, CardView.Camera, 520, 540, 510, 520, 20);

            MovementCard nidhoggMovementCard = new MovementCard(22131226, CardView.Movement, 6.5f, 40f, 20f, 2f, 2f, 2f);

            AddCard(nidhoggShipCard);
            AddCard(nidhoggGuiCard);
            AddCard(nidhoggShopItemCard);
            AddCard(nidhoggWorldCard);
            AddCard(nidhoggShipLightCard);
            AddCard(nidhoggCameraCard);
            AddCard(nidhoggMovementCard);

            // Surtur
            ShipCard surturShipCard = new ShipCard(22131228, CardView.Ship, 118, 1, 2, 30, 15, 0, 9000, 4, new ShipRole[1] { ShipRole.Carrier }, ShipRoleDeprecated.Carrier, "ship_surtur_paperdoll_layouts", new List<ShipSlotCard>(), false, new List<uint>(), -1, new ObjectStats(new Dictionary<ObjectStat, float>()), Faction.Cylon, new List<ShipImmutableSlot>());
            GUICard surturGuiCard = new GUICard(22131228, CardView.GUI, "ship_surtur", 1, "GUI/AbilityToolbar/abilities_atlas", 0, "", "GUI/Slots/cylont4carrier_surtur", "", new string[0]);
            ShopItemCard surturShopItemCard = new ShopItemCard(22131228, CardView.Price, ShopCategory.Ship, ShopItemType.Ship, 1, new string[0], 14, new Price(), new Price(), new Price(), Faction.Cylon, false);
            WorldCard surturWorldCard = new WorldCard(22131228, CardView.World, "CylonT4Carrier", 0, 6, new SpotDesc[0], "", 0, 0, true, true, false);

            AddCard(surturShipCard);
            AddCard(surturGuiCard);
            AddCard(surturShopItemCard);
            AddCard(surturWorldCard);

            ShipListCard cylShipListCard = new ShipListCard(188756164u);
            cylShipListCard.AddShip(raiderShipCard);
            cylShipListCard.AddShip(heavyRaiderShipCard);
            cylShipListCard.AddShip(marauderShipCard);
            cylShipListCard.AddShip(warRaiderMk2ShipCard);
            cylShipListCard.AddShip(advWarRaiderShipCard);
            cylShipListCard.AddShip(malefactorShipCard);
            cylShipListCard.AddShip(wraithShipCard);
            cylShipListCard.AddShip(bansheeShipCard);
            cylShipListCard.AddShip(spectreShipCard);
            cylShipListCard.AddShip(licheShipCard);
            cylShipListCard.AddShip(fenrirShipCard);
            cylShipListCard.AddShip(jormungShipCard);
            cylShipListCard.AddShip(helShipCard);
            cylShipListCard.AddShip(nidhoggShipCard);
            cylShipListCard.AddShip(surturShipCard);

            AddCard(cylShipListCard);
        }
    }
}
