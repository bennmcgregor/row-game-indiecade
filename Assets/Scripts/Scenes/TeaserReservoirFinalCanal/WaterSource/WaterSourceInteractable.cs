using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class WaterSourceInteractable : ObjectInteractable, IActivateable
    {
        public Action OnStartFilling;
        public Action OnStopFilling;

        private float _kFillAmount = 0.01f;

        [SerializeField] private float _fillRateSeconds = 0.05f;
        [SerializeField] private ToggleableAnimation _animation;

        private Coroutine _fillWaterCoroutine;
        private PlayerWaterInventory _playerWaterInventory;

        [Inject]
        private void Initialize(PlayerWaterInventory playerWaterInventory)
        {
            _playerWaterInventory = playerWaterInventory;
        }

        private void Awake()
        {
            OnStartFilling += _animation.Play;
            OnStopFilling += _animation.Stop;
        }

        protected override void OnStartInteractionInternal()
        {
            _objectInteractionControl.UpdateInputState(PlayerControlInputState.INTERACTING);
            _fillWaterCoroutine = StartCoroutine(FillWaterCoroutine());
            OnStartFilling?.Invoke();
        }

        protected override void OnEndInteractionInternal()
        {
            _objectInteractionControl.RevertInputState();
            if (_fillWaterCoroutine != null)
            {
                StopCoroutine(_fillWaterCoroutine);
            }
            OnStopFilling?.Invoke();
        }

        private IEnumerator FillWaterCoroutine()
        {
            while (!_playerWaterInventory.IsFilled)
            {
                _playerWaterInventory.Fill(_kFillAmount);
                yield return new WaitForSeconds(_fillRateSeconds);
            }
            OnStopFilling?.Invoke();
        }

        public void Activate() {}

        public void Deactivate()
        {
            OnStopFilling?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
