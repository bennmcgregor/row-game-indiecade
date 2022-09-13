using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class WorldMapSceneChangeEffector : MonoBehaviour
    {
        private WorldMapSceneLoader _sceneLoader;
        private GameSceneState _gameSceneState;

        [Inject]
        public void Initialize(WorldMapSceneLoader sceneLoader, GameSceneState gameSceneState)
        {
            _sceneLoader = sceneLoader;
            _gameSceneState = gameSceneState;
        }

        public void ActivateScene(GameSceneName sceneName)
        {
            _gameSceneState.UpdateCurrentScene(sceneName);
            // TODO: add OnSceneExit invocation
            _sceneLoader.ActivateScene(sceneName);
        }
    }
}