using System;
using UnityEngine;

namespace IndieCade
{
    public class LightsInitializer : MonoBehaviour
    {
        [SerializeField] private Lightpost _testLightpost;

        private void Awake()
        {
            StateMachineContext<LightState, LightStateMachineTransition> context = new StateMachineContext<LightState, LightStateMachineTransition>(LightState.ENABLED);

            LightStateData disabledData = new LightStateData(LightState.DISABLED);
            disabledData.RegisterOff();

            LightStateData enabledData = new LightStateData(LightState.ENABLED);
            enabledData.RegisterOn(
                true,
                new SteadyMotionTypeLightStateAttribute(),
                new ColorLightStateAttribute(new Color(1, 0.855f, 0), new Color(1, 0.855f, 0)),
                1f
            );

            LightStateData collisionData = new LightStateData(LightState.COLLISION);
            collisionData.RegisterOn(
                true,
                new SteadyMotionTypeLightStateAttribute(),
                new ColorLightStateAttribute(new Color(1, 0.855f, 0), new Color(1, 0.855f, 0)),
                1f
            );

            LightStateMachineFactory factory = new LightStateMachineFactory(context);
            factory.RegisterNewData(disabledData);
            factory.RegisterNewData(enabledData);
            factory.RegisterNewData(collisionData);
            factory.RegisterLightpost(_testLightpost);
            factory.SetLightShape(new NoneShapeLightStateAttribute());

            factory.Make();
        }
    }
}
