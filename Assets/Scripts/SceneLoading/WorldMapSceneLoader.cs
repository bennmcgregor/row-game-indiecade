using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IndieCade
{
    public class WorldMapSceneLoader : MonoBehaviour
    {
        private Dictionary<string, Coroutine> _sceneLoadCoroutineMap;
        private Dictionary<string, bool> _sceneShouldActivateMap;
        private Dictionary<string, bool> _sceneShouldUnloadMap;

        private void Awake()
        {
            _sceneLoadCoroutineMap = new Dictionary<string, Coroutine>();
            _sceneShouldActivateMap = new Dictionary<string, bool>();
            _sceneShouldUnloadMap = new Dictionary<string, bool>();
        }

        public void LoadScene(string sceneName)
        {
            if (!_sceneLoadCoroutineMap.ContainsKey(sceneName))
            {
                StartNewSceneLoad(sceneName);
            }
        }

        public void UnloadScene(string sceneName)
        {
            // you can only unload a scene that is currently being loaded
            if (_sceneLoadCoroutineMap.ContainsKey(sceneName))
            {
                _sceneShouldUnloadMap[sceneName] = true;
            }
        }

        public void ActivateScene(string sceneName)
        {
            if (!_sceneLoadCoroutineMap.ContainsKey(sceneName))
            {
                LoadScene(sceneName);
            }
            _sceneShouldActivateMap[sceneName] = true;
        }

        private void StartNewSceneLoad(string sceneName)
        {
            _sceneLoadCoroutineMap[sceneName] = StartCoroutine(LoadSceneCoroutine(sceneName));
            _sceneShouldActivateMap[sceneName] = false;
            _sceneShouldUnloadMap[sceneName] = false;
        }

        private void CancelNewSceneLoad(string sceneName)
        {
            _sceneLoadCoroutineMap.Remove(sceneName);
            _sceneShouldActivateMap.Remove(sceneName);
            _sceneShouldUnloadMap.Remove(sceneName);
        }

        private IEnumerator LoadSceneCoroutine(string sceneName)
        {
            yield return null;

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            asyncOperation.allowSceneActivation = _sceneShouldActivateMap[sceneName];
            while (!asyncOperation.isDone)
            {
                if (_sceneShouldUnloadMap[sceneName])
                {
                    _sceneShouldActivateMap[sceneName] = true;
                    asyncOperation.completed += (AsyncOperation op) => SceneManager.UnloadSceneAsync(sceneName);
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
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
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