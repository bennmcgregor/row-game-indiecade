using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class StealthCollisionDetector : MonoBehaviour, IEnableable
    {
        public Action OnCaught;

        [SerializeField] private float _inSpotlightDelayTimeSeconds = 0.2f;

        private GameObject _player;
        private Coroutine _collisionCoroutine;
        private bool _enabled = true;

        [Inject]
        public void Initialize(GameObject player)
        {
            _player = player;
        }

        public void Disable()
        {
            _enabled = false;
        }

        public void Enable()
        {
            _enabled = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.name == _player.name && _enabled)
            {
                _collisionCoroutine = StartCoroutine(ProcessCollision());
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.name == _player.name && _collisionCoroutine != null)
            {
                StopCoroutine(_collisionCoroutine);
                _collisionCoroutine = null;
            }
        }

        private IEnumerator ProcessCollision()
        {
            yield return new WaitForSeconds(_inSpotlightDelayTimeSeconds);
            OnCaught?.Invoke();
        }
    }
}
