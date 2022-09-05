using System;
using Zenject;
using UnityEngine;

namespace IndieCade
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private PlayerInputManager _playerInputManager;

        public override void InstallBindings()
        {
            Container.BindInstance(_player).AsSingle();
            Container.BindInstance(_playerInputManager).AsSingle();
        }
    }
}
