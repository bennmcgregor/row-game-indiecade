using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace IndieCade
{
    public class WaterInventoryHUD : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        private PlayerWaterInventory _playerWaterInventory;

        [Inject]
        private void Initialize(PlayerWaterInventory playerWaterInventory)
        {
            _playerWaterInventory = playerWaterInventory;
        }

        private void Update()
        {
            _slider.value = _playerWaterInventory.FillAmount;
        }
    }
}
