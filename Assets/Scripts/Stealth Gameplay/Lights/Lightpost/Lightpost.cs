using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace IndieCade
{
    public class Lightpost : MonoBehaviour
    {
        [SerializeField] private LightInitializationManager _initializationManager;
        [SerializeField] private BrightnessLightAttributeManager _brightnessManager;
        [SerializeField] private CollisionDetectionLightAttributeManager _collisionDetectionManager;
        [SerializeField] private ColorLightAttributeManager _colorManager;
        [SerializeField] private MotionTypeLightAttributeManager _motionTypeManager;
        [SerializeField] private Light2D _baseLight;

        public void Attach(LightStateMachine lightStateMachine)
        {
            lightStateMachine.OnDataUpdated += OnLightStateUpdated;
            InitializeLightState(lightStateMachine.InitializationData, lightStateMachine.CurrentData);
        }

        private void InitializeLightState(InitializationLightStateData initializationData, StateData<LightState> data)
        {
            // PROCESS: build the shape initialization.
            // Then build each manager, for the full flow (initialization then update) and get it working
            // Build starting with motionTypeManager (because it will require the most complexity and fiddling)

            LightInstance newLight = _initializationManager.InitializeLight(initializationData);

            newLight.Lights.Add(_baseLight);

            _brightnessManager.InitializeWithData((LightStateData)data, newLight);
            _colorManager.InitializeWithData((LightStateData)data, newLight);
            _collisionDetectionManager.InitializeWithData((LightStateData)data, newLight);
            _motionTypeManager.InitializeWithData((LightStateData)data, newLight);

            OnLightStateUpdated((LightStateData)data);
        }

        private void OnLightStateUpdated(StateData<LightState> data)
        {
            _brightnessManager.UpdateState((LightStateData)data);
            _colorManager.UpdateState((LightStateData)data);
            _collisionDetectionManager.UpdateState((LightStateData)data);
            _motionTypeManager.UpdateState((LightStateData)data);
        }
    }
}
