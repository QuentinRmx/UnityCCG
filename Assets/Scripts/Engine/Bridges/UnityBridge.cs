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

        private List<CardGameObject> _playerHand;

        private List<CardGameObject> _enemies;

        // PUBLIC ATTRIBUTES

        public CardGameObject[] Enemies = new CardGameObject[8];

        public CardGameObject[] PlayerHandArray;

        public GameObject CardGameObjectPrefab;

        // METHODS

        private void Start()
        {
            _uiCenter = FindObjectOfType<UICenter>();
            _enemies = new List<CardGameObject>();
            _playerHand = new List<CardGameObject>();
            _gameManager = new GameManager(this);
        }
        
        
        // REQUESTS FROM UI TO GAME MANAGER.

        public void PlayCard(int instanceId)
        {
            Debug.Log("card played: " + instanceId);
            CardGameObject cardInHand = _playerHand.FirstOrDefault(c => c.InstanceId == instanceId);
            if (cardInHand != null)
            {
                _gameManager.PlayCard(instanceId);
                // Play logic here.
                Destroy(cardInHand.gameObject);
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

//        public void PlayCard(Card card)
//        {
//            _gameManager.PlayCard(card);
////            Destroy(card.gameObject);
//            _uiCenter.SetCardsPlayed(_gameManager.GetCombatManager().NbCardsPlayed);
//        }

//        public void RerollCard(Card card)
//        {
//            _gameManager.RerollCard(card);
////            Destroy(card.gameObject);
//            _uiCenter.SetCardsRerolled(_gameManager.GetCombatManager().NbCardsRerolled);
//        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enemy"></param>
        public void AddEnemy(Card enemy)
        {
            GameObject enemyGo = Instantiate(CardGameObjectPrefab);
            CardInfo data = enemy.CardInfo;
            CardGameObject cardGameObject = enemyGo.GetComponent<CardGameObject>();
            cardGameObject.SetCardData(data.InstanceId, data.Name, data.Health, data.Attack);
            enemy.OnUpdate += cardGameObject.Update;
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
            CardGameObject cardGo = newCard.GetComponent<CardGameObject>();
            if (cardGo != null)
            {
                cardGo.RegisterBridge(this);
                cardGo.SetCardData(
                    toAdd.CardInfo.InstanceId,
                    toAdd.CardInfo.Name,
                    toAdd.CardInfo.Health,
                    toAdd.CardInfo.Attack);
            }
            toAdd.OnUpdate += cardGo.Update;
            _playerHand.Add(cardGo);
            _uiCenter.AddCardToHand(newCard);
        }

        public void AddAllCardsToPlayerHand(List<Card> toAdd)
        {
            toAdd.ForEach(AddCardToPlayerHand);
        }
    }
}