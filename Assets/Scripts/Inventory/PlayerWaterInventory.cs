using System;
using UnityEngine;

namespace IndieCade
{
    public class PlayerWaterInventory : MonoBehaviour
    {
        [SerializeField] private float _maxValue = 1f;
        [SerializeField] private float _minValue = 0f;

        private float _value = 0f;

        public bool IsFilled => _value == _maxValue;
        public float FillAmount => _value;

        public void Fill(float amount)
        {
            _value = Mathf.Clamp(_value + amount, _minValue, _maxValue);
        }
    }
}
