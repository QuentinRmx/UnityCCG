using System.Collections.Generic;
using System.Linq;
using Engine.Cards;
using Engine.Cards.CardEffects;
using Engine.Managers;
using Unity.Centers;
using Unity.GameObjects;
using UnityEngine;

namespace Engine.Bridges
{
    public class UnityBridge : MonoBehaviour, IBridge
    {
        // PRIVATE ATTRIBUTES

        private UICenter _uiCenter;

        private GameManager _gameManager;

        private List<HandCardGameObject> _playerHand;

        private List<EnemyCardGameObject> _enemies;

        // PUBLIC ATTRIBUTES

        public CardGameObject[] Enemies = new CardGameObject[8];

        public CardGameObject[] PlayerHandArray;

        public GameObject CardGameObjectPrefab;

        public GameObject EnemyPrefab;

        // METHODS

        private void Start()
        {
            _uiCenter = FindObjectOfType<UICenter>();
            _uiCenter.SetBridge(this);
            _enemies = new List<EnemyCardGameObject>();
            _playerHand = new List<HandCardGameObject>();
            _gameManager = new GameManager(this);
        }


        // REQUESTS FROM UI TO GAME MANAGER.

        public void PlayCard(int instanceId)
        {
            HandCardGameObject cardInHand = _playerHand.FirstOrDefault(c => c.InstanceId == instanceId);
            if (cardInHand != null)
            {
                _gameManager.PlayCard(instanceId);
                // Play logic here.
                Destroy(cardInHand.gameObject);
                _uiCenter.SetCardsPlayed(_gameManager.CombatManager.NbCardsPlayed);
                _uiCenter.SetMana(_gameManager.CombatManager.CurrentMana, _gameManager.CombatManager.MaxMana);
                // TODO: Implement proper win detection.
                if (_gameManager.CombatManager.HasWon)
                {
                    _uiCenter.Win();
                }
            }
        }

        public void RerollCard(int instanceId)
        {
            CardGameObject cardInHand = _playerHand.FirstOrDefault(c => c.InstanceId == instanceId);

            if (cardInHand != null)
            {
                // Reroll logic here.
                Destroy(cardInHand.gameObject);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enemy"></param>
        public void AddEnemy(Card enemy)
        {
            GameObject enemyGo = Instantiate(EnemyPrefab);
            EnemyCardGameObject cardGameObject = enemyGo.GetComponent<EnemyCardGameObject>();
            cardGameObject.SetCardData(enemy.CardInfo);
            enemy.OnDataChanged += cardGameObject.DataChangedCardData;
            _enemies.Add(cardGameObject);
            _uiCenter.AddEnemy(enemyGo);
        }

        public void RemoveEnemy(Card enemy)
        {
            _gameManager.RemoveEnemy(enemy);
        }


        // REQUESTS FROM GAME MANAGER TO UI

        /// <summary>
        /// Add a CardGameObject to the scene corresponding to a card in the player's hand.
        /// </summary>
        /// <param name="toAdd">The Card added to the hand for which we need to create a GameObject.</param>
        public void AddCardToPlayerHand(Card toAdd)
        {
            GameObject newCard = Instantiate(CardGameObjectPrefab);
            HandCardGameObject cardGo = newCard.GetComponent<HandCardGameObject>();
            if (cardGo != null)
            {
                cardGo.RegisterBridge(this);
                cardGo.SetCardData(toAdd.CardInfo);
            }

            toAdd.OnDataChanged += cardGo.DataChangedCardData;
            _playerHand.Add(cardGo);
            _uiCenter.AddCardToHand(newCard);
        }

        /// <inheritdoc />
        public void EndTurn()
        {
            _gameManager.EndTurn();
            _uiCenter.SetTurn(_gameManager.GetTurnNumber());
            _uiCenter.SetMana(_gameManager.CombatManager.CurrentMana, _gameManager.CombatManager.MaxMana);
        }

        public void AddAllCardsToPlayerHand(List<Card> toAdd)
        {
            toAdd.ForEach(AddCardToPlayerHand);
        }
    }
}