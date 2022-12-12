using System;
using System.Collections.Generic;

namespace IndieCade
{
    public class LightStateMachine : StateMachineWithData<LightState, LightStateMachineTransition, StateMachineContext<LightState, LightStateMachineTransition>, StateProcessor<LightState, LightStateMachineTransition, StateMachineContext<LightState, LightStateMachineTransition>>, StateData<LightState>>
    {
        private string _id;

        public string Id => _id;

        public LightStateMachine(StateMachineContext<LightState, LightStateMachineTransition> context, Dictionary<LightState, StateProcessor<LightState, LightStateMachineTransition, StateMachineContext<LightState, LightStateMachineTransition>>> stateProcessors, Dictionary<LightState, StateData<LightState>> stateDatas, string id)
            : base(context, stateProcessors, stateDatas)
        {
            _id = id;
        }
    }
}
