using System;
using System.Collections.Generic;
using System.Linq;
using Engine.Cards;
using Engine.Cards.CardEffects;

namespace Engine.Managers
{
    public class CombatManager
    {
        // STATS

        public int NbCardsPlayed = 0;

        public int NbCardsRerolled = 0;
        
        // ATTRIBUTES

        private readonly BoardManager _boardManager;

        private readonly List<Card> _playerHand;

        private readonly List<Card> _graveyard;

        // TODO: Refactor mana in a player class.
        public int CurrentMana { get; private set; }
        
        public int MaxMana { get; private set; }
        
        public int Turn { get; set; }

        public bool HasWon => _boardManager.Count() <= 0;

        // CONSTRUCTOR

        public CombatManager(BoardManager boardManager)
        {
            _boardManager = boardManager;
            _playerHand = new List<Card>();
            _graveyard = new List<Card>();
            // TODO: Magic number.
            MaxMana = 3;
            CurrentMana = 3;
            Turn = 1;
        }

        // METHODS

        public void AddEnemy(Card card)
        {
            card.OnDeath += (sender, enemy) => RemoveEnemy(enemy);
            _boardManager.AddEnemy(card);
        }

        public void RemoveEnemy(Card card)
        {
            _boardManager.RemoveEnemy(card);
        }

        public List<Card> GetEnemyBoard()
        {
            return _boardManager.GetEnemyBoard();
        }

        public List<Card> GetPlayerHand()
        {
            return _playerHand;
        }

        public void AddCardToHand(Card c)
        {
            _playerHand.Add(c);
        }

        public void PlayCardFromHand(int instanceId)
        {
            Card card = GetPlayerHand().FirstOrDefault(c => c.CardInfo.InstanceId == instanceId);
            if (card != null)
            {
                card.Play(this);
                _playerHand.Remove(card);
                _graveyard.Add(card);
                NbCardsPlayed++;
                CurrentMana--;
            }
        }

        public void EndTurn()
        {
            CurrentMana = MaxMana;
            Turn++;
            // TODO: Implement additional end of turn effects (cleaning hand?/checking active status effects).
        }
    }
}