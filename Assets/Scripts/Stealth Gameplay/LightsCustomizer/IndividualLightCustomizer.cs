using System;
using UnityEngine;

namespace IndieCade
{
    public class IndividualLightCustomizer
    {
        private LightSystem _lightSystem;
        private LightCustomizationData _lightCustomizationData;
        private CustomizableLightProvider _customizableLightProvider;
        private string _lightpostId;
        private LightsCustomizerParameters _lightsCustomizerParameters;
        private bool _isBottom;
        private GameObject _player;

        public IndividualLightCustomizer(LightSystem lightSystem, LightCustomizationData lightCustomizationData, CustomizableLightProvider customizableLightProvider, string lightpostId, LightsCustomizerParameters lightsCustomizerParameters, bool isBottom, GameObject player)
        {
            _lightSystem = lightSystem;
            _lightCustomizationData = lightCustomizationData;
            _customizableLightProvider = customizableLightProvider;
            _lightpostId = lightpostId;
            _lightsCustomizerParameters = lightsCustomizerParameters;
            _isBottom = isBottom;
            _player = player;

            _lightCustomizationData.OnEdit += UpdateLightEditing;
        }

        private void UpdateLightEditing()
        {
            LightController controller = _customizableLightProvider.GetEditingLightController(GetCustomShape(), GetCustomMotionType());
            _lightSystem.ReplaceAllLightsWithLight(_lightpostId, controller);
            _lightSystem.Entry();
        }

        public void UpdateLightFinishEdit()
        {
            LightController controller = _customizableLightProvider.GetInGameLightController(GetCustomShape(), GetCustomMotionType());
            _lightSystem.ReplaceAllLightsWithLight(_lightpostId, controller);
            _lightSystem.Entry();
        }

        public void UpdateLightInitialization()
        {
            LightController controller = _customizableLightProvider.GetInGameLightController(GetCustomShape(), GetCustomMotionType());
            _lightSystem.ReplaceAllLightsWithLight(_lightpostId, controller);
        }

        private ShapeLightStateAttribute GetCustomShape()
        {
            if (_lightCustomizationData.Shape == LightShape.CIRCULAR)
            {
                return new CircularShapeLightStateAttribute(_lightCustomizationData.Radius, _lightsCustomizerParameters.CircleShapeHeight);
            }

            return new BeamShapeLightStateAttribute(_lightCustomizationData.Radius, _lightCustomizationData.Radius + 1, _lightsCustomizerParameters.BeamInnerArcAngle, _lightsCustomizerParameters.BeamOuterArcAngle);
        }

        private MotionTypeLightStateAttribute GetCustomMotionType()
        {
            switch (_lightCustomizationData.MotionType)
            {
                case LightMotionType.STEADY:
                    return new SteadyMotionTypeLightStateAttribute();
                case LightMotionType.FLASHING:
                    return new FlashingMotionTypeLightStateAttribute(_lightCustomizationData.MotionSpeed * _lightsCustomizerParameters.MaxFlashSpeed, _lightCustomizationData.MotionSpeed * _lightsCustomizerParameters.MaxFlashSpeed);
                case LightMotionType.ROTATING:
                    if (_isBottom)
                    {
                        return new RotatingMotionTypeLightStateAttribute(_lightCustomizationData.MotionSpeed * _lightsCustomizerParameters.MaxRotatingSpeed, _lightsCustomizerParameters.BottomCanalStartRotatingAngle, _lightsCustomizerParameters.BottomCanalEndRotatingAngle);
                    }
                    return new RotatingMotionTypeLightStateAttribute(_lightCustomizationData.MotionSpeed * _lightsCustomizerParameters.MaxRotatingSpeed, _lightsCustomizerParameters.TopCanalStartRotatingAngle, _lightsCustomizerParameters.TopCanalEndRotatingAngle);
                default:
                    return new FollowingMotionTypeLightStateAttribute(_player, 0f, _lightCustomizationData.MotionSpeed * _lightsCustomizerParameters.MaxFollowSpeed, _lightsCustomizerParameters.FollowDelaySeconds);
            }
        }
    }
}
