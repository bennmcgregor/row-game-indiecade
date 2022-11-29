using System;
using System.Collections.Generic;

namespace IndieCade
{
    public class LightStateMachineFactory : StateMachineWithDataFactory<LightState, LightStateMachineTransition, StateMachineContext<LightState, LightStateMachineTransition>, StateProcessor<LightState, LightStateMachineTransition, StateMachineContext<LightState, LightStateMachineTransition>>, StateData<LightState>, LightStateMachine>
    {
        private Lightpost _lightpost;
        private ShapeLightStateAttribute _lightShape;

        public LightStateMachineFactory(StateMachineContext<LightState, LightStateMachineTransition> context) : base(context) { }

        public void RegisterLightpost(Lightpost lightpost)
        {
            _lightpost = lightpost;
        }

        public void SetLightShape(ShapeLightStateAttribute shape)
        {
            _lightShape = shape;
        }

        public override LightStateMachine Make()
        {
            LightStateProcessorFactory disabledFactory = new LightStateProcessorFactory(LightState.DISABLED, _context);
            disabledFactory.RegisterTransition(LightStateMachineTransition.ENABLE, LightState.ENABLED);

            LightStateProcessorFactory enabledFactory = new LightStateProcessorFactory(LightState.ENABLED, _context);
            enabledFactory.RegisterTransition(LightStateMachineTransition.DISABLE, LightState.DISABLED);
            enabledFactory.RegisterTransition(LightStateMachineTransition.COLLIDE, LightState.COLLISION);

            LightStateProcessorFactory collisionFactory = new LightStateProcessorFactory(LightState.COLLISION, _context);
            collisionFactory.RegisterTransition(LightStateMachineTransition.DISABLE, LightState.DISABLED);
            collisionFactory.RegisterTransition(LightStateMachineTransition.FINISH_COLLIDE, LightState.ENABLED);

            // TODO: implement collision timer to trigger FINISH_COLLIDE? - or leave this to the control groups?

            _stateProcessors = new Dictionary<LightState, StateProcessor<LightState, LightStateMachineTransition, StateMachineContext<LightState, LightStateMachineTransition>>>();
            RegisterNewState(disabledFactory.Make());
            RegisterNewState(enabledFactory.Make());
            RegisterNewState(collisionFactory.Make());

            string id = Guid.NewGuid().ToString();

            foreach (var stateDataPair in _stateDatas)
            {
                ((LightStateData)stateDataPair.Value).SetId(id);
            }

            InitializationLightStateData initializationData = new InitializationLightStateData(_lightShape, id);

            return new LightStateMachine(_context, _stateProcessors, _stateDatas, initializationData, _lightpost, id);
        }
    }
}
