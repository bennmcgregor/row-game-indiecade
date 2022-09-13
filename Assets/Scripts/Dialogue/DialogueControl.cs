using System;
using System.Collections;
using UnityEngine;
using Yarn.Unity;
using Zenject;

namespace IndieCade
{
    public class DialogueControl : MonoBehaviour
    {
        public Action<PlayerControlInputState> OnPlayerControlInputStateUpdated;
        public Action OnPlayerControlInputStateReverted;

        [SerializeField] private DialogueRunner _dialogueRunner;
        [SerializeField] private LineView _lineView;

        private bool _revertToPrevInputStateOnDialogueCompleted = true;
        private PlayerControlInputState _onDialogueCompletedInputState;

        public void OnDialogueComplete()
        {
            if (_revertToPrevInputStateOnDialogueCompleted)
            {
                OnPlayerControlInputStateReverted?.Invoke();
            } else
            {
                OnPlayerControlInputStateUpdated?.Invoke(_onDialogueCompletedInputState);
            }
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
            OnPlayerControlInputStateUpdated?.Invoke(PlayerControlInputState.DIALOGUE);
            StopDialogue();
            _dialogueRunner.StartDialogue(nodeName);
        }

        public void StopDialogue()
        {
            _lineView.OnContinueClicked();
            _dialogueRunner.Stop();
        }

        private void SetOnDialogueCompletedInputState(PlayerControlInputState inputState)
        {
            _revertToPrevInputStateOnDialogueCompleted = false;
            _onDialogueCompletedInputState = inputState;
        }

        // TODO: add actions for keyboard control of dialogue
    }
}
