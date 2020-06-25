using System.Collections.Generic;
using Engine.Managers;
using UnityEngine;

namespace Engine.Cards.CardEffects
{
    public class CardEffectAttack : ICardEffect
    {
        // ATTRIBUTES

        private readonly ETargetSelector _selector;

        // CONSTRUCTORS

        public CardEffectAttack(ETargetSelector selector)
        {
            _selector = selector;
        }

        // METHODS


        /// <inheritdoc />
        public void Resolve(Card owner, CombatManager combatManager)
        {
            List<Card> targets = combatManager.GetEnemyBoard();

            int damage = CalculateDamage(owner);
            switch (_selector)
            {
                case ETargetSelector.AllEnemy:
                    targets.ForEach(t => t.DealDamage(damage));
                    break;
                case ETargetSelector.RandomEnemy:
                    int randomTarget = Random.Range(0, targets.Count);
                    targets[randomTarget].DealDamage(damage);
                    break;
                default:
                    break;
            }
        }

        private int CalculateDamage(Card owner)
        {
            // Logic here to change damage calculation
            return owner.CardInfo.Attack;
        }
    }
}