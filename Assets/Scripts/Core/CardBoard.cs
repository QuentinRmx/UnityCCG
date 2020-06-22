using System;
using System.Collections.Generic;
using System.Linq;
using Cards;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core
{
    public class CardBoard : MonoBehaviour
    {
        private Dictionary<(int, int), Card> _currentBoard;

        public BoardSlot[,] Slots;

        public int Height = 5;

        public int Width = 1;

        public GameObject SlotPrefab;
        
        // CONSTRUCTOR

        public CardBoard()
        {
               
        }
        
        
        // METHODS

        
        
        private void PrepareBoard()
        {
            _currentBoard = new Dictionary<(int, int), Card>();
            Slots = new BoardSlot[Width, Height];
            for (var i = 1; i <= Width; i++)
            {
                for (var j = 1; j <= Height; j++)
                {
                    _currentBoard.Add((i, j), null);
                    GameObject go = Instantiate(SlotPrefab, new Vector3(0, 100 * (i - 1), 0), Quaternion.identity);
                    Slots[i, j] = go.GetComponent<BoardSlot>();
                }
            }

            
        }
        
        
        public void PlaceCard(int x, int y, Card card)
        {
            if (_currentBoard.ContainsKey((x, y)))
            {
                _currentBoard[(x, y)] = card;
            }
        }

        private void Start()
        {
            PrepareBoard();
        }
    }
}