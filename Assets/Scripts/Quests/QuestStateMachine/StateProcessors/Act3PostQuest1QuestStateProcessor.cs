using System;
namespace IndieCade
{
    public class Act3PostQuest1QuestStateProcessor : IStateProcessor
    {
        private QuestStateMachineContext _context;

        public Act3PostQuest1QuestStateProcessor(QuestStateMachineContext context)
        {
            _context = context;
        }

        public void Process()
        {
            if (_context.CurrentTransition == QuestStateMachineTransition.NEXT_QUEST)
            {
                _context.CurrentState = QuestState.ACT_3_PRE_QUEST_2;
            }
        }
    }
}
