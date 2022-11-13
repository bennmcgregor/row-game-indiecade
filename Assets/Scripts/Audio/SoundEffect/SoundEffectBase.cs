using System;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public abstract class SoundEffectBase : MonoBehaviour, IPlayable
    {
        [SerializeField] protected string _soundEffectKey;

        protected SoundFXPlayer _soundFXPlayer;

        [Inject]
        public void Initialize(AudioManager audioManager)
        {
            _soundFXPlayer = audioManager.SoundFXPlayer;
        }

        public abstract void Play();

        public void SetSoundEffectKey(string soundEffectKey)
        {
            _soundEffectKey = soundEffectKey;
        }
    }
}
