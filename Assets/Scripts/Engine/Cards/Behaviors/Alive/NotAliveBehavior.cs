namespace Engine.Cards.Behaviors
{
    public class NotAliveBehavior : AbstractAliveBehavior
    {

        // ATTRIBUTES

        // CONSTRUCTORS

        // METHODS


        /// <inheritdoc />
        public override bool TakeDamage(int damage)
        {
            return false;
        }
    }
}