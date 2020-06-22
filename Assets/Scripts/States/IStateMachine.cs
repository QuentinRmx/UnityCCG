using System;
using Assets.Scripts.States;

namespace States
{
    public interface IStateMachine
    {
        void ChangeState(State newState, Action enterAction, Action exitAction);
    }
}