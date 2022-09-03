using System;
using System.Collections.Generic;

namespace IndieCade
{
    public class QuestStateMachine
    {
        private QuestStateMachineContext _context;
        private Dictionary<QuestState, QuestStateProcessor> _stateProcessors;
        private Dictionary<QuestState, Quest> _stateQuestMap;

        public QuestStateMachineContext Context => _context;
        public Quest CurrentQuest => _stateQuestMap[_context.CurrentState];

        public QuestStateMachine()
        {
            _context = new QuestStateMachineContext(QuestState.ENTRY);

            _stateProcessors = GameQuests.GetTeaserQuestStateProcessors(_context);

            // TODO: initialize _stateQuestMap
            _stateQuestMap = GameQuests.GetTeaserQuests();
        }

        public void Transition(QuestStateMachineTransition transition)
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
