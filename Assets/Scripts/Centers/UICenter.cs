using UnityEngine;
using UnityEngine.UI;

namespace Centers
{
    public class UICenter : MonoBehaviour
    {
        public Text TextCardsPlayed;

        public Text TextCardsRerolled;

        public void SetCardsPlayed(int amount)
        {
            TextCardsPlayed.text = "Cards played: " + amount;
        }

        public void SetCardsRerolled(int amount)
        {
            TextCardsRerolled.text = "Cards rerolled: " + amount;
        }
    }
}