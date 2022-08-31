using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class ColliderSceneChangeEffector : WorldMapSceneChangeEffector
    {
        private GameObject _player;

        [Inject]
        public void Initialize(GameObject player)
        {
            _player = player;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.name == _player.name)
            {
                ActivateScene();
            }
        }
    }
}