using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class RowboatAnimator : MonoBehaviour
    {
        public Action OnSpinFinished;

        [SerializeField] private Sprite[] _eastForwardsDriveSequence = default;
        [SerializeField] private Sprite[] _eastForwardsRecoverySequence = default;
        [SerializeField] private Sprite[] _eastBackwardsDriveSequence = default;
        [SerializeField] private Sprite[] _eastBackwardsRecoverySequence = default;
        [SerializeField] private Sprite[] _westForwardsDriveSequence = default;
        [SerializeField] private Sprite[] _westForwardsRecoverySequence = default;
        [SerializeField] private Sprite[] _westBackwardsDriveSequence = default;
        [SerializeField] private Sprite[] _westBackwardsRecoverySequence = default;
        [SerializeField] private Sprite[] _spinSequence = default;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private RowboatSlideState _slideState;
        private RowingStateMachine _rowingStateMachine;
        private GlobalDirectionStateMachine _directionStateMachine;
        private RowboatPhysicsParametersProvider _rowboatPhysicsParametersProvider;
        private Coroutine _spinCoroutine;

        [Inject]
        public void Initialize(RowboatSlideState slideState, RowingStateMachine rowingStateMachine, GlobalDirectionStateMachine globalDirectionStateMachine, RowboatPhysicsParametersProvider rowboatPhysicsParametersProvider)
        {
            _slideState = slideState;
            _rowingStateMachine = rowingStateMachine;
            _directionStateMachine = globalDirectionStateMachine;
            _rowboatPhysicsParametersProvider = rowboatPhysicsParametersProvider;
        }

        private void Update()
        {
            Sprite sprite = null; // placeholder

            if (_rowingStateMachine.CurrentState == RowingState.FORWARDS_RECOV)
            {
                sprite = GetRecoverySprite(_eastForwardsRecoverySequence, _westForwardsRecoverySequence);
            } else if (_rowingStateMachine.CurrentState == RowingState.BACKWARDS_RECOV)
            {
                sprite = GetRecoverySprite(_eastBackwardsRecoverySequence, _westBackwardsRecoverySequence);
            } else if (_rowingStateMachine.CurrentState == RowingState.FORWARDS_DRIVE)
            {
                sprite = GetDriveSprite(_eastForwardsDriveSequence, _westForwardsDriveSequence);
            } else if (_rowingStateMachine.CurrentState == RowingState.BACKWARDS_DRIVE)
            {
                sprite = GetDriveSprite(_eastBackwardsDriveSequence, _westBackwardsDriveSequence);
            } else if (_rowingStateMachine.CurrentState == RowingState.STOP)
            {
                if (_rowingStateMachine.PreviousState == RowingState.FORWARDS_DRIVE ||
                    _rowingStateMachine.PreviousState == RowingState.FORWARDS_RECOV)
                {
                    sprite = GetDriveSprite(_eastBackwardsDriveSequence, _westBackwardsDriveSequence);
                } else if (_rowingStateMachine.PreviousState == RowingState.BACKWARDS_DRIVE ||
                    _rowingStateMachine.PreviousState == RowingState.BACKWARDS_RECOV)
                {
                    sprite = GetDriveSprite(_eastForwardsDriveSequence, _westForwardsDriveSequence);
                } else if (_rowingStateMachine.PreviousState == RowingState.SPIN)
                {
                    sprite = GetDriveSprite(_eastForwardsDriveSequence, _westForwardsDriveSequence);
                }
            }

            if (sprite != null)
            {
                _spriteRenderer.sprite = sprite;
            }
        }

        private int GetSpriteIndexDriveRecov(int newScale, float slideValue)
        {
            float scale = (float)newScale - 0.01f;
            float unrounded = scale * Math.Abs(slideValue);
            int idx = (int)Math.Floor(unrounded); // floor so that the sprites are evenly distributed
            if (idx > newScale - 1)
            {
                idx = newScale - 1;
            }
            else if (idx < 0)
            {
                idx = 0;
            }
            return idx;
        }

        private Sprite GetRecoverySprite(Sprite[] eastSprites, Sprite[] westSprites)
        {
            if (_directionStateMachine.CurrentState == GlobalDirectionState.EAST)
            {
                return eastSprites[GetSpriteIndexDriveRecov(eastSprites.Length, _slideState.Value)];
            }

            return westSprites[GetSpriteIndexDriveRecov(westSprites.Length, _slideState.Value)];
        }

        private Sprite GetDriveSprite(Sprite[] eastSprites, Sprite[] westSprites)
        {
            if (_directionStateMachine.CurrentState == GlobalDirectionState.WEST)
            {
                return westSprites[GetSpriteIndexDriveRecov(westSprites.Length, _slideState.Value)];
            }

            return eastSprites[GetSpriteIndexDriveRecov(eastSprites.Length, _slideState.Value)];
        }

        public void StartSpin()
        {
            _spinCoroutine = StartCoroutine(SpinCoroutine());
        }

        public void StopSpin()
        {
            if (_spinCoroutine != null)
            {
                StopCoroutine(_spinCoroutine);
            }
        }

        private IEnumerator SpinCoroutine()
        {
            // calculate the framerate based on length of spin animation
            float framerateSeconds = _rowboatPhysicsParametersProvider.SpinAnimationTimeSeconds / _spinSequence.Length;

            // go to catch before starting spin
            while (_slideState.Value < 1f)
            {
                _slideState.AddValue(_rowboatPhysicsParametersProvider.SliderDriveSpeed);
                _spriteRenderer.sprite = GetDriveSprite(_eastForwardsDriveSequence, _westForwardsDriveSequence);
                yield return new WaitForSeconds(framerateSeconds / 5);
            }

            if (_directionStateMachine.CurrentState == GlobalDirectionState.EAST)
            {
                foreach (var sprite in _spinSequence)
                {
                    _spriteRenderer.sprite = sprite;
                    yield return new WaitForSeconds(framerateSeconds);
                }
            } else if (_directionStateMachine.CurrentState == GlobalDirectionState.WEST)
            {
                for (int i = _spinSequence.Length - 1; i >= 0; i--)
                {
                    _spriteRenderer.sprite = _spinSequence[i];
                    yield return new WaitForSeconds(framerateSeconds);
                }
            }

            OnSpinFinished?.Invoke();
        }
    }
}
