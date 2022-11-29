using System;
using System.Collections;
using System.Collections.Generic;

namespace IndieCade
{
    public class RowingStateMachine<TStateEnum, TTransitionEnum> : StateMachine<TStateEnum, TTransitionEnum, RowingStateMachineContext<TStateEnum, TTransitionEnum>, RowingStateProcessor<TStateEnum, TTransitionEnum>>
        where TStateEnum : Enum
        where TTransitionEnum : Enum
    {
        public TStateEnum PreviousState => _context.PreviousState;

        public RowingStateMachine(RowingStateMachineContext<TStateEnum, TTransitionEnum> context, Dictionary<TStateEnum, RowingStateProcessor<TStateEnum, TTransitionEnum>> stateProcessors)
            : base(context, stateProcessors)
        {
            foreach (var processor in _stateProcessors.Values)
            {
                processor.OnStateUpdated += ProcessHold;
            }
        }

        private void ProcessHold()
        {
            _stateProcessors[_context.CurrentState].ProcessHold();
        }
    }
}