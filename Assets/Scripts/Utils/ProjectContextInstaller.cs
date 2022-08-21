using Zenject;
using UnityEngine;
using System.Collections;

public class ProjectContextInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<WorldMapSceneLoader>().FromNewComponentOnNewGameObject().AsSingle();
        Container.Bind<AudioManager>().FromNewComponentOnNewGameObject().AsSingle();
    }
}