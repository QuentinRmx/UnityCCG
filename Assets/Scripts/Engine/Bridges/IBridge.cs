using Engine.Cards;
using Unity.Centers;

namespace Engine.Bridges
{
    public interface IBridge
    {
        void PlayCardFromHand(int instanceId);

        void AddEnemy(Card enemy);

//        void AddCardToPlayerHand(Card card, int position);

        void EndTurn();
        
        void AddCardToPlayerHand(object sender, int position);

        void OnDeckChange(object sender, Card e);
    }
}