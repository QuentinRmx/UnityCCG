using System;
using Cards;
using States;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace Core
{
    public class BoardSlot : MonoBehaviour
    {
        public Card Card;
        
        private void Start()
        {
            Card = null;
        }

        public void SetCard(Card card)
        {
            this.Card = card;
            Card.transform.position = this.transform.position + new Vector3(0, 45, 0);
            Card.ChangeState(StateInSlot.Instance, null, null);
        }

        public void OnTriggerEnter(Collider other)
        {
            Debug.Log($"collision: {other.gameObject.name}");
            if (other.gameObject.GetComponent<Card>() != null)
            {
                SetCard(other.gameObject.GetComponent<Card>());
            }
        }
    }
}