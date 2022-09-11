using System;

namespace IndieCade
{
    public class TestGameInitializationStateListener : GameInitializationStateListener
    {
        protected override void InitializeScene(SceneInitializationData sceneInitializationData)
        {
            UnityEngine.Debug.Log($"InitializeScene {sceneInitializationData.SceneName}");
        }

        protected override void SaveScene(SceneInitializationData sceneInitializationData)
        {
            UnityEngine.Debug.Log($"SaveScene {sceneInitializationData.SceneName}");
        }

        protected override void InitializeChallenge(ChallengeInitializationData challengeInitializationData)
        {
            UnityEngine.Debug.Log($"InitializeChallenge {challengeInitializationData.StateName}");
        }

        protected override void InitializeQuest(QuestInitializationData questInitializationData)
        {
            UnityEngine.Debug.Log($"InitializeQuest {questInitializationData.StateName}");
        }
    }
}
