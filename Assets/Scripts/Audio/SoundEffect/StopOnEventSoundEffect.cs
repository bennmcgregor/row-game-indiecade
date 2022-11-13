using System;
using UnityEngine;

namespace IndieCade
{
    public class StopOnEventSoundEffect : SoundEffectBase
    {
        public Action ShouldStopPlaying;

        public override void Play()
        {
            _soundFXPlayer.PlayUntilStopped(_soundEffectKey, this);
        }

        public void InvokeStopPlayingAction()
        {
            ShouldStopPlaying?.Invoke();
        }
    }
}
