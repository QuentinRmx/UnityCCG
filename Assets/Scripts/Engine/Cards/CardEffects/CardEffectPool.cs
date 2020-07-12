using System;
using System.Linq;
using Engine.Managers;
using Newtonsoft.Json;
using Random = UnityEngine.Random;

namespace Engine.Cards.CardEffects
{
    [JsonObject]
    public class CardEffectPool : AbstractCardEffect
    {
        // ATTRIBUTES

        [JsonProperty] public AbstractCardEffect[] Pool { get; set; }

        // CONSTRUCTORS

        public CardEffectPool(int effectIdentifier, AbstractCardEffect[] pool) : base(effectIdentifier)
        {
            Pool = pool;
        }

        // METHODS


        /// <inheritdoc />
        public override void Resolve(Card owner, CombatManager combatManager)
        {
            // Pick a random effect amongst the one available.
            ICardEffect effect = GetNext();
            effect?.Resolve(owner, combatManager);
            NextEffect = null;
        }

        /// <inheritdoc />
        public override AbstractCardEffect GetNext()
        {
            return NextEffect = NextEffect ?? Pool.ToList().OrderBy(x => Random.Range(0, 100)).Take(1).FirstOrDefault();
        }

        /// <inheritdoc />
        public override string GetDescription(Card card, CombatManager manager)
        {
            return GetNext().GetDescription(card, manager);
        }
    }
}