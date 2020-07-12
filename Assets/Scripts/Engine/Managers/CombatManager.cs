using System;
using System.Collections.Generic;
using System.Linq;
using Engine.Cards;
using Engine.Utils;
using Random = UnityEngine.Random;

namespace Engine.Managers
{
    /// <summary>
    /// This class manages everything related to the combat part of the engine.<br/>
    /// It's currently designed with the following features/principles/ideas in mind:<br/>
    /// - Enemies are placed in a 3x3 layout where position matters.<br/>
    /// - The player can have 5 cards in hand at max.<br/>
    /// - The cards are always drawn one by one from any source (deck, graveyard, banish, etc).<br/>
    /// - When the deck is empty, the graveyard is shuffled back into the player deck. It can only happen when the player
    /// draws a card or because of a card's effect.<br/>
    /// </summary>
    public class CombatManager
    {
        public const int PLAYER_HAND_SIZE = 5;

        /// <summary>
        /// Raised when a Card is added to the player's hand. The argument is the position of the card.
        /// </summary>
        public event EventHandler<int> OnCardAddedToHand;

        /// <summary>
        /// Raised when any modification (add, remove cards) is done.
        /// </summary>
        public event EventHandler<Card> OnDeckChanged;

        /// <summary>
        /// Raised when an enemy enters the board.
        /// This event should ONLY be raised by the <see cref="AddEnemy"/> method.
        /// </summary>
        public event EventHandler<Card> OnEnemyAdded;

        // STATS

        public int NbCardsPlayed = 0;

        public int NbCardsRerolled = 0;

        // ATTRIBUTES

        private readonly BoardManager _boardManager;

        // TODO: Refactor hand size into a constant or config file eventually.
        private readonly Card[] _playerHand = new Card[PLAYER_HAND_SIZE];

        private readonly List<Card> _playerDeck = new List<Card>();

        private readonly List<Card> _graveyard = new List<Card>();

        // TODO: Refactor mana in a player class.
        public int CurrentMana { get; private set; }

        public int MaxMana { get; private set; }

        public int CurrentTurn { get; set; }

        public bool HasWon => _boardManager.GetEnemiesCount() <= 0;

        // CONSTRUCTOR

        public CombatManager(BoardManager boardManager)
        {
            _boardManager = boardManager;
            // TODO: Magic number.
            MaxMana = 3;
            CurrentMana = 3;
            CurrentTurn = 1;
        }

        // METHODS

        public Card GetEnemyByInstanceId(int id)
        {
            return _boardManager.GetEnemyByInstanceId(id);
        }

        public int GetPlayerDeckSize()
        {
            return _playerDeck.Count;
        }

        public IEnumerable<Card> GetGraveyard()
        {
            return _graveyard;
        }

        public void SetPlayerDeck(IEnumerable<Card> deck)
        {
            foreach (Card card in deck)
            {
                AddCardToDeck(card);
            }
        }

        /// <summary>
        /// Bind the OnDeath event of the enemy card to the RemoveEnemy method and then pass the card to the
        /// BoardManager.
        /// If the enemy card has been added to the board then the <see cref="OnEnemyAdded"/> event is raised.
        /// This should be the only place this event is called no matter what.
        /// </summary>
        /// <param name="enemyCard">The enemy card to add to the board.</param>
        public void AddEnemy(Card enemyCard)
        {
            enemyCard.OnDeath += (sender, enemy) => RemoveEnemy(enemy);
            bool correctlyAdded = _boardManager.AddEnemy(enemyCard);
            if (correctlyAdded)
            {
                OnEnemyAdded?.Invoke(this, enemyCard);
            }
        }

        public void RemoveEnemy(Card card)
        {
            _boardManager.RemoveEnemy(card);
        }

        public List<Card> GetEnemyBoard()
        {
            return _boardManager.GetEnemyBoard();
        }

        public Card[] GetPlayerHand()
        {
            return _playerHand;
        }


//        public void AddCardToHand(Card c)
//        {
//            _playerHand.Add(c);
//            OnCardAddedToHand?.Invoke(c, _playerHand.IndexOf(c));
//        }

        public void AddCardToHand(Card c, int position)
        {
            _playerHand[position] = c;
            OnCardAddedToHand?.Invoke(c, position);
        }

        public void PlayCardFromHand(int instanceId)
        {
            Card card = GetPlayerHand().FirstOrDefault(c => c.CardInfo.InstanceId == instanceId);
            if (card != null)
            {
                int pos = _playerHand.IndexOf(card);
                card.Play(this);
                _playerHand[pos] = null;
                _graveyard.Add(card);
                NbCardsPlayed++;
                CurrentMana--;
                DrawRandomCard(pos);
                OnDeckChanged?.Invoke(this, card);
            }
        }

        /// <summary>
        /// End the player's turn by following this sequence of action :<br/>
        /// 1. The hand is added to the graveyard.<br/>
        /// 2. The hand is cleared.<br/><br/>
        /// Once all those actions have been done with success, the <see cref="StartEnemyTurn"/> method is called.
        /// </summary>
        public void EndPlayerTurn()
        {
            // TODO: Implement additional end of turn effects (cleaning hand?/checking active status effects).
            _graveyard.AddRange(_playerHand);
            ClearPlayerHand();
        }

        public void StartEnemyTurn()
        {
        }

        public void EndEnemyTurn()
        {
        }

        public void StartPlayerTurn()
        {
            CurrentMana = MaxMana;
            CurrentTurn++;
            DrawRandomCards(PLAYER_HAND_SIZE);
        }

        /// <summary>
        /// Draw a given amount of cards randomly amongst the ones currently in deck.
        /// If the deck gets emptied during the process, the graveyard is shuffled back into it.
        /// If there is not enough card in the deck and graveyard combined the remaining cards to draw are ignored
        /// and the method returns.<br/><br/>
        /// It must be noted that this method will replace the end starting from the index 0.<br/>
        /// By definition, this method should be called when drawing a full hand from scratch and not to replace
        /// multiple cards at once. Instead, the <see cref="DrawRandomCard"/> should be used.
        /// </summary>
        /// <param name="amount">The amount of card to draw from the deck.</param>
        public void DrawRandomCards(int amount)
        {
            int realAmount = Math.Max(PLAYER_HAND_SIZE, amount);
            for (int pos = 0; pos < realAmount; pos++)
            {
                DrawRandomCard(pos);
            }
        }

        /// <summary>
        /// Draw a random card and place it at the given position.
        /// </summary>
        /// <param name="position"></param>
        public void DrawRandomCard(int position)
        {
            if (_playerDeck.Count == 0)
            {
                ShuffleGraveyardIntoDeck();
            }

            Card c = _playerDeck.OrderBy(x => Random.Range(0, 100)).Take(1).First();
            _playerDeck.Remove(c);
            AddCardToHand(c, position);
            OnDeckChanged?.Invoke(this, c);
        }

        public void RerollCardInHand(Card card)
        {
            card.Reroll(this);
            int position = _playerHand.IndexOf(card);
            _playerHand[position] = null;
            DrawRandomCard(position);
        }

        public void ShuffleGraveyardIntoDeck()
        {
            foreach (Card c in _graveyard)
            {
                AddCardToDeck(c);
            }

            _graveyard.Clear();
        }

        private void AddCardToDeck(Card card)
        {
            _playerDeck.Add(card);
            OnDeckChanged?.Invoke(this, card);
        }

        /// <summary>
        /// This method clears all references in the <see cref="_playerHand"/> array.<br/>
        /// This method must be used only after copying the instance somewhere else otherwise the card will be
        /// permanently deleted.
        /// </summary>
        private void ClearPlayerHand()
        {
            for (int pos = 0; pos < _playerHand.Length; pos++)
            {
                _playerHand[pos] = null;
            }
        }
        
    }
}