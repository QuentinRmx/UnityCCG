using System;
using System.Collections.Generic;
using Cards;
using UnityEngine;

namespace Managers
{
    public class BoardManager
    {

        private List<Card> _enemies;

        public BoardManager()
        {
            _enemies = new List<Card>();
        }

        public void AddEnemy(Card c)
        {
            _enemies.Add(c);
        }

        public void RemoveEnemy(Card c)
        {
            _enemies.Remove(c);
        }

        public List<Card> GetEnemyBoard()
        {
            Debug.Log(_enemies.Count);
            return _enemies;
        }
    }
}
