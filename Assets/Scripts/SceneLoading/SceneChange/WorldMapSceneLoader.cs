using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IndieCade
{
    public class WorldMapSceneLoader : MonoBehaviour
    {
        public Action OnNewSceneLoaded;

        private Dictionary<GameSceneName, Coroutine> _sceneLoadCoroutineMap;
        private Dictionary<GameSceneName, bool> _sceneShouldActivateMap;
        private Dictionary<GameSceneName, bool> _sceneShouldUnloadMap;

        private void Awake()
        {
            _sceneLoadCoroutineMap = new Dictionary<GameSceneName, Coroutine>();
            _sceneShouldActivateMap = new Dictionary<GameSceneName, bool>();
            _sceneShouldUnloadMap = new Dictionary<GameSceneName, bool>();
        }

        public void LoadScene(GameSceneName sceneName)
        {
            if (!_sceneLoadCoroutineMap.ContainsKey(sceneName))
            {
                StartNewSceneLoad(sceneName);
            }
        }

        public void UnloadScene(GameSceneName sceneName)
        {
            // you can only unload a scene that is currently being loaded
            if (_sceneLoadCoroutineMap.ContainsKey(sceneName))
            {
                _sceneShouldUnloadMap[sceneName] = true;
            }
        }

        public void ActivateScene(GameSceneName sceneName)
        {
            if (!_sceneLoadCoroutineMap.ContainsKey(sceneName))
            {
                LoadScene(sceneName);
            }
            _sceneShouldActivateMap[sceneName] = true;
        }

        private void StartNewSceneLoad(GameSceneName sceneName)
        {
            _sceneLoadCoroutineMap[sceneName] = StartCoroutine(LoadSceneCoroutine(sceneName));
            _sceneShouldActivateMap[sceneName] = false;
            _sceneShouldUnloadMap[sceneName] = false;
        }

        private void CancelNewSceneLoad(GameSceneName sceneName)
        {
            _sceneLoadCoroutineMap.Remove(sceneName);
            _sceneShouldActivateMap.Remove(sceneName);
            _sceneShouldUnloadMap.Remove(sceneName);
        }

        private IEnumerator LoadSceneCoroutine(GameSceneName sceneName)
        {
            yield return null;

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(GameSceneNameMap.GetNameString(sceneName), LoadSceneMode.Additive);
            asyncOperation.allowSceneActivation = _sceneShouldActivateMap[sceneName];
            while (!asyncOperation.isDone)
            {
                if (_sceneShouldUnloadMap[sceneName])
                {
                    _sceneShouldActivateMap[sceneName] = true;
                    asyncOperation.completed += (AsyncOperation op) => SceneManager.UnloadSceneAsync(GameSceneNameMap.GetNameString(sceneName));
                }

                if (asyncOperation.progress >= 0.9f)
                {
                    if (_sceneShouldActivateMap[sceneName])
                    {
                        asyncOperation.allowSceneActivation = _sceneShouldActivateMap[sceneName];
                    }
                }

                yield return null;
            }

            bool wasBeingUnloaded = _sceneShouldUnloadMap[sceneName];

            CancelNewSceneLoad(sceneName);

            if (!wasBeingUnloaded)
            {
                string currentSceneName = SceneManager.GetActiveScene().name;
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(GameSceneNameMap.GetNameString(sceneName)));
                SceneManager.UnloadSceneAsync(currentSceneName);
                OnSwitchNewScene();
            }
        }

        private void OnSwitchNewScene()
        {
            foreach (var key in _sceneLoadCoroutineMap.Keys)
            {
                UnloadScene(key);
            }
        }
    }
}