using System;
using System.Collections.Generic;

namespace IndieCade
{
    public class StateProcessor<TStateEnum, TTransitionEnum, TContext> : IStateProcessor
        where TStateEnum : Enum
        where TTransitionEnum : Enum
        where TContext : StateMachineContext<TStateEnum, TTransitionEnum>
    {
        public Action OnStateUpdated;

        protected TContext _context;
        private TStateEnum _stateName;
        private List<Predicate<TTransitionEnum>> _transitionFunctionList;
        private List<TStateEnum> _transitionNewStateList;
        private Dictionary<TStateEnum, Action> _newStateActionMap;

        public TStateEnum StateName => _stateName;

        public StateProcessor(TContext context, TStateEnum stateName, List<Predicate<TTransitionEnum>> transitionFunctionList, List<TStateEnum> transitionStateList, Dictionary<TStateEnum, Action> newStateActionMap)
        {
            _context = context;
            _stateName = stateName;
            _transitionFunctionList = transitionFunctionList;
            _transitionNewStateList = transitionStateList;
            _newStateActionMap = newStateActionMap;
        }

        public void Process()
        {
            for (int i = 0; i < _transitionFunctionList.Count; i++)
            {
                if (_transitionFunctionList[i](_context.CurrentTransition))
                {
                    TStateEnum newState = _transitionNewStateList[i];
                    SetCurrentState(newState);

                    if (_newStateActionMap.ContainsKey(newState))
                    {
                        _newStateActionMap[newState]?.Invoke();
                    }

                    break;
                }
            }

            PostProcess();
        }

        protected virtual void PostProcess() { }

        protected void SetCurrentState(TStateEnum newState)
        {
            _context.SetCurrentState(newState);
            OnStateUpdated?.Invoke();
        }
    }
}
