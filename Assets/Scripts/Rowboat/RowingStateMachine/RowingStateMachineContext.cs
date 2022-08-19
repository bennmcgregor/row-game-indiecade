using System;
using UnityEngine;
namespace IndieCade
{
    public class RowingStateMachineContext
    {
        public RowingState CurrentState
        {
            get
            {
                return CurrentState;
            }
            set
            {
                PreviousState = CurrentState;
                CurrentState = value;
            }
        }
        public RowingStateMachineTransition CurrentTransition;
        public RowingState PreviousState { get; private set; }
        public Vector3 BoatVelocity;
    }
}
