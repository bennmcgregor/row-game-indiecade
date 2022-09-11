using System;
using Zenject;
using UnityEngine;

namespace IndieCade
{
    public class StealthGameInstaller : MonoInstaller
    {
        [SerializeField] private StealthRunner _stealthRunner;

        public override void InstallBindings()
        {
            Container.BindInstance(_stealthRunner).AsSingle();
        }
    }
}
