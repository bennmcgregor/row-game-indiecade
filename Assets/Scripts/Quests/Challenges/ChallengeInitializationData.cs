using System;
using Yarn.Unity;

namespace IndieCade
{
    public class ChallengeInitializationData : StateData<TeaserChallenges>, IChallengeData
    {
        public ChallengeInitializationData(TeaserChallenges stateName)
            : base(stateName) {}

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

        private string _backgroundMusicFilename;
        public string BackgroundMusicFilename
        {
            get
            {
                return _backgroundMusicFilename;
            }
            set
            {
                _backgroundMusicFilename = value;
            }
        }

        private bool _hasStealthGameplay = false;
        private bool _isChase = false;
        private bool _followLightsOn = false;

        public bool HasStealthGameplay => _hasStealthGameplay;
        public bool IsChase => _isChase;
        public bool FollowLightsOn => _followLightsOn;

        public void SetStealthGameplay(bool hasStealthGameplay, bool isChase, bool followLightsOn = false)
        {
            _hasStealthGameplay = hasStealthGameplay;
            _isChase = isChase;
            _followLightsOn = followLightsOn;
        }

        private bool _restartSceneOnFailure = false;
        public bool RestartSceneOnFailure
        {
            get
            {
                return _restartSceneOnFailure;
            }
            set
            {
                _restartSceneOnFailure = value;
            }
        }

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
