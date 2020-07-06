using System;

namespace States
{
    public interface IStateMachine
    {
        void ChangeState(State newState, Action enterAction, Action exitAction);
    }
}