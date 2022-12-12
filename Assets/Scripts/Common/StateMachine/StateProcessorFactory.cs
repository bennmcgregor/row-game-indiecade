using System;
using System.Collections.Generic;

namespace IndieCade
{
    public abstract class StateProcessorFactory<TStateEnum, TTransitionEnum, TContext, TProcessor> : IFactory<TProcessor>
        where TStateEnum : Enum
        where TTransitionEnum : Enum
        where TContext : StateMachineContext<TStateEnum, TTransitionEnum>
        where TProcessor : StateProcessor<TStateEnum, TTransitionEnum, TContext>
    {
        protected TStateEnum _stateName;
        protected TContext _context;

        protected List<Predicate<TTransitionEnum>> _transitionFunctionList;
        protected List<TStateEnum> _transitionNewStateList;
        protected Dictionary<TStateEnum, Action> _newStateActionMap;

        public StateProcessorFactory(TStateEnum stateName, TContext context)
        {
            _stateName = stateName;
            _context = context;

            _transitionFunctionList = new List<Predicate<TTransitionEnum>>();
            _transitionNewStateList = new List<TStateEnum>();
            _newStateActionMap = new Dictionary<TStateEnum, Action>();
        }

        public void RegisterTransition(TTransitionEnum transition, TStateEnum newState)
        {
            RegisterTransition(
                delegate (TTransitionEnum transitionEnum)
                {
                    return transition.Equals(transitionEnum);
                },
                newState,
                null
            );
        }

        public void RegisterTransition(Predicate<TTransitionEnum> pred, TStateEnum newState, Action transitionLogic)
        {
            _transitionFunctionList.Add(pred);
            _transitionNewStateList.Add(newState);

            if (transitionLogic != null)
            {
                _newStateActionMap.Add(newState, transitionLogic);
            }
        }

        public abstract TProcessor Make();
    }
}
