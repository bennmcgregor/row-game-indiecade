using System;
using System.Collections.Generic;
using UnityEngine;

namespace IndieCade
{
    public class SoundFXPlayer : MonoBehaviour
    {
        [SerializeField] private AudioClip[] _soundFXClips;

        private Dictionary<string, AudioClip> _soundFX;
        private float _volume = 1f;
        private AudioSource _oneShotAudioSource;

        public float Volume => _volume;

        private void Awake()
        {
            _oneShotAudioSource = gameObject.AddComponent<AudioSource>();
            _oneShotAudioSource.volume = _volume;
        }

        private void Start()
        {
            _soundFX = new Dictionary<string, AudioClip>();
            foreach (var clip in _soundFXClips)
            {
                _soundFX[clip.name] = clip;
            }
        }

        public void SetVolume(float volume)
        {
            _volume = volume;
            _oneShotAudioSource.volume = _volume;
        }

        public void PlayUntilStopped(string soundFXKey, StopOnEventSoundEffect soundEffect)
        {
            /// Play a sound effect with filename "soundFXKey" (without file extension)
            PlayUntilStopped(_soundFX[soundFXKey], soundEffect);
        }

        private void PlayUntilStopped(AudioClip clip, StopOnEventSoundEffect soundEffect)
        {
            LoopingClipPlayer player = gameObject.AddComponent<LoopingClipPlayer>();
            player.Initialize(this);
            player.Play(clip, soundEffect);
        }

        public void PlayOneShot(string soundFXKey)
        {
            /// Play a sound effect with filename "soundFXKey" (without file extension)
            PlayOneShot(_soundFX[soundFXKey]);
        }

        private void PlayOneShot(AudioClip clip)
        {
            _oneShotAudioSource.PlayOneShot(clip);
        }
    }
}
