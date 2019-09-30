using System;
using System.Collections.Generic;
using System.Text;

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
        }

        // All cards should be requested using this method. It will return either null or the card.
        public static Card FetchCard(uint cardGUID, CardView cardView)
        {
            if (cardGUID == 0)
            {
                return null;
            }
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
