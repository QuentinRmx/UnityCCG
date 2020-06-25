using UnityEngine;
using UnityEngine.UI;

namespace Unity.Centers
{
    public class UICenter : MonoBehaviour
    {
        public Text TextCardsPlayed;

        public Text TextCardsRerolled;

        public GameObject[] EnemyInstances;

        public GameObject EnemyBoard;

        public GameObject PlayerHand;

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
        
        
    }
}