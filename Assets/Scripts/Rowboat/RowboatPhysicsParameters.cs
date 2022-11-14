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
        public float DragVelocityScalingFactor = 0.05f;
        public float BaseDriveRudderTurnSpeedDragFactor = 0.1f;
        public float BaseRecoveryRudderTurnSpeedDragFactor = 0.35f;

        public float BoatForwardsDriveForce = 5f;
        public float BoatBackwardsDriveForce = 2f;
        public float BoatStopForce = 1f;
        public float MinRecoverySpeed = 0.01f;
        public float MinRudderTurnSpeed = 0.1f;
        public float RudderTurnDriveSpeedMultiplier = 0.7f;
        public float RudderTurnRecoverySpeedMultiplier = 0.2f;
        public float SpinAnimationTimeSeconds = 1f;
        public float BaseDragFactor = 0.25f;

        public float InputSpamTimeoutSeconds = 0.2f;
    }
}
