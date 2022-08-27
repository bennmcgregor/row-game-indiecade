using System;
using System.Collections;
using System.Collections.Generic;
namespace IndieCade
{
    public class RowboatPlayerInputs
    {
        private Dictionary<InputKey, InputStateMachine> _inputStateMachines;

        public Dictionary<InputKey, InputStateMachine> InputStateMachines => _inputStateMachines;

        public RowboatPlayerInputs()
        {
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
    }
}
