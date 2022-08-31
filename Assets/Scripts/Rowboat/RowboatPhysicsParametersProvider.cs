using System;
using UnityEngine;

namespace IndieCade
{
    public class RowboatPhysicsParametersProvider : MonoBehaviour
    {
        [SerializeField] private RowboatPhysicsParameters _rowboatPhysicsParameters;

        private float _boatDriveForcePercentageIncrease = 0f;
        private float _boatStopForcePercentageIncrease = 0f;
        private float _minRecoverySpeedPercentageIncrease = 0f;
        private float _switchLaneSpeedPercentageIncrease = 0f;
        private float _spinAnimationTimeSecondsPercentageIncrease = 0f;

        public float SlideEndThreshold => _rowboatPhysicsParameters.SlideEndThreshold;
        public float StopSpeedThreshold => _rowboatPhysicsParameters.StopSpeedThreshold;
        public float CanTransitionFromStopSpeedThreshold => _rowboatPhysicsParameters.CanTransitionFromStopSpeedThreshold;
        public float SliderDriveSpeed => _rowboatPhysicsParameters.SliderDriveSpeed;
        public float MaxVelClamp => _rowboatPhysicsParameters.MaxVelClamp;
        public float WaitBeforeSwitchLaneTime => _rowboatPhysicsParameters.WaitBeforeSwitchLaneTime;
        public float MaxSwitchLaneTime => _rowboatPhysicsParameters.MaxSwitchLaneTime;

        public float BoatForwardsDriveForce => GetValueFromPercentage(_rowboatPhysicsParameters.BoatForwardsDriveForce, _boatDriveForcePercentageIncrease);
        public float BoatBackwardsDriveForce => GetValueFromPercentage(_rowboatPhysicsParameters.BoatBackwardsDriveForce, _boatDriveForcePercentageIncrease);
        public float BoatStopForce => GetValueFromPercentage(_rowboatPhysicsParameters.BoatStopForce, _boatStopForcePercentageIncrease);
        public float MinRecoverySpeed => GetValueFromPercentage(_rowboatPhysicsParameters.MinRecoverySpeed, _minRecoverySpeedPercentageIncrease);
        public float SwitchLaneSpeed => GetValueFromPercentage(_rowboatPhysicsParameters.SwitchLaneSpeed, _switchLaneSpeedPercentageIncrease);
        public float SpinAnimationTimeSeconds => GetValueFromPercentage(_rowboatPhysicsParameters.SpinAnimationTimeSeconds, _spinAnimationTimeSecondsPercentageIncrease);

        public void SetBoatDriveForcePercentageIncrease(float percentage)
        {
            _boatDriveForcePercentageIncrease = percentage;
        }

        public void SetBoatStopForcePercentageIncrease(float percentage)
        {
            _boatStopForcePercentageIncrease = percentage;
        }

        public void SetMinRecoverySpeedPercentageIncrease(float percentage)
        {
            _minRecoverySpeedPercentageIncrease = percentage;
        }

        public void SetSwitchLaneSpeedPercentageIncrease(float percentage)
        {
            _switchLaneSpeedPercentageIncrease = percentage;
        }

        public void SetSpinAnimationTimeSecondsPercentageIncrease(float percentage)
        {
            _spinAnimationTimeSecondsPercentageIncrease = percentage;
        }

        private float GetValueFromPercentage(float min, float percentage)
        {
            float result = min + (min * (percentage / 100f));
            return result < 0 ? 0f : result;
        }
    }
}
