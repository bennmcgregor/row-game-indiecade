using System;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class ObjectInteractionControl : MonoBehaviour
    {
        public Action OnStartInteraction;
        public Action OnEndInteraction;

        private PressReleaseStateMachine _pressReleaseStateMachine;
        private PlayerInputManager _playerInputManager;

        public bool HoldingInteractKey => _pressReleaseStateMachine.CurrentState == PressReleaseState.HOLD;

        [Inject]
        public void Initialize(PlayerInputManager playerInputManager)
        {
            _playerInputManager = playerInputManager;
        }

        private void Start()
        {
            _pressReleaseStateMachine = new PressReleaseStateMachine();
        }

        public void Interact()
        {
            _pressReleaseStateMachine.Transition(PressReleaseStateMachineTransition.ON_KEY);

            if (HoldingInteractKey)
            {
                OnStartInteraction?.Invoke();
            } else
            {
                OnEndInteraction?.Invoke();
            }
        }

        // TODO: Turn the player input state into a state machine and issue transitions instead of just setting values
        public void UpdateInputState(PlayerControlInputState playerControlInputState)
        {
            _playerInputManager.SetInputState(playerControlInputState);
        }

        public void RevertInputState()
        {
            _playerInputManager.RevertToPrevInputState();
        }
    }
}
