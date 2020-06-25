using System.Linq;
using Engine.Bridges;
using Engine.Cards;
using Engine.Cards.CardEffects;

namespace Engine.Managers
{
    public class GameManager
    {
        private readonly CombatManager _combatManager;

        private readonly IBridge _bridge;

        // ATTRIBUTES


        // CONSTRUCTOR

        public GameManager(IBridge bridge)
        {
            _bridge = bridge;
            _combatManager = new CombatManager(new BoardManager());
            CardInfo info = new CardInfo()
            {
                Attack = 25,
                Health = 30,
                Name = "Dark Spirit"
            };
            Card toAdd = new Card(new CardEffectAttack(ETargetSelector.AllEnemy));
            info.InstanceId = Card.lastInstanceId;
            toAdd.CardInfo = info;
            AddEnemy(toAdd);
            info = new CardInfo()
            {
                Attack = 10,
                Health = 10,
                Name = "Black Spirit"
            };
            toAdd = new Card(new CardEffectAttack(ETargetSelector.AllEnemy));
            info.InstanceId = Card.lastInstanceId;
            toAdd.CardInfo = info;
            AddCardToHand(toAdd);
            toAdd = new Card(new CardEffectAttack(ETargetSelector.AllEnemy));
            info.InstanceId = Card.lastInstanceId;
            toAdd.CardInfo = info;
            AddCardToHand(toAdd);
            toAdd = new Card(new CardEffectAttack(ETargetSelector.AllEnemy));
            info.InstanceId = Card.lastInstanceId;
            toAdd.CardInfo = info;
            AddCardToHand(toAdd);
            toAdd = new Card(new CardEffectAttack(ETargetSelector.AllEnemy));
            info.InstanceId = Card.lastInstanceId;
            toAdd.CardInfo = info;
            AddCardToHand(toAdd);
        }

        // METHODS

        public void PlayCard(int instanceId)
        {
            Card card = _combatManager.GetPlayerHand().FirstOrDefault(c => c.CardInfo.InstanceId == instanceId);
            if (card != null)
            {
                card.Play(_combatManager);
                // TODO: Notify board that the card needs to be removed from hand and put into sanctuary.
                _combatManager.NbCardsPlayed++;
            }
        }

        public void RerollCard(Card card)
        {
            card.Reroll(_combatManager);
            // TODO: Notify board that the card needs to be removed from hand and put into sanctuary.
            _combatManager.NbCardsRerolled++;
        }

        public void AddEnemy(Card card)
        {
            _combatManager?.AddEnemy(card);
            _bridge?.AddEnemy(card);
        }

        public void AddCardToHand(Card card)
        {
            _combatManager?.AddCardToHand(card);
            _bridge?.AddCardToPlayerHand(card);
        }

        public void RemoveEnemy(Card card)
        {
            _combatManager.RemoveEnemy(card);
        }

    }
}