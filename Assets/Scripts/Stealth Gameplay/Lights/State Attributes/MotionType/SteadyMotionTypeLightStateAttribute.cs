using System;
using UnityEngine;

namespace IndieCade
{
    public class FollowingMotionTypeLightStateAttribute : MotionTypeLightStateAttribute
    {
        private GameObject _player;
        private float _followDelaySeconds;

        public GameObject Player => _player;
        public float FollowDelaySeconds => _followDelaySeconds;

        public FollowingMotionTypeLightStateAttribute(GameObject player, float followDelaySeconds)
            : base(LightMotionType.FOLLOWING)
        {
            _player = player;
            _followDelaySeconds = followDelaySeconds;
        }
    }
}
