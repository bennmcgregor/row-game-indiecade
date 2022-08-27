using UnityEngine;

namespace IndieCade
{
    public class PlayerInputManager : MonoBehaviour
    {
        [SerializeField] private CharacterControl _characterControl;
        [SerializeField] private RowingControl _rowingControl;
        [SerializeField] private ObjectInteractionControl _objectInteractionControl;
        [SerializeField] private PlayerControlInputState _inputState = PlayerControlInputState.WALKING_INTERACTING;

        public void OnMovePlayerLeft()
        {
            if (_inputState == PlayerControlInputState.WALKING_INTERACTING)
            {
                _characterControl.MoveLeft();
            }
        }

        public void OnMovePlayerRight()
        {
            if (_inputState == PlayerControlInputState.WALKING_INTERACTING)
            {
                _characterControl.MoveRight();
            }
        }

        public void OnMovePlayerUp()
        {
            if (_inputState == PlayerControlInputState.WALKING_INTERACTING)
            {
                _characterControl.MoveUp();
            }
        }

        public void OnMovePlayerDown()
        {
            if (_inputState == PlayerControlInputState.WALKING_INTERACTING)
            {
                _characterControl.MoveDown();
            }
        }

        public void OnRowingUp()
        {
            if (_inputState == PlayerControlInputState.ROWING)
            {
                _rowingControl.RowUp();
            }
        }

        public void OnRowingDown()
        {
            if (_inputState == PlayerControlInputState.ROWING)
            {
                _rowingControl.RowDown();
            }
        }

        public void OnRowingLeft()
        {
            if (_inputState == PlayerControlInputState.ROWING)
            {
                _rowingControl.RowLeft();
            }
        }

        public void OnRowingRight()
        {
            if (_inputState == PlayerControlInputState.ROWING)
            {
                _rowingControl.RowRight();
            }
        }

        public void OnRowingShift()
        {
            if (_inputState == PlayerControlInputState.ROWING)
            {
                _rowingControl.RowShift();
            }
        }

        public void OnInteract()
        {
            if (_inputState == PlayerControlInputState.WALKING_INTERACTING)
            {
                _objectInteractionControl.Interact();
            }
        }
    }

}