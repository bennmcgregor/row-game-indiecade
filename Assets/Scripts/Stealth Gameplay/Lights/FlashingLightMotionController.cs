using System;
using System.Collections;
using UnityEngine;

namespace IndieCade
{
    public class FlashingLightMotionController : LightMotionController
    {
        [SerializeField] private LightStateController _lightStateController;
        [SerializeField] private float _onTimeSeconds = 0.5f;
        [SerializeField] private float _offTimeSeconds = 0.5f;

        private Coroutine _flashCoroutine;

        private void Start()
        {
            Resume();
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
                yield return new WaitForSeconds(_onTimeSeconds);
                _lightStateController.Deactivate();
                yield return new WaitForSeconds(_offTimeSeconds);
            }
        }
    }
}
