using System;
using Zenject;
using UnityEngine;

namespace IndieCade
{
    public class PlayerWaterInventoryInstaller : MonoInstaller
    {
        [SerializeField] private PlayerWaterInventory _playerWaterInventory;

        public override void InstallBindings()
        {
            Container.BindInstance(_playerWaterInventory).AsSingle();
        }
    }
}
