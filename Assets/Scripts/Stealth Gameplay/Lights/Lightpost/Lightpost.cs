using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Zenject;

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
        [SerializeField] private LightInstanceStore _lightInstanceStore;

        public LightInstance Attach(LightStateMachine lightStateMachine)
        {
            lightStateMachine.OnDataUpdated += OnLightStateUpdated;

            StateData<LightState> data = lightStateMachine.CurrentData;
            LightInstance newLight = _initializationManager.InitializeLight(data, _baseLight);

            _lightInstanceStore.AttachLightInstance(lightStateMachine.Id, newLight);

            _brightnessManager.InitializeWithData((LightStateData)data);
            _colorManager.InitializeWithData((LightStateData)data);
            _collisionDetectionManager.InitializeWithData((LightStateData)data);
            _motionTypeManager.InitializeWithData((LightStateData)data);

            return newLight;
        }

        public void Detach(string id)
        {
            _lightInstanceStore.DetachLightInstance(id);
        }

        public void DetachAllLights()
        {
            foreach (var id in _lightInstanceStore.AttachedLightIds)
            {
                Detach(id);
            }
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
