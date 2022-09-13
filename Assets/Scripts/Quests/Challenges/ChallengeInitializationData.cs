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

        private bool _shouldChangeSceneOnFailure = false;
        private bool _shouldChangeSceneOnComplete = false;
        private GameSceneName _sceneChangeName;

        public bool ShouldChangeSceneOnFailure => _shouldChangeSceneOnFailure;
        public bool ShouldChangeSceneOnComplete => _shouldChangeSceneOnComplete;
        public GameSceneName SceneChangeName => _sceneChangeName;

        public void ChangeSceneOnChallengeFailure(GameSceneName sceneChangeName)
        {
            _sceneChangeName = sceneChangeName;
            _shouldChangeSceneOnFailure = true;
        }

        public void ChangeSceneOnChallengeComplete(GameSceneName sceneChangeName)
        {
            _sceneChangeName = sceneChangeName;
            _shouldChangeSceneOnComplete = true;
        }
    }
}
