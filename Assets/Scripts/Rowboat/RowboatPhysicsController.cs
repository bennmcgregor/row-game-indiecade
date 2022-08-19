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

        private RowingStateMachine _rowingStateMachine;
        private RowboatSlideState _slideState;
        private Rigidbody2D _boatRigidbody;
        private Coroutine _driveCoroutine;
        private Coroutine _recoveryCoroutine;

        private float _directionMultiplier;
        private Vector2 _boatForce;
        private Vector2 _boatTorque;

        [Inject]
        public void Initialize(RowingStateMachine rowingStateMachine, RowboatSlideState slideState, Rigidbody2D boatRigidbody)
        {
            _rowingStateMachine = rowingStateMachine;
            _slideState = slideState;
            _boatRigidbody = boatRigidbody;
        }

        private void FixedUpdate()
        {
            // apply a force to the boat
        }

        public void StartDrive(bool forwards)
        {
            _directionMultiplier = forwards ? 1f : -1f;

            // start the drive coroutine
            _driveCoroutine = StartCoroutine(DriveCoroutine());
        }

        public void StopDrive()
        {
            StopCoroutine(_driveCoroutine);
        }

        private IEnumerator DriveCoroutine()
        {
            while (!IsAtFinish())
            {

            }

            // while the slider is not at the finish (sliderVal = 0 if forward, else 1)
                // compute the value to add to the slider (constant for now but eventually: so that it follows the force curve of the drive)
                // add the value to the slider, but clamp it between 0 and 1 (this should be done in slider logic)
                // calculate the force to apply to the boat (constant for now but eventually: so that it follows the force curve of the drive)
                // set torque to zero
                // yield wait for fixed update

            // set the force and torque to zero
            // tell the rowing state machine to transition

            yield return null;
        }

        public void StartRecovery(bool forwards)
        {
            _directionMultiplier = forwards ? 1f : -1f;

            _recoveryCoroutine = StartCoroutine(RecoveryCoroutine());
        }

        public void StopRecovery()
        {
            StopCoroutine(_recoveryCoroutine);
        }

        private IEnumerator RecoveryCoroutine()
        {
            // while the slider is not at the catch
                // compute the value to add to the slider, proportional to the speed of the boat
                // add the value to the slider, but clamp it between 0 and 1 (this should be done in slider logic)
                // set force and torque to zero
                // yield wait for fixed update

            // while true
                // set the force and torque to zero
                // yield wait for fixed update

            yield return null;
        }

        private bool IsAtFinish()
        {
            return _slideState.Value <= _kSlideEndThreshold;
        }

        private bool IsAtCatch()
        {
            return _slideState.Value >= 1 - _kSlideEndThreshold;
        }
    }
}
