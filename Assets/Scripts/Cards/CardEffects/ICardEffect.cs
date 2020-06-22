using Managers;

namespace Cards.CardEffects
{
    public interface ICardEffect
    {
        void Resolve(Card owner, CombatManager combatManager);
    }
}