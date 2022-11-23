using System;
using System.Collections;
using System.Collections.Generic;

namespace IndieCade
{
    public class InputStateMachine : StateMachine<InputState, InputStateMachineTransition, StateMachineContext<InputState, InputStateMachineTransition>, StateProcessor<InputState, InputStateMachineTransition, StateMachineContext<InputState, InputStateMachineTransition>>>
    {
        private class InputStateMachineProcessorFactory : StateProcessorFactory<InputState, InputStateMachineTransition, StateMachineContext<InputState, InputStateMachineTransition>, StateProcessor<InputState, InputStateMachineTransition, StateMachineContext<InputState, InputStateMachineTransition>>>
        {
            public InputStateMachineProcessorFactory(InputState stateName, StateMachineContext<InputState, InputStateMachineTransition> context)
                : base(stateName, context) { }

            public override StateProcessor<InputState, InputStateMachineTransition, StateMachineContext<InputState, InputStateMachineTransition>> Make()
            {
                return new StateProcessor<InputState, InputStateMachineTransition, StateMachineContext<InputState, InputStateMachineTransition>>(
                    _context,
                    _stateName,
                    _transitionFunctionList,
                    _transitionNewStateList,
                    _newStateActionMap
                );
            }
        }

        private class InputStateMachineFactory : StateMachineFactory<InputState, InputStateMachineTransition, StateMachineContext<InputState, InputStateMachineTransition>, StateProcessor<InputState, InputStateMachineTransition, StateMachineContext<InputState, InputStateMachineTransition>>, InputStateMachine>
        {
            public InputStateMachineFactory(StateMachineContext<InputState, InputStateMachineTransition> context) : base(context) { }

            public override InputStateMachine Make()
            {
                return new InputStateMachine(_context, _stateProcessors);
            }
        }

        public InputStateMachine(StateMachineContext<InputState, InputStateMachineTransition> context, Dictionary<InputState, StateProcessor<InputState, InputStateMachineTransition, StateMachineContext<InputState, InputStateMachineTransition>>> stateProcessors)
            : base(context, stateProcessors) { }

        public static InputStateMachine Make()
        {
            StateMachineContext<InputState, InputStateMachineTransition> context = new StateMachineContext<InputState, InputStateMachineTransition>(InputState.ENTRY);

            InputStateMachineProcessorFactory entryFactory = new InputStateMachineProcessorFactory(InputState.ENTRY, context);
            entryFactory.RegisterTransition(InputStateMachineTransition.ENTRY, InputState.NONE);

            InputStateMachineProcessorFactory downFactory = new InputStateMachineProcessorFactory(InputState.DOWN, context);
            downFactory.RegisterTransition(InputStateMachineTransition.ON_RELEASE, InputState.UP);
            downFactory.RegisterTransition(InputStateMachineTransition.ON_STATE_PROCESSED, InputState.HOLD);

            InputStateMachineProcessorFactory upFactory = new InputStateMachineProcessorFactory(InputState.UP, context);
            upFactory.RegisterTransition(InputStateMachineTransition.ON_PRESS, InputState.DOWN);
            upFactory.RegisterTransition(InputStateMachineTransition.ON_STATE_PROCESSED, InputState.NONE);

            InputStateMachineProcessorFactory holdFactory = new InputStateMachineProcessorFactory(InputState.HOLD, context);
            holdFactory.RegisterTransition(InputStateMachineTransition.ON_RELEASE, InputState.UP);

            InputStateMachineProcessorFactory noneFactory = new InputStateMachineProcessorFactory(InputState.NONE, context);
            noneFactory.RegisterTransition(InputStateMachineTransition.ON_PRESS, InputState.DOWN);

            InputStateMachineFactory factory = new InputStateMachineFactory(context);
            factory.RegisterNewState(entryFactory.Make());
            factory.RegisterNewState(downFactory.Make());
            factory.RegisterNewState(upFactory.Make());
            factory.RegisterNewState(holdFactory.Make());
            factory.RegisterNewState(noneFactory.Make());

            return factory.Make();
        }
    }
}
