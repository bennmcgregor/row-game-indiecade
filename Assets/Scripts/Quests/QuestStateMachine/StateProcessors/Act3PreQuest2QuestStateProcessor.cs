﻿using System;
namespace IndieCade
{
    public class Act3PreQuest2QuestStateProcessor : IStateProcessor
    {
        private QuestStateMachineContext _context;

        public Act3PreQuest2QuestStateProcessor(QuestStateMachineContext context)
        {
            _context = context;
        }

        public void Process()
        {
            if (_context.CurrentTransition == QuestStateMachineTransition.NEXT_QUEST)
            {
                _context.CurrentState = QuestState.ACT_3_QUEST_2;
            }
        }
    }
}