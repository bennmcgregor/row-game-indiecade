using System;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class ObjectInteractionControl : MonoBehaviour
    {
        public Action<PlayerControlInputState> OnPlayerControlInputStateUpdated;
        public Action OnPlayerControlInputStateReverted;
        public Action OnStartInteraction;
        public Action OnEndInteraction;

        private PressReleaseStateMachine _pressReleaseStateMachine;

        public bool HoldingInteractKey => _pressReleaseStateMachine.CurrentState == PressReleaseState.HOLD;

        private void Start()
        {
            _pressReleaseStateMachine = PressReleaseStateMachine.Make();
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
            OnPlayerControlInputStateUpdated?.Invoke(playerControlInputState);
        }

        public void RevertInputState()
        {
            OnPlayerControlInputStateReverted?.Invoke();
        }
    }
}
