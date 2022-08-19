using System;
using System.Collections;
using System.Collections.Generic;

namespace IndieCade
{
    public class InputStateMachine
    {
        private InputStateMachineContext _context;
        private Dictionary<InputState, IStateProcessor> _stateProcessors;

        public InputState CurrentState => _context.CurrentState;

        public InputStateMachine(InputState initialState)
        {
            _context = new InputStateMachineContext
            {
                CurrentState = initialState
            };

            _stateProcessors = new Dictionary<InputState, IStateProcessor>
            {
                { InputState.DOWN, new DownInputStateProcessor(_context) },
                { InputState.HOLD, new HoldInputStateProcessor(_context) },
                { InputState.UP, new UpInputStateProcessor(_context) },
                { InputState.NONE, new NoneInputStateProcessor(_context) }
            };
        }

        public void Transition(InputStateMachineTransition transition)
        {
            // Note: transitions happen when input is triggered and
            // when the rowing state machine finishes processing state (state doesn't have to change)
            _context.CurrentTransition = transition;
            ProcessState();
        }

        private void ProcessState()
        {
            _stateProcessors[_context.CurrentState].Process();
        }
    }
}
