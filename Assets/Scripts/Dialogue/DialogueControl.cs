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

        public void BeginWithDialogue(YarnProject yarnProject, string nodeName, PlayerControlInputState onDialogueCompletedInputState)
        {
            _playerInputManager.SetInputState(PlayerControlInputState.DIALOGUE);
            _dialogueRunner.yarnProject = yarnProject;
            _dialogueRunner.startNode = nodeName;
            _dialogueRunner.startAutomatically = true;
            SetOnDialogueCompletedInputState(onDialogueCompletedInputState);
        }

        private void SetOnDialogueCompletedInputState(PlayerControlInputState inputState)
        {
            _revertToPrevInputStateOnDialogueCompleted = false;
            _onDialogueCompletedInputState = inputState;
        }

        // TODO: add actions for keyboard control of dialogue
    }
}
