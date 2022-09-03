using System;
namespace IndieCade
{
    public class Act1PreQuest2QuestStateProcessor : IStateProcessor
    {
        private QuestStateMachineContext _context;

        public Act1PreQuest2QuestStateProcessor(QuestStateMachineContext context)
        {
            _context = context;
        }

        public void Process()
        {
            if (_context.CurrentTransition == QuestStateMachineTransition.NEXT_QUEST)
            {
                _context.CurrentState = QuestState.ACT_1_QUEST_2;
            }
        }
    }
}
