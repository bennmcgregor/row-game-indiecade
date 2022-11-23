using System;
using System.Collections.Generic;

namespace IndieCade
{
    public class ChallengeStateMachine<TChallengeState> : StateMachineWithData<TChallengeState, ChallengeStateMachineTransition, StateMachineContext<TChallengeState, ChallengeStateMachineTransition>, StateProcessor<TChallengeState, ChallengeStateMachineTransition, StateMachineContext<TChallengeState, ChallengeStateMachineTransition>>, StateData<TChallengeState>>
        where TChallengeState : Enum        
    {
        public ChallengeStateMachine(StateMachineContext<TChallengeState, ChallengeStateMachineTransition> context, Dictionary<TChallengeState, StateProcessor<TChallengeState, ChallengeStateMachineTransition, StateMachineContext<TChallengeState, ChallengeStateMachineTransition>>> stateProcessors, Dictionary<TChallengeState, StateData<TChallengeState>> stateDatas)
            : base(context, stateProcessors, stateDatas) { }
    }
}
