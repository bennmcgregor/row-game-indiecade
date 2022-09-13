using System;
using System.Collections;
using UnityEngine;

namespace IndieCade
{
    public class RotatingLightMotionController : LightMotionController
    {
        private Action<bool> OnRotateFinished;

        [SerializeField] private Transform _rotatable;
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _endPoint;
        [SerializeField] private float _angularSpeedMultiplier = 0.5f;

        private bool _forwards = true;
        private float _timeCount = 0.0f;
        private Coroutine _rotationCoroutine;

        private void Awake()
        {
            OnRotateFinished += StartRotation;
        }

        private void Start()
        {
            StartRotation();
        }

        public override void Pause()
        {
            if (_rotationCoroutine != null)
            {
                StopCoroutine(_rotationCoroutine);
            }
        }

        public override void Resume()
        {
            StartRotation(true);
        }

        private void StartRotation(bool resuming = false)
        {
            if (_forwards)
            {
                if (!resuming)
                {
                    _timeCount = 0.0f;
                }
                _rotationCoroutine = StartCoroutine(Rotate(_startPoint, _endPoint));
            } else
            {
                if (!resuming)
                {
                    _timeCount = 0.0f;
                }
                _rotationCoroutine = StartCoroutine(Rotate(_endPoint, _startPoint));
            }
        }

        private IEnumerator Rotate(Transform startPoint, Transform endPoint)
        {
            while (_timeCount < 1)
            {
                _rotatable.rotation = Quaternion.Slerp(startPoint.rotation, endPoint.rotation, _timeCount);
                _timeCount += (Time.fixedDeltaTime * _angularSpeedMultiplier);
                yield return new WaitForFixedUpdate();
            }

            _forwards = !_forwards;
            OnRotateFinished?.Invoke(false);
        }
    }
}
