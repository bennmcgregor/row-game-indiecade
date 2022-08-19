using Zenject;
using UnityEngine;
using System.Collections;

namespace IndieCade
{
    public class RowboatInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            //Container.Bind<RowboatPhysicsController>().FromNewComponentOnNewGameObject().AsSingle();
            // TODO: bind RowboatPhysicsController
            // TODO: bind RowingStateMachine, initialized with RowingState.FORWARDS_RECOV
            // TODO: bind RowboatSlideState, initialized with 0
        }
    }
}