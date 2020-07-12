using System.IO;

namespace Engine.Cards.Behaviors
{
    public class AliveBehavior : AbstractAliveBehavior
    {
        // ATTRIBUTES

        // CONSTRUCTORS

        // METHODS


        /// <inheritdoc />
        public override bool TakeDamage(int damage)
        {
            _card.CardInfo.Health -= damage;
            _card.Update();
            return _card.CardInfo.Health < 0;
        }
    }
}