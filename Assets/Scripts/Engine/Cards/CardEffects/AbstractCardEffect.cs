using System;
using Engine.Managers;
using Newtonsoft.Json;

namespace Engine.Cards.CardEffects
{
    [JsonObject]
    public abstract class AbstractCardEffect : ICardEffect
    {

        
        // ATTRIBUTES

        [JsonProperty]
        public int EffectIdentifier { get; set; }
        
        [JsonIgnore]
        public int InstanceId { get; set; }

        [JsonProperty]
        protected AbstractCardEffect NextEffect { get; set; }

        public string Description { get; set; }

        // CONSTRUCTORS

        protected AbstractCardEffect(int effectIdentifier)
        {
            EffectIdentifier = effectIdentifier;
        }

        // METHODS
        /// <inheritdoc />
        public abstract void Resolve(Card owner, CombatManager combatManager);

        public abstract AbstractCardEffect GetNext();

        public abstract string GetDescription(Card card, CombatManager manager);
    }
}