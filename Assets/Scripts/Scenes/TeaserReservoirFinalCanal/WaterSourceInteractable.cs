using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class WaterSourceInteractable : ObjectInteractable
    {
        private float _kFillAmount = 0.01f;

        [SerializeField] private float _fillRateSeconds = 0.05f;
        [SerializeField] private ColliderInteractionValidator _colliderInteractionValidator;
        [SerializeField] private RowboatStoppedInteractionValidator _rowboatStoppedInteractionValidator;

        private Coroutine _fillWaterCoroutine;
        private PlayerWaterInventory _playerWaterInventory;

        [Inject]
        private void Initialize(PlayerWaterInventory playerWaterInventory)
        {
            _playerWaterInventory = playerWaterInventory;
        }

        protected override void InitializeValidators()
        {
            _validators.Add(_colliderInteractionValidator);
            _validators.Add(_rowboatStoppedInteractionValidator);
        }

        protected override void OnStartInteractionInternal()
        {
            _objectInteractionControl.UpdateInputState(PlayerControlInputState.INTERACTING);
            _fillWaterCoroutine = StartCoroutine(FillWaterCoroutine());
        }

        protected override void OnEndInteractionInternal()
        {
            _objectInteractionControl.RevertInputState();
            StopCoroutine(_fillWaterCoroutine);
        }

        private IEnumerator FillWaterCoroutine()
        {
            while (!_playerWaterInventory.IsFilled)
            {
                _playerWaterInventory.Fill(_kFillAmount);
                yield return new WaitForSeconds(_fillRateSeconds);
            }
        }
    }
}
