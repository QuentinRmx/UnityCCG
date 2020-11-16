using System;
using System.Collections.Generic;
using System.IO;
using Engine.Bridges;
using Unity.GameObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.Centers
{
    public class DuelUICenter : UICenter
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

        public Text TextPlayerDeck;

        public Text TextPlayerHealth;

        // ATTRIBUTES

        public GameObject[] EnemyInstances;

        /// <summary>
        /// Game Object that will contain the enemies' game objects.
        /// </summary>
        public GameObject EnemyBoard;


        public List<GameObject> PlayerHandInstances = new List<GameObject>();

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

        public string TextLoseText;


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


        public void SetCardsPlayed(int amount)
        {
            TextCardsPlayed.text = "Graveyard cards: " + amount;
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

        public void AddCardToHand(GameObject go, int pos)
        {
            // TODO: Add spatial placement logic.
            go.transform.parent = PlayerHand.transform;
            Vector3 position = go.transform.position;
            position.z = -50;
            position.x += 20 * pos;
            go.transform.position = position;
            PlayerHandInstances.Add(go);
        }

        /// <summary>
        /// Callbacks invoked by the 'end of turn' button.
        /// It clears the player's hand and calls the EndTurn method of the current bridge.
        /// </summary>
        public void EndTurnButtonOnClick()
        {
            // TODO: Change condition to get the information from the GameManager.
            if (TextVictory.text == "")
            {
                foreach (GameObject go in PlayerHandInstances)
                {
                    Destroy(go.gameObject);
                }

                PlayerHandInstances.Clear();
                _bridge?.EndTurn();
            }
        }

        /// <summary>
        /// Sets the current turn and updates the UI.
        /// </summary>
        /// <param name="turn">The current turn.</param>
        public void SetCurrentTurn(int turn)
        {
            TextTurn.text = $"Turn {turn}";
        }

        /// <summary>
        /// Sets the player's current and max mana and updates the UI.
        /// </summary>
        /// <param name="current">The current player's mana.</param>
        /// <param name="max">The maximum mana the player can have.</param>
        public void SetMana(int current, int max)
        {
            TextMana.text = $"{current}/{max}";
        }

        /// <summary>
        /// Displays the Victory text.
        /// </summary>
        public void Win()
        {
            TextVictory.text = TextVictoryText;
        }

        public void Lose()
        {
            TextVictory.text = TextLoseText;
        }

        /// <summary>
        /// Updates the UI displaying the current deck size.
        /// </summary>
        /// <param name="size">The current deck's size</param>
        public void UpdateDeckSize(int size)
        {
            this.TextPlayerDeck.text = $"{size}";
        }

        public void UpdateHealthText(int current, int max)
        {
            TextPlayerHealth.text = $"{current}/{max}";
        }
    }
}