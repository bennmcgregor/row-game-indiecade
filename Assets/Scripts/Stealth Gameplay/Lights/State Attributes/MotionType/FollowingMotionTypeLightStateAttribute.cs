using System;
using UnityEngine;

namespace IndieCade
{
    public class FollowingMotionTypeLightStateAttribute : MotionTypeLightStateAttribute
    {
        private GameObject _player;
        private float _initialStartRotation;
        private float _angularSpeed;
        private float _followDelaySeconds;

        public GameObject Player => _player;
        public float InitialStartRotation => _initialStartRotation;
        public float AngularSpeed => _angularSpeed;
        public float FollowDelaySeconds => _followDelaySeconds;

        public FollowingMotionTypeLightStateAttribute(GameObject player, float initialStartRotation, float angularSpeed, float followDelaySeconds)
            : base(LightMotionType.FOLLOWING)
        {
            _player = player;
            _initialStartRotation = initialStartRotation;
            _angularSpeed = angularSpeed;
            _followDelaySeconds = followDelaySeconds;
        }
    }
}
