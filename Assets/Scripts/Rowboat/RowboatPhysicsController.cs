using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class RowboatPhysicsController : MonoBehaviour
    {
        // TODO: add this to a common scriptableobject that is injected
        private float _kSlideEndThreshold = .05f;

        public Action OnDriveFinished;
        public Action OnSwitchLaneFinished;

        [SerializeField] private float _stopSpeedThreshold = 0.01f;
        [SerializeField] private float _canTransitionFromStopSpeedThreshold = 1f;
        [SerializeField] private float _sliderDriveSpeed = 0.05f;
        [SerializeField] private float _boatDriveForce = 5f;
        [SerializeField] private float _boatStopForce = 10f;
        [SerializeField] private float _maxVelClamp = 20f;
        [SerializeField] private float _minRecoverySpeed = 0.01f;
        [SerializeField] private float _waitBeforeSwitchLaneTime = 0.1f;
        [SerializeField] private float _maxSwitchLaneTime = 0.9f;
        [SerializeField] private float _switchLaneSpeed = 5f;
        [SerializeField] private Rigidbody2D _boatRigidbody;
        [SerializeField] private LayerMask _verticalMovementMask;

        private RowboatSlideState _slideState;
        private GlobalDirectionStateMachine _globalDirectionStateMachine;

        private Coroutine _driveCoroutine;
        private Coroutine _recoveryCoroutine;
        private Coroutine _stopCoroutine;
        private float _directionMultiplier;
        private Vector2 _boatForce = Vector2.zero;
        private float _boatTorque = 0f;

        [Inject]
        public void Initialize(RowboatSlideState slideState, GlobalDirectionStateMachine globalDirectionStateMachine)
        {
            _slideState = slideState;
            _globalDirectionStateMachine = globalDirectionStateMachine;
        }

        private void FixedUpdate()
        {
            // apply a force to the boat
            _boatRigidbody.AddForce(_boatForce);
            _boatRigidbody.AddTorque(_boatTorque);
        }

        public void StartDrive(bool forwards)
        {
            _directionMultiplier = forwards ? 1f : -1f;

            // start the drive coroutine
            _driveCoroutine = StartCoroutine(DriveCoroutine(forwards));
        }

        public void StopDrive()
        {
            if (_driveCoroutine != null)
            {
                StopCoroutine(_driveCoroutine);
            }
        }

        private IEnumerator DriveCoroutine(bool forwards)
        {
            while (!IsAtFinish(forwards))
            {
                _slideState.AddValue(-1 * _sliderDriveSpeed * _directionMultiplier);
                _boatForce = GetForceDirectionVectorFromBoatDirection() * _directionMultiplier * _boatDriveForce;
                _boatTorque = 0;
                yield return new WaitForFixedUpdate();
            }

            _boatForce = Vector2.zero;
            _boatTorque = 0;

            OnDriveFinished?.Invoke();
        }

        public void StartRecovery(bool forwards)
        {
            _directionMultiplier = forwards ? 1f : -1f;

            _recoveryCoroutine = StartCoroutine(RecoveryCoroutine(forwards));
        }

        public void StopRecovery()
        {
            if (_recoveryCoroutine != null)
            {
                StopCoroutine(_recoveryCoroutine);
            }
        }

        private IEnumerator RecoveryCoroutine(bool forwards)
        {
            while (!IsAtCatch(forwards))
            {
                // slider value is proportional to the speed of the boat
                float boatVel = GetBoatVelocityFromBoatDirection();
                float slideVal = (.1f / _maxVelClamp) * Mathf.Clamp(Mathf.Abs(boatVel), 0.01f, _maxVelClamp);
                _slideState.AddValue((slideVal > _minRecoverySpeed ? slideVal : _minRecoverySpeed) * _directionMultiplier);

                _boatForce = Vector2.zero;
                _boatTorque = 0;
                yield return new WaitForFixedUpdate();
            }

            while (true)
            {
                _boatForce = Vector2.zero;
                _boatTorque = 0;
                yield return new WaitForFixedUpdate();
            }
        }

        public void StartStopBoat()
        {
            _stopCoroutine = StartCoroutine(StopBoatCoroutine());
        }

        public void EndStopBoat()
        {
            if (_stopCoroutine != null)
            {
                StopCoroutine(_stopCoroutine);
            }
        }

        private IEnumerator StopBoatCoroutine()
        {
            while (!IsStopped(_stopSpeedThreshold))
            {
                float boatVel = GetBoatVelocityFromBoatDirection();
                float slideVal = (.1f / _maxVelClamp) * Mathf.Clamp(Mathf.Abs(boatVel), 0.01f, _maxVelClamp);
                _slideState.AddValue(slideVal * _directionMultiplier);

                _boatForce = -(_boatRigidbody.mass * _boatRigidbody.velocity) * _boatStopForce;
                _boatTorque = 0;
                yield return new WaitForFixedUpdate();
            }

            while (true)
            {
                _boatForce = Vector2.zero;
                _boatTorque = 0;
                yield return new WaitForFixedUpdate();
            }
        }

        public void StartSwitchLane(bool star)
        {
            StartCoroutine(SwitchLaneCoroutine(star));
        }

        private IEnumerator SwitchLaneCoroutine(bool star)
        {
            _boatForce = Vector2.zero;
            _boatTorque = 0;

            Vector3 directionMultiplier = GetPerpendicularForceDirectionVectorFromBoatDirection(star);
            Vector3 targetPosition = directionMultiplier + transform.position;

            if (!Physics2D.OverlapCircle(targetPosition, .2f, _verticalMovementMask))
            {
                float timer = 0;
                while (timer < _waitBeforeSwitchLaneTime)
                {
                    timer += Time.fixedDeltaTime;
                    yield return new WaitForFixedUpdate();
                }

                timer = 0;
                while (Math.Abs(transform.position.y - targetPosition.y) > 0f && timer < _maxSwitchLaneTime)
                {
                    timer += Time.fixedDeltaTime;
                    Vector3 newYPos = new Vector3(transform.position.x, targetPosition.y, transform.position.z);
                    transform.position = Vector3.MoveTowards(
                        transform.position,
                        newYPos,
                        _switchLaneSpeed * Time.fixedDeltaTime
                    );
                    yield return new WaitForFixedUpdate();
                }
            }

            OnSwitchLaneFinished?.Invoke();
        }

        private bool IsAtFinish(bool forwards)
        {
            if (!forwards)
            {
                return IsAtCatch(true);
            }
            return _slideState.Value <= _kSlideEndThreshold;
        }

        private bool IsAtCatch(bool forwards)
        {
            if (!forwards)
            {
                return IsAtFinish(true);
            }
            return _slideState.Value >= 1 - _kSlideEndThreshold;
        }

        private bool IsStopped(float threshold)
        {
            return Mathf.Abs(_boatRigidbody.velocity.x) < threshold;
        }

        public bool CanTransitionFromStopped()
        {
            return IsStopped(_canTransitionFromStopSpeedThreshold);
        }

        private Vector2 GetForceDirectionVectorFromBoatDirection()
        {
            switch (_globalDirectionStateMachine.CurrentState)
            {
                case GlobalDirectionState.EAST:
                    return Vector2.right;
                default:
                    return Vector2.left;
            }
        }

        private Vector2 GetPerpendicularForceDirectionVectorFromBoatDirection(bool starPerpendicular)
        {
            switch (_globalDirectionStateMachine.CurrentState)
            {
                case GlobalDirectionState.EAST:
                    if (starPerpendicular)
                    {
                        return Vector2.down;
                    }
                    return Vector2.up;
                default:
                    if (starPerpendicular)
                    {
                        return Vector2.up;
                    }
                    return Vector2.down;
            }
        }

        private float GetBoatVelocityFromBoatDirection()
        {
            return _boatRigidbody.velocity.x;
        }
    }
}
