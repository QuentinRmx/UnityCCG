using System.Linq;
using Engine.Bridges;
using Engine.Cards;
using Engine.Cards.CardEffects;

namespace Engine.Managers
{
    public class GameManager
    {
        private readonly CombatManager _combatManager;

        public CombatManager CombatManager => _combatManager;

        private readonly IBridge _bridge;

        // ATTRIBUTES


        // CONSTRUCTOR

        public GameManager(IBridge bridge)
        {
            _bridge = bridge;
            _combatManager = new CombatManager(new BoardManager());

            // TODO: Load enemies.
            AddEnemy(Card.Factory.Create(1));

            // TODO: Load player hand.
            AddCardToHand(Card.Factory.Create(0));
            AddCardToHand(Card.Factory.Create(0));
            AddCardToHand(Card.Factory.Create(0));
        }

        // METHODS

        public void PlayCard(int instanceId)
        {
            _combatManager.PlayCardFromHand(instanceId);
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

        public void EndTurn()
        {
            _combatManager.EndTurn();
        }

        public int GetTurnNumber()
        {
            return _combatManager.Turn;
        }
    }
}