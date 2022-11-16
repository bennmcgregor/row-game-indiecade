using System;
using System.Collections.Generic;

namespace IndieCade
{
    public class RowingStateMachineFactory<TStateEnum, TTransitionEnum> : StateMachineFactory<TStateEnum, TTransitionEnum, RowingStateMachineContext<TStateEnum, TTransitionEnum>, RowingStateProcessor<TStateEnum, TTransitionEnum>, RowingStateMachine<TStateEnum, TTransitionEnum>>
        where TStateEnum : Enum
        where TTransitionEnum : Enum
    {
        public RowingStateMachineFactory(RowingStateMachineContext<TStateEnum, TTransitionEnum> context) : base(context) { }

        public override RowingStateMachine<TStateEnum, TTransitionEnum> Make()
        {
            return new RowingStateMachine<TStateEnum, TTransitionEnum>(_context, _stateProcessors);
        }
    }
}
