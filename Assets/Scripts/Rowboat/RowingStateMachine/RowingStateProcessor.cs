using System;
namespace IndieCade
{
    public abstract class RowingStateProcessor : IStateProcessor
    {
        public Action OnStateUpdated;

        protected RowingStateMachineContext _context;
        protected RowboatPlayerInputs _rowboatPlayerInputs;
        protected bool _notifyInputStateMachine = true;

        public RowingStateProcessor(RowingStateMachineContext context, RowboatPlayerInputs rowboatPlayerInputs)
        {
            _context = context;
            _rowboatPlayerInputs = rowboatPlayerInputs;
        }

        public void Process()
        {
            ProcessInternal();
            if (_notifyInputStateMachine)
            {
                foreach (var stateMachine in _rowboatPlayerInputs.InputStateMachines.Values)
                {
                    stateMachine.Transition(InputStateMachineTransition.ON_STATE_PROCESSED);
                }
            }
        }

        protected abstract void ProcessInternal();

        public virtual void ProcessHold() {}

        protected void SetCurrentState(RowingState newState)
        {
            _context.CurrentState = newState;
            OnStateUpdated?.Invoke();
        }
    }
}
