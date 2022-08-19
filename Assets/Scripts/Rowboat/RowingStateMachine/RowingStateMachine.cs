using System;
using System.Collections;
using System.Collections.Generic;

namespace IndieCade
{
    public class RowingStateMachine
    {
        private RowingStateMachineContext _context;
        private Dictionary<RowingState, IStateProcessor> _stateProcessors;

        // current state
        public RowingState CurrentState => _context.CurrentState;

        public RowingStateMachine(RowingState initialState)
        {
            _context.CurrentState = initialState;
            _stateProcessors = new Dictionary<RowingState, IStateProcessor>
            {
                { RowingState.FORWARDS_RECOV, new ForwardsRecoveryRowingStateProcessor(_context) },
                { RowingState.FORWARDS_DRIVE, new ForwardsDriveRowingStateProcessor(_context) }
            };
        }

        public void Transition(RowingStateMachineTransition transition)
        {
            // TODO: pass transition to relevant stateprocessor
            // in the state processor, do any transition logic
            _context.CurrentTransition = transition;
            ProcessState();
        }

        private void ProcessState()
        {
            _stateProcessors[_context.CurrentState].Process();
        }
    }
}
