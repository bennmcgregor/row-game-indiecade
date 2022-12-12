using System;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class LightsInitializer : MonoBehaviour
    {
        [SerializeField] private Lightpost _testLightpost;

        private LightController _controller;
        private GameObject _player;
        private ObjectInteractionControl _objectInteractionControl;

        [Inject]
        public void Initialize(GameObject player, ObjectInteractionControl objectInteractionControl)
        {
            _player = player;
            _objectInteractionControl = objectInteractionControl;
        }

        private void Awake()
        {
            LightStateData disabledData = new LightStateData(LightState.DISABLED);
            disabledData.RegisterOff();
            //disabledData.RegisterOn(
            //    new CollisionLightStateAttribute(true, 2f, 1f),
            //    new FollowingMotionTypeLightStateAttribute(_player, 125f, 1f, 0.2f),
            //    new ColorLightStateAttribute(new Color(1, 0.855f, 0), new Color(1, 0.855f, 0)),
            //    1f
            //);

            LightStateData enabledData = new LightStateData(LightState.ENABLED);
            enabledData.RegisterOn(
                new CollisionLightStateAttribute(true, true, 0.2f, 1f),
                new RotatingMotionTypeLightStateAttribute(1f, 270, 90),
                new ColorLightStateAttribute(new Color(1, 0.855f, 0.5f)),
                new BrightnessLightStateAttribute(2f, 0.81f)
            );

            LightStateData collisionData = new LightStateData(LightState.COLLISION);
            collisionData.RegisterOn(
                new CollisionLightStateAttribute(true, false, 0.2f, 1f),
                new SteadyMotionTypeLightStateAttribute(),
                new ColorLightStateAttribute(new Color(1, 0.855f, 0)),
                new BrightnessLightStateAttribute(2f, 0.5f)
            );

            LightStateMachineFactory factory = new LightStateMachineFactory();
            factory.RegisterNewData(disabledData);
            factory.RegisterNewData(enabledData);
            factory.RegisterNewData(collisionData);
            //factory.SetLightShape(new BeamShapeLightStateAttribute(5f, 7f, 18.5f, 26f));
            factory.SetLightShape(new CircularShapeLightStateAttribute(0.1f, 8f));

            LightTransitionControllerFactory transitionControllerFactory = new LightTransitionControllerFactory();
            EventLightTransitionEffector enableEvent = new EventLightTransitionEffector();
            _objectInteractionControl.OnStartInteraction += () => enableEvent.OnTransition?.Invoke();
            EventLightTransitionEffector disableEvent = new EventLightTransitionEffector();
            _objectInteractionControl.OnEndInteraction += () => disableEvent.OnTransition?.Invoke();

            transitionControllerFactory.RegisterEnableTransition(enableEvent);
            transitionControllerFactory.RegisterDisableTransition(disableEvent);

            LightControllerFactory lightControllerFactory = new LightControllerFactory();
            lightControllerFactory.RegisterTransitionController(transitionControllerFactory.Make());
            lightControllerFactory.RegisterLightStateMachine(factory.Make());

            _controller = lightControllerFactory.Make();
        }

        private void Start()
        {
        }
    }
}
