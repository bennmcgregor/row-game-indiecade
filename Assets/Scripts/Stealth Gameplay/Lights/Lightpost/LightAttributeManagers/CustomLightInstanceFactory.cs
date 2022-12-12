using System;
using Zenject;

namespace IndieCade
{
    public class CustomLightInstanceFactory : Zenject.IFactory<LightInstance>
    {
        DiContainer _container;
        ShapeLightAttributeProvider _shapeLightAttributeProvider;

        public CustomLightInstanceFactory(DiContainer container, ShapeLightAttributeProvider shapeLightAttributeProvider)
        {
            _container = container;
            _shapeLightAttributeProvider = shapeLightAttributeProvider;
        }

        public LightInstance Create()
        {
            return _container.InstantiatePrefabForComponent<LightInstance>(_shapeLightAttributeProvider.GetShapePrefab());
        }
    }
}
