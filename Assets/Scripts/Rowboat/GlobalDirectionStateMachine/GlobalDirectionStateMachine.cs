using System;
using System.Collections;
using System.Collections.Generic;

namespace IndieCade
{
    public class GlobalDirectionStateMachine : StateMachine<GlobalDirectionState, GlobalDirectionStateMachineTransition, StateMachineContext<GlobalDirectionState, GlobalDirectionStateMachineTransition>, StateProcessor<GlobalDirectionState, GlobalDirectionStateMachineTransition, StateMachineContext<GlobalDirectionState, GlobalDirectionStateMachineTransition>>>
    {
        private class GlobalStateMachineProcessorFactory : StateProcessorFactory<GlobalDirectionState, GlobalDirectionStateMachineTransition, StateMachineContext<GlobalDirectionState, GlobalDirectionStateMachineTransition>, StateProcessor<GlobalDirectionState, GlobalDirectionStateMachineTransition, StateMachineContext<GlobalDirectionState, GlobalDirectionStateMachineTransition>>>
        {
            public GlobalStateMachineProcessorFactory(GlobalDirectionState stateName, StateMachineContext<GlobalDirectionState, GlobalDirectionStateMachineTransition> context)
                : base(stateName, context) { }

            public override StateProcessor<GlobalDirectionState, GlobalDirectionStateMachineTransition, StateMachineContext<GlobalDirectionState, GlobalDirectionStateMachineTransition>> Make()
            {
                return new StateProcessor<GlobalDirectionState, GlobalDirectionStateMachineTransition, StateMachineContext<GlobalDirectionState, GlobalDirectionStateMachineTransition>>(
                    _context,
                    _stateName,
                    _transitionFunctionList,
                    _transitionNewStateList,
                    _newStateActionMap
                );
            }
        }

        private class GlobalStateMachineFactory : StateMachineFactory<GlobalDirectionState, GlobalDirectionStateMachineTransition, StateMachineContext<GlobalDirectionState, GlobalDirectionStateMachineTransition>, StateProcessor<GlobalDirectionState, GlobalDirectionStateMachineTransition, StateMachineContext<GlobalDirectionState, GlobalDirectionStateMachineTransition>>, GlobalDirectionStateMachine>
        {
            public GlobalStateMachineFactory(StateMachineContext<GlobalDirectionState, GlobalDirectionStateMachineTransition> context) : base(context) { }

            public override GlobalDirectionStateMachine Make()
            {
                return new GlobalDirectionStateMachine(_context, _stateProcessors);
            }
        }

        public GlobalDirectionStateMachine(StateMachineContext<GlobalDirectionState, GlobalDirectionStateMachineTransition> context, Dictionary<GlobalDirectionState, StateProcessor<GlobalDirectionState, GlobalDirectionStateMachineTransition, StateMachineContext<GlobalDirectionState, GlobalDirectionStateMachineTransition>>> stateProcessors)
            : base(context, stateProcessors) { }

        public static GlobalDirectionStateMachine Make()
        {
            StateMachineContext<GlobalDirectionState, GlobalDirectionStateMachineTransition> context = new StateMachineContext<GlobalDirectionState, GlobalDirectionStateMachineTransition>(GlobalDirectionState.ENTRY);

            GlobalStateMachineProcessorFactory entryFactory = new GlobalStateMachineProcessorFactory(GlobalDirectionState.ENTRY, context);
            entryFactory.RegisterTransition(GlobalDirectionStateMachineTransition.ENTRY, GlobalDirectionState.WEST);

            GlobalStateMachineProcessorFactory westFactory = new GlobalStateMachineProcessorFactory(GlobalDirectionState.WEST, context);
            westFactory.RegisterTransition(GlobalDirectionStateMachineTransition.SPIN, GlobalDirectionState.EAST);

            GlobalStateMachineProcessorFactory eastFactory = new GlobalStateMachineProcessorFactory(GlobalDirectionState.EAST, context);
            eastFactory.RegisterTransition(GlobalDirectionStateMachineTransition.SPIN, GlobalDirectionState.WEST);

            GlobalStateMachineFactory factory = new GlobalStateMachineFactory(context);
            factory.RegisterNewState(entryFactory.Make());
            factory.RegisterNewState(westFactory.Make());
            factory.RegisterNewState(eastFactory.Make());

            return factory.Make();
        }
    }
}
