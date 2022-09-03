using System;
namespace IndieCade
{
    public class Act2QuestStateProcessor : IStateProcessor
    {
        private QuestStateMachineContext _context;

        public Act2QuestStateProcessor(QuestStateMachineContext context)
        {
            _context = context;
        }

        public void Process()
        {
            if (_context.CurrentTransition == QuestStateMachineTransition.NEXT_QUEST)
            {
                _context.CurrentState = QuestState.ACT_3_PRE_QUEST_1;
            }
        }
    }
}
