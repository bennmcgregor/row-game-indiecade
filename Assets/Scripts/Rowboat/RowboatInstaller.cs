using Zenject;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace IndieCade
{
    public class RowboatInstaller : MonoInstaller 
    {
        [SerializeField] private RowboatPhysicsController _physicsController;
        [SerializeField] private RowboatAnimator _rowboatAnimator;
        [SerializeField] private RowboatPhysicsParametersProvider _rowboatPhysicsParametersProvider;
        [SerializeField] private RowboatPlayerInputs _rowboatPlayerInputs;

        public override void InstallBindings()
        {
            Container.Bind<RowingStateMachine>().FromNew().AsSingle();
            Container.BindInstance(_physicsController).AsSingle();
            Container.BindInstance(_rowboatAnimator).AsSingle();
            Container.Bind<RowboatSlideState>().FromNew().AsSingle().WithArguments(0f);
            Container.Bind<GlobalDirectionStateMachine>().FromNew().AsSingle();
            Container.BindInstance(_rowboatPlayerInputs).AsSingle();
            Container.Bind<RowboatMaps>().FromNew().AsSingle();
            Container.BindInstance(_rowboatPhysicsParametersProvider).AsSingle();
        }
    }
}