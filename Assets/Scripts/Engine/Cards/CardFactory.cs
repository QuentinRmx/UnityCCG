using System.Collections.Generic;
using System.IO;
using System.Linq;
using Engine.Cards.Behaviors.Alive;
using Engine.Cards.CardEffects;
using Engine.Cards.Targets;
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
//            CardEffectFactory.Instance.Serialize();

            CardInfo info;

            AbstractAliveBehavior behavior;
            info = _cardInfos.FirstOrDefault(i => i.Identifier == cardId);
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
    }
}