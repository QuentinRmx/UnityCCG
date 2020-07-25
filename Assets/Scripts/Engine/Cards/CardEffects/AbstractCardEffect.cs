using System;
using Engine.Managers;
using Newtonsoft.Json;

namespace Engine.Cards.CardEffects
{
    [JsonObject]
    public class AbstractCardEffect : ICardEffect
    {
        // ATTRIBUTES

        [JsonProperty] public int EffectIdentifier { get; set; }

        [JsonIgnore] public int InstanceId { get; set; }

        [JsonProperty] protected AbstractCardEffect NextEffect { get; set; }

        [JsonProperty]
        public string Description { get; set; }

        // CONSTRUCTORS

        public AbstractCardEffect(int effectIdentifier)
        {
            EffectIdentifier = effectIdentifier;
        }

        public AbstractCardEffect()
        {
            
        }

        // METHODS
        /// <inheritdoc />
        public virtual void Resolve(Card owner, CombatManager combatManager)
        {
        }

        public virtual AbstractCardEffect GetNext()
        {
            return null;
        }

        public virtual string GetDescription(Card card, CombatManager manager)
        {
            return string.Empty;
        }
    }
}