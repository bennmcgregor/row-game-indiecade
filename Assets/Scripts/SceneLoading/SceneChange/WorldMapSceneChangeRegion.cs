using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class WorldMapSceneChangeRegion : MonoBehaviour
    {
        [SerializeField] private GameSceneName _sceneName;

        private WorldMapSceneLoader _sceneLoader;
        private GameObject _player;

        [Inject]
        public void Initialize(WorldMapSceneLoader sceneLoader, GameObject player)
        {
            _sceneLoader = sceneLoader;
            _player = player;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.name == _player.name)
            {
                _sceneLoader.LoadScene(_sceneName);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.name == _player.name)
            {
                _sceneLoader.UnloadScene(_sceneName);
            }
        }
    }
}