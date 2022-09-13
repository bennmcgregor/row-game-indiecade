using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Zenject;

namespace IndieCade
{
    public class Spotlight : MonoBehaviour
    {
        [SerializeField] private float _rechargeAfterCollisionTimeSeconds = 0.5f;
        [SerializeField] private Color _regularColor;
        [SerializeField] private Color _caughtColor;
        [SerializeField] private StealthCollisionDetector[] _collisionDetectors;
        [SerializeField] private LightStateController _lightStateController;
        [SerializeField] private LightMotionController _lightMotionController;

        private StealthRunner _stealthRunner;
        private bool _recharging = false;

        private void Awake()
        {
            foreach (var detector in _collisionDetectors)
            {
                detector.OnCaught += ProcessCatch;
            }
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
            }
        }

        private IEnumerator PauseLightOnCatch()
        {
            _lightMotionController.Pause();
            _lightStateController.ChangeColor(_caughtColor);
            _recharging = true;

            yield return new WaitForSeconds(_rechargeAfterCollisionTimeSeconds);

            _lightMotionController.Resume();
            _lightStateController.ChangeColor(_regularColor);
            _recharging = false;
        }
    }
}
