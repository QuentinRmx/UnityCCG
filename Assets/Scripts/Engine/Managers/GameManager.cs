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
            CombatManager.OnCurrentManaChanged += _bridge.OnCurrentManaChange;
            CombatManager.OnCardAddedToHand += _bridge.AddCardToPlayerHand;
            CombatManager.OnDeckChanged += _bridge.OnDeckChange;
            CombatManager.OnCurrentHealthChanged += bridge.OnCurrentHealthChange;
            CombatManager.OnMaxHealthChanged += bridge.OnMaxHealthChange;
            CombatManager.OnPlayerDefeated += bridge.OnPlayerDefeat;
        }

        public void Init()
        {
            
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
            CombatManager.StartPlayerTurn();
        }

        // METHODS

        public void PlayCard(int instanceId)
        {
            CombatManager.PlayCardFromHand(instanceId);
        }

        public void RerollCard(int instanceId)
        {
            CombatManager.RerollCardInHand(instanceId);
            CombatManager.NbCardsRerolled++;
        }

        public void AddEnemy(Card card)
        {
            CombatManager?.AddEnemy(card);
            _bridge?.AddEnemy(card);
        }

        public void RemoveEnemy(Card card)
        {
            CombatManager.RemoveEnemy(card);
        }

        public void EndTurn()
        {
            CombatManager.EndPlayerTurn();
            // TODO: Add enemy turn logic here.
            CombatManager.StartPlayerTurn();
        }

        public int GetTurnNumber()
        {
            return CombatManager.CurrentTurn;
        }

        public int GetCurrentMana()
        {
            return CombatManager.CurrentMana;
        }
    }
}