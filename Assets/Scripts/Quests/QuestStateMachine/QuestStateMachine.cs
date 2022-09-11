using System;
using System.Collections.Generic;
using Zenject;

namespace IndieCade
{
    public class QuestStateMachine
    {
        private QuestStateMachineContext _context;
        private Dictionary<QuestState, QuestStateProcessor> _stateProcessors;
        private Dictionary<QuestState, Quest> _stateQuestMap;

        public QuestStateMachineContext Context => _context;
        public Quest CurrentQuest => _stateQuestMap[_context.CurrentState];

        [Inject]
        public QuestStateMachine(GameQuests gameQuests)
        {
            _context = gameQuests.GetTeaserQuestStateMachineContext();
            _stateProcessors = gameQuests.GetTeaserQuestStateProcessors(_context);
            _stateQuestMap = gameQuests.GetTeaserQuests();
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
