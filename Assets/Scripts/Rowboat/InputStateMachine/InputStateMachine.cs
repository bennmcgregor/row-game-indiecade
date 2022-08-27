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

        public InputStateMachine()
        {
            _context = new InputStateMachineContext(InputState.ENTRY);

            _stateProcessors = new Dictionary<InputState, IStateProcessor>
            {
                { InputState.ENTRY, new EntryInputStateProcessor(_context) },
                { InputState.DOWN, new DownInputStateProcessor(_context) },
                { InputState.HOLD, new HoldInputStateProcessor(_context) },
                { InputState.UP, new UpInputStateProcessor(_context) },
                { InputState.NONE, new NoneInputStateProcessor(_context) }
            };
        }

        public void Transition(InputStateMachineTransition transition)
        {
            // Transitions happen when input is triggered and when state machine finishes processing state
            _context.CurrentTransition = transition;
            ProcessState();
        }

        private void ProcessState()
        {
            _stateProcessors[_context.CurrentState].Process();
        }
    }
}
