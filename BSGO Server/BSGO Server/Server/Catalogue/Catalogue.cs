using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Numerics;

namespace BSGO_Server
{
    class Catalogue
    {
        private static Dictionary<long, Card> cards = new Dictionary<long, Card>();

        // This method will generate all "static" cards that the server is going to send to the client.
        public static void SetupCards()
        {
            RewardCard colonialBonusReward = new RewardCard(1, CardView.Reward, 0, AugmentActionType.None, "", 0);
            RewardCard cylonBonusReward = new RewardCard(2, CardView.Reward, 0, AugmentActionType.None, "", 0);

            AddCard(colonialBonusReward);
            AddCard(cylonBonusReward);

            // These two cards shouldn't be static since they are most likely set according to the database.
            // Since we are just debugging this, there's no need to hook it up even with the fake database yet.
            GUICard colonialBonus = new GUICard(colonialBonusReward.CardGUID, CardView.GUI, "bonus_faction_balance_neutral", (byte)0, "", 0, "", "", "", new string[0]);
            GUICard cylonBonus = new GUICard(cylonBonusReward.CardGUID, CardView.GUI, "bonus_faction_balance_neutral", (byte)0, "", 0, "", "", "", new string[0]);

            AddCard(colonialBonus);
            AddCard(cylonBonus);

            AvatarCatalogueCard avatarCatalogue = new AvatarCatalogueCard(109873795, CardView.AvatarCatalogue);

            AddCard(avatarCatalogue);

            Color ambientColor = Color.FromArgb(255, 100, 100, 100);
            Color fogColor = Color.FromArgb(255, 100, 100, 100);
            Color dustColor = Color.FromArgb(255, 100, 100, 100);
            BackgroundDesc backgroundDesc = new BackgroundDesc("", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0));
            BackgroundDesc starsDesc = new BackgroundDesc("", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0));
            BackgroundDesc starsMult = new BackgroundDesc("", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0));
            BackgroundDesc starsVariance = new BackgroundDesc("", new Quaternion(0, 0, 0, 0), Color.FromArgb(100, 100, 100, 100), new Vector3(0, 0, 0));
            MovingNebulaDesc[] movingNebulas = new MovingNebulaDesc[0];
            LightDesc[] lightDescs = new LightDesc[0];
            SunDesc[] sunDescs = new SunDesc[0];
            JGlobalFog jGlobalFog = new JGlobalFog(false, Color.FromArgb(0, 0, 0, 0), 0, 0);
            JCameraFx jCameraFx = new JCameraFx(false);

            SectorCard sector4 = new SectorCard(4, CardView.Sector, 1000, 1000, 1000, 4, ambientColor, fogColor, 12, dustColor, 12, backgroundDesc, starsDesc, starsMult, starsVariance, movingNebulas, lightDescs, sunDescs, jGlobalFog, jCameraFx, new string[0]);
            GUICard sector4GUI = new GUICard(4, CardView.GUI, "sector4", 0, "", 0, "", "", "", new string[0]);
            RegulationCard sector4Reg = new RegulationCard(4, CardView.Regulation, new ConsumableEffectType[0], new Dictionary<uint, HashSet<ShipAbilitySide>>(), new Dictionary<uint, HashSet<ShipAbilityTarget>>(), TargetBracketMode.Default, true);

            AddCard(sector4);
            AddCard(sector4GUI);
            AddCard(sector4Reg);

            ShipCard mk7ShipCard = new ShipCard(100, CardView.Ship, 100, 2, 2, 1, 100, 0, 9000, 1, new ShipRole[1] { ShipRole.Assault }, ShipRoleDeprecated.Fighter, "ship_gunstar_paperdoll_layouts", new List<ShipSlotCard>(), false, new List<uint>(), -1, new ObjectStats(new Dictionary<ObjectStat, float>()), Faction.Colonial, new List<ShipImmutableSlot>());
            GUICard mk7GuiCard = new GUICard(100, CardView.GUI, "gui/infojournal/ships/Human11", 0, "", 0, "gui/infojournal/ships/Human11", "gui/infojournal/ships/Human11", "gui/infojournal/ships/Human11", new string[0]);
            ShopItemCard mk7ShopItemCard = new ShopItemCard(100, CardView.Price, ShopCategory.Ship, ShopItemType.Ship, 1, new string[0], 0, new Price(), new Price(), new Price(), Faction.Colonial, false);
            CameraCard mk7CameraCard = new CameraCard(100, CardView.Camera, 20, 40, 10, 20, 20);
            WorldCard mk7WorldCard = new WorldCard(100, CardView.World, "HumanT1Merit", 0, 0, new SpotDesc[0], "", 0, 0, true, true, true);

            AddCard(mk7ShipCard);
            AddCard(mk7GuiCard);
            AddCard(mk7ShopItemCard);
            AddCard(mk7CameraCard);
            AddCard(mk7WorldCard);

            RewardCard idk2 = new RewardCard(49842157, CardView.Reward, 0, AugmentActionType.LootItem, "", 0);
            GlobalCard idk = new GlobalCard(49842157, CardView.Global, 100, 100, 100, 10, idk2, new Dictionary<int, RewardCard>());
            GUICard idk3 = new GUICard(49842157, CardView.GUI, "", (byte)0, "", 0, "", "", "", new string[0]);

            AddCard(idk2);
            AddCard(idk);
            AddCard(idk3);

            GUICard idk4 = new GUICard(130920111, CardView.GUI, "", (byte)0, "", 0, "", "", "", new string[0]);

            AddCard(idk4);
        }

        // All cards should be requested using this method. It will return either null or the card.
        public static Card FetchCard(uint cardGUID, CardView cardView)
        {
            if (cardGUID == 0)
            {
                return null;
            }
            Log.Add(LogSeverity.WARNING, string.Format("Received a card request: CardGUID={0}, CardView={1}", cardGUID, cardView));

            long key = GenerateKey(cardGUID, cardView);
            Card value;
            if (!cards.TryGetValue(key, out value))
            {
                Log.Add(LogSeverity.ERROR, string.Format("The {0}CardView({1}) isn't on the cards dictionary.", cardView, cardGUID));
            }
            return value;
        }

        private static void AddCard(Card card)
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
