using System;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class LightSystemSceneInstaller : MonoInstaller
    {
        [SerializeField] private LightpostProvider _lightpostProvider;

        public override void InstallBindings()
        {
            Container.BindInstance(_lightpostProvider).AsSingle();
        }
    }
}
