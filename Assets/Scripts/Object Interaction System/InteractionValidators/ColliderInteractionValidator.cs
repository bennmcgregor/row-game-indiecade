using System;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class ColliderInteractionValidator : MonoBehaviour, IInteractionValidator
    {
        public Action OnInRange;
        public Action OnOutOfRange;

        private GameObject _player;
        private bool _isInRange = false;

        [Inject]
        private void Initialize(GameObject player)
        {
            _player = player;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == _player.name)
            {
                _isInRange = true;
                OnInRange?.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.name == _player.name)
            {
                _isInRange = false;
                OnOutOfRange?.Invoke();
            }
        }


        public bool Validate()
        {
            return _isInRange;
        }
    }
}
