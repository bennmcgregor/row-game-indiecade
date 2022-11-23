using System;
namespace IndieCade
{
    public class ChallengeStateProcessorFactory<TChallengeState> : StateProcessorFactory<TChallengeState, ChallengeStateMachineTransition, StateMachineContext<TChallengeState, ChallengeStateMachineTransition>, StateProcessor<TChallengeState, ChallengeStateMachineTransition, StateMachineContext<TChallengeState, ChallengeStateMachineTransition>>>
        where TChallengeState : Enum
    {
        public ChallengeStateProcessorFactory(TChallengeState stateName, StateMachineContext<TChallengeState, ChallengeStateMachineTransition> context)
            : base(stateName, context) { }

        public override StateProcessor<TChallengeState, ChallengeStateMachineTransition, StateMachineContext<TChallengeState, ChallengeStateMachineTransition>> Make()
        {
            return new StateProcessor<TChallengeState, ChallengeStateMachineTransition, StateMachineContext<TChallengeState, ChallengeStateMachineTransition>>(
                _context,
                _stateName,
                _transitionFunctionList,
                _transitionNewStateList,
                _newStateActionMap
            );
        }
    }
}
