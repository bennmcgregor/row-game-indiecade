using System;
using System.Collections.Generic;
using Zenject;

namespace IndieCade
{
    public class PressReleaseStateMachine : StateMachine<PressReleaseState, PressReleaseStateMachineTransition, StateMachineContext<PressReleaseState, PressReleaseStateMachineTransition>, StateProcessor<PressReleaseState, PressReleaseStateMachineTransition, StateMachineContext<PressReleaseState, PressReleaseStateMachineTransition>>>
    {
        private class PressReleaseStateMachineProcessorFactory : StateProcessorFactory<PressReleaseState, PressReleaseStateMachineTransition, StateMachineContext<PressReleaseState, PressReleaseStateMachineTransition>, StateProcessor<PressReleaseState, PressReleaseStateMachineTransition, StateMachineContext<PressReleaseState, PressReleaseStateMachineTransition>>>
        {
            public PressReleaseStateMachineProcessorFactory(PressReleaseState stateName, StateMachineContext<PressReleaseState, PressReleaseStateMachineTransition> context)
                : base(stateName, context) { }

            public override StateProcessor<PressReleaseState, PressReleaseStateMachineTransition, StateMachineContext<PressReleaseState, PressReleaseStateMachineTransition>> Make()
            {
                return new StateProcessor<PressReleaseState, PressReleaseStateMachineTransition, StateMachineContext<PressReleaseState, PressReleaseStateMachineTransition>>(
                    _context,
                    _stateName,
                    _transitionFunctionList,
                    _transitionNewStateList,
                    _newStateActionMap
                );
            }
        }

        private class PressReleaseStateMachineFactory : StateMachineFactory<PressReleaseState, PressReleaseStateMachineTransition, StateMachineContext<PressReleaseState, PressReleaseStateMachineTransition>, StateProcessor<PressReleaseState, PressReleaseStateMachineTransition, StateMachineContext<PressReleaseState, PressReleaseStateMachineTransition>>, PressReleaseStateMachine>
        {
            public PressReleaseStateMachineFactory(StateMachineContext<PressReleaseState, PressReleaseStateMachineTransition> context) : base(context) { }

            public override PressReleaseStateMachine Make()
            {
                return new PressReleaseStateMachine(_context, _stateProcessors);
            }
        }

        public PressReleaseStateMachine(StateMachineContext<PressReleaseState, PressReleaseStateMachineTransition> context, Dictionary<PressReleaseState, StateProcessor<PressReleaseState, PressReleaseStateMachineTransition, StateMachineContext<PressReleaseState, PressReleaseStateMachineTransition>>> stateProcessors)
            : base(context, stateProcessors) { }

        public static PressReleaseStateMachine Make()
        {
            StateMachineContext<PressReleaseState, PressReleaseStateMachineTransition> context = new StateMachineContext<PressReleaseState, PressReleaseStateMachineTransition>(PressReleaseState.NONE);

            PressReleaseStateMachineProcessorFactory noneFactory = new PressReleaseStateMachineProcessorFactory(PressReleaseState.NONE, context);
            noneFactory.RegisterTransition(PressReleaseStateMachineTransition.ON_KEY, PressReleaseState.HOLD);

            PressReleaseStateMachineProcessorFactory holdFactory = new PressReleaseStateMachineProcessorFactory(PressReleaseState.HOLD, context);
            holdFactory.RegisterTransition(PressReleaseStateMachineTransition.ON_KEY, PressReleaseState.NONE);

            PressReleaseStateMachineFactory factory = new PressReleaseStateMachineFactory(context);
            factory.RegisterNewState(noneFactory.Make());
            factory.RegisterNewState(holdFactory.Make());

            return factory.Make();
        }
    }
}
