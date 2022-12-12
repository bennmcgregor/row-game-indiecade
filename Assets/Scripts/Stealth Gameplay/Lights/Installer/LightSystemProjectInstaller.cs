using System;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class LightSystemProjectInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _lightSystemPrefab;

        public override void InstallBindings()
        {
            Container.Bind<ShapeLightAttributeProvider>().FromComponentInNewPrefab(_lightSystemPrefab).AsSingle();
            Container.BindFactory<LightInstance, LightInstance.Factory>().FromFactory<CustomLightInstanceFactory>();
        }
    }
}
