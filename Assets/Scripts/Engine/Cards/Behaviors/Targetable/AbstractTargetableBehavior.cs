using Engine.Cards.CardEffects;
using Engine.Cards.Targets;

namespace Engine.Cards.Behaviors.Targetable
{
    public abstract class AbstractTargetableBehavior : AbstractBehavior
    {
        /// <summary>
        /// Return true if this card corresponds to the target selector passed in parameter, i.e the player can target
        /// the card with that.
        /// </summary>
        /// <param name="selector">The target selector to use.</param>
        /// <returns></returns>
        public abstract bool IsValidTarget(ETargetSelector selector);
    }
}