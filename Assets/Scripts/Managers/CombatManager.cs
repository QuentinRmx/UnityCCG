using System.Collections.Generic;
using Cards;

namespace Managers
{
    public class CombatManager
    {

        private BoardManager _boardManager;
        

        public int NbCardsPlayed = 0;
        public int NbCardsRerolled = 0;

        public CombatManager(BoardManager boardManager)
        {
            _boardManager = boardManager;
        }
        
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
    }
}