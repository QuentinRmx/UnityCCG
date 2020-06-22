using System;
using Assets.Scripts.States;
using UnityEngine;

namespace States
{
    [Serializable]
    public class StateIdle : State
    {
        // FIELDS

        public static StateIdle Instance = new StateIdle();

        public Color DefaultColor = Color.white;

        // CONSTRUCTORS

        private StateIdle()
        {
        }


        // METHODS
    }
}