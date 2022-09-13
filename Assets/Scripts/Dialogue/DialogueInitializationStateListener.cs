using UnityEngine;
using Yarn.Unity;
using Zenject;

namespace IndieCade
{
    public class DialogueInitializationStateListener : GameStateListener
    {
        [SerializeField] private DialogueControl _dialogueControl;

        protected override void InitializeChallenge(ChallengeInitializationData challengeInitializationData)
        {
            if (challengeInitializationData.BeginWithDialogue)
            {
                _dialogueControl.BeginWithDialogue(
                    challengeInitializationData.BeginningDialogueNodeName,
                    challengeInitializationData.OnDialogueCompleteInputState
                );
            }
        }
    }
}