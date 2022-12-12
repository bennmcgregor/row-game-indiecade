using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Zenject;

namespace IndieCade
{
    public class LightInstance : MonoBehaviour
    {
        [SerializeField] private List<Light2D> _lights;
        [SerializeField] private List<ColliderLightTransitionEffector> _colliderTransitionEffectors;
        [SerializeField] private FinishCollisionLightTransitionEffector _finishCollisionTransitionEffector;
        [SerializeField] private Transform _moveableTransform;
        [SerializeField] private Transform _nonMoveableTransform;
        [SerializeField] private Transform _rotatableFollowPoint;
        [SerializeField] private BrightnessLightAttributeProcessor _brightnessProcessor;
        [SerializeField] private ShapeLightAttributeProcessor _shapeProcessor;

        private Light2D _baseLight;

        public List<Light2D> Lights => _lights;
        public Light2D BaseLight => _baseLight;
        public List<ColliderLightTransitionEffector> ColliderTransitionEffectors => _colliderTransitionEffectors;
        public FinishCollisionLightTransitionEffector FinishCollisionLightTransitionEffector => _finishCollisionTransitionEffector;
        public Transform MoveableTransform => _moveableTransform;
        public Transform NonMoveableTransform => _nonMoveableTransform;
        public Transform RotatableFollowPoint => _rotatableFollowPoint;
        public BrightnessLightAttributeProcessor BrightnessProcessor => _brightnessProcessor;
        public ShapeLightAttributeProcessor ShapeProcessor => _shapeProcessor;

        public void SetBaseLight(Light2D light)
        {
            _baseLight = light;
        }

        public class Factory : PlaceholderFactory<LightInstance> {}
    }
}
