using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Zenject;

namespace IndieCade
{
    public class Spotlight : GameStateListener
    {
        [SerializeField] private float _rechargeAfterCollisionTimeSeconds = 0.5f;
        [SerializeField] private Color _regularColor;
        [SerializeField] private Color _caughtColor;
        [SerializeField] private StealthCollisionDetector[] _collisionDetectors;
        [SerializeField] private LightStateController _lightStateController;
        [SerializeField] private LightMotionController _lightMotionController;
        [SerializeField] private OneShotSoundEffect _alarmSoundEffect;

        private StealthRunner _stealthRunner;
        private bool _recharging = false;
        private bool _isChasing = false;

        protected override void Awake()
        {
            foreach (var detector in _collisionDetectors)
            {
                detector.OnCaught += ProcessCatch;
            }

            base.Awake();
        }

        [Inject]
        public void Initialize(StealthRunner stealthRunner)
        {
            _stealthRunner = stealthRunner;
        }

        private void ProcessCatch()
        {
            if (!_recharging)
            {
                StartCoroutine(PauseLightOnCatch());
                _stealthRunner.CaughtInSpotlight();
                _alarmSoundEffect.Play();
            }
        }

        private IEnumerator PauseLightOnCatch()
        {
            _lightMotionController.Pause();
            if (!_isChasing)
            {
                _lightStateController.ChangeColor(_caughtColor);
            }
            _recharging = true;

            yield return new WaitForSeconds(_rechargeAfterCollisionTimeSeconds);

            _lightMotionController.Resume();
            if (!_isChasing)
            {
                _lightStateController.ChangeColor(_regularColor);
            }
            _recharging = false;
        }

        protected override void InitializeChallenge(ChallengeInitializationData challengeInitializationData)
        {
            _lightMotionController.SetChase(challengeInitializationData.IsChase);

            if (challengeInitializationData.IsChase)
            {
                _isChasing = true;
                _lightStateController.ChangeColor(_caughtColor);
                _alarmSoundEffect.Play();
            }
        }

        public void DisableCollisionDetection()
        {
            foreach (var detector in _collisionDetectors)
            {
                detector.Disable();
            }
        }

        public void EnableCollisionDetection()
        {
            foreach (var detector in _collisionDetectors)
            {
                detector.Enable();
            }
        }

        public void ActivateLights()
        {
            _lightStateController.Activate();
        }

        public void DeactivateLights()
        {
            _lightStateController.Deactivate();
        }

        // TODO: add follow light initialization (perhaps put this in a new component designed to listen for follow calls?)
            // Turn on light at start of escape canal challenge, but turn on light collision detection and motion after dialogue finishes
    }
}
