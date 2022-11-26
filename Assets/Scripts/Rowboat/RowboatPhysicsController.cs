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
        public Action PlayCatchSoundEffect;
        public Action PlayFinishSoundEffect;

        [SerializeField] private Rigidbody2D _boatRigidbody;

        private RowboatSlideState _slideState;
        private GlobalDirectionStateMachine _globalDirectionStateMachine;
        private RowboatPhysicsParametersProvider _rowboatPhysicsParametersProvider;

        private Coroutine _driveCoroutine;
        private Coroutine _recoveryCoroutine;
        private Coroutine _stopCoroutine;
        private Coroutine _portRudderCoroutine;
        private Coroutine _starRudderCoroutine;
        private float _directionMultiplier;
        private Vector2 _boatForce = Vector2.zero;
        private float _rudderDrag = 0f;
        private float _boatYVelocity = 0f;
        private bool _isSwitchingLanes = false;
        private bool _isDrive = false;

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

            _boatRigidbody.drag = _rowboatPhysicsParametersProvider.BaseDragFactor + _rudderDrag +
                _rowboatPhysicsParametersProvider.DragVelocityScalingFactor *
                Mathf.Abs(_boatRigidbody.velocity.x);

            _boatRigidbody.velocity = new Vector2(_boatRigidbody.velocity.x, _boatYVelocity);

            // reset the _rudderDrag after it has been applied
            _rudderDrag = 0f;
            _boatYVelocity = 0f;
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
            _isDrive = true;
            PlayCatchSoundEffect?.Invoke();

            while (!IsAtFinish(forwards))
            {
                _slideState.AddValue(-1 * _rowboatPhysicsParametersProvider.SliderDriveSpeed * _directionMultiplier);
                float driveForce = forwards ? _rowboatPhysicsParametersProvider.BoatForwardsDriveForce : _rowboatPhysicsParametersProvider.BoatBackwardsDriveForce;
                _boatForce = GetForceDirectionVectorFromBoatDirection() * _directionMultiplier * driveForce;
                yield return new WaitForFixedUpdate();
            }

            _boatForce = Vector2.zero;

            _isDrive = false;
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
                yield return new WaitForFixedUpdate();
            }

            while (true)
            {
                _boatForce = Vector2.zero;
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
                yield return new WaitForFixedUpdate();
            }

            while (true)
            {
                _boatForce = Vector2.zero;
                yield return new WaitForFixedUpdate();
            }
        }

        public void StartTurnRudder(bool star)
        {
            if (star)
            {
                _starRudderCoroutine = StartCoroutine(TurnRudderCoroutine(star));
            }
            else
            {
                _portRudderCoroutine = StartCoroutine(TurnRudderCoroutine(star));
            }
        }

        public void EndTurnRudder(bool star)
        {
            if (star)
            {
                if (_starRudderCoroutine != null)
                {
                    StopCoroutine(_starRudderCoroutine);
                }
            }
            else
            {
                if (_portRudderCoroutine != null)
                {
                    StopCoroutine(_portRudderCoroutine);
                }
            }
        }

        private IEnumerator TurnRudderCoroutine(bool star)
        {
            while (true)
            {
                Vector3 directionMultiplier = GetPerpendicularForceDirectionVectorFromBoatDirection(star);
                float speed = _rowboatPhysicsParametersProvider.RudderTurnDriveSpeedMultiplier * Math.Abs(_boatRigidbody.velocity.x);
                if (!_isDrive)
                {
                    speed *= _rowboatPhysicsParametersProvider.RudderTurnRecoverySpeedMultiplier;
                    _rudderDrag = _rowboatPhysicsParametersProvider.BaseRecoveryRudderTurnSpeedDragFactor;
                }
                else
                {
                    speed = speed < _rowboatPhysicsParametersProvider.MinRudderTurnSpeed ? _rowboatPhysicsParametersProvider.MinRudderTurnSpeed : speed;
                    _rudderDrag = _rowboatPhysicsParametersProvider.BaseDriveRudderTurnSpeedDragFactor;
                }
                _boatYVelocity = speed * directionMultiplier.y;
                
                yield return new WaitForFixedUpdate();
            }
        }

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
