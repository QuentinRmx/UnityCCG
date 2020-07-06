using System.Collections.Generic;
using System.IO;
using System.Linq;
using Engine.Cards.CardEffects;
using Engine.Utils;
using Newtonsoft.Json;


namespace Engine.Cards
{
    public class CardFactory
    {
        // ATTRIBUTES

        private static CardFactory _instance;

        private List<CardInfo> _cardInfos;

        public static CardFactory Instance => _instance = _instance ?? new CardFactory();

        // CONSTRUCTORS

        private CardFactory()
        {
            LoadJsonData();
        }

        private void LoadJsonData()
        {
            string json = File.ReadAllText("Assets/Resources/Data/cardData.json");
            List<CardInfo> data = JsonConvert.DeserializeObject<List<CardInfo>>(json);

            _cardInfos = data;
        }

        // METHODS

        public Card Create(int cardId)
        {
            // TODO: Refactor factory to load card data from a file.
            CardInfo info;
            ICardEffect effect;
            info = _cardInfos.FirstOrDefault(i => i.Identifier == cardId);
            // TODO: Serialize effects.
            switch (cardId)
            {
                case 0:
                    effect = new CardEffectNone();
                    break;

                case 1:
                    effect = new CardEffectAttack(ETargetSelector.RandomEnemy);
                    break;

                case 2:
                    effect = new CardEffectAttack(ETargetSelector.AllEnemy);
                    break;
                default:
                    return null;
            }

            Card card = new Card(effect);
            info.InstanceId = InstanceIdManager.NextInstanceId;
            card.CardInfo = info;

            return card;
        }
    }
}