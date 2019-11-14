using System;

namespace Assets.Scripts.States
{
    public interface IStateMachine
    {
        void ChangeState(State newState, Action enterAction, Action exitAction);
    }
}