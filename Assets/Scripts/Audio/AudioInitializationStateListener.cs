using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class AudioInitializationStateListener : GameInitializationStateListener
    {
        private AudioManager _audioManager;

        [Inject]
        public void Initialize(AudioManager audioManager)
        {
            _audioManager = audioManager;
        }

        protected override void InitializeScene(SceneInitializationData sceneInitializationData)
        {
            if (sceneInitializationData.BackgroundMusicFilename != null)
            {
                _audioManager.ChangeBackgroundMusic(sceneInitializationData.BackgroundMusicFilename);
            }
        }

        protected override void SaveScene(SceneInitializationData sceneInitializationData)
        {
            sceneInitializationData.BackgroundMusicFilename = _audioManager.CurrentBackgroundMusic;
        }

        protected override void InitializeChallenge(ChallengeInitializationData challengeInitializationData)
        {
            if (challengeInitializationData.BackgroundMusicFilename != null)
            {
                _audioManager.ChangeBackgroundMusic(challengeInitializationData.BackgroundMusicFilename);
            }
        }
    }
}