using System.Collections.Generic;
using Managers;

namespace Cards.CardEffects
{
    public class CardEffectDealDamage : ICardEffect
    {

        // ATTRIBUTES

        private ETargetSelector _selector;
        
        // CONSTRUCTORS

        public CardEffectDealDamage(ETargetSelector selector)
        {
            _selector = selector;
        }

        // METHODS


        /// <inheritdoc />
        public void Resolve(Card owner, CombatManager combatManager)
        {
            switch (_selector)
            {
                case ETargetSelector.AllEnemy:
                    List<Card> targets = combatManager.GetEnemyBoard();
                    targets.ForEach(t => t.DealDamage(owner.CardInfo.Attack));
                    break;
            }
        }
    }
}