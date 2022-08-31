using UnityEngine;
using Zenject;
using UnityEngine.SceneManagement;

namespace IndieCade
{
    public class WorldMapSceneChangeEffector : MonoBehaviour
    {
        [SerializeField] private GameSceneName _sceneName;
        [SerializeField] private PlayerSpawnPoint _newPlayerSpawnPoint = null;

        private WorldMapSceneLoader _sceneLoader;
        private SceneInitializationDataIndex _sceneInitializationDataIndex;
        private GameSceneState _gameSceneState;

        [Inject]
        public void Initialize(WorldMapSceneLoader sceneLoader, SceneInitializationDataIndex sceneInitializationDataIndex, GameSceneState gameSceneState)
        {
            _sceneLoader = sceneLoader;
            _sceneInitializationDataIndex = sceneInitializationDataIndex;
            _gameSceneState = gameSceneState;
        }

        protected void ActivateScene()
        {
            _gameSceneState.UpdateCurrentScene(_sceneName);
            _sceneInitializationDataIndex.GetData(_sceneName).PlayerSpawnPoint = _newPlayerSpawnPoint;
            _sceneLoader.ActivateScene(_sceneName);
        }
    }
}