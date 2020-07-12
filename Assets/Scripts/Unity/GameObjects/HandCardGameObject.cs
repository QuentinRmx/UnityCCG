using System;
using Engine.Bridges;
using Engine.Cards;
using Engine.Cards.CardEffects;
using Unity.Components;
using Unity.States;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.GameObjects
{
    public class HandCardGameObject : CardGameObject, IStateMachine
    {

        // ATTRIBUTES
        [SerializeField] public State CurrentState;
        
        private DraggingComponent _draggingComponent;

        public Text TextEffect;

        // CONSTRUCTORS

        // METHODS
        
        private void Start()
        {
            _draggingComponent = GetComponent<DraggingComponent>();


            ChangeState(StateIdle.Instance, null, null);
        }

        private void Update()
        {
            if (_draggingComponent != null && CurrentState is StateDragged)
            {
                _draggingComponent.Drag();
            }
        }
        
        
        

        private void OnMouseDown()
        {
            ChangeState(StateDragged.Instance, StartDragging, StopDragging);
        }

        private void OnMouseUp()
        {
            if (CurrentState is StateDragged)
            {
                ChangeState(StateIdle.Instance, null, null);
            }
        }

        private void StartDragging()
        {
            GetComponent<DraggingComponent>()?.StartDragging();
        }

        private void StopDragging()
        {
            GetComponent<DraggingComponent>()?.StopDragging();
        }

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

        public void RegisterBridge(UnityBridge ub)
        {
            // TODO: Cleanup code smell
            if (_draggingComponent == null)
            {
                _draggingComponent = GetComponent<DraggingComponent>();
            }

            if (_draggingComponent != null)
            {
                _draggingComponent.OnDragForwardSuccessful += (sender, args) => ub.PlayCardFromHand(InstanceId);
                _draggingComponent.OnDragBackwardSuccessful += (sender, args) => ub.RerollCard(InstanceId);
            }
        }
        
        public new void SetCardData(CardInfo infos)
        {
            base.SetCardData(infos);
            TextEffect.text = infos.Text;
        }

    }
}