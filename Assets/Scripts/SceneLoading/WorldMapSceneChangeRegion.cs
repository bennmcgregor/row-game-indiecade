using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class WorldMapSceneChangeRegion : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private string _sceneName;

        private WorldMapSceneLoader _sceneLoader;

        [Inject]
        public void Initialize(WorldMapSceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
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