using System;
using UnityEngine;

namespace IndieCade
{
    [CreateAssetMenu(fileName = "New RowboatPhysicsParameters", menuName = "Rowboat Physics Parameters")]
    public class RowboatPhysicsParameters : ScriptableObject
    {
        public float SlideEndThreshold = .05f;
        public float StopSpeedThreshold = 0.01f;
        public float CanTransitionFromStopSpeedThreshold = 1f;
        public float SliderDriveSpeed = 0.05f;
        public float MaxVelClamp = 20f;
        // TODO(rudder): remove MaxSwitchLaneTime and WaitBeforeSwitchLaneTime
        public float WaitBeforeSwitchLaneTime = 0.1f;
        public float MaxSwitchLaneTime = 0.9f;
        public float DragVelocityScalingFactor = 0.05f;
        public float BaseSwitchLaneSpeedDragFactor = 0.05f;

        public float BoatForwardsDriveForce = 5f;
        public float BoatBackwardsDriveForce = 2f;
        public float BoatStopForce = 1f;
        public float MinRecoverySpeed = 0.01f;
        // TODO(rudder): remove SwitchLaneSpeed
        public float SwitchLaneSpeed = 5f;
        public float SwitchLaneSpeedMultiplier = 0.2f;
        public float SpinAnimationTimeSeconds = 1f;
        public float BaseDragFactor = 0.25f;

        public float InputSpamTimeoutSeconds = 0.2f;
    }
}
