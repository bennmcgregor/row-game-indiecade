using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class RowingControl : MonoBehaviour
    {
        private Dictionary<GlobalDirectionState, Dictionary<InputKey, BoatDirection>> _kGlobalToBoatInputDirectionMap
            = new Dictionary<GlobalDirectionState, Dictionary<InputKey, BoatDirection>>
            {
                {
                    GlobalDirectionState.EAST, new Dictionary<InputKey, BoatDirection>
                    {
                        { InputKey.RIGHT, BoatDirection.BOW },
                        { InputKey.LEFT, BoatDirection.STERN },
                        { InputKey.UP, BoatDirection.PORT },
                        { InputKey.DOWN, BoatDirection.STARBOARD },
                    }
                },
                {
                    GlobalDirectionState.WEST, new Dictionary<InputKey, BoatDirection>
                    {
                        { InputKey.RIGHT, BoatDirection.STERN },
                        { InputKey.LEFT, BoatDirection.BOW },
                        { InputKey.UP, BoatDirection.STARBOARD },
                        { InputKey.DOWN, BoatDirection.PORT },
                    }
                },
                {
                    GlobalDirectionState.NORTH, new Dictionary<InputKey, BoatDirection>
                    {
                        { InputKey.RIGHT, BoatDirection.STARBOARD },
                        { InputKey.LEFT, BoatDirection.PORT },
                        { InputKey.UP, BoatDirection.BOW },
                        { InputKey.DOWN, BoatDirection.STERN },
                    }
                },
                {
                    GlobalDirectionState.SOUTH, new Dictionary<InputKey, BoatDirection>
                    {
                        { InputKey.RIGHT, BoatDirection.PORT },
                        { InputKey.LEFT, BoatDirection.STARBOARD },
                        { InputKey.UP, BoatDirection.STERN },
                        { InputKey.DOWN, BoatDirection.BOW },
                    }
                }
            };

        private Dictionary<(BoatDirection, InputState), RowingStateMachineTransition> _kDirectionInputToRowingStateTransitionMap
            = new Dictionary<(BoatDirection, InputState), RowingStateMachineTransition>
            {
                { (BoatDirection.BOW, InputState.DOWN), RowingStateMachineTransition.BOW_DOWN },
                { (BoatDirection.BOW, InputState.HOLD), RowingStateMachineTransition.BOW_HOLD },
                { (BoatDirection.BOW, InputState.UP), RowingStateMachineTransition.BOW_UP },
                { (BoatDirection.BOW, InputState.NONE), RowingStateMachineTransition.BOW_NONE },
                { (BoatDirection.STERN, InputState.DOWN), RowingStateMachineTransition.STERN_DOWN },
                { (BoatDirection.STERN, InputState.HOLD), RowingStateMachineTransition.STERN_HOLD },
                { (BoatDirection.STERN, InputState.UP), RowingStateMachineTransition.STERN_UP },
                { (BoatDirection.STERN, InputState.NONE), RowingStateMachineTransition.STERN_NONE },
                { (BoatDirection.PORT, InputState.DOWN), RowingStateMachineTransition.PORT_DOWN },
                { (BoatDirection.PORT, InputState.HOLD), RowingStateMachineTransition.PORT_HOLD },
                { (BoatDirection.PORT, InputState.UP), RowingStateMachineTransition.PORT_UP },
                { (BoatDirection.PORT, InputState.NONE), RowingStateMachineTransition.PORT_NONE },
                { (BoatDirection.STARBOARD, InputState.DOWN), RowingStateMachineTransition.STAR_DOWN },
                { (BoatDirection.STARBOARD, InputState.HOLD), RowingStateMachineTransition.STAR_HOLD },
                { (BoatDirection.STARBOARD, InputState.UP), RowingStateMachineTransition.STAR_UP },
                { (BoatDirection.STARBOARD, InputState.NONE), RowingStateMachineTransition.STAR_NONE }
            };

        private GlobalDirectionStateMachine _globalDirectionStateMachine;
        private Dictionary<InputKey, InputStateMachine> _inputStateMachines;
        private RowingStateMachine _rowingStateMachine;

        private void Awake()
        {
            _globalDirectionStateMachine = new GlobalDirectionStateMachine(GlobalDirectionState.EAST);
            _inputStateMachines = new Dictionary<InputKey, InputStateMachine>
            {
                { InputKey.RIGHT, new InputStateMachine(InputState.NONE) },
                { InputKey.LEFT, new InputStateMachine(InputState.NONE) },
                { InputKey.UP, new InputStateMachine(InputState.NONE) },
                { InputKey.DOWN, new InputStateMachine(InputState.NONE) },
                { InputKey.SHIFT, new InputStateMachine(InputState.NONE) }
            };
        }

        [Inject]
        public void Initialize(RowingStateMachine rowingStateMachine)
        {
            _rowingStateMachine = rowingStateMachine;
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
            // using direction state machine, determine boat-perspective direction
            // determine the kind of action (down, hold, up)
            // pass the direction + action type to the state machine
        }

        public void RowDown()
        {
            // using direction state machine, determine boat-perspective direction
            // determine the kind of action (down, hold, up)
            // pass the direction + action type to the state machine
        }

        public void RowShift()
        {
            // determine the kind of action (down, hold, up)
            // pass the direction + action type to the state machine
        }

        private void OnRowInput(InputKey inputKey)
        {
            _inputStateMachines[inputKey].Transition(InputStateMachineTransition.ON_KEY);
            InputState inputState = _inputStateMachines[inputKey].CurrentState;

            RowingStateMachineTransition transition;
            if (inputKey == InputKey.SHIFT)
            {
                transition = GetRowingStateMachineTransitionFromInput(inputKey, inputState);
            } else
            {
                BoatDirection boatDirection = GetBoatDirectionStateFromGlobal(_globalDirectionStateMachine.CurrentState, inputKey);
                    // TODO: update action type when new input comes in AND after rowing state machine processing
                transition = GetRowingStateMachineTransitionFromInput(boatDirection, inputState);
            }

            _rowingStateMachine.Transition(transition);
        }

        private BoatDirection GetBoatDirectionStateFromGlobal(GlobalDirectionState globalState, InputKey inputKey)
        {
            return _kGlobalToBoatInputDirectionMap[globalState][inputKey];
        }

        private RowingStateMachineTransition GetRowingStateMachineTransitionFromInput(BoatDirection boatDirection, InputState inputState)
        {
            return _kDirectionInputToRowingStateTransitionMap[(boatDirection, inputState)];
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