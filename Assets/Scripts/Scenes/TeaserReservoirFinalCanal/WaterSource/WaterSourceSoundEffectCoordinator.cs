using System;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class WaterSourceSoundEffectCoordinator : MonoBehaviour
    {
        [SerializeField] private WaterSourceInteractable _waterSource;
        [SerializeField] private StopOnEventSoundEffect _soundEffect;

        private PlayerWaterInventory _playerWaterInventory;
        private bool _playedSoundEffect = false;

        [Inject]
        private void Initialize(PlayerWaterInventory playerWaterInventory)
        {
            _playerWaterInventory = playerWaterInventory;
        }

        private void Awake()
        {
            _waterSource.OnStartFilling += OnStartFilling;
            _waterSource.OnStopFilling += OnStopFilling;
        }

        private void OnStartFilling()
        {
            if (!_playerWaterInventory.IsFilled)
            {
                _soundEffect.Play();
                _playedSoundEffect = true;
            }
        }

        private void OnStopFilling()
        {
            if (_playedSoundEffect)
            {
                _soundEffect.InvokeStopPlayingAction();
                _playedSoundEffect = false;
            }
        }
    }
}
