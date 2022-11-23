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
                { InputKey.RIGHT, PressReleaseStateMachine.Make() },
                { InputKey.LEFT, PressReleaseStateMachine.Make() },
                { InputKey.UP, PressReleaseStateMachine.Make() },
                { InputKey.DOWN, PressReleaseStateMachine.Make() },
                { InputKey.SHIFT, PressReleaseStateMachine.Make() },
            };

            _inputStateMachines = new Dictionary<InputKey, InputStateMachine>
            {
                { InputKey.RIGHT, InputStateMachine.Make() },
                { InputKey.LEFT, InputStateMachine.Make() },
                { InputKey.UP, InputStateMachine.Make() },
                { InputKey.DOWN, InputStateMachine.Make() },
                { InputKey.SHIFT, InputStateMachine.Make() }
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
