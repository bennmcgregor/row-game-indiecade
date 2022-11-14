using System;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class DownstreamSettlementInstaller : MonoInstaller
    {
        [SerializeField] private OarPickupValidator _oarPickupValidator;
        [SerializeField] private RollableRowboatValidator _rollableRowboatValidator;

        public override void InstallBindings()
        {
            Container.BindInstance(_oarPickupValidator).AsSingle();
            Container.BindInstance(_rollableRowboatValidator).AsSingle();
        }
    }
}
