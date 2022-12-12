using System;
using System.Collections;
using System.Collections.Generic;

namespace IndieCade
{
    public class StateMachine<TStateEnum, TTransitionEnum, TContext, TProcessor> : IStateMachineTransition<TTransitionEnum>
        where TStateEnum : Enum
        where TTransitionEnum : Enum
        where TContext : StateMachineContext<TStateEnum, TTransitionEnum>
        where TProcessor : StateProcessor<TStateEnum, TTransitionEnum, TContext>
    {
        public Action OnStateUpdated;

        protected TContext _context;
        protected Dictionary<TStateEnum, TProcessor> _stateProcessors;

        // current state
        public TStateEnum CurrentState => _context.CurrentState;
        public Dictionary<TStateEnum, TProcessor> StateProcessors => _stateProcessors;

        public StateMachine(TContext context, Dictionary<TStateEnum, TProcessor> stateProcessors)
        {
            _context = context;
            _stateProcessors = stateProcessors;

            _context.OnStateUpdated += () => OnStateUpdated?.Invoke();
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
    }
}
