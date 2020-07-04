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

        private Card(ICardEffect effect)
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

        public static class Factory
        {
            /// <summary>
            /// Create the Card instance associated to the given ID. If the ID doesn't correspond to a card then this method
            /// returns null.
            /// </summary>
            /// <param name="cardId">The unique ID (positive integer) of the desired Card.</param>
            /// <returns>The Card instance or null if the ID is not valid).</returns>
            public static Card Create(int cardId)
            {
                // TODO: Refactor factory to load card data from a file.
                CardInfo info;
                ICardEffect effect;
                switch (cardId)
                {
                    case 1:
                        info = new CardInfo()
                        {
                            Attack = 0,
                            Health = 30,
                            MaxHealth = 30,
                            Name = "Dark Spirit"
                        };
                        effect = new CardEffectAttack(ETargetSelector.AllEnemy);
                        break;

                    case 0:
                        info = new CardInfo()
                        {
                            Attack = 10,
                            Health = 1,
                            MaxHealth = 1,
                            Name = "ATTACK"
                        };
                        effect = new CardEffectAttack(ETargetSelector.AllEnemy);
                        break;
                    default:
                        return null;
                }

                Card card = new Card(effect);
                info.InstanceId = InstanceIdManager.NextInstanceId;
                card.CardInfo = info;

                return card;
            }
        }
    }
}