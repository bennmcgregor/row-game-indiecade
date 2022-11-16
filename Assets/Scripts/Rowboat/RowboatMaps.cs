using System;
using System.Collections.Generic;
namespace IndieCade
{
    public class RowboatMaps
    {
        private Dictionary<GlobalDirectionState, Dictionary<InputKey, BoatDirection>> _kGlobalToBoatInputDirectionMap;
        private Dictionary<(BoatDirection, InputState), RowingMotionStateMachineTransition> _kDirectionInputToRowingStateTransitionMap;
        private Dictionary<(BoatDirection, InputState), RudderStateMachineTransition> _kDirectionInputToRudderStateTransitionMap;

        public RowboatMaps()
        {
            _kGlobalToBoatInputDirectionMap = new Dictionary<GlobalDirectionState, Dictionary<InputKey, BoatDirection>>
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
                }
            };

            _kDirectionInputToRowingStateTransitionMap = new Dictionary<(BoatDirection, InputState), RowingMotionStateMachineTransition>
            {
                { (BoatDirection.BOW, InputState.DOWN), RowingMotionStateMachineTransition.BOW_DOWN },
                { (BoatDirection.BOW, InputState.HOLD), RowingMotionStateMachineTransition.BOW_HOLD },
                { (BoatDirection.BOW, InputState.UP), RowingMotionStateMachineTransition.BOW_UP },
                { (BoatDirection.BOW, InputState.NONE), RowingMotionStateMachineTransition.BOW_NONE },
                { (BoatDirection.STERN, InputState.DOWN), RowingMotionStateMachineTransition.STERN_DOWN },
                { (BoatDirection.STERN, InputState.HOLD), RowingMotionStateMachineTransition.STERN_HOLD },
                { (BoatDirection.STERN, InputState.UP), RowingMotionStateMachineTransition.STERN_UP },
                { (BoatDirection.STERN, InputState.NONE), RowingMotionStateMachineTransition.STERN_NONE }
            };

            _kDirectionInputToRudderStateTransitionMap = new Dictionary<(BoatDirection, InputState), RudderStateMachineTransition>
            {
                { (BoatDirection.PORT, InputState.DOWN), RudderStateMachineTransition.PORT_DOWN },
                { (BoatDirection.PORT, InputState.HOLD), RudderStateMachineTransition.PORT_HOLD },
                { (BoatDirection.PORT, InputState.UP), RudderStateMachineTransition.PORT_UP },
                { (BoatDirection.PORT, InputState.NONE), RudderStateMachineTransition.PORT_NONE },
                { (BoatDirection.STARBOARD, InputState.DOWN), RudderStateMachineTransition.STAR_DOWN },
                { (BoatDirection.STARBOARD, InputState.HOLD), RudderStateMachineTransition.STAR_HOLD },
                { (BoatDirection.STARBOARD, InputState.UP), RudderStateMachineTransition.STAR_UP },
                { (BoatDirection.STARBOARD, InputState.NONE), RudderStateMachineTransition.STAR_NONE }
            };
        }

        public BoatDirection GetBoatDirectionStateFromGlobal(GlobalDirectionState globalState, InputKey inputKey)
        {
            return _kGlobalToBoatInputDirectionMap[globalState][inputKey];
        }

        public RowingMotionStateMachineTransition GetRowingStateMachineTransitionFromInput(BoatDirection boatDirection, InputState inputState)
        {
            return _kDirectionInputToRowingStateTransitionMap[(boatDirection, inputState)];
        }

        public RudderStateMachineTransition GetRudderStateMachineTransitionFromInput(BoatDirection boatDirection, InputState inputState)
        {
            return _kDirectionInputToRudderStateTransitionMap[(boatDirection, inputState)];
        }

        public InputKey GetInputKeyFromBoatAndGlobalDirection(BoatDirection boatDirection, GlobalDirectionState globalDirectionState)
        {
            Dictionary<InputKey, BoatDirection> dict = _kGlobalToBoatInputDirectionMap[globalDirectionState];
            foreach (var pair in dict)
            {
                if (pair.Value == boatDirection)
                {
                    return pair.Key;
                }
            }

            return InputKey.SHIFT;
        }
    }
}
