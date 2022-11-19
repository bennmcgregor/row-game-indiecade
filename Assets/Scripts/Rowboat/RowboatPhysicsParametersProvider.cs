using System;
using UnityEngine;

namespace IndieCade
{
    public class RowboatPhysicsParametersProvider : MonoBehaviour
    {
        [SerializeField] protected RowboatPhysicsParameters _rowboatPhysicsParameters;

        private float _boatDriveForcePercentageIncrease = 0f;
        private float _boatStopForcePercentageIncrease = 0f;
        private float _minRecoverySpeedPercentageIncrease = 0f;
        private float _rudderTurnDriveSpeedMultiplierPercentageIncrease = 0f;
        private float _spinAnimationTimeSecondsPercentageIncrease = 0f;
        private float _baseDragFactorPercentageIncrease = 0f;

        public virtual float SlideEndThreshold => _rowboatPhysicsParameters.SlideEndThreshold;
        public virtual float StopSpeedThreshold => _rowboatPhysicsParameters.StopSpeedThreshold;
        public virtual float CanTransitionFromStopSpeedThreshold => _rowboatPhysicsParameters.CanTransitionFromStopSpeedThreshold;
        public virtual float SliderDriveSpeed => _rowboatPhysicsParameters.SliderDriveSpeed;
        public virtual float MaxVelClamp => _rowboatPhysicsParameters.MaxVelClamp;
        public virtual float DragVelocityScalingFactor => _rowboatPhysicsParameters.DragVelocityScalingFactor;
        public virtual float InputSpamTimeoutSeconds => _rowboatPhysicsParameters.InputSpamTimeoutSeconds;
        public virtual float MinRudderTurnSpeed => _rowboatPhysicsParameters.MinRudderTurnSpeed;
        public virtual float RudderTurnRecoverySpeedMultiplier => _rowboatPhysicsParameters.RudderTurnRecoverySpeedMultiplier;
        public virtual float BaseDriveRudderTurnSpeedDragFactor => _rowboatPhysicsParameters.BaseDriveRudderTurnSpeedDragFactor;
        public virtual float BaseRecoveryRudderTurnSpeedDragFactor => _rowboatPhysicsParameters.BaseRecoveryRudderTurnSpeedDragFactor;

        public virtual float BoatForwardsDriveForce => GetValueFromPercentage(_rowboatPhysicsParameters.BoatForwardsDriveForce, _boatDriveForcePercentageIncrease);
        public virtual float BoatBackwardsDriveForce => GetValueFromPercentage(_rowboatPhysicsParameters.BoatBackwardsDriveForce, _boatDriveForcePercentageIncrease);
        public virtual float BoatStopForce => GetValueFromPercentage(_rowboatPhysicsParameters.BoatStopForce, _boatStopForcePercentageIncrease);
        public virtual float MinRecoverySpeed => GetValueFromPercentage(_rowboatPhysicsParameters.MinRecoverySpeed, _minRecoverySpeedPercentageIncrease);
        public virtual float RudderTurnDriveSpeedMultiplier => GetValueFromPercentage(_rowboatPhysicsParameters.RudderTurnDriveSpeedMultiplier, _rudderTurnDriveSpeedMultiplierPercentageIncrease);
        public virtual float SpinAnimationTimeSeconds => GetValueFromPercentage(_rowboatPhysicsParameters.SpinAnimationTimeSeconds, _spinAnimationTimeSecondsPercentageIncrease);
        public virtual float BaseDragFactor => GetValueFromPercentage(_rowboatPhysicsParameters.BaseDragFactor, _baseDragFactorPercentageIncrease);

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
