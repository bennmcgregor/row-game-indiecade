using System;
using System.Collections;
using System.Collections.Generic;
namespace IndieCade
{
    public class RowboatPlayerInputs
    {
        private Dictionary<InputKey, PressReleaseStateMachine> _pressReleaseStateMachines;
        private Dictionary<InputKey, InputStateMachine> _inputStateMachines;

        public Dictionary<InputKey, InputStateMachine> InputStateMachines => _inputStateMachines;
        public Dictionary<InputKey, PressReleaseStateMachine> PressReleaseStateMachines => _pressReleaseStateMachines;

        public RowboatPlayerInputs()
        {
            _pressReleaseStateMachines = new Dictionary<InputKey, PressReleaseStateMachine>
            {
                { InputKey.RIGHT, new PressReleaseStateMachine() },
                { InputKey.LEFT, new PressReleaseStateMachine() },
                { InputKey.UP, new PressReleaseStateMachine() },
                { InputKey.DOWN, new PressReleaseStateMachine() },
                { InputKey.SHIFT, new PressReleaseStateMachine() },
            };

            _inputStateMachines = new Dictionary<InputKey, InputStateMachine>
            {
                { InputKey.RIGHT, new InputStateMachine() },
                { InputKey.LEFT, new InputStateMachine() },
                { InputKey.UP, new InputStateMachine() },
                { InputKey.DOWN, new InputStateMachine() },
                { InputKey.SHIFT, new InputStateMachine() }
            };
        }

        public InputStateMachine GetInputStateMachine(InputKey inputKey)
        {
            return _inputStateMachines[inputKey];
        }

        public PressReleaseStateMachine GetPressReleaseStateMachine(InputKey inputKey)
        {
            return _pressReleaseStateMachines[inputKey];
        }
    }
}
