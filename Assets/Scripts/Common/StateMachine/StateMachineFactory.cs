using System;
using System.Collections.Generic;

namespace IndieCade
{
    public abstract class StateMachineFactory<TStateEnum, TTransitionEnum, TContext, TProcessor, TStateMachine> : IFactory<TStateMachine>
        where TStateEnum : Enum
        where TTransitionEnum : Enum
        where TContext : StateMachineContext<TStateEnum, TTransitionEnum>
        where TProcessor : StateProcessor<TStateEnum, TTransitionEnum, TContext>
        where TStateMachine : StateMachine<TStateEnum, TTransitionEnum, TContext, TProcessor>
    {
        protected Dictionary<TStateEnum, TProcessor> _stateProcessors;
        protected TContext _context;

        public StateMachineFactory(TContext context)
        {
            _stateProcessors = new Dictionary<TStateEnum, TProcessor>();
            _context = context;
        }

        public virtual void RegisterNewState(TProcessor stateProcessor)
        {
            _stateProcessors[stateProcessor.StateName] = stateProcessor;
        }

        public abstract TStateMachine Make();
    }
}
