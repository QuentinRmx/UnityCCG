using System.Collections.Generic;
using System.IO;
using System.Linq;
using Engine.Cards.Behaviors.Alive;
using Engine.Cards.CardEffects;
using Engine.Cards.Targets;
using Engine.JsonModels;
using Engine.Utils;
using Newtonsoft.Json;


namespace Engine.Cards
{
    public class CardFactory
    {
        // ATTRIBUTES

        private static CardFactory _instance;

        private CardsData _cardsDataJson;

        public static CardFactory Instance => _instance = _instance ?? new CardFactory();

        // CONSTRUCTORS

        private CardFactory()
        {
            LoadJsonData();
        }

        private void LoadJsonData()
        {
            string json = File.ReadAllText("Assets/Resources/Data/cardData.json");
            CardsData data = JsonConvert.DeserializeObject<CardsData>(json);
            _cardsDataJson = data;
        }

        // METHODS

        public Card Create(int cardId)
        {
            // TODO: Refactor factory to load card data from a file.
//            CardEffectFactory.Instance.Serialize();

            CardInfo info;

            AbstractAliveBehavior behavior;
            info = _cardsDataJson.CardInfos.FirstOrDefault(i => i.Identifier == cardId);
            AbstractCardEffect effect = CardEffectFactory.Instance.Create(info.CardEffectAssociated);
            // TODO: Serialize effects.
            switch (cardId)
            {
                case 0:
                    behavior = new AliveBehavior();
                    break;
                case 1:
                    behavior = new NotAliveBehavior();
                    break;
                case 2:
                    behavior = new NotAliveBehavior();
                    break;
                case 3:
                    behavior = new NotAliveBehavior();
                    break;
                case 4:
                    behavior = new NotAliveBehavior();
                    break;
                default:
                    behavior = new NotAliveBehavior();
                    return null;
            }

            Card card = new Card(effect) {AliveBehavior = behavior};
            info.InstanceId = InstanceIdManager.NextInstanceId;
            card.CardInfo = info;

            return card;
        }

        public IEnumerable<Card> GetAllCards()
        {
            List<Card> cards = new List<Card>();
            foreach (var data in _cardsDataJson.CardInfos) ;
            {
                for (int i = 0; i <= _cardsDataJson.LastIdentifier; i++)
                {
                    cards.Add(Create(i));
                }
            }

            return cards;
        }

        public void SerializeCards(IEnumerable<Card> cards)
        {
            IEnumerable<Card> enumerable = cards as Card[] ?? cards.ToArray();
            _cardsDataJson.LastIdentifier = enumerable.Max(c => c.CardInfo.Identifier);
            _cardsDataJson.CardInfos = enumerable.Select(c => c.CardInfo).ToList();
            JsonConvert.SerializeObject(_cardsDataJson);
        }
    }
}