using System;
using System.Collections.Generic;

namespace IndieCade
{
    public class ChallengeStateMachine<TChallengeState> : StateMachineWithData<TChallengeState, ChallengeStateMachineTransition, StateMachineContext<TChallengeState, ChallengeStateMachineTransition>, StateProcessor<TChallengeState, ChallengeStateMachineTransition, StateMachineContext<TChallengeState, ChallengeStateMachineTransition>>, ChallengeInitializationData<TChallengeState>>
        where TChallengeState : Enum        
    {
        public ChallengeInitializationData<TChallengeState> CurrentChallenge => _stateDatas[_context.CurrentState];

        public ChallengeStateMachine(StateMachineContext<TChallengeState, ChallengeStateMachineTransition> context, Dictionary<TChallengeState, StateProcessor<TChallengeState, ChallengeStateMachineTransition, StateMachineContext<TChallengeState, ChallengeStateMachineTransition>>> stateProcessors, Dictionary<TChallengeState, ChallengeInitializationData<TChallengeState>> stateDatas)
            : base(context, stateProcessors, stateDatas) {}

        //private ChallengeStateMachineContext _context;
        //private Dictionary<string, ChallengeStateProcessor> _stateProcessors;
        //private Dictionary<string, ChallengeInitializationData> _stateChallengeMap;

        //public ChallengeStateMachineContext Context => _context;
        //public ChallengeInitializationData CurrentChallenge => _stateChallengeMap[_context.CurrentState];

        //public ChallengeStateMachine(ChallengeStateMachineContext context, List<ChallengeStateProcessor> stateProcessors, List<ChallengeInitializationData> challenges) 
        //{
        //    _context = context;
        //    _stateProcessors = new Dictionary<string, ChallengeStateProcessor>();
        //    _stateChallengeMap = new Dictionary<string, ChallengeInitializationData>();

        //    foreach (var stateProcessor in stateProcessors)
        //    {
        //        _stateProcessors[stateProcessor.StateName] = stateProcessor;
        //    }

        //    foreach (var challenge in challenges)
        //    {
        //        _stateChallengeMap[challenge.StateName] = challenge;
        //    }
        //}

        //public void Transition(ChallengeStateMachineTransition transition)
        //{
        //    _context.CurrentTransition = transition;
        //    ProcessState();
        //}

        //private void ProcessState()
        //{
        //    _stateProcessors[_context.CurrentState].Process();
        //}
    }
}
