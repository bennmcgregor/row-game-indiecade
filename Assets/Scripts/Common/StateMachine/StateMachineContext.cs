using System;
using UnityEngine;
namespace IndieCade
{
    public class StateMachineContext<TStateEnum, TTransitionEnum>
        where TStateEnum : Enum
        where TTransitionEnum : Enum
    {
        protected TStateEnum _currentState;

        public StateMachineContext(TStateEnum initialState)
        {
            _currentState = initialState;
        }

        public virtual TStateEnum CurrentState
        {
            get
            {
                return _currentState;
            }
            set
            {
                _currentState = value;
            }
        }
        public TTransitionEnum CurrentTransition;
    }
}
