using Zenject;
using UnityEngine;
using System.Collections;

namespace IndieCade
{
    public class ProjectContextInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _audioManagerPrefab;

        public override void InstallBindings()
        {
            Container.Bind<AudioManager>().FromComponentInNewPrefab(_audioManagerPrefab).AsSingle();
        }
    }
}