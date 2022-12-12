using System;
using UnityEngine;

namespace IndieCade
{
    [CreateAssetMenu(fileName = "New LightsCustomizerParameters", menuName = "Lights Customizer Parameters")]
    public class LightsCustomizerParameters : ScriptableObject
    {
        public float CircleShapeHeight = 5f;
        public float MaxFlashSpeed = 5f;
        public float MaxRotatingSpeed = 5f;
        public float BottomCanalStartRotatingAngle = 90f;
        public float BottomCanalEndRotatingAngle = 270f;
        public float TopCanalStartRotatingAngle = 270f;
        public float TopCanalEndRotatingAngle = 90f;
        public float BeamInnerArcAngle = 18.5f;
        public float BeamOuterArcAngle = 26f;
        public float FollowingMotionStartAngle = 0f;
        public float MaxFollowSpeed = 5f;
        public float FollowDelaySeconds = 0.2f;
    }
}
