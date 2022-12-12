using System;
namespace IndieCade
{
    public class LightStateProcessorFactory : StateProcessorFactory<LightState, LightStateMachineTransition, StateMachineContext<LightState, LightStateMachineTransition>, StateProcessor<LightState, LightStateMachineTransition, StateMachineContext<LightState, LightStateMachineTransition>>>
    {
        public LightStateProcessorFactory(LightState stateName, StateMachineContext<LightState, LightStateMachineTransition> context)
            : base(stateName, context) { }

        public override StateProcessor<LightState, LightStateMachineTransition, StateMachineContext<LightState, LightStateMachineTransition>> Make()
        {
            return new StateProcessor<LightState, LightStateMachineTransition, StateMachineContext<LightState, LightStateMachineTransition>>(
                _context,
                _stateName,
                _transitionFunctionList,
                _transitionNewStateList,
                _newStateActionMap
            );
        }
    }

}
