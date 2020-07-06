using System.Collections.Generic;
using System.Linq;
using Engine.Bridges;
using Engine.Cards;
using Engine.Cards.CardEffects;

namespace Engine.Managers
{
    public class GameManager
    {
        public CombatManager CombatManager { get; }

        private readonly IBridge _bridge;

        // ATTRIBUTES


        // CONSTRUCTOR

        public GameManager(IBridge bridge)
        {
            _bridge = bridge;
            CombatManager = new CombatManager(new BoardManager());
            CombatManager.OnCardAddedToHand += _bridge.AddCardToPlayerHand;

            // TODO: Load enemies.
            AddEnemy(CardFactory.Instance.Create(0));

            // TODO: Load player hand.
            List<Card> deckTest = new List<Card>
            {
                CardFactory.Instance.Create(1), 
                CardFactory.Instance.Create(1), 
                CardFactory.Instance.Create(1), 
                CardFactory.Instance.Create(2),
                CardFactory.Instance.Create(2),
                CardFactory.Instance.Create(2),
                CardFactory.Instance.Create(3),
            };
            CombatManager.SetPlayerDeck(deckTest);
            CombatManager.StartTurn();
        }

        // METHODS

        public void PlayCard(int instanceId)
        {
            CombatManager.PlayCardFromHand(instanceId);
        }

        public void RerollCard(Card card)
        {
            card.Reroll(CombatManager);
            // TODO: Notify board that the card needs to be removed from hand and put into sanctuary.
            CombatManager.NbCardsRerolled++;
        }

        public void AddEnemy(Card card)
        {
            CombatManager?.AddEnemy(card);
            _bridge?.AddEnemy(card);
        }

//        public void AddCardToHand(Card card)
//        {
//            CombatManager?.AddCardToHand(card);
//            _bridge?.AddCardToPlayerHand(card);
//        }

        public void RemoveEnemy(Card card)
        {
            CombatManager.RemoveEnemy(card);
        }

        public void EndTurn()
        {
            CombatManager.EndTurn();
            // TODO: Add enemy turn logic here.
            CombatManager.StartTurn();
        }

        public int GetTurnNumber()
        {
            return CombatManager.Turn;
        }
    }
}