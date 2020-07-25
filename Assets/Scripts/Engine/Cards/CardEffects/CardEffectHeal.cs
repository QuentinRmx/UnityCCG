using System;
using Engine.Cards.Targets;
using Engine.Managers;
using Newtonsoft.Json;

namespace Engine.Cards.CardEffects
{
    [JsonObject]
    public class CardEffectHeal : AbstractCardEffect
    {
        // ATTRIBUTES

        [JsonProperty] private readonly ETargetSelector _selector;

        // CONSTRUCTORS


        /// <inheritdoc />
        public CardEffectHeal(int effectIdentifier, ETargetSelector selector) : base(effectIdentifier)
        {
            _selector = selector;
        }
        
        public CardEffectHeal() : base()
        {
            
        }
        // METHODS



        /// <inheritdoc />
        public override void Resolve(Card owner, CombatManager combatManager)
        {
            switch (_selector)
            {
                case ETargetSelector.Player:
                    combatManager.CurrentHealth =
                        Math.Min(combatManager.CurrentHealth + owner.CardInfo.HealAmount, combatManager.MaxHealth);
                    break;
                case ETargetSelector.SelfEnemy:
                    owner.CardInfo.Health =
                        Math.Min(owner.CardInfo.Health + owner.CardInfo.HealAmount, owner.CardInfo.MaxHealth);
                    owner.Update();
                    break;
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
            return $"Heal {card.CardInfo.HealAmount}";
        }
    }
}