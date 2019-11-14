using System;
using Assets.Scripts.States;
using Components;
using UnityEngine;

namespace Cards
{
    /// <summary>
    /// TODO: Write the doc.
    /// </summary>
    public class Card : MonoBehaviour, IStateMachine
    {
        // FIELDS

        [SerializeField] public State CurrentState;

        public Color DefaultColor = Color.white;

        public Color HoverColor = Color.green;

        private DraggingComponent _draggingComponent;

        private GameObject _hoverGameObject;

        // CONSTRUCTORS

        public Card()
        {
        }


        // METHODS

        /// <inheritdoc />
        public void ChangeState(State newState, Action enterAction, Action exitAction)
        {
            if (CurrentState.Locked)
            {
                return;
            }

            CurrentState.ExitState();
            CurrentState = newState;
            CurrentState.SetContext(this, enterAction, exitAction);
            CurrentState.EnterState();
        }

        private void Start()
        {
            _draggingComponent = GetComponent<DraggingComponent>();
            _hoverGameObject = transform.GetChild(0).gameObject;
            _hoverGameObject.SetActive(false);
            ChangeState(StateIdle.Instance, null, null);
        }

        private void Update()
        {
            if (CurrentState is StateDragged)
            {
                _draggingComponent.Drag();
            }
        }

        private void OnMouseEnter()
        {
            if (!(CurrentState is StateDragged))
            {
                ChangeState(StateHovered.Instance, OnHoverEnter, OnHoverExit);
            }
        }

        private void OnMouseExit()
        {
            if (CurrentState is StateHovered)
            {
                ChangeState(StateIdle.Instance, null, null);
            }
        }

        private void OnMouseDown()
        {
            ChangeState(StateDragged.Instance, StartDragging, StopDragging);
        }

        private void OnMouseDrag()
        {
            //if (CurrentState is StateDragged)
            //{
            //    GetComponent<DraggingComponent>()?.Drag();
            //}
        }

        private void OnMouseUp()
        {
            if (CurrentState is StateDragged)
            {
                ChangeState(StateIdle.Instance, OnHoverExit, null);
            }
        }

        private void OnHoverEnter()
        {
            // GetComponent<Renderer>().material.color = HoverColor;
            _hoverGameObject.SetActive(true);
        }

        private void OnHoverExit()
        {
            // GetComponent<Renderer>().material.color = DefaultColor;
            _hoverGameObject.SetActive(false);
        }

        private void StartDragging()
        {
            GetComponent<DraggingComponent>()?.StartDragging();
        }

        private void StopDragging()
        {
            GetComponent<DraggingComponent>()?.StopDragging();
        }
    }
}