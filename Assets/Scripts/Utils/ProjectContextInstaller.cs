using Zenject;
using UnityEngine;
using System.Collections;

namespace IndieCade
{
    public class ProjectContextInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<AudioManager>().FromNewComponentOnNewGameObject().AsSingle();
        }
    }
}