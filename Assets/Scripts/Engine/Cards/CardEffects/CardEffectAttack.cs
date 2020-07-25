using System;
using System.Collections.Generic;
using System.Linq;
using Engine.Cards.Targets;
using Engine.Managers;
using Newtonsoft.Json;
using Random = UnityEngine.Random;

namespace Engine.Cards.CardEffects
{
    [JsonObject]
    public class CardEffectAttack : AbstractCardEffect
    {
        // ATTRIBUTES

        [JsonProperty]
        protected readonly ETargetSelector _selector;

        // CONSTRUCTORS

        public CardEffectAttack(int effectIdentifier, ETargetSelector selector) : base(effectIdentifier)
        {
            _selector = selector;
        }
        

        // METHODS


        /// <inheritdoc />
        public override void Resolve(Card owner, CombatManager combatManager)
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
                case ETargetSelector.Player:
                    combatManager.CurrentHealth -= damage;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <inheritdoc />
        public override AbstractCardEffect GetNext()
        {
            return NextEffect;
        }

        /// <inheritdoc />
        public override string GetDescription(Card card, CombatManager manager)
        {
            return $"Attack for {CalculateDamage(card, manager)}";
        }

        /// <summary>
        /// Calculates the damage this effect deals depending on the current situation.
        /// Any damage modifiers is retrieved from the combatManager.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="combatManager"></param>
        /// <returns></returns>
        protected int CalculateDamage(Card owner, CombatManager combatManager)
        {
            // Logic here to change damage calculation
            return owner.CardInfo.Attack;
        }
    }
}