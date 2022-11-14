using System;
using UnityEngine;

namespace IndieCade
{
    public class LoopingClipPlayer : MonoBehaviour
    {
        private SoundFXPlayer _soundFXPlayer;
        private AudioSource _audioSource;
        private StopOnEventSoundEffect _soundEffect;

        public void Initialize(SoundFXPlayer soundFXPlayer)
        {
            _soundFXPlayer = soundFXPlayer;
        }

        public void Play(AudioClip clip, StopOnEventSoundEffect soundEffect)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.loop = true;
            _audioSource.volume = _soundFXPlayer.Volume;

            _soundEffect = soundEffect;
            _soundEffect.ShouldStopPlaying += OnStopClip;

            _audioSource.clip = clip;
            _audioSource.Play();
        }

        private void OnStopClip()
        {
            _soundEffect.ShouldStopPlaying -= OnStopClip;

            _audioSource.Stop();
            Destroy(_audioSource);
            Destroy(this);
        }
    }
}
