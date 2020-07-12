using System;
using Engine.Cards;
using UnityEngine.UI;

namespace Unity.GameObjects
{
    public class EnemyCardGameObject : CardGameObject
    {
        // ATTRIBUTES


        public Text CardUiHealth;

        // CONSTRUCTORS

        // METHODS


        public void SetCurrentHealth(int health, int maxHeath)
        {
            // TODO: Add verification logic.
            CardUiHealth.text = $"{health.ToString()}/{maxHeath.ToString()}";
            if (health <= 0)
            {
                Destroy(gameObject.gameObject);
            }
        }

        public new void SetCardData(CardInfo infos)
        {
            base.SetCardData(infos);
            SetCurrentHealth(infos.Health, infos.MaxHealth);
        }
        
        
        /// <summary>
        /// Called by the Card object when something is updated and the CardGameObject associated needs to be associated
        /// (typically health changes or status).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public new void DataChangedCardData(object sender, EventArgs args)
        {
            // TODO: Pass raw data so we don't use an engine class here.
            if (sender is Card c)
            {
                SetCardData(c.CardInfo);
            }
        }
    }
}