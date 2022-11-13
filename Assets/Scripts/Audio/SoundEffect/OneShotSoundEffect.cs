using System;
namespace IndieCade
{
    public class OneShotSoundEffect : SoundEffectBase
    {
        public override void Play()
        {
            _soundFXPlayer.PlayOneShot(_soundEffectKey);
        }
    }
}
