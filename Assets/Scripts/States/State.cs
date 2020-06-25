using System;

namespace States
{
    [Serializable]
    public class State
    {
        protected IStateMachine _context;

        private Action _enterAction;

        private Action _exitAction;

        public string Name;

        public bool Locked;

        public int Priority;

        public void SetContext(IStateMachine context, Action enterAction, Action exitAction)
        {
            Name = this.GetType().Name;
            _context = context;
            _enterAction = enterAction;
            _exitAction = exitAction;
        }

        public void EnterState()
        {
            _enterAction?.Invoke();
        }

        public void ExitState()
        {
            _exitAction?.Invoke();
        }
    }
}