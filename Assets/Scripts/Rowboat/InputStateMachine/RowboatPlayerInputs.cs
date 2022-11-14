using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class RowboatPlayerInputs : MonoBehaviour
    {
        private Dictionary<InputKey, PressReleaseStateMachine> _pressReleaseStateMachines;
        private Dictionary<InputKey, InputStateMachine> _inputStateMachines;
        private Dictionary<InputKey, SpamTimer> _spamTimers;
        private RowboatPhysicsParametersProvider _rowboatPhysicsParametersProvider;

        public Dictionary<InputKey, InputStateMachine> InputStateMachines => _inputStateMachines;
        public Dictionary<InputKey, PressReleaseStateMachine> PressReleaseStateMachines => _pressReleaseStateMachines;
        public Dictionary<InputKey, SpamTimer> SpamTimers => _spamTimers;

        [Inject]
        public void Initialize(RowboatPhysicsParametersProvider rowboatPhysicsParametersProvider)
        {
            _rowboatPhysicsParametersProvider = rowboatPhysicsParametersProvider;
        }

        private void Awake()
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

            _spamTimers = new Dictionary<InputKey, SpamTimer>
            {
                { InputKey.RIGHT, gameObject.AddComponent<SpamTimer>() },
                { InputKey.LEFT, gameObject.AddComponent<SpamTimer>() },
                { InputKey.UP, gameObject.AddComponent<SpamTimer>() },
                { InputKey.DOWN, gameObject.AddComponent<SpamTimer>() },
                { InputKey.SHIFT, gameObject.AddComponent<SpamTimer>() }
            };

            foreach (var timer in _spamTimers.Values)
            {
                timer.SetTimeout(_rowboatPhysicsParametersProvider.InputSpamTimeoutSeconds);
            }
        }

        public InputStateMachine GetInputStateMachine(InputKey inputKey)
        {
            return _inputStateMachines[inputKey];
        }

        public PressReleaseStateMachine GetPressReleaseStateMachine(InputKey inputKey)
        {
            return _pressReleaseStateMachines[inputKey];
        }

        public SpamTimer GetSpamTimer(InputKey inputKey)
        {
            return _spamTimers[inputKey];
        }
    }
}
