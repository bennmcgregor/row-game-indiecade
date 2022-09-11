using System;
using Zenject;

namespace IndieCade
{
    public class SceneLoadingInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<WorldMapSceneLoader>().FromNewComponentOnNewGameObject().AsSingle();
            Container.Bind<WorldMapSceneChangeEffector>().FromNewComponentOnNewGameObject().AsSingle();

            Container.Bind<SceneInitializationDataIndex>().FromNew().AsSingle();
            Container.Bind<GameSceneState>().FromNew().AsSingle();
        }
    }
}
