using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class RowingControl : MonoBehaviour, IEnableable
    {
        private GlobalDirectionStateMachine _globalDirectionStateMachine;
        private RowboatPlayerInputs _rowboatPlayerInputs;
        private RowingStateMachine<RowingState, RowingStateMachineTransition> _rowingStateMachine;
        private RowingStateMachine<RudderState, RudderStateMachineTransition> _rudderStateMachine;
        private RowboatMaps _rowboatMaps;

        private bool _enabled = true;

        [Inject]
        public void Initialize(RowboatMaps rowboatMaps, RowingStateMachine<RowingState, RowingStateMachineTransition> rowingStateMachine, RowingStateMachine<RudderState, RudderStateMachineTransition> rudderStateMachine, GlobalDirectionStateMachine globalDirectionStateMachine, RowboatPlayerInputs rowboatPlayerInputs)
        {
            _rowboatMaps = rowboatMaps;
            _rowingStateMachine = rowingStateMachine;
            _rudderStateMachine = rudderStateMachine;
            _globalDirectionStateMachine = globalDirectionStateMachine;
            _rowboatPlayerInputs = rowboatPlayerInputs;
        }

        private void Start()
        {
            // the direction state machine has to transition before the rowing state machine
            _globalDirectionStateMachine.Transition(GlobalDirectionStateMachineTransition.ENTRY);
            _rowingStateMachine.Transition(RowingStateMachineTransition.ENTRY);
            foreach (var stateMachine in _rowboatPlayerInputs.InputStateMachines.Values)
            {
                stateMachine.Transition(InputStateMachineTransition.ENTRY);
            }
        }

        public void Disable()
        {
            _enabled = false;
        }

        public void Enable()
        {
            _enabled = true;
        }

        public void RowRight()
        {
            OnRowInput(InputKey.RIGHT);
        }

        public void RowLeft()
        {
            OnRowInput(InputKey.LEFT);
        }

        public void RowUp()
        {
            OnRowInput(InputKey.UP);
        }

        public void RowDown()
        {
            OnRowInput(InputKey.DOWN);
        }

        public void RowShift()
        {
            OnRowInput(InputKey.SHIFT);
        }

        private void OnRowInput(InputKey inputKey)
        {
            _rowboatPlayerInputs.GetPressReleaseStateMachine(inputKey).Transition(PressReleaseStateMachineTransition.ON_KEY);
            InputStateMachineTransition inputStateMachineTransition = InputStateMachineTransition.ON_RELEASE;
            bool canPress = true;
            if (_rowboatPlayerInputs.GetPressReleaseStateMachine(inputKey).CurrentState == PressReleaseState.HOLD)
            {
                inputStateMachineTransition = InputStateMachineTransition.ON_PRESS;
                canPress = _rowboatPlayerInputs.GetSpamTimer(inputKey).OnKeyPress();
            }

            if (canPress && (_enabled || (!_enabled && _rowboatPlayerInputs.GetInputStateMachine(inputKey).CurrentState != InputState.NONE)))
            {
                _rowboatPlayerInputs.GetInputStateMachine(inputKey).Transition(inputStateMachineTransition);
                InputState inputState = _rowboatPlayerInputs.GetInputStateMachine(inputKey).CurrentState;

                if (inputKey == InputKey.UP || inputKey == InputKey.DOWN)
                {
                    BoatDirection boatDirection = _rowboatMaps.GetBoatDirectionStateFromGlobal(_globalDirectionStateMachine.CurrentState, inputKey);
                    _rudderStateMachine.Transition(_rowboatMaps.GetRudderStateMachineTransitionFromInput(boatDirection, inputState));
                }
                else
                {
                    RowingStateMachineTransition transition;
                    if (inputKey == InputKey.SHIFT)
                    {
                        transition = GetRowingStateMachineTransitionFromInput(inputKey, inputState);
                    }
                    else
                    {
                        BoatDirection boatDirection = _rowboatMaps.GetBoatDirectionStateFromGlobal(_globalDirectionStateMachine.CurrentState, inputKey);
                        transition = _rowboatMaps.GetRowingStateMachineTransitionFromInput(boatDirection, inputState);
                    }

                    _rowingStateMachine.Transition(transition);
                }
            }
        }

        private RowingStateMachineTransition GetRowingStateMachineTransitionFromInput(InputKey inputKey, InputState inputState)
        {
            if (inputKey == InputKey.SHIFT)
            {
                switch (inputState)
                {
                    case InputState.DOWN:
                        return RowingStateMachineTransition.SHIFT_DOWN;
                    case InputState.HOLD:
                        return RowingStateMachineTransition.SHIFT_HOLD;
                    case InputState.UP:
                        return RowingStateMachineTransition.SHIFT_UP;
                    case InputState.NONE:
                        return RowingStateMachineTransition.SHIFT_NONE;
                }
            }

            throw new KeyNotFoundException($"Function called with wrong InputKey: {inputKey}");
        }
    }
}