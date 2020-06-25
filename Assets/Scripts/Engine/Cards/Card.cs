using System;
using Engine.Cards.CardEffects;
using Engine.Managers;
using States;
using Unity.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Cards
{
    /// <summary>
    /// TODO: Write the doc.
    /// </summary>
    public class Card
    {
        // EVENTS

        public event EventHandler OnUpdate;

        // STATIC

        public static int lastInstanceId { get; private set; } = 0;

        // FIELDS

        public CardInfo CardInfo;

        public readonly ICardEffect CardEffect;


        // CONSTRUCTOR

        public Card(ICardEffect effect)
        {
            CardInfo.InstanceId = lastInstanceId;
            Debug.Log("New card: " + CardInfo.InstanceId);
            lastInstanceId++;
            // TOD
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
        }

        private void Update()
        {
            OnUpdate?.Invoke(this, null);
        }
    }
}