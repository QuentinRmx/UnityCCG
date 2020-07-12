namespace Engine.Cards.Behaviors
{
    public abstract class AbstractAliveBehavior : AbstractBehavior
    {
        /// <summary>
        /// Deals the damage passed in parameter to this card.
        /// If the card dies, its OnDeath event is raised.
        /// </summary>
        /// <param name="damage">The damage to take.</param>
        /// <returns>True if the cards die.</returns>
        public abstract bool TakeDamage(int damage);
    }
}