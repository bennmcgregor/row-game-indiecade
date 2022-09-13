using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class ChallengeSceneChangeEffector : GameStateListener
    {
        private WorldMapSceneChangeEffector _sceneChangeEffector;

        [Inject]
        public void Initialize(WorldMapSceneChangeEffector worldMapSceneChangeEffector)
        {
            _sceneChangeEffector = worldMapSceneChangeEffector;
        }

        protected override void OnChallengeCompleted()
        {
            ChallengeInitializationData challengeInitializationData = _questRunner.CurrentQuest.CurrentChallenge;
            if (challengeInitializationData.ShouldChangeSceneOnComplete)
            {
                _sceneChangeEffector.ActivateScene(challengeInitializationData.SceneChangeName);
            }
        }

        protected override void OnChallengeFailed()
        {
            ChallengeInitializationData challengeInitializationData = _questRunner.CurrentQuest.CurrentChallenge;
            if (challengeInitializationData.ShouldChangeSceneOnFailure)
            {
                _sceneChangeEffector.ActivateScene(challengeInitializationData.SceneChangeName);
            }
        }
    }
}