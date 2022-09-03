using System;
namespace IndieCade
{
    public class Act1Quest1QuestStateProcessor : IStateProcessor
    {
        private QuestStateMachineContext _context;

        public Act1Quest1QuestStateProcessor(QuestStateMachineContext context)
        {
            _context = context;
        }

        public void Process()
        {
            if (_context.CurrentTransition == QuestStateMachineTransition.NEXT_QUEST)
            {
                _context.CurrentState = QuestState.ACT_1_POST_QUEST_1;
            }
        }
    }
}
