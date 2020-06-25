using System.Collections.Generic;
using System.Linq;
using Engine.Cards;
using UnityEngine;

namespace Engine.Managers
{
    public class BoardManager
    {

        private readonly List<Card> _enemies;
        
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

        public void ClearEnemies()
        {
            _enemies.Clear();
        }

        public List<Card> GetEnemyBoard()
        {
            return _enemies;
        }

        public Card GetEnemyByInstanceId(int instanceId)
        {
            return _enemies.FirstOrDefault(e => e.CardInfo.InstanceId == instanceId);
        }
    }
}
