using System;
namespace IndieCade
{
    public class EntryQuestStateProcessor : IStateProcessor
    {
        private QuestStateMachineContext _context;

        public EntryQuestStateProcessor(QuestStateMachineContext context)
        {
            _context = context;
        }

        public void Process()
        {
            if (_context.CurrentTransition == QuestStateMachineTransition.ENTRY)
            {
                _context.CurrentState = QuestState.ACT_1_PRE_QUEST_1;
            }
        }
    }
}
