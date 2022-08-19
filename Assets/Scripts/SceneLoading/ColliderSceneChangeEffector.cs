using UnityEngine;

namespace IndieCade
{
    public class ColliderSceneChangeEffector : WorldMapSceneChangeEffector
    {
        [SerializeField] private GameObject _player;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.name == _player.name)
            {
                ActivateScene();
            }
        }
    }
}