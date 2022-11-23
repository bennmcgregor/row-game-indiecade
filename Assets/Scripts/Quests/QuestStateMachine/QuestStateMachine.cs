using System;
using System.Collections.Generic;
using Zenject;

namespace IndieCade
{
    public class QuestStateMachine : StateMachineWithData<QuestState, QuestStateMachineTransition, StateMachineContext<QuestState, QuestStateMachineTransition>, StateProcessor<QuestState, QuestStateMachineTransition, StateMachineContext<QuestState, QuestStateMachineTransition>>, StateData<QuestState>>
    {
        public QuestStateMachine(StateMachineContext<QuestState, QuestStateMachineTransition> context, Dictionary<QuestState, StateProcessor<QuestState, QuestStateMachineTransition, StateMachineContext<QuestState, QuestStateMachineTransition>>> stateProcessors, Dictionary<QuestState, StateData<QuestState>> stateDatas)
            : base(context, stateProcessors, stateDatas) { }

        private class QuestStateMachineProcessorFactory : StateProcessorFactory<QuestState, QuestStateMachineTransition, StateMachineContext<QuestState, QuestStateMachineTransition>, StateProcessor<QuestState, QuestStateMachineTransition, StateMachineContext<QuestState, QuestStateMachineTransition>>>
        {
            public QuestStateMachineProcessorFactory(QuestState stateName, StateMachineContext<QuestState, QuestStateMachineTransition> context)
                : base(stateName, context) { }

            public override StateProcessor<QuestState, QuestStateMachineTransition, StateMachineContext<QuestState, QuestStateMachineTransition>> Make()
            {
                return new StateProcessor<QuestState, QuestStateMachineTransition, StateMachineContext<QuestState, QuestStateMachineTransition>>(
                    _context,
                    _stateName,
                    _transitionFunctionList,
                    _transitionNewStateList,
                    _newStateActionMap
                );
            }
        }

        private class QuestStateMachineFactory : StateMachineWithDataFactory<QuestState, QuestStateMachineTransition, StateMachineContext<QuestState, QuestStateMachineTransition>, StateProcessor<QuestState, QuestStateMachineTransition, StateMachineContext<QuestState, QuestStateMachineTransition>>, StateData<QuestState>, QuestStateMachine>
        {
            public QuestStateMachineFactory(StateMachineContext<QuestState, QuestStateMachineTransition> context) : base(context) { }

            public override QuestStateMachine Make()
            {
                return new QuestStateMachine(_context, _stateProcessors, _stateDatas);
            }
        }

        // Pass it a list of quests in order of size > 0
        // Will automatically assemble quests in sequential order
        public static QuestStateMachine Make(List<StateData<QuestState>> quests)
        {
            StateMachineContext<QuestState, QuestStateMachineTransition> context = new StateMachineContext<QuestState, QuestStateMachineTransition>(quests[0].StateName);
            QuestStateMachineFactory factory = new QuestStateMachineFactory(context);

            for (int i = 0; i < quests.Count; i++)
            {
                StateData<QuestState> quest = quests[i];
                // TODO: register the stateQuestMap with the state machine
                QuestStateMachineProcessorFactory processorFactory = new QuestStateMachineProcessorFactory(quest.StateName, context);
                if (i < quests.Count - 1)
                {
                    processorFactory.RegisterTransition(QuestStateMachineTransition.NEXT_QUEST, quests[i + 1].StateName);
                }
                factory.RegisterNewState(processorFactory.Make(), quest);
            }

            return factory.Make();
        }
    }
}
