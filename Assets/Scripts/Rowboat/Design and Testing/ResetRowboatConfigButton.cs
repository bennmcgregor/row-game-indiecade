using System;
using UnityEngine;
using UnityEngine.UI;

namespace IndieCade
{
    public class ResetRowboatConfigButton : MonoBehaviour
    {
        [SerializeField] private Slider _boatStoppingForceSlider;
        [SerializeField] private Slider _boatForwardsDriveForceSlider;
        [SerializeField] private Slider _turnSpeedMultiplierSlider;
        [SerializeField] private Slider _turnSpeedDragFactorSlider;
        [SerializeField] private Slider _stopSpeedThresholdSlider;
        [SerializeField] private Slider _maxVelocityFactorSlider;

        private float _initialStopForce;
        private float _initialDriveForce;
        private float _initialTurnSpeedMult;
        private float _initialTurnSpeedDrag;
        private float _initialStopSpeedThreshold;
        private float _initialMaxVelocityFactor;

        private void Awake()
        {
            _initialStopForce = _boatStoppingForceSlider.value;
            _initialDriveForce = _boatForwardsDriveForceSlider.value;
            _initialTurnSpeedMult = _turnSpeedMultiplierSlider.value;
            _initialTurnSpeedDrag = _turnSpeedDragFactorSlider.value;
            _initialStopSpeedThreshold = _stopSpeedThresholdSlider.value;
            _initialMaxVelocityFactor = _maxVelocityFactorSlider.value;
        }

        public void ResetSliders()
        {
            _boatStoppingForceSlider.value = _initialStopForce;
            _boatForwardsDriveForceSlider.value = _initialDriveForce;
            _turnSpeedMultiplierSlider.value = _initialTurnSpeedMult;
            _turnSpeedDragFactorSlider.value = _initialTurnSpeedDrag;
            _stopSpeedThresholdSlider.value = _initialStopSpeedThreshold;
            _maxVelocityFactorSlider.value = _initialMaxVelocityFactor;
        }
    }
}
