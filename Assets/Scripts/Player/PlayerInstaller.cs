using System;
using Zenject;
using UnityEngine;

namespace IndieCade
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _player;

        public override void InstallBindings()
        {
            Container.BindInstance(_player).AsSingle();
        }
    }
}
