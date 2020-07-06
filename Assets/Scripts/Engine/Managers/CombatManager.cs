using System;
using System.Collections.Generic;
using System.Linq;
using Engine.Bridges;
using Engine.Cards;
using Engine.Cards.CardEffects;
using Random = UnityEngine.Random;

namespace Engine.Managers
{
    public class CombatManager
    {
        public event EventHandler<Card> OnCardAddedToHand;
        
        // STATS

        public int NbCardsPlayed = 0;

        public int NbCardsRerolled = 0;
        
        // ATTRIBUTES

        private readonly BoardManager _boardManager;

        private readonly List<Card> _playerHand;

        private readonly List<Card> _playerDeck;

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
            _playerDeck = new List<Card>();
            // TODO: Magic number.
            MaxMana = 3;
            CurrentMana = 3;
            Turn = 1;
        }

        // METHODS

        public Card GetEnemyByInstanceId(int id)
        {
            return _boardManager.GetEnemyByInstanceId(id);
        }

        public IEnumerable<Card> GetGraveyard()
        {
            return _graveyard;
        }
        public void SetPlayerDeck(IEnumerable<Card> deck)
        {
            _playerDeck.AddRange(deck);
        }

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
            OnCardAddedToHand?.Invoke(c, c);
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
            // TODO: Implement additional end of turn effects (cleaning hand?/checking active status effects).
            _playerDeck.AddRange(_playerHand);
            _playerDeck.AddRange(_graveyard);
            _graveyard.Clear();
            _playerHand.Clear();
        }

        public void StartTurn()
        {
            CurrentMana = MaxMana;
            Turn++;
            List<Card> randomHand = _playerDeck.OrderBy(x => Random.Range(0, 100)).Take(5).ToList();
            foreach (Card card in randomHand)
            {
                AddCardToHand(card);
                _playerDeck.Remove(card);
            }
        }
    }
}