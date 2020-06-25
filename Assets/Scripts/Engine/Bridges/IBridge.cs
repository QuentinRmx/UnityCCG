using Engine.Cards;

namespace Engine.Bridges
{
    public interface IBridge
    {
        void PlayCard(int instanceId);

        void AddEnemy(Card enemy);

        void AddCardToPlayerHand(Card card);
    }
}