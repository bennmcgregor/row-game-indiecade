using System;
using UnityEngine;
using Zenject;
using UnityEngine.SceneManagement;

namespace IndieCade
{
    public abstract class GameObjectSceneInitializer : MonoBehaviour
    {
        protected SceneInitializationDataIndex _sceneInitializationDataIndex;

        [Inject]
        public void Initialize(SceneInitializationDataIndex initializationDataIndex)
        {
            _sceneInitializationDataIndex = initializationDataIndex;
        }

        private void Start()
        {
            Execute();
        }

        private void Execute()
        {
            ExecuteInternal(_sceneInitializationDataIndex.GetDataFromCurrentScene());
        }

        protected abstract void ExecuteInternal(SceneInitializationData sceneInitializationData);
    }
}
