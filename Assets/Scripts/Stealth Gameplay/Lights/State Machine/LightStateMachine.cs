using System;
using System.Collections.Generic;

namespace IndieCade
{
    public class LightStateMachine : StateMachineWithData<LightState, LightStateMachineTransition, StateMachineContext<LightState, LightStateMachineTransition>, StateProcessor<LightState, LightStateMachineTransition, StateMachineContext<LightState, LightStateMachineTransition>>, StateData<LightState>>
    {
        private Lightpost _lightpost;
        private string _id;
        private InitializationLightStateData _initializationData;

        public string Id => _id;
        public InitializationLightStateData InitializationData => _initializationData;

        public LightStateMachine(StateMachineContext<LightState, LightStateMachineTransition> context, Dictionary<LightState, StateProcessor<LightState, LightStateMachineTransition, StateMachineContext<LightState, LightStateMachineTransition>>> stateProcessors, Dictionary<LightState, StateData<LightState>> stateDatas, InitializationLightStateData initializationData, Lightpost lightpost, string id)
            : base(context, stateProcessors, stateDatas)
        {
            _initializationData = initializationData;
            _id = id;
            _lightpost = lightpost;
            _lightpost.Attach(this);
        }
    }
}
