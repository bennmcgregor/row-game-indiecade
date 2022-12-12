using System;
using System.Collections;
using UnityEngine;

namespace IndieCade
{
    public class RotatingLightMotionTypeManager : LightMotionTypeManager
    {
        private Action<bool> OnRotateFinished;

        private Quaternion _startAngle;
        private Quaternion _endAngle;
        private float _angularSpeed;
        private bool _forwards = true;
        private float _timeCount = 0.0f;
        private Coroutine _rotationCoroutine;

        private void Awake()
        {
            OnRotateFinished += StartRotation;
        }

        public override LightMotionType Type => LightMotionType.ROTATING;

        public override void Resume(MotionTypeLightStateAttribute motionTypeLightStateAttribute)
        {
            RotatingMotionTypeLightStateAttribute rotatingData = (RotatingMotionTypeLightStateAttribute) motionTypeLightStateAttribute;

            _startAngle = Quaternion.Euler(_lightInstance.MoveableTransform.rotation.x, _lightInstance.MoveableTransform.rotation.y, rotatingData.StartAngle);
            _endAngle = Quaternion.Euler(_lightInstance.MoveableTransform.rotation.x, _lightInstance.MoveableTransform.rotation.y, rotatingData.EndAngle);
            _angularSpeed = rotatingData.Speed;

            StartRotation(true);
        }

        public override void Pause()
        {
            if (_rotationCoroutine != null)
            {
                StopCoroutine(_rotationCoroutine);
            }
        }

        private void StartRotation(bool resuming = false)
        {
            if (_forwards)
            {
                if (!resuming)
                {
                    _timeCount = 0.0f;
                }
                _rotationCoroutine = StartCoroutine(Rotate(_startAngle, _endAngle));
            }
            else
            {
                if (!resuming)
                {
                    _timeCount = 0.0f;
                }
                _rotationCoroutine = StartCoroutine(Rotate(_endAngle, _startAngle));
            }
        }

        private IEnumerator Rotate(Quaternion startAngle, Quaternion endAngle)
        {
            while (_timeCount < 1)
            {
                _lightInstance.MoveableTransform.rotation = Quaternion.Slerp(startAngle, endAngle, _timeCount);
                _timeCount += (Time.fixedDeltaTime * _angularSpeed);
                yield return new WaitForFixedUpdate();
            }

            _forwards = !_forwards;
            OnRotateFinished?.Invoke(false);
        }
    }
}
