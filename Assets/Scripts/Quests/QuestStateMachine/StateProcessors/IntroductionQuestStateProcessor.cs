using System;
namespace IndieCade
{
    public class IntroductionQuestStateProcessor : IStateProcessor
    {
        private QuestStateMachineContext _context;

        public IntroductionQuestStateProcessor(QuestStateMachineContext context)
        {
            _context = context;
        }

        public void Process()
        {
            if (_context.CurrentTransition == QuestStateMachineTransition.NEXT_QUEST)
            {
                _context.CurrentState = QuestState.ACT_1_PRE_QUEST_1;
            }
        }
    }
}
