using System;
namespace IndieCade
{
    public class PressReleaseStateMachineContext
    {
        public PressReleaseStateMachineContext(PressReleaseState initialState)
        {
            CurrentState = initialState;
        }

        public PressReleaseState CurrentState;
        public PressReleaseStateMachineTransition CurrentTransition;
    }
}
