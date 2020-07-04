using System;
using Engine.Bridges;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.Centers
{
    public class UICenter : MonoBehaviour
    {
        // UI OBJECTS

        // TODO: Refactor debugging display.
        public Text TextCardsPlayed;

        // TODO: Refactor debugging display.
        public Text TextCardsRerolled;

        public Text TextMana;

        public Button ButtonEndTurn;

        public Text TextTurn;

        public Text TextVictory;

        // ATTRIBUTES

        public GameObject[] EnemyInstances;

        /// <summary>
        /// Game Object that will contain the enemies' game objects.
        /// </summary>
        public GameObject EnemyBoard;


        public GameObject[] PlayerHandInstances;

        /// <summary>
        /// Game Object that will contain the player's current hand. 
        /// </summary>
        public GameObject PlayerHand;

        private string _manaString;

        public string ManaString
        {
            get => _manaString;
            set
            {
                _manaString = value;
                TextMana.text = _manaString;
            }
        }
        
        public string TextVictoryText;

        private IBridge _bridge;

        // UNITY METHODS
        
        private void Start()
        {
            if (ButtonEndTurn != null)
            {
                ButtonEndTurn.onClick.AddListener(EndTurnButtonOnClick);
            }

            if (TextVictory != null)
            {
                TextVictory.text = "";
            }
        }

        // METHODS

        public void SetBridge(IBridge bridge)
        {
            _bridge = bridge;
        }

        public void SetCardsPlayed(int amount)
        {
            TextCardsPlayed.text = "Cards played: " + amount;
        }

        public void SetCardsRerolled(int amount)
        {
            TextCardsRerolled.text = "Cards rerolled: " + amount;
        }

        public void AddEnemy(GameObject go)
        {
            // TODO: Add spatial placement logic.
            go.transform.position = new Vector3(0, go.transform.position.y, 0);
            go.transform.parent = EnemyBoard.transform;
            
        }

        public void AddCardToHand(GameObject go)
        {
            // TODO: Add spatial placement logic.
            go.transform.parent = PlayerHand.transform;
            Vector3 position = go.transform.position;
            position.z = -50;
            go.transform.position = position;
        }

        public void EndTurnButtonOnClick()
        {
            if (TextVictory.text != "VICTORY")
            {
                _bridge?.EndTurn();
            }
        }

        public void SetTurn(int turn)
        {
            TextTurn.text = $"Turn {turn}";
        }

        public void SetMana(int current, int max)
        {
            TextMana.text = $"{current}/{max}";
        }

        public void Win()
        {
            TextVictory.text = TextVictoryText;
        }
    }
}