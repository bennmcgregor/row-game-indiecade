using System;
namespace IndieCade
{
    public interface IChallengeData
    {
        public bool BeginWithDialogue { get; }
        public string BeginningDialogueNodeName { get; }
        public PlayerControlInputState OnDialogueCompleteInputState { get; }

        public void StartChallengeWithDialogue(string nodeName, PlayerControlInputState onDialogueCompleteInputState);

        public string BackgroundMusicFilename { get; set; }

        public bool HasStealthGameplay { get; }
        public bool IsChase { get; }
        public bool FollowLightsOn { get; }

        public void SetStealthGameplay(bool hasStealthGameplay, bool isChase, bool followLightsOn = false);

        public bool RestartSceneOnFailure { get; set; }

        public bool ShouldChangeSceneOnFailure { get; }
        public bool ShouldChangeSceneOnComplete { get; }
        public GameSceneName SceneChangeName { get; }

        public void ChangeSceneOnChallengeFailure(GameSceneName sceneChangeName);

        public void ChangeSceneOnChallengeComplete(GameSceneName sceneChangeName);
    }
}
