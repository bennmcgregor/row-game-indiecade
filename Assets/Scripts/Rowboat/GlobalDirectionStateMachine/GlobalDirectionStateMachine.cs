﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace IndieCade
{
    public class GlobalDirectionStateMachine
    {
        private GlobalDirectionStateMachineContext _context;
        private Dictionary<GlobalDirectionState, IStateProcessor> _stateProcessors;

        // current state
        public GlobalDirectionState CurrentState => _context.CurrentState;

        public GlobalDirectionStateMachine()
        {
            _context = new GlobalDirectionStateMachineContext(GlobalDirectionState.ENTRY);
            _stateProcessors = new Dictionary<GlobalDirectionState, IStateProcessor>
            {
                { GlobalDirectionState.ENTRY, new EntryGlobalDirectionStateProcessor(_context) },
                { GlobalDirectionState.EAST, new EastGlobalDirectionStateProcessor(_context) },
                { GlobalDirectionState.WEST, new WestGlobalDirectionStateProcessor(_context) }
            };
        }

        public void Transition(GlobalDirectionStateMachineTransition transition)
        {
            // TODO: pass transition to relevant stateprocessor
            // in the state processor, do any transition logic
            _context.CurrentTransition = transition;
            ProcessState();
        }

        private void ProcessState()
        {
            _stateProcessors[_context.CurrentState].Process();
        }
    }
}