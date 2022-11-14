using System;
using System.Collections.Generic;

namespace IndieCade
{
    public class RowingStateMachineFactory<TStateEnum, TTransitionEnum>
        where TStateEnum : Enum
        where TTransitionEnum : Enum
    {
        private Dictionary<TStateEnum, RowingStateProcessor<TStateEnum, TTransitionEnum>> _stateProcessors;

        public RowingStateMachineFactory()
        {
            _stateProcessors = new Dictionary<TStateEnum, RowingStateProcessor<TStateEnum, TTransitionEnum>>();
        }

        public void RegisterNewState(TStateEnum state, RowingStateProcessor<TStateEnum, TTransitionEnum> stateProcessor)
        {
            _stateProcessors[state] = stateProcessor;
        }

        public RowingStateMachine<TStateEnum, TTransitionEnum> Make(RowingStateMachineContext<TStateEnum, TTransitionEnum> context)
        {
            return new RowingStateMachine<TStateEnum, TTransitionEnum>(context, _stateProcessors);
        }
    }
}
