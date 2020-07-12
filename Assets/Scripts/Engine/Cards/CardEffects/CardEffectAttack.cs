using System.Collections.Generic;
using System.Linq;
using Engine.Cards.Targets;
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
            int damage = CalculateDamage(owner, combatManager);
            switch (_selector)
            {
                case ETargetSelector.AllEnemy:
                    Card c;
                    List<int> targetIds = combatManager.GetEnemyBoard().Select(ca => ca.CardInfo.InstanceId).ToList();
                    targetIds.ForEach(id =>
                    {
                        c = combatManager.GetEnemyByInstanceId(id);
                        c.TakeDamage(damage);
                    });
                    break;
                case ETargetSelector.RandomEnemy:
                    List<Card> targets = combatManager.GetEnemyBoard();
                    int randomTarget = Random.Range(0, targets.Count);
                    targets[randomTarget].TakeDamage(damage);
                    break;
            }
        }

        /// <summary>
        /// Calculates the damage this effect deals depending on the current situation.
        /// Any damage modifiers is retrieved from the combatManager.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="combatManager"></param>
        /// <returns></returns>
        private int CalculateDamage(Card owner, CombatManager combatManager)
        {
            // Logic here to change damage calculation
            return owner.CardInfo.Attack;
        }
    }
}