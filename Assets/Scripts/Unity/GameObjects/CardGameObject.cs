using System;
using Engine;
using Engine.Bridges;
using Engine.Cards;
using Engine.Cards.CardEffects;
using States;
using Unity.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.GameObjects
{
    public class CardGameObject : MonoBehaviour, IStateMachine
    {
        // PUBLIC ATTRIBUTES

        [SerializeField] public State CurrentState;

        public Text CardUiHealth;

        public int InstanceId;

        // PRIVATE ATTRIBUTES
        
//        public UnityBridge _ub;

        private DraggingComponent _draggingComponent;


        // METHODS

        private void Start()
        {
            _draggingComponent = GetComponent<DraggingComponent>();
            
            
            ChangeState(StateIdle.Instance, null, null);
        }
        
        private void Update()
        {
            if (CurrentState is StateDragged)
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
                _draggingComponent.OnDragForwardSuccessful += (sender, args) => ub.PlayCard(InstanceId);
                _draggingComponent.OnDragBackwardSuccessful += (sender, args) => ub.RerollCard(InstanceId);
            }
        }

        public void SetCardData(int id, string cardName, int health, int attack)
        {
            SetCurrentHealth(health);
            InstanceId = id;
        }

        public void SetCurrentHealth(int health)
        {
            CardUiHealth.text = health.ToString();
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }

        public void Update(object sender, EventArgs args)
        {
            // TODO: Decouple so we don't use an engine class here.
            Debug.Log(sender);
            if (sender is Card c)
            {
                CardInfo ci = c.CardInfo;
                SetCardData(ci.InstanceId, ci.Name, ci.Health, ci.Attack);
            }
        }
    }
}