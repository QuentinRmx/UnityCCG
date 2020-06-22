using System;
using Cards;
using Centers;
using UnityEngine;

namespace Managers
{
    public class UnityBridge : MonoBehaviour
    {

        // ATTRIBUTES

        private UICenter _uiCenter;

        private GameManager _gameManager;

        public Card[] Enemies;

        // CONSTRUCTORS
        
        

        // METHODS


        private void Start()
        {
            _uiCenter = FindObjectOfType<UICenter>();
            _gameManager = GameManager.Instance;
            foreach (Card enemy in Enemies)
            {
                AddEnemy(enemy);
            }
        }

        public void PlayCard(Card card)
        {
            _gameManager.PlayCard(card);
            Destroy(card.gameObject);
            _uiCenter.SetCardsPlayed(_gameManager.GetCombatManager().NbCardsPlayed);
        }
        
        public void RerollCard(Card card)
        {
            _gameManager.RerollCard(card);
            Destroy(card.gameObject);
            _uiCenter.SetCardsRerolled(_gameManager.GetCombatManager().NbCardsRerolled);
        }

        public void AddEnemy(Card enemy)
        {
            _gameManager.AddEnemy(enemy);
        }

        public void RemoveEnemy(Card enemy)
        {
            _gameManager.RemoveEnemy(enemy);
        }
    }
}