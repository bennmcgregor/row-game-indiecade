using System;

namespace IndieCade
{
    public class CharacterWalkingStateMachine
    {
        public Action OnStateChanged;

        private CharacterWalkingState _currentState;

        public CharacterWalkingState CurrentState => _currentState;

        private bool _isPressingRight = false;
        private bool _isPressingLeft = false;
        private bool _isPressingUp = false;
        private bool _isPressingDown = false;

        public CharacterWalkingStateMachine(CharacterWalkingState initialState)
        {
            _currentState = initialState;
        }

        public void RightAction()
        {
            _isPressingRight = !_isPressingRight;
            if (_isPressingRight)
            {
                UpdateState(CharacterWalkingState.RIGHT);
            }
            else
            {
                UpdateStateAfterRelease();
            }
        }

        public void LeftAction()
        {
            _isPressingLeft = !_isPressingLeft;
            if (_isPressingLeft)
            {
                UpdateState(CharacterWalkingState.LEFT);
            }
            else
            {
                UpdateStateAfterRelease();
            }
        }

        public void UpAction()
        {
            _isPressingUp = !_isPressingUp;
            if (_isPressingUp)
            {
                UpdateState(CharacterWalkingState.UP);
            }
            else
            {
                UpdateStateAfterRelease();
            }
        }

        public void DownAction()
        {
            _isPressingDown = !_isPressingDown;
            if (_isPressingDown)
            {
                UpdateState(CharacterWalkingState.DOWN);
            }
            else
            {
                UpdateStateAfterRelease();
            }
        }

        private void UpdateStateAfterRelease()
        {
            CharacterWalkingState newState = CharacterWalkingState.STOPPED;
            if (_isPressingRight && !_isPressingLeft && !_isPressingUp && !_isPressingDown)
            {
                newState = CharacterWalkingState.RIGHT;
            }
            else if (!_isPressingRight && _isPressingLeft && !_isPressingUp && !_isPressingDown)
            {
                newState = CharacterWalkingState.LEFT;
            }
            else if (!_isPressingRight && !_isPressingLeft && _isPressingUp && !_isPressingDown)
            {
                newState = CharacterWalkingState.UP;
            }
            else if (!_isPressingRight && !_isPressingLeft && !_isPressingUp && _isPressingDown)
            {
                newState = CharacterWalkingState.DOWN;
            }
            UpdateState(newState);
        }

        private void UpdateState(CharacterWalkingState state)
        {
            if (state != _currentState)
            {
                _currentState = state;
                OnStateChanged?.Invoke();
            }
        }
    }
}