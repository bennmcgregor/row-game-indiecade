using System;
using Yarn.Unity;

namespace IndieCade
{
    public class ChallengeInitializationData
    {
        private string _stateName;

        public string StateName => _stateName;

        public ChallengeInitializationData(string stateName)
        {
            _stateName = stateName;
        }

        private bool _beginWithDialogue = false;
        private YarnProject _dialogueYarnProject;
        private string _beginningDialogueNodeName;
        private PlayerControlInputState _onDialogueCompleteInputState;

        public bool BeginWithDialogue => _beginWithDialogue;
        public string BeginningDialogueNodeName => _beginningDialogueNodeName;
        public PlayerControlInputState OnDialogueCompleteInputState => _onDialogueCompleteInputState;

        public void StartChallengeWithDialogue(string nodeName, PlayerControlInputState onDialogueCompleteInputState)
        {
            _beginWithDialogue = true;
            _beginningDialogueNodeName = nodeName;
            _onDialogueCompleteInputState = onDialogueCompleteInputState;
        }

        public string BackgroundMusicFilename;

        public bool HasStealthGameplay = false;

        public bool RestartSceneOnFailure = false;

        public Nullable<GameSceneName> ShouldChangeSceneName = null;
    }
}
