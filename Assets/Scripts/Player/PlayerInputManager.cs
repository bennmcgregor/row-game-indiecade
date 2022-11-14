using System;
using System.Collections.Generic;
using UnityEngine;

namespace IndieCade
{
    public class PlayerInputManager : MonoBehaviour
    {
        public Action<PlayerControlInputState> OnInputStateUpdated;

        [SerializeField] private CharacterControl _characterControl;
        [SerializeField] private RowingControl _rowingControl;
        [SerializeField] private ObjectInteractionControl _objectInteractionControl;
        [SerializeField] private DialogueControl _dialogueControl;
        [SerializeField] private PlayerControlInputState _inputState = PlayerControlInputState.WALKING;

        private PlayerControlInputState _prevInputState;

        private void Awake()
        {
            _prevInputState = _inputState;

            if (_objectInteractionControl != null)
            {
                _objectInteractionControl.OnPlayerControlInputStateUpdated += SetInputState;
                _objectInteractionControl.OnPlayerControlInputStateReverted += RevertToPrevInputState;
            }

            if (_dialogueControl != null)
            {
                _dialogueControl.OnPlayerControlInputStateUpdated += SetInputState;
                _dialogueControl.OnPlayerControlInputStateReverted += RevertToPrevInputState;
            }
        }

        public void OnMovePlayerLeft()
        {
            if (_inputState == PlayerControlInputState.WALKING)
            {
                _characterControl.MoveLeft();
            }
        }

        public void OnMovePlayerRight()
        {
            if (_inputState == PlayerControlInputState.WALKING)
            {
                _characterControl.MoveRight();
            }
        }

        public void OnMovePlayerUp()
        {
            if (_inputState == PlayerControlInputState.WALKING)
            {
                _characterControl.MoveUp();
            }
        }

        public void OnMovePlayerDown()
        {
            if (_inputState == PlayerControlInputState.WALKING)
            {
                _characterControl.MoveDown();
            }
        }

        public void OnRowingUp()
        {
            if (_rowingControl != null)
            {
                _rowingControl.RowUp();
            }
        }

        public void OnRowingDown()
        {
            if (_rowingControl != null)
            {
                _rowingControl.RowDown();
            }
        }

        public void OnRowingLeft()
        {
            if (_rowingControl != null)
            {
                _rowingControl.RowLeft();
            }
        }

        public void OnRowingRight()
        {
            if (_rowingControl != null)
            {
                _rowingControl.RowRight();
            }
        }

        public void OnRowingShift()
        {
            if (_rowingControl != null)
            {
                _rowingControl.RowShift();
            }
        }

        public void OnInteract()
        {
            if (_inputState != PlayerControlInputState.DIALOGUE)
            {
                _objectInteractionControl.Interact();
            }
        }

        public void SetInputState(PlayerControlInputState inputState)
        {
            _prevInputState = _inputState;
            _inputState = inputState;
            if (inputState == PlayerControlInputState.ROWING)
            {
                if (_rowingControl != null)
                {
                    _rowingControl.Enable();
                }
            } else
            {
                if (_rowingControl != null)
                {
                    _rowingControl.Disable();
                }
            }

            OnInputStateUpdated?.Invoke(_inputState);
        }

        public void RevertToPrevInputState()
        {
            SetInputState(_prevInputState);
        }
    }

}