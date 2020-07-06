using System;
using Engine.Cards.CardEffects;
using Engine.Managers;
using Engine.Utils;
using UnityEngine;

namespace Engine.Cards
{
    /// <summary>
    /// TODO: Represents a Card in the Engine.
    /// It implements several events
    /// </summary>
    public class Card
    {
        // EVENTS

        /// <summary>
        /// Raised when one of the data in the CardInfo object is updated.
        /// </summary>
        public event EventHandler OnDataChanged;

        /// <summary>
        /// Raised when the Card is killed (i.e its health value is reduced down to 0 or below).
        /// </summary>
        public event EventHandler<Card> OnDeath;

        // STATIC


        // FIELDS

        public CardInfo CardInfo;

        public readonly ICardEffect CardEffect;


        // CONSTRUCTOR

        public Card(ICardEffect effect)
        {
            CardEffect = effect;
        }

        public void Play(CombatManager cm)
        {
            CardEffect.Resolve(this, cm);
        }

        public void Reroll(CombatManager cm)
        {
            Debug.Log("Card:Reroll");
        }


        /// <summary>
        /// Deal damage equal to the parameter and return the new life total.
        /// </summary>
        /// <param name="damage">Damage to inflict to this card.</param>
        /// <returns>The new life total.</returns>
        public void DealDamage(int damage)
        {
            CardInfo.Health -= damage;
            Update();
            if (CardInfo.Health <= 0)
            {
                OnDeath?.Invoke(this, this);
            }
        }

        private void Update()
        {
            OnDataChanged?.Invoke(this, null);
        }
    }
}