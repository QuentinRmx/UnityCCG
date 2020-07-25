using System.Collections.Generic;
using Engine.Cards;

namespace Engine
{
    public class Player
    {

        // ATTRIBUTES
        
        public Deck CurrentDeck { get; set; }
        
        public Card Captain => CurrentDeck.Captain;

        public List<Card> Crewmates => CurrentDeck.Crewmates;

        // CONSTRUCTORS

        // METHODS


    }
}