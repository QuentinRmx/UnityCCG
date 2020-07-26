using System;
using System.Collections.Generic;
using Engine.Cards;
using Unity.Centers;

namespace Engine.Bridges
{
    public interface IBridge
    {
        void PlayCardFromHand(int instanceId);

        void AddEnemy(Card enemy);

        void AddCardToPlayerHand(object sender, int position);

        void EndTurn();
        
//        void AddCardToPlayerHand(object sender, int position);

        void OnDeckChange(object sender, Card e);

        void OnCurrentManaChange(object sender, int amount);
        
        void OnMaxHealthChange(object sender, int currentHealth);
        
        void OnCurrentHealthChange(object sender, int maxHealth);
        
        void OnPlayerDefeat(object sender, EventArgs e);

        IEnumerable<Card> GetAllCardsFromJson();

        void OverrideJsonCardData(IEnumerable<Card> cards);
    }
}