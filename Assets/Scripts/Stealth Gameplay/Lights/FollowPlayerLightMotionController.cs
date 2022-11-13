using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class FollowPlayerLightMotionController : LightMotionController
    {
        [SerializeField] private Transform _rotatable;
        [SerializeField] private Transform _rotatableFollowPoint;
        [SerializeField] private Transform _initialStartPoint;
        [SerializeField] private float _followDelaySeconds = 0.2f;
        [SerializeField] private float _normalAngularSpeedMultiplier = 0.25f;
        [SerializeField] private float _chaseAngularSpeedMultiplier = 0.5f;

        private GameObject _player;
        private bool _isChasing = false;
        private Vector3 _prevPlayerPosition;
        private Quaternion _rotationToPlayer;
        private Quaternion _startRotation;
        private float _timeCount = 0.0f;

        private Coroutine _rotateToPlayerCoroutine;
        private Coroutine _checkPlayerPositionCoroutine;

        private float AngularSpeedMultiplier => _isChasing ? _chaseAngularSpeedMultiplier : _normalAngularSpeedMultiplier;

        [Inject]
        public void Initialize(GameObject player)
        {
            _player = player;
        }

        private void Start()
        {
            StartFollow();
        }

        public override void SetChase(bool isChasing)
        {
            _isChasing = isChasing;
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

        public override void Resume()
        {
            _startRotation = _rotatable.rotation;
            StartCoroutines();
        }

        private void StartFollow()
        {
            _startRotation = _initialStartPoint.rotation;
            StartCoroutines();
        }

        private void StartCoroutines()
        {
            _rotateToPlayerCoroutine = StartCoroutine(RotateToPlayer());
            _checkPlayerPositionCoroutine = StartCoroutine(CheckPlayerPosition());
        }

        private IEnumerator CheckPlayerPosition()
        {
            _rotationToPlayer = _rotatable.rotation;
            while (true)
            {
                _prevPlayerPosition = _player.transform.position;
                yield return new WaitForSeconds(_followDelaySeconds);

                Vector3 from = _rotatableFollowPoint.position - transform.position;
                Vector3 to = _player.transform.position - transform.position;
                _rotationToPlayer = _rotatable.rotation * Quaternion.AngleAxis(Vector2.SignedAngle(from, to), Vector3.forward);
                _startRotation = _rotatable.rotation;
                _timeCount = 0.0f;
            }
        }

        private IEnumerator RotateToPlayer()
        {
            while (true)
            {
                _rotatable.rotation = Quaternion.Slerp(_startRotation, _rotationToPlayer, _timeCount);
                _timeCount += (Time.fixedDeltaTime * AngularSpeedMultiplier);
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
