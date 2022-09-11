using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class ColliderSceneChangeEffector : MonoBehaviour
    {
        [SerializeField] private GameSceneName _sceneName;

        private WorldMapSceneChangeEffector _sceneChangeEffector;
        private GameObject _player;

        [Inject]
        public void Initialize(GameObject player, WorldMapSceneChangeEffector worldMapSceneChangeEffector)
        {
            _player = player;
            _sceneChangeEffector = worldMapSceneChangeEffector;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.name == _player.name)
            {
                _sceneChangeEffector.ActivateScene(_sceneName);
            }
        }
    }
}