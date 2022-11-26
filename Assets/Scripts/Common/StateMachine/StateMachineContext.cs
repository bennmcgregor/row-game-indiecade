using System;
using UnityEngine;
namespace IndieCade
{
    public class StateMachineContext<TStateEnum, TTransitionEnum>
        where TStateEnum : Enum
        where TTransitionEnum : Enum
    {
        public Action OnStateUpdated;

        protected TStateEnum _currentState;

        public StateMachineContext(TStateEnum initialState)
        {
            _currentState = initialState;
        }

        public TStateEnum CurrentState => _currentState;

        public void SetCurrentState(TStateEnum val)
        {
            _currentState = val;
            SetCurrentStateInternal(val);
            OnStateUpdated?.Invoke();
        }

        protected virtual void SetCurrentStateInternal(TStateEnum val) { }

        public TTransitionEnum CurrentTransition;
    }
}
