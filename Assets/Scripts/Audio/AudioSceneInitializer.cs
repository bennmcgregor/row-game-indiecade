using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class AudioSceneInitializer : GameInitializationStateListener
    {
        private AudioManager _audioManager;

        [Inject]
        public void Initialize(AudioManager audioManager)
        {
            _audioManager = audioManager;
        }

        protected override void InitializeScene(SceneInitializationData sceneInitializationData)
        {
            Debug.Log($"initializing scene: {sceneInitializationData.SceneName}");
            if (sceneInitializationData.BackgroundMusicFilename != null)
            {
                _audioManager.ChangeBackgroundMusic(sceneInitializationData.BackgroundMusicFilename);
            }
        }

        protected override void InitializeQuest(QuestInitializationData questInitializationData)
        {
            Debug.Log($"initializing quest: {questInitializationData.StateName}");
        }

        protected override void InitializeChallenge(ChallengeInitializationData challengeInitializationData)
        {
            Debug.Log($"initializing challenge: {challengeInitializationData.StateName}");
            if (challengeInitializationData.BackgroundMusicFilename != null)
            {
                _audioManager.ChangeBackgroundMusic(challengeInitializationData.BackgroundMusicFilename);
            }
        }
    }
}