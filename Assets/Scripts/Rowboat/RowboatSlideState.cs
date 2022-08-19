using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieCade
{
    public class RowboatSlideState
    {
        // TODO: move this to a common scriptable object that is injected
        private float _kMinValue = 0f;
        private float _kMaxValue = 1f;

        public float Value => _value;

        private float _value;

        public RowboatSlideState(float initialValue)
        {
            _value = initialValue;
        }

        public void AddValue(float value)
        {
            _value = Mathf.Clamp(value + _value, _kMinValue, _kMaxValue);
        }
    }
}
