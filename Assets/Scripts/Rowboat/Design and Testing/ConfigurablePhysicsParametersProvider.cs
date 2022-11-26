using System;
using UnityEngine;
using UnityEngine.UI;

namespace IndieCade
{
    // NOTE: this is very rough, just for testing purposes
    public class ConfigurablePhysicsParametersProvider : RowboatPhysicsParametersProvider
    {
        /*
            Boat stopping force-
            Boat motion force (and set a fixed ratio for the backwards motion)-
            Turn speed (make the recovery turning speed a fixed ratio)-
            Turning drag magnitude-
            Minimum speed at which a spin is possible-
            Maximum boat speed (probably need to do some math or feedback control for this)-
         */

        [SerializeField] private Slider _boatStoppingForceSlider;
        [SerializeField] private Slider _boatForwardsDriveForceSlider;
        [SerializeField] private Slider _turnSpeedMultiplierSlider;
        [SerializeField] private Slider _turnSpeedDragFactorSlider;
        [SerializeField] private Slider _stopSpeedThresholdSlider;
        [SerializeField] private Slider _maxVelocityFactorSlider;

        private float _boatStoppingForce = 0f;
        private float _boatForwardsDriveForce = 0f;
        private float _rudderTurnDriveSpeedMultiplier = 0f;
        private float _driveRudderTurnSpeedDragFactor = 0f;
        private float _canTransitionFromStopSpeedThreshold = 0f;
        private float _maxVelocityFactor = 0f;

        private void Awake()
        {
            _boatStoppingForce = _rowboatPhysicsParameters.BoatStopForce;
            _boatForwardsDriveForce = _rowboatPhysicsParameters.BoatForwardsDriveForce;
            _rudderTurnDriveSpeedMultiplier = _rowboatPhysicsParameters.RudderTurnDriveSpeedMultiplier;
            _driveRudderTurnSpeedDragFactor = _rowboatPhysicsParameters.BaseDriveRudderTurnSpeedDragFactor;
            _canTransitionFromStopSpeedThreshold = _rowboatPhysicsParameters.CanTransitionFromStopSpeedThreshold;
            _maxVelocityFactor = _rowboatPhysicsParameters.DragVelocityScalingFactor;

            _boatStoppingForceSlider.onValueChanged.AddListener(delegate { SetBoatStopForce(_boatStoppingForceSlider.value); });
            _boatForwardsDriveForceSlider.onValueChanged.AddListener(delegate { SetBoatForwardsDriveForce(_boatForwardsDriveForceSlider.value); });
            _turnSpeedMultiplierSlider.onValueChanged.AddListener(delegate { SetRudderTurnDriveSpeed(_turnSpeedMultiplierSlider.value); });
            _turnSpeedDragFactorSlider.onValueChanged.AddListener(delegate { SetDriveRudderTurnSpeedDragFactor(_turnSpeedDragFactorSlider.value); });
            _stopSpeedThresholdSlider.onValueChanged.AddListener(delegate { SetCanTransitionFromStopSpeedThreshold(_stopSpeedThresholdSlider.value); });
            _maxVelocityFactorSlider.onValueChanged.AddListener(delegate { SetMaxVelocityFactor(_maxVelocityFactorSlider.value); });
        }

        public override float BoatStopForce => _boatStoppingForce;
        public override float BoatForwardsDriveForce => _boatForwardsDriveForce;
        public override float BoatBackwardsDriveForce => BoatForwardsDriveForce * (_rowboatPhysicsParameters.BoatBackwardsDriveForce / _rowboatPhysicsParameters.BoatForwardsDriveForce);
        public override float RudderTurnDriveSpeedMultiplier => _rudderTurnDriveSpeedMultiplier;
        public override float RudderTurnRecoverySpeedMultiplier => RudderTurnDriveSpeedMultiplier * (_rowboatPhysicsParameters.RudderTurnRecoverySpeedMultiplier / _rowboatPhysicsParameters.RudderTurnDriveSpeedMultiplier);
        public override float BaseDriveRudderTurnSpeedDragFactor => _driveRudderTurnSpeedDragFactor;
        public override float BaseRecoveryRudderTurnSpeedDragFactor => BaseDriveRudderTurnSpeedDragFactor * (_rowboatPhysicsParameters.BaseRecoveryRudderTurnSpeedDragFactor / _rowboatPhysicsParameters.BaseDriveRudderTurnSpeedDragFactor);
        public override float CanTransitionFromStopSpeedThreshold => _canTransitionFromStopSpeedThreshold;
        public override float DragVelocityScalingFactor => _maxVelocityFactor;

        public void SetBoatStopForce(float force)
        {
            _boatStoppingForce = force;
        }

        public void SetBoatForwardsDriveForce(float force)
        {
            _boatForwardsDriveForce = _boatStoppingForce;
        }

        public void SetRudderTurnDriveSpeed(float speed)
        {
            _rudderTurnDriveSpeedMultiplier = speed;
        }

        public void SetDriveRudderTurnSpeedDragFactor(float factor)
        {
            _driveRudderTurnSpeedDragFactor = factor;
        }

        public void SetCanTransitionFromStopSpeedThreshold(float threshold)
        {
            _canTransitionFromStopSpeedThreshold = threshold;
        }

        public void SetMaxVelocityFactor(float factor)
        {
            _maxVelocityFactor = factor;
        }
    }
}
