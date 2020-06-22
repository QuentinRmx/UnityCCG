using System;
using System.Collections;
using System.Collections.Generic;
using Cards;
using UnityEngine;

namespace Managers
{
    public class GameManager
    {
        // SINGLETON4
        private static GameManager _instance;
        public static GameManager Instance => _instance = _instance ?? new GameManager();

        private readonly CombatManager _combatManager;

        // ATTRIBUTES


        // CONSTRUCTOR

        private GameManager()
        {
            _combatManager = new CombatManager(new BoardManager());
        }

        // METHODS

        public void PlayCard(Card card)
        {
            card.Play(_combatManager);
            // TODO: Notify board that the card needs to be removed from hand and put into sanctuary.
            _combatManager.NbCardsPlayed++;
        }
        
        public void RerollCard(Card card)
        {
            card.Reroll(_combatManager);
            // TODO: Notify board that the card needs to be removed from hand and put into sanctuary.
            _combatManager.NbCardsRerolled++;
        }

        public void AddEnemy(Card card)
        {
            _combatManager.AddEnemy(card);
        }

        public void RemoveEnemy(Card card)
        {
            _combatManager.RemoveEnemy(card);
        }

        public CombatManager GetCombatManager()
        {
            return _combatManager;
        }
    }
}