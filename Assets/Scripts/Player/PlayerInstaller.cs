using System;
using Zenject;
using UnityEngine;

namespace IndieCade
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private PlayerInputManager _playerInputManager;
        [SerializeField] private ObjectInteractionControl _objectInteractionControl;

        public override void InstallBindings()
        {
            // TODO: change _player to be a Player class (just not a gameObject!)
            Container.BindInstance(_player).AsSingle();
            Container.BindInstance(_playerInputManager).AsSingle();
            Container.BindInstance(_objectInteractionControl).AsSingle();
        }
    }
}
