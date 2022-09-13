using System;
using System.Collections.Generic;
using Zenject;

namespace IndieCade
{
    public class PressReleaseStateMachine
    {
        private PressReleaseStateMachineContext _context;
        private Dictionary<PressReleaseState, PressReleaseStateProcessor> _stateProcessors;

        public PressReleaseState CurrentState => _context.CurrentState;

        public PressReleaseStateMachine()
        {
            _context = new PressReleaseStateMachineContext(PressReleaseState.NONE);
            _stateProcessors = new Dictionary<PressReleaseState, PressReleaseStateProcessor>
            {
                {
                    PressReleaseState.NONE, new PressReleaseStateProcessor(_context, PressReleaseState.NONE,
                    new Dictionary<PressReleaseStateMachineTransition, PressReleaseState> { { PressReleaseStateMachineTransition.ON_KEY, PressReleaseState.HOLD } })
                },
                {
                    PressReleaseState.HOLD, new PressReleaseStateProcessor(_context, PressReleaseState.HOLD,
                    new Dictionary<PressReleaseStateMachineTransition, PressReleaseState> { { PressReleaseStateMachineTransition.ON_KEY, PressReleaseState.NONE } })
                }
            };
        }

        public void Transition(PressReleaseStateMachineTransition transition)
        {
            _context.CurrentTransition = transition;
            ProcessState();
        }

        private void ProcessState()
        {
            _stateProcessors[_context.CurrentState].Process();
        }
    }
}
