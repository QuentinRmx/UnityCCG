using System;
using System.Collections.Generic;
using Engine.Cards;
using Engine.Cards.CardEffects;

namespace Engine.Managers
{
    public class CombatManager
    {
        // ATTRIBUTES

        private readonly BoardManager _boardManager;

        private List<Card> _playerHand;

        private List<Card> _graveyard;

        // STATS

        public int NbCardsPlayed = 0;

        public int NbCardsRerolled = 0;

        // CONSTRUCTOR

        public CombatManager(BoardManager boardManager)
        {
            _boardManager = boardManager;
            _playerHand = new List<Card>();
            _graveyard = new List<Card>();
            
        }

        // METHODS

        public void AddEnemy(Card card)
        {
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
        }
    }
}