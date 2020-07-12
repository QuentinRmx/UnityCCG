using System;
using UnityEngine;

namespace Unity.States
{
    [Serializable]
    public class StateHovered : State
    {
        // FIELDS

        public static StateHovered Instance = new StateHovered();

        public Color HoverColor = Color.blue;

        // CONSTRUCTORS

        private StateHovered()
        {
        }


        // METHODS
    }
}
