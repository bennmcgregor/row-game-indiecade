using System;
namespace IndieCade
{
    public abstract class RowingStateProcessor<TStateEnum, TTransitionEnum> : IStateProcessor
        where TStateEnum : Enum
        where TTransitionEnum : Enum
    {
        public Action OnStateUpdated;

        protected RowingStateMachineContext<TStateEnum, TTransitionEnum> _context;
        protected RowboatPlayerInputs _rowboatPlayerInputs;
        protected bool _notifyInputStateMachine = true;

        public RowingStateProcessor(RowingStateMachineContext<TStateEnum, TTransitionEnum> context, RowboatPlayerInputs rowboatPlayerInputs)
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

        protected void SetCurrentState(TStateEnum newState)
        {
            _context.CurrentState = newState;
            OnStateUpdated?.Invoke();
        }
    }
}
