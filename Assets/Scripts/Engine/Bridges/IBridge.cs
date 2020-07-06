using Engine.Cards;
using Unity.Centers;

namespace Engine.Bridges
{
    public interface IBridge
    {
        void PlayCard(int instanceId);

        void AddEnemy(Card enemy);

        void AddCardToPlayerHand(Card card);

        void EndTurn();
        
        void AddCardToPlayerHand(object sender, Card e);

        ResourceCenter GetResourceCenter();
    }
}