using System;
using UnityEngine;
namespace IndieCade
{
    public class RowingStateMachineContext<TStateEnum, TTransitionEnum>
        where TStateEnum : Enum
        where TTransitionEnum : Enum
    {
        private TStateEnum _currentState;

        public RowingStateMachineContext(TStateEnum currentState)
        {
            _currentState = currentState;
        }

        public TStateEnum CurrentState
        {
            get
            {
                return _currentState;
            }
            set
            {
                PreviousState = _currentState;
                _currentState = value;
            }
        }
        public TTransitionEnum CurrentTransition;
        public TStateEnum PreviousState { get; private set; }
    }
}
