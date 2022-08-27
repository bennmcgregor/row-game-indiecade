using System;
using UnityEngine;
namespace IndieCade
{
    public class RowingStateMachineContext
    {
        private RowingState _currentState;

        public RowingStateMachineContext(RowingState currentState)
        {
            _currentState = currentState;
        }

        public RowingState CurrentState
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
        public RowingStateMachineTransition CurrentTransition;
        public RowingState PreviousState { get; private set; }
    }
}
