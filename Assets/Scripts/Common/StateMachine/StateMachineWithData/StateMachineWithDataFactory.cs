using System;
using System.Collections.Generic;

namespace IndieCade
{
    public abstract class StateMachineWithDataFactory<TStateEnum, TTransitionEnum, TContext, TProcessor, TData, TStateMachine> : StateMachineFactory<TStateEnum, TTransitionEnum, TContext, TProcessor, TStateMachine>
        where TStateEnum : Enum
        where TTransitionEnum : Enum
        where TContext : StateMachineContext<TStateEnum, TTransitionEnum>
        where TProcessor : StateProcessor<TStateEnum, TTransitionEnum, TContext>
        where TData : StateData<TStateEnum>
        where TStateMachine : StateMachineWithData<TStateEnum, TTransitionEnum, TContext, TProcessor, TData>
    {
        protected Dictionary<TStateEnum, TData> _stateDatas;

        public StateMachineWithDataFactory(TContext context)
            : base(context)
        {
            _stateDatas = new Dictionary<TStateEnum, TData>();
        }

        public void RegisterNewState(TProcessor stateProcessor, TData stateData)
        {
            RegisterNewState(stateProcessor);
            _stateDatas[stateData.StateName] = stateData;
        }

        public override abstract TStateMachine Make();
    }
}
