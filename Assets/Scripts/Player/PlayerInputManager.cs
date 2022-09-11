using System.Collections.Generic;
using UnityEngine;

namespace IndieCade
{
    public class PlayerInputManager : MonoBehaviour
    {
        [SerializeField] private CharacterControl _characterControl;
        [SerializeField] private RowingControl _rowingControl;
        [SerializeField] private ObjectInteractionControl _objectInteractionControl;
        [SerializeField] private DialogueControl _dialogueControl;
        [SerializeField] private PlayerControlInputState _inputState = PlayerControlInputState.WALKING;

        private PlayerControlInputState _prevInputState;

        // TODO: add actions to handle DialogueControl (for keyboard input on dialogue)

        private void Awake()
        {
            _prevInputState = _inputState;
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
            //if (_inputState == PlayerControlInputState.ROWING)
            //{
                _rowingControl.RowUp();
            //}
        }

        public void OnRowingDown()
        {
            //if (_inputState == PlayerControlInputState.ROWING)
            //{
                _rowingControl.RowDown();
            //}
        }

        public void OnRowingLeft()
        {
            //if (_inputState == PlayerControlInputState.ROWING)
            //{
                _rowingControl.RowLeft();
            //}
        }

        public void OnRowingRight()
        {
            //if (_inputState == PlayerControlInputState.ROWING)
            //{
                _rowingControl.RowRight();
            //}
        }

        public void OnRowingShift()
        {
            //if (_inputState == PlayerControlInputState.ROWING)
            //{
                _rowingControl.RowShift();
            //}
        }

        public void OnInteract()
        {
            _objectInteractionControl.Interact();
        }

        public void SetInputState(PlayerControlInputState inputState)
        {
            _prevInputState = _inputState;
            _inputState = inputState;
            if (inputState == PlayerControlInputState.ROWING)
            {
                _rowingControl.Enable();
            } else
            {
                _rowingControl.Disable();
            }
        }

        public void RevertToPrevInputState()
        {
            SetInputState(_prevInputState);
        }
    }

}