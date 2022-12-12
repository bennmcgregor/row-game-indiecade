using System;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class FollowLightStateListener : GameStateListener
    {
        [SerializeField] private Spotlight _spotlight;

        private PlayerInputManager _playerInputManager;
        private bool _activated = false;

        [Inject]
        public void Initialize(PlayerInputManager playerInputManager)
        {
            _playerInputManager = playerInputManager;
        }

        protected override void Awake()
        {
            base.Awake();
            _playerInputManager.OnInputStateUpdated += OnInputStateUpdated;
        }

        protected override void InitializeChallenge(ChallengeInitializationData challengeInitializationData)
        {
            if (challengeInitializationData.FollowLightsOn)
            {
                _spotlight.ActivateLights();
                _activated = true;
            } else
            {
                _spotlight.DeactivateLights();
                _activated = false;
                _spotlight.DisableCollisionDetection();
            }
        }

        private void OnInputStateUpdated(PlayerControlInputState inputState)
        {
            if (inputState == PlayerControlInputState.ROWING && _activated)
            {
                _spotlight.EnableCollisionDetection();
            }
        }
    }
}
