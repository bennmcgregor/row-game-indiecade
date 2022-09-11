using System;
using System.Collections.Generic;

namespace IndieCade
{
    public class PressReleaseStateProcessor : IStateProcessor
    {
        private PressReleaseStateMachineContext _context;
        private PressReleaseState _stateName;
        private Dictionary<PressReleaseStateMachineTransition, PressReleaseState> _transitionStateMap;

        public PressReleaseState StateName => _stateName;

        public PressReleaseStateProcessor(PressReleaseStateMachineContext context, PressReleaseState stateName, Dictionary<PressReleaseStateMachineTransition, PressReleaseState> transitionStateMap)
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
