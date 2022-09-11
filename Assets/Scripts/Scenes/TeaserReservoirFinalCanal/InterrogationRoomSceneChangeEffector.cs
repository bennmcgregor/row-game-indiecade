using System;
using Zenject;

namespace IndieCade
{
    public class InterrogationRoomSceneChangeEffector : GameInitializationStateListener
    {
        private WorldMapSceneChangeEffector _worldMapSceneChangeEffector;
        private int _challengeInitializeCount = 0;

        [Inject]
        public void Initialize(WorldMapSceneChangeEffector worldMapSceneChangeEffector)
        {
            _worldMapSceneChangeEffector = worldMapSceneChangeEffector;
        }

        protected override void InitializeChallenge(ChallengeInitializationData challengeInitializationData)
        {
            _challengeInitializeCount++;
            if (_challengeInitializeCount == 2 && challengeInitializationData.ShouldChangeSceneName != null)
            {
                _worldMapSceneChangeEffector.ActivateScene((GameSceneName)challengeInitializationData.ShouldChangeSceneName);
            }
        }
    }
}
