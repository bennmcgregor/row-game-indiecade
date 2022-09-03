using System;
using System.Collections.Generic;

namespace IndieCade
{
    public class QuestStateProcessor : IStateProcessor
    {
        private QuestStateMachineContext _context;
        private QuestState _stateName;
        private Dictionary<QuestStateMachineTransition, QuestState> _transitionStateMap;

        public QuestState StateName => _stateName;

        public QuestStateProcessor(QuestStateMachineContext context, QuestState stateName, Dictionary<QuestStateMachineTransition, QuestState> transitionStateMap)
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
