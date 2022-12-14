using System;
using System.Collections.Generic;

namespace IndieCade
{
    public class StateMachineWithData<TStateEnum, TTransitionEnum, TContext, TProcessor, TData> : StateMachine<TStateEnum, TTransitionEnum, TContext, TProcessor>
        where TStateEnum : Enum
        where TTransitionEnum : Enum
        where TContext : StateMachineContext<TStateEnum, TTransitionEnum>
        where TProcessor : StateProcessor<TStateEnum, TTransitionEnum, TContext>
        where TData : StateData<TStateEnum>
    {
        public Action<TData> OnDataUpdated;

        protected Dictionary<TStateEnum, TData> _stateDatas;

        public TData CurrentData => _stateDatas[CurrentState];

        public StateMachineWithData(TContext context, Dictionary<TStateEnum, TProcessor> stateProcessors, Dictionary<TStateEnum, TData> stateDatas)
            : base(context, stateProcessors)
        {
            _stateDatas = stateDatas;

            _context.OnStateUpdated += () => OnDataUpdated?.Invoke(CurrentData);
        }
    }
}
