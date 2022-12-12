using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class FollowingLightMotionTypeManager : LightMotionTypeManager
    {
        private bool _hasNeverStarted = true;
        private Quaternion _rotationToPlayer;
        private Quaternion _startRotation;
        private float _timeCount = 0.0f;

        private Coroutine _rotateToPlayerCoroutine;
        private Coroutine _checkPlayerPositionCoroutine;

        public override LightMotionType Type => LightMotionType.FOLLOWING;

        public override void Resume(MotionTypeLightStateAttribute motionTypeLightStateAttribute)
        {
            FollowingMotionTypeLightStateAttribute followingData = (FollowingMotionTypeLightStateAttribute)motionTypeLightStateAttribute;

            if (_hasNeverStarted)
            {
                _startRotation = Quaternion.Euler(_lightInstance.MoveableTransform.rotation.x, _lightInstance.MoveableTransform.rotation.y, followingData.InitialStartRotation);
            }
            else
            {
                _startRotation = _lightInstance.MoveableTransform.rotation;
            }
            StartCoroutines(followingData);
        }

        public override void Pause()
        {
            if (_rotateToPlayerCoroutine != null)
            {
                StopCoroutine(_rotateToPlayerCoroutine);
            }
            if (_checkPlayerPositionCoroutine != null)
            {
                StopCoroutine(_checkPlayerPositionCoroutine);
            }
        }

        private void StartCoroutines(FollowingMotionTypeLightStateAttribute followingData)
        {
            _rotateToPlayerCoroutine = StartCoroutine(RotateToPlayer(followingData));
            _checkPlayerPositionCoroutine = StartCoroutine(CheckPlayerPosition(followingData));
        }

        private IEnumerator CheckPlayerPosition(FollowingMotionTypeLightStateAttribute followingData)
        {
            _rotationToPlayer = _lightInstance.MoveableTransform.rotation;
            while (true)
            {
                yield return new WaitForSeconds(followingData.FollowDelaySeconds);

                Vector3 from = _lightInstance.RotatableFollowPoint.position - transform.position;
                Vector3 to = followingData.Player.transform.position - transform.position;
                _rotationToPlayer = _lightInstance.MoveableTransform.rotation * Quaternion.AngleAxis(Vector2.SignedAngle(from, to), Vector3.forward);
                _startRotation = _lightInstance.MoveableTransform.rotation;
                _timeCount = 0.0f;
            }
        }

        private IEnumerator RotateToPlayer(FollowingMotionTypeLightStateAttribute followingData)
        {
            while (true)
            {
                _lightInstance.MoveableTransform.rotation = Quaternion.Slerp(_startRotation, _rotationToPlayer, _timeCount);
                _timeCount += (Time.fixedDeltaTime * followingData.AngularSpeed);
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
