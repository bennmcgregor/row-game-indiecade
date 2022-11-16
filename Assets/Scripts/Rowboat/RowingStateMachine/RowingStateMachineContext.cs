using System;
using UnityEngine;
namespace IndieCade
{
    public class RowingStateMachineContext<TStateEnum, TTransitionEnum> : StateMachineContext<TStateEnum, TTransitionEnum>
        where TStateEnum : Enum
        where TTransitionEnum : Enum
    {
        public RowingStateMachineContext(TStateEnum initialState) : base(initialState) { }

        public override TStateEnum CurrentState
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

        public TStateEnum PreviousState { get; private set; }
    }
}
