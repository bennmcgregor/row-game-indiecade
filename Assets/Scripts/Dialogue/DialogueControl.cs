using System;
using UnityEngine;
using Yarn.Unity;
using Zenject;

namespace IndieCade
{
    public class DialogueControl : MonoBehaviour
    {
        [SerializeField] private DialogueRunner _dialogueRunner;

        private PlayerInputManager _playerInputManager;
        private bool _revertToPrevInputStateOnDialogueCompleted = true;
        private PlayerControlInputState _onDialogueCompletedInputState;
        private bool _hasPlayedDialogue = false;

        [Inject]
        public void Initialize(PlayerInputManager playerInputManager)
        {
            _playerInputManager = playerInputManager;
        }

        public void OnDialogueComplete()
        {
            if (_revertToPrevInputStateOnDialogueCompleted)
            {
                _playerInputManager.RevertToPrevInputState();
            } else
            {
                _playerInputManager.SetInputState(_onDialogueCompletedInputState);
            }
        }

        public void BeginWithDialogue(string nodeName, PlayerControlInputState onDialogueCompletedInputState)
        {
            StartDialogue(nodeName);
            SetOnDialogueCompletedInputState(onDialogueCompletedInputState);
        }

        public void StartDialogue(string nodeName)
        {
            _playerInputManager.SetInputState(PlayerControlInputState.DIALOGUE);
            if (_hasPlayedDialogue)
            {
                _dialogueRunner.StartDialogue(nodeName);
            } else
            {
                _dialogueRunner.startNode = nodeName;
                _dialogueRunner.startAutomatically = true;
                _hasPlayedDialogue = true;
            }
        }

        private void SetOnDialogueCompletedInputState(PlayerControlInputState inputState)
        {
            _revertToPrevInputStateOnDialogueCompleted = false;
            _onDialogueCompletedInputState = inputState;
        }

        // TODO: add actions for keyboard control of dialogue
    }
}
