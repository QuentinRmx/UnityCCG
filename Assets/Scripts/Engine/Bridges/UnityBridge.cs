using System;
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

        private ResourceCenter _resourceCenter;

        // PUBLIC ATTRIBUTES

        public GameObject CardGameObjectPrefab;

        public GameObject EnemyPrefab;

        // METHODS

        private void Start()
        {
            _uiCenter = FindObjectOfType<UICenter>();
            _uiCenter.SetBridge(this);
            _resourceCenter = FindObjectOfType<ResourceCenter>();
            _enemies = new List<EnemyCardGameObject>();
            _playerHand = new List<HandCardGameObject>();
            _gameManager = new GameManager(this);

            _gameManager.Init();
        }


        // REQUESTS FROM UI TO GAME MANAGER.

        public void PlayCardFromHand(int instanceId)
        {
            if (_gameManager.GetCurrentMana() <= 0 || _gameManager.CombatManager.HasWon || _gameManager.CombatManager.HasLost)
                return;
            HandCardGameObject cardInHand = _playerHand.FirstOrDefault(c => c.InstanceId == instanceId);
            if (cardInHand != null)
            {
                _gameManager.PlayCard(instanceId);
                // Play logic here.
                _playerHand.Remove(cardInHand);
                Destroy(cardInHand.gameObject);
                _uiCenter.SetCardsPlayed(_gameManager.CombatManager.GetGraveyard().Count());
                // TODO: Implement proper win detection.
                if (_gameManager.CombatManager.HasWon)
                {
                    _uiCenter.Win();
                }
            }
        }

        public void RerollCard(int instanceId)
        {
            if (_gameManager.GetCurrentMana() <= 0)
                return;
            HandCardGameObject cardInHand = _playerHand.FirstOrDefault(c => c.InstanceId == instanceId);

            if (cardInHand != null)
            {
                // Reroll logic here.
                _gameManager.RerollCard(cardInHand.InstanceId);
                _playerHand.Remove(cardInHand);
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
            enemy.OnNextActionPicked += cardGameObject.SetNextAction;
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
        /// <param name="position">The position to add the card at.</param>
        public void AddCardToPlayerHand(Card toAdd, int position)
        {
            GameObject newCard = Instantiate(CardGameObjectPrefab);
            HandCardGameObject cardGo = newCard.GetComponent<HandCardGameObject>();
            if (cardGo != null)
            {
                cardGo.SetCardData(toAdd.CardInfo);
                cardGo.RegisterBridge(this);
                cardGo.LoadArtwork(_resourceCenter.CardArtworks[toAdd.CardInfo.Identifier]);
            }

            toAdd.OnDataChanged += cardGo.DataChangedCardData;
            _playerHand.Add(cardGo);
            _uiCenter.AddCardToHand(newCard, position);
        }

        /// <inheritdoc />
        public void EndTurn()
        {
            _playerHand.Clear();
            _gameManager.EndTurn();
            _uiCenter.SetCurrentTurn(_gameManager.GetTurnNumber());
        }

        /// <inheritdoc />
        public void AddCardToPlayerHand(object sender, int position)
        {
            AddCardToPlayerHand(_gameManager.CombatManager.GetPlayerHand()[position], position);
        }

        /// <inheritdoc />
        public void OnDeckChange(object sender, Card e)
        {
            _uiCenter.UpdateDeckSize(_gameManager.CombatManager.GetPlayerDeckSize());
        }

        /// <inheritdoc />
        public void OnCurrentManaChange(object sender, int amount)
        {
            _uiCenter.SetMana(amount, _gameManager.CombatManager.MaxMana);
        }

        /// <inheritdoc />
        public void OnMaxHealthChange(object sender, int currentHealth)
        {
            _uiCenter.UpdateHealthText(_gameManager.CombatManager.CurrentHealth, _gameManager.CombatManager.MaxHealth);
        }

        /// <inheritdoc />
        public void OnCurrentHealthChange(object sender, int maxHealth)
        {
            _uiCenter.UpdateHealthText(_gameManager.CombatManager.CurrentHealth,  _gameManager.CombatManager.MaxHealth);
        }

        /// <inheritdoc />
        public void OnPlayerDefeat(object sender, EventArgs e)
        {
            _uiCenter.Lose();
        }
    }
}