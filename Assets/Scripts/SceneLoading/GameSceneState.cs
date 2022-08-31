using System;
using UnityEngine.SceneManagement;

namespace IndieCade
{
    public class GameSceneState
    {
        public Action OnGameSceneUpdated;

        private GameSceneName _currentScene;

        public GameSceneName CurrentScene => _currentScene;

        public GameSceneState()
        {
            UpdateCurrentScene(GameSceneNameMap.GetNameEnum(SceneManager.GetActiveScene().name));
        }

        public void UpdateCurrentScene(GameSceneName newScene)
        {
            _currentScene = newScene;
            OnGameSceneUpdated?.Invoke();
        }
    }
}
