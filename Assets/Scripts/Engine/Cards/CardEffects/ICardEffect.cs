using Engine.Managers;

namespace Engine.Cards.CardEffects
{
    
    public interface ICardEffect
    {
        void Resolve(Card owner, CombatManager combatManager);
    }
}