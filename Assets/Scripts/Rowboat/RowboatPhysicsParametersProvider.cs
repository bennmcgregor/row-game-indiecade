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
        private float _rudderTurnDriveSpeedMultiplierPercentageIncrease = 0f;
        private float _spinAnimationTimeSecondsPercentageIncrease = 0f;
        private float _baseDragFactorPercentageIncrease = 0f;

        public float SlideEndThreshold => _rowboatPhysicsParameters.SlideEndThreshold;
        public float StopSpeedThreshold => _rowboatPhysicsParameters.StopSpeedThreshold;
        public float CanTransitionFromStopSpeedThreshold => _rowboatPhysicsParameters.CanTransitionFromStopSpeedThreshold;
        public float SliderDriveSpeed => _rowboatPhysicsParameters.SliderDriveSpeed;
        public float MaxVelClamp => _rowboatPhysicsParameters.MaxVelClamp;
        public float DragVelocityScalingFactor => _rowboatPhysicsParameters.DragVelocityScalingFactor;
        public float InputSpamTimeoutSeconds => _rowboatPhysicsParameters.InputSpamTimeoutSeconds;
        public float MinRudderTurnSpeed => _rowboatPhysicsParameters.MinRudderTurnSpeed;
        public float RudderTurnRecoverySpeedMultiplier => _rowboatPhysicsParameters.RudderTurnRecoverySpeedMultiplier;
        public float BaseDriveRudderTurnSpeedDragFactor => _rowboatPhysicsParameters.BaseDriveRudderTurnSpeedDragFactor;
        public float BaseRecoveryRudderTurnSpeedDragFactor => _rowboatPhysicsParameters.BaseRecoveryRudderTurnSpeedDragFactor;

        public float BoatForwardsDriveForce => GetValueFromPercentage(_rowboatPhysicsParameters.BoatForwardsDriveForce, _boatDriveForcePercentageIncrease);
        public float BoatBackwardsDriveForce => GetValueFromPercentage(_rowboatPhysicsParameters.BoatBackwardsDriveForce, _boatDriveForcePercentageIncrease);
        public float BoatStopForce => GetValueFromPercentage(_rowboatPhysicsParameters.BoatStopForce, _boatStopForcePercentageIncrease);
        public float MinRecoverySpeed => GetValueFromPercentage(_rowboatPhysicsParameters.MinRecoverySpeed, _minRecoverySpeedPercentageIncrease);
        public float RudderTurnDriveSpeedMultiplier => GetValueFromPercentage(_rowboatPhysicsParameters.RudderTurnDriveSpeedMultiplier, _rudderTurnDriveSpeedMultiplierPercentageIncrease);
        public float SpinAnimationTimeSeconds => GetValueFromPercentage(_rowboatPhysicsParameters.SpinAnimationTimeSeconds, _spinAnimationTimeSecondsPercentageIncrease);
        public float BaseDragFactor => GetValueFromPercentage(_rowboatPhysicsParameters.BaseDragFactor, _baseDragFactorPercentageIncrease);

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

        public void SetRudderTurnDriveSpeedMultiplierPercentageIncrease(float percentage)
        {
            _rudderTurnDriveSpeedMultiplierPercentageIncrease = percentage;
        }

        public void SetSpinAnimationTimeSecondsPercentageIncrease(float percentage)
        {
            _spinAnimationTimeSecondsPercentageIncrease = percentage;
        }

        public void SetBaseDragFactorPercentageIncrease(float percentage)
        {
            _baseDragFactorPercentageIncrease = percentage;
        }

        private float GetValueFromPercentage(float min, float percentage)
        {
            float result = min + (min * (percentage / 100f));
            return result < 0 ? 0f : result;
        }
    }
}
