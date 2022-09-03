using System;
using System.Collections.Generic;

namespace IndieCade
{
    public class ChallengeStateProcessor : IStateProcessor
    {
        private ChallengeStateMachineContext _context;
        private string _stateName;
        private Dictionary<ChallengeStateMachineTransition, string> _transitionStateMap;

        public string StateName => _stateName;

        public ChallengeStateProcessor(ChallengeStateMachineContext context, string stateName, Dictionary<ChallengeStateMachineTransition, string> transitionStateMap)
        {
            _context = context;
            _stateName = stateName;
            _transitionStateMap = transitionStateMap;
        }

        public void Process()
        {
            if (_transitionStateMap.ContainsKey(_context.CurrentTransition))
            {
                _context.CurrentState = _transitionStateMap[_context.CurrentTransition];
            }
        }
    }
}
