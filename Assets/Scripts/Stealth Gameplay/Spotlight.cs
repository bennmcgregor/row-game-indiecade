using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class Spotlight : MonoBehaviour
    {
        [SerializeField] private float _inSpotlightDelayTimeSeconds = 0.2f;
        [SerializeField] private float _rechargeAfterCollisionTimeSeconds = 0.5f;

        private GameObject _player;
        private StealthRunner _stealthRunner;

        private Coroutine _collisionCoroutine;
        private bool _recharging = false;

        [Inject]
        public void Initialize(GameObject player, StealthRunner stealthRunner)
        {
            _player = player;
            _stealthRunner = stealthRunner;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.name == _player.name && !_recharging)
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
            _stealthRunner.CaughtInSpotlight();
            StartCoroutine(RechargeAfterCollision());
        }

        private IEnumerator RechargeAfterCollision()
        {
            _recharging = true;
            yield return new WaitForSeconds(_rechargeAfterCollisionTimeSeconds);
            _recharging = false;
        }
    }
}
