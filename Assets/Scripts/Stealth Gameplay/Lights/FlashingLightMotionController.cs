using System;
using System.Collections;
using UnityEngine;

namespace IndieCade
{
    public class FlashingLightMotionController : LightMotionController
    {
        [SerializeField] private LightStateController _lightStateController;
        [SerializeField] private float _normalOnTimeSeconds = 1f;
        [SerializeField] private float _normalOffTimeSeconds = 1.5f;
        [SerializeField] private float _chaseOnTimeSeconds = 2f;
        [SerializeField] private float _chaseOffTimeSeconds = 0.75f;

        private Coroutine _flashCoroutine;
        private bool _isChasing = false;

        private float OnTimeSeconds => _isChasing ? _chaseOnTimeSeconds : _normalOnTimeSeconds;
        private float OffTimeSeconds => _isChasing ? _chaseOffTimeSeconds : _normalOffTimeSeconds;

        private void Start()
        {
            Resume();
        }

        public override void SetChase(bool isChasing)
        {
            _isChasing = isChasing;
        }

        public override void Pause()
        {
            StopCoroutine(_flashCoroutine);
        }

        public override void Resume()
        {
            _flashCoroutine = StartCoroutine(Flash());
        }

        private IEnumerator Flash()
        {
            while (true)
            {
                _lightStateController.Activate();
                yield return new WaitForSeconds(OnTimeSeconds);
                _lightStateController.Deactivate();
                yield return new WaitForSeconds(OffTimeSeconds);
            }
        }
    }
}
