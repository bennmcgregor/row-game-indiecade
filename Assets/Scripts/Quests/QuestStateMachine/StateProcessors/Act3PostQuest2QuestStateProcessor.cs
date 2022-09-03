using System;
namespace IndieCade
{
    public class Act3PostQuest2QuestStateProcessor : IStateProcessor
    {
        private QuestStateMachineContext _context;

        public Act3PostQuest2QuestStateProcessor(QuestStateMachineContext context)
        {
            _context = context;
        }

        public void Process() {}
    }
}
