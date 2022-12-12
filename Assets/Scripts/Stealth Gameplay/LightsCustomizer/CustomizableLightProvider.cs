using System;
using UnityEngine;

namespace IndieCade
{
    public class CustomizableLightProvider
    {
        private LightStateMachineFactory _editingSMFactory;
        private LightTransitionControllerFactory _editingTransitionFactory;
        private LightControllerFactory _editingControllerFactory;
        private LightStateMachineFactory _inGameSMFactory;
        private LightTransitionControllerFactory _inGameTransitionFactory;
        private LightControllerFactory _inGameControllerFactory;

        public CustomizableLightProvider()
        {
            _editingSMFactory = BuildEditingLightStateMachineFactory();
            _editingTransitionFactory = BuildLightTransitionControllerFactory();
            _editingControllerFactory = BuildLightControllerFactory();

            _inGameSMFactory = BuildInGameLightStateMachineFactory();
            _inGameTransitionFactory = BuildLightTransitionControllerFactory();
            _inGameControllerFactory = BuildLightControllerFactory();
        }

        public LightController GetInGameLightController(ShapeLightStateAttribute shapeLightStateAttribute, MotionTypeLightStateAttribute motionTypeLightStateAttribute)
        {
            _inGameSMFactory.RegisterNewData(GetInGameLightEnabledState(motionTypeLightStateAttribute));
            _inGameSMFactory.SetLightShape(shapeLightStateAttribute);
            LightStateMachine sm = _inGameSMFactory.Make();

            EventLightTransitionEffector enableEvent = new EventLightTransitionEffector();
            sm.OnStateUpdated += () =>
            {
                if (sm.CurrentState == LightState.DISABLED)
                {
                    enableEvent.OnTransition?.Invoke();
                }
            };
            _inGameTransitionFactory.RegisterEnableTransition(enableEvent);
            LightTransitionController transitionController = _inGameTransitionFactory.Make();

            _inGameControllerFactory.RegisterLightStateMachine(sm);
            _inGameControllerFactory.RegisterTransitionController(transitionController);

            return _inGameControllerFactory.Make();
        }

        public LightController GetEditingLightController(ShapeLightStateAttribute shapeLightStateAttribute, MotionTypeLightStateAttribute motionTypeLightStateAttribute)
        {
            _editingSMFactory.RegisterNewData(GetEditingLightEnabledState(motionTypeLightStateAttribute));
            _editingSMFactory.SetLightShape(shapeLightStateAttribute);
            LightStateMachine sm = _editingSMFactory.Make();

            EventLightTransitionEffector enableEvent = new EventLightTransitionEffector();
            sm.OnStateUpdated += () =>
            {
                if (sm.CurrentState == LightState.DISABLED)
                {
                    enableEvent.OnTransition?.Invoke();
                }
            };
            _editingTransitionFactory.RegisterEnableTransition(enableEvent);
            LightTransitionController transitionController = _editingTransitionFactory.Make();

            _editingControllerFactory.RegisterLightStateMachine(sm);
            _editingControllerFactory.RegisterTransitionController(transitionController);

            return _editingControllerFactory.Make();
        }

        private LightStateMachineFactory BuildInGameLightStateMachineFactory()
        {
            LightStateData disabledData = new LightStateData(LightState.DISABLED);
            disabledData.RegisterOff();

            LightStateData collisionData = new LightStateData(LightState.COLLISION);
            collisionData.RegisterOn(
                new CollisionLightStateAttribute(true, false, 0, 3f),
                new SteadyMotionTypeLightStateAttribute(),
                new ColorLightStateAttribute(new Color(1, 0, 0)),
                new BrightnessLightStateAttribute(2f, 0.81f)
            );

            LightStateMachineFactory factory = new LightStateMachineFactory();
            factory.RegisterNewData(disabledData);
            factory.RegisterNewData(collisionData);

            return factory;
        }

        private LightStateData GetInGameLightEnabledState(MotionTypeLightStateAttribute motionTypeLightStateAttribute)
        {
            LightStateData enabledData = new LightStateData(LightState.ENABLED);
            enabledData.RegisterOn(
                new CollisionLightStateAttribute(true, true, 0.2f, 0),
                motionTypeLightStateAttribute,
                new ColorLightStateAttribute(new Color(1, 0.855f, 0.5f)),
                new BrightnessLightStateAttribute(2f, 0.81f)
            );

            return enabledData;
        }

        private LightTransitionControllerFactory BuildLightTransitionControllerFactory()
        {
            LightTransitionControllerFactory factory = new LightTransitionControllerFactory();
            EventLightTransitionEffector disableEvent = new EventLightTransitionEffector();
            factory.RegisterDisableTransition(disableEvent);
            return factory;
        }

        private LightControllerFactory BuildLightControllerFactory()
        {
            return new LightControllerFactory();
        }

        private LightStateMachineFactory BuildEditingLightStateMachineFactory()
        {
            LightStateData disabledData = new LightStateData(LightState.DISABLED);
            disabledData.RegisterOff();

            LightStateData collisionData = new LightStateData(LightState.COLLISION);
            collisionData.RegisterOff();

            LightStateMachineFactory factory = new LightStateMachineFactory();
            factory.RegisterNewData(disabledData);
            //factory.RegisterNewData(GetEditingLightEnabledState(new SteadyMotionTypeLightStateAttribute()));
            factory.RegisterNewData(collisionData);
            //factory.SetLightShape(new CircularShapeLightStateAttribute(4f, 5f));

            return factory;
        }

        private LightStateData GetEditingLightEnabledState(MotionTypeLightStateAttribute motionTypeLightStateAttribute)
        {
            LightStateData enabledData = new LightStateData(LightState.ENABLED);
            enabledData.RegisterOn(
                new CollisionLightStateAttribute(false, false, 0, 0),
                motionTypeLightStateAttribute,
                new ColorLightStateAttribute(new Color(1, 0.855f, 0.5f)),
                new BrightnessLightStateAttribute(2f, 0.81f)
            );

            return enabledData;
        }
    }
}
