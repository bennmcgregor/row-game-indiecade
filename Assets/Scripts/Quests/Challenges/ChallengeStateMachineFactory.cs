using System;
using System.Collections.Generic;
namespace IndieCade
{
    public class ChallengeStateMachineFactory<TChallengeState> : StateMachineWithDataFactory<TChallengeState, ChallengeStateMachineTransition, StateMachineContext<TChallengeState, ChallengeStateMachineTransition>, StateProcessor<TChallengeState, ChallengeStateMachineTransition, StateMachineContext<TChallengeState, ChallengeStateMachineTransition>>, StateData<TChallengeState>, ChallengeStateMachine<TChallengeState>>
        where TChallengeState : Enum
    {
        public ChallengeStateMachineFactory(StateMachineContext<TChallengeState, ChallengeStateMachineTransition> context) : base(context) { }

        public override ChallengeStateMachine<TChallengeState> Make()
        {
            return new ChallengeStateMachine<TChallengeState>(_context, _stateProcessors, _stateDatas);
        }
    }

    //public class ChallengeStateMachineFactory
    //{
    //    private Dictionary<string, Dictionary<ChallengeStateMachineTransition, string>> _stateMachine;

    //    public ChallengeStateMachineFactory()
    //    {
    //        _stateMachine = new Dictionary<string, Dictionary<ChallengeStateMachineTransition, string>>();
    //    }

    //    public void RegisterNewState(string newState, Dictionary<ChallengeStateMachineTransition, string> transitionStateMap)
    //    {
    //        _stateMachine[newState] = transitionStateMap;
    //    }

    //    public ChallengeStateMachine Make(string initialState, List<ChallengeInitializationData> challenges)
    //    {
    //        ChallengeStateMachineContext context = new ChallengeStateMachineContext(initialState);

    //        List<ChallengeStateProcessor> stateProcessors = new List<ChallengeStateProcessor>();
    //        foreach (var pair in _stateMachine)
    //        {
    //            stateProcessors.Add(new ChallengeStateProcessor(context, pair.Key, pair.Value));
    //        }

    //        return new ChallengeStateMachine(context, stateProcessors, challenges);
    //    }
    //}
}
