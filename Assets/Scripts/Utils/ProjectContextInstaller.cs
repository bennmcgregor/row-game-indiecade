using Zenject;
using UnityEngine;
using System.Collections;

namespace IndieCade
{
    public class ProjectContextInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<WorldMapSceneLoader>().FromNewComponentOnNewGameObject().AsSingle();
            Container.Bind<AudioManager>().FromNewComponentOnNewGameObject().AsSingle();
            Container.Bind<QuestRunner>().FromNewComponentOnNewGameObject().AsSingle();

            Container.Bind<QuestStateMachine>().FromNew().AsSingle();
            Container.Bind<SceneInitializationDataIndex>().FromNew().AsSingle();
            Container.Bind<GameSceneState>().FromNew().AsSingle();
        }
    }
}