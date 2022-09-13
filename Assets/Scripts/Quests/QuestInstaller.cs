using System;
using Zenject;
using UnityEngine;

namespace IndieCade
{
    public class QuestInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameQuests>().FromNew().AsSingle();
            Container.Bind<QuestRunner>().FromNewComponentOnNewGameObject().AsSingle();
            Container.Bind<QuestStateMachine>().FromNew().AsSingle();
        }
    }
}
