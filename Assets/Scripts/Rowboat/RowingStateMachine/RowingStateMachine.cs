using System;
using System.Collections;
using System.Collections.Generic;

namespace IndieCade
{
    public class RowingStateMachine<TStateEnum, TTransitionEnum>
        where TStateEnum : Enum
        where TTransitionEnum : Enum
    {
        private RowingStateMachineContext<TStateEnum, TTransitionEnum> _context;
        private Dictionary<TStateEnum, RowingStateProcessor<TStateEnum, TTransitionEnum>> _stateProcessors;

        // current state
        public TStateEnum CurrentState => _context.CurrentState;
        public TStateEnum PreviousState => _context.PreviousState;
        public Dictionary<TStateEnum, RowingStateProcessor<TStateEnum, TTransitionEnum>> StateProcessors => _stateProcessors;

        public RowingStateMachine(RowingStateMachineContext<TStateEnum, TTransitionEnum> context, Dictionary<TStateEnum, RowingStateProcessor<TStateEnum, TTransitionEnum>> stateProcessors)
        {
            _context = context;
            _stateProcessors = stateProcessors;

            foreach (var processor in _stateProcessors.Values)
            {
                processor.OnStateUpdated += ProcessHold;
            }
        }

        public void Transition(TTransitionEnum transition)
        {
            _context.CurrentTransition = transition;
            ProcessState();
        }

        private void ProcessState()
        {
            _stateProcessors[_context.CurrentState].Process();
        }

        private void ProcessHold()
        {
            _stateProcessors[_context.CurrentState].ProcessHold();
        }
    }
}
