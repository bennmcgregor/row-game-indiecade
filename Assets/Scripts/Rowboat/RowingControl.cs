using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class RowingControl : MonoBehaviour
    {
        private GlobalDirectionStateMachine _globalDirectionStateMachine;
        private RowboatPlayerInputs _rowboatPlayerInputs;
        private RowingStateMachine _rowingStateMachine;
        private RowboatMaps _rowboatMaps;

        [Inject]
        public void Initialize(RowboatMaps rowboatMaps, RowingStateMachine rowingStateMachine, GlobalDirectionStateMachine globalDirectionStateMachine, RowboatPlayerInputs rowboatPlayerInputs)
        {
            _rowboatMaps = rowboatMaps;
            _rowingStateMachine = rowingStateMachine;
            _globalDirectionStateMachine = globalDirectionStateMachine;
            _rowboatPlayerInputs = rowboatPlayerInputs;
        }

        private void Start()
        {
            _rowingStateMachine.Transition(RowingStateMachineTransition.ENTRY);
            _globalDirectionStateMachine.Transition(GlobalDirectionStateMachineTransition.ENTRY);
            foreach (var stateMachine in _rowboatPlayerInputs.InputStateMachines.Values)
            {
                stateMachine.Transition(InputStateMachineTransition.ENTRY);
            }
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
            // TODO: re-enable when we have the animations
            //OnRowInput(InputKey.SHIFT);
        }

        private void OnRowInput(InputKey inputKey)
        {
            _rowboatPlayerInputs.GetInputStateMachine(inputKey).Transition(InputStateMachineTransition.ON_KEY);
            InputState inputState = _rowboatPlayerInputs.GetInputStateMachine(inputKey).CurrentState;

            RowingStateMachineTransition transition;
            if (inputKey == InputKey.SHIFT)
            {
                transition = GetRowingStateMachineTransitionFromInput(inputKey, inputState);
            } else
            {
                BoatDirection boatDirection = _rowboatMaps.GetBoatDirectionStateFromGlobal(_globalDirectionStateMachine.CurrentState, inputKey);
                transition = _rowboatMaps.GetRowingStateMachineTransitionFromInput(boatDirection, inputState);
            }

            _rowingStateMachine.Transition(transition);
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