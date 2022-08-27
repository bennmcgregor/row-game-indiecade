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

        [SerializeField] private Sprite[] _eastDriveSequence = default;
        [SerializeField] private Sprite[] _eastRecoverySequence = default;
        [SerializeField] private Sprite[] _westDriveSequence = default;
        [SerializeField] private Sprite[] _westRecoverySequence = default;
        [SerializeField] private Sprite[] _spinSequence = default;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private float _spinAnimationTimeSeconds = 1f;

        private RowboatSlideState _slideState;
        private RowingStateMachine _rowingStateMachine;
        private GlobalDirectionStateMachine _directionStateMachine;
        private Coroutine _spinCoroutine;

        [Inject]
        public void Initialize(RowboatSlideState slideState, RowingStateMachine rowingStateMachine, GlobalDirectionStateMachine globalDirectionStateMachine)
        {
            _slideState = slideState;
            _rowingStateMachine = rowingStateMachine;
            _directionStateMachine = globalDirectionStateMachine;
        }

        private void Update()
        {
            Sprite sprite = _eastDriveSequence[0]; // placeholder

            if (_rowingStateMachine.CurrentState == RowingState.FORWARDS_RECOV ||
                _rowingStateMachine.CurrentState == RowingState.BACKWARDS_RECOV)
            {
                if (_directionStateMachine.CurrentState == GlobalDirectionState.EAST)
                {
                    sprite = _eastRecoverySequence[GetSpriteIndexDriveRecov(_eastRecoverySequence.Length, _slideState.Value)];
                } else if (_directionStateMachine.CurrentState == GlobalDirectionState.WEST)
                {
                    sprite = _westRecoverySequence[GetSpriteIndexDriveRecov(_westRecoverySequence.Length, _slideState.Value)];
                }
            } else if (_rowingStateMachine.CurrentState == RowingState.FORWARDS_DRIVE ||
                _rowingStateMachine.CurrentState == RowingState.BACKWARDS_DRIVE)
            {
                sprite = GetDriveSprite();
            } else if (_rowingStateMachine.CurrentState == RowingState.STOP)
            {
                sprite = GetDriveSprite();
            }
            _spriteRenderer.sprite = sprite;
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

        private Sprite GetDriveSprite()
        {
            if (_directionStateMachine.CurrentState == GlobalDirectionState.WEST)
            {
                return _westDriveSequence[GetSpriteIndexDriveRecov(_westDriveSequence.Length, _slideState.Value)];
            }

            return _eastDriveSequence[GetSpriteIndexDriveRecov(_eastDriveSequence.Length, _slideState.Value)];
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
            float framerateSeconds = _spinAnimationTimeSeconds / _spinSequence.Length;

            foreach (var sprite in _spinSequence)
            {
                _spriteRenderer.sprite = sprite;
                yield return new WaitForSeconds(framerateSeconds);
            }

            OnSpinFinished?.Invoke();
        }
    }
}
