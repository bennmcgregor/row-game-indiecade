using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class RowboatPhysicsController : MonoBehaviour
    {
        public Action OnDriveFinished;
        private Action OnSwitchLaneFinished;
        public Action PlayCatchSoundEffect;
        public Action PlayFinishSoundEffect;

        [SerializeField] private Rigidbody2D _boatRigidbody;
        [SerializeField] private LayerMask _verticalMovementMask;

        private RowboatSlideState _slideState;
        private GlobalDirectionStateMachine _globalDirectionStateMachine;
        private RowboatPhysicsParametersProvider _rowboatPhysicsParametersProvider;

        private Coroutine _driveCoroutine;
        private Coroutine _recoveryCoroutine;
        private Coroutine _stopCoroutine;
        private float _directionMultiplier;
        private Vector2 _boatForce = Vector2.zero;
        private float _boatTorque = 0f;
        private float _rudderDrag = 0f;
        private bool _isSwitchingLanes = false;

        [Inject]
        public void Initialize(RowboatSlideState slideState, GlobalDirectionStateMachine globalDirectionStateMachine, RowboatPhysicsParametersProvider rowboatPhysicsParametersProvider)
        {
            _slideState = slideState;
            _globalDirectionStateMachine = globalDirectionStateMachine;
            _rowboatPhysicsParametersProvider = rowboatPhysicsParametersProvider;
        }

        private void FixedUpdate()
        {
            // apply a force to the boat
            _boatRigidbody.AddForce(_boatForce);
            _boatRigidbody.AddTorque(_boatTorque);

            _boatRigidbody.drag = _rowboatPhysicsParametersProvider.BaseDragFactor + _rudderDrag +
                _rowboatPhysicsParametersProvider.DragVelocityScalingFactor *
                Mathf.Abs(_boatRigidbody.velocity.x);

            // reset the _rudderDrag after it has been applied
            _rudderDrag = 0f;
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
            PlayCatchSoundEffect?.Invoke();

            while (!IsAtFinish(forwards))
            {
                _slideState.AddValue(-1 * _rowboatPhysicsParametersProvider.SliderDriveSpeed * _directionMultiplier);
                float driveForce = forwards ? _rowboatPhysicsParametersProvider.BoatForwardsDriveForce : _rowboatPhysicsParametersProvider.BoatBackwardsDriveForce;
                _boatForce = GetForceDirectionVectorFromBoatDirection() * _directionMultiplier * driveForce;
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
            PlayFinishSoundEffect?.Invoke();

            while (!IsAtCatch(forwards))
            {
                // slider value is proportional to the speed of the boat
                float boatVel = GetBoatVelocityFromBoatDirection();
                float slideVal = (.1f / _rowboatPhysicsParametersProvider.MaxVelClamp) * Mathf.Clamp(Mathf.Abs(boatVel), 0.01f, _rowboatPhysicsParametersProvider.MaxVelClamp);
                _slideState.AddValue((slideVal > _rowboatPhysicsParametersProvider.MinRecoverySpeed ? slideVal : _rowboatPhysicsParametersProvider.MinRecoverySpeed) * _directionMultiplier);

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
                _boatForce = Vector2.zero;
                _boatTorque = 0f;
            }
        }

        private IEnumerator StopBoatCoroutine()
        {
            // TODO: find a way to play catch sound if you're putting oar in the water

            while (!IsStopped(_rowboatPhysicsParametersProvider.StopSpeedThreshold))
            {
                float boatVel = GetBoatVelocityFromBoatDirection();
                float slideVal = (.1f / _rowboatPhysicsParametersProvider.MaxVelClamp) * Mathf.Clamp(Mathf.Abs(boatVel), 0.01f, _rowboatPhysicsParametersProvider.MaxVelClamp);
                _slideState.AddValue(slideVal * _directionMultiplier);

                _boatForce = -(_boatRigidbody.mass * _boatRigidbody.velocity) * _rowboatPhysicsParametersProvider.BoatStopForce;
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
            if (!_isSwitchingLanes)
            {
                StartCoroutine(SwitchLaneCoroutine(star));
            }
        }

        public IEnumerator SwitchLaneCoroutine(bool star)
        {
            _isSwitchingLanes = true;
            _boatForce = Vector2.zero;
            _boatTorque = 0;

            // only move if exactly one key is being pressed
            Vector3 directionMultiplier = GetPerpendicularForceDirectionVectorFromBoatDirection(star);
            Vector3 targetPosition = directionMultiplier + transform.position;
            float speed = _rowboatPhysicsParametersProvider.SwitchLaneSpeedMultiplier * Math.Abs(_boatRigidbody.velocity.x) * Time.fixedDeltaTime;

            if (!Physics2D.OverlapCircle(targetPosition, .2f, _verticalMovementMask) &&
                Math.Abs(transform.position.y - targetPosition.y) > 0f)
            {
                Vector3 newYPos = new Vector3(transform.position.x, targetPosition.y, transform.position.z);
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    newYPos,
                    speed
                );

                _rudderDrag = _rowboatPhysicsParametersProvider.BaseSwitchLaneSpeedDragFactor;
                yield return new WaitForFixedUpdate();
            }

            _isSwitchingLanes = false;
        }

        // TODO(rudder): delete this
        //private IEnumerator SwitchLaneCoroutine(bool star)
        //{
        //    _boatForce = Vector2.zero;
        //    _boatTorque = 0;

        //    Vector3 directionMultiplier = GetPerpendicularForceDirectionVectorFromBoatDirection(star);
        //    Vector3 targetPosition = directionMultiplier + transform.position;

        //    if (!Physics2D.OverlapCircle(targetPosition, .2f, _verticalMovementMask))
        //    {
        //        float timer = 0;
        //        while (timer < _rowboatPhysicsParametersProvider.WaitBeforeSwitchLaneTime)
        //        {
        //            timer += Time.fixedDeltaTime;
        //            yield return new WaitForFixedUpdate();
        //        }

        //        timer = 0;
        //        while (Math.Abs(transform.position.y - targetPosition.y) > 0f && timer < _rowboatPhysicsParametersProvider.MaxSwitchLaneTime)
        //        {
        //            timer += Time.fixedDeltaTime;
        //            Vector3 newYPos = new Vector3(transform.position.x, targetPosition.y, transform.position.z);
        //            transform.position = Vector3.MoveTowards(
        //                transform.position,
        //                newYPos,
        //                _rowboatPhysicsParametersProvider.SwitchLaneSpeed * Time.fixedDeltaTime
        //            );
        //            yield return new WaitForFixedUpdate();
        //        }
        //    }

        //    OnSwitchLaneFinished?.Invoke();
        //}

        private bool IsAtFinish(bool forwards)
        {
            if (!forwards)
            {
                return IsAtCatch(true);
            }
            return _slideState.Value <= _rowboatPhysicsParametersProvider.SlideEndThreshold;
        }

        private bool IsAtCatch(bool forwards)
        {
            if (!forwards)
            {
                return IsAtFinish(true);
            }
            return _slideState.Value >= 1 - _rowboatPhysicsParametersProvider.SlideEndThreshold;
        }

        private bool IsStopped(float threshold)
        {
            return Mathf.Abs(_boatRigidbody.velocity.x) < threshold;
        }

        public bool CanTransitionFromStopped()
        {
            return IsStopped(_rowboatPhysicsParametersProvider.CanTransitionFromStopSpeedThreshold);
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
