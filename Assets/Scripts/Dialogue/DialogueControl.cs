using System;
using System.Collections;
using UnityEngine;
using Yarn.Unity;
using Zenject;

namespace IndieCade
{
    public class DialogueControl : CommandRegistrar
    {
        public Action<PlayerControlInputState> OnPlayerControlInputStateUpdated;
        public Action OnPlayerControlInputStateReverted;

        [SerializeField] private DialogueViewBase _dialogueLineView;

        private bool _revertToPrevInputStateOnDialogueCompleted = true;
        private PlayerControlInputState _onDialogueCompletedInputState;
        private bool _enabledInput = false;

        protected override void RegisterCommand()
        {
            _dialogueRunner.AddCommandHandler(
                "enable_input",
                EnableInput
            );
        }

        public void OnDialogueComplete()
        {
            if (!_enabledInput)
            {
                if (_revertToPrevInputStateOnDialogueCompleted)
                {
                    OnPlayerControlInputStateReverted?.Invoke();
                }
                else
                {
                    OnPlayerControlInputStateUpdated?.Invoke(_onDialogueCompletedInputState);
                }
            }
            _enabledInput = false;
        }

        public void BeginWithDialogue(string nodeName, PlayerControlInputState onDialogueCompletedInputState)
        {
            StartDialogue(nodeName);
            SetOnDialogueCompletedInputState(onDialogueCompletedInputState);
        }

        public void StartDialogue(string nodeName)
        {
            StartCoroutine(WaitToStartDialogue(nodeName));
        }

        private IEnumerator WaitToStartDialogue(string nodeName)
        {
            yield return new WaitUntil(() => _dialogueRunner.NodeExists(nodeName));
            if (!_enabledInput)
            {
                OnPlayerControlInputStateUpdated?.Invoke(PlayerControlInputState.DIALOGUE);
            }
            CancelDialogue();
            _dialogueRunner.StartDialogue(nodeName);
        }

        public void StopDialogue()
        {
            CancelDialogue();
            _enabledInput = false;
        }

        private void CancelDialogue()
        {
            _dialogueLineView.UserRequestedViewAdvancement();
            _dialogueRunner.Stop();
        }

        private void SetOnDialogueCompletedInputState(PlayerControlInputState inputState)
        {
            _revertToPrevInputStateOnDialogueCompleted = false;
            _onDialogueCompletedInputState = inputState;
        }

        // this should usually be called after WaitToStartDialogue
        private void EnableInput()
        {
            if (!_enabledInput)
            {
                OnPlayerControlInputStateReverted?.Invoke();
                _enabledInput = true;
            }
        }
    }
}
