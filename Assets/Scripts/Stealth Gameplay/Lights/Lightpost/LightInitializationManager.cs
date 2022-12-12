using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Zenject;

namespace IndieCade
{
    public class LightInitializationManager : MonoBehaviour
    {
        [SerializeField] private LightToggler _lightToggler;

        private ShapeLightAttributeProvider _shapeLightAttributeProvider;
        private LightInstance.Factory _lightInstanceFactory;

        [Inject]
        public void Initialize(ShapeLightAttributeProvider shapeLightAttributeProvider, LightInstance.Factory lightInstanceFactory)
        {
            _shapeLightAttributeProvider = shapeLightAttributeProvider;
            _lightInstanceFactory = lightInstanceFactory;
        }

        public LightInstance InitializeLight(StateData<LightState> data, Light2D baseLight)
        {
            _shapeLightAttributeProvider.UpdateLightStateData((LightStateData)data);

            LightInstance newLight = _lightInstanceFactory.Create();
            newLight.transform.SetParent(transform, false);
            newLight.SetBaseLight(baseLight);
            _lightToggler.SetLightInstance(newLight);

            CustomizeLightShape(newLight, (LightStateData)data);

            return newLight;
        }

        private void CustomizeLightShape(LightInstance lightInstance, LightStateData data)
        {
            ShapeLightStateAttribute shapeData = data.Shape;
            lightInstance.ShapeProcessor.SetProperties(shapeData);
        }
    }
}
