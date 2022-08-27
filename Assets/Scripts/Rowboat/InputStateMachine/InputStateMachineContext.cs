using System;
namespace IndieCade
{
    public class InputStateMachineContext
    {
        public InputStateMachineContext(InputState initialState)
        {
            CurrentState = initialState;
        }

        public InputState CurrentState;
        public InputStateMachineTransition CurrentTransition;
    }
}
