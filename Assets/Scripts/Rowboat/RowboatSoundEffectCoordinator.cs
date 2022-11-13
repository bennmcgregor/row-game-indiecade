using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class RowboatSoundEffectCoordinator : MonoBehaviour
    {
        [SerializeField] private string[] _catchSoundEffectNames;
        [SerializeField] private string[] _finishSoundEffectNames;

        private List<OneShotSoundEffect> _catchSoundEffects;
        private List<OneShotSoundEffect> _finishSoundEffects;

        private RowboatPhysicsController _rowboatPhysicsController;
        private DiContainer _diContainer;

        [Inject]
        public void Initialize(RowboatPhysicsController rowboatPhysicsController, DiContainer diContainer)
        {
            _rowboatPhysicsController = rowboatPhysicsController;
            _diContainer = diContainer;
        }

        private void Awake()
        {
            _catchSoundEffects = new List<OneShotSoundEffect>();
            _finishSoundEffects = new List<OneShotSoundEffect>();

            foreach (var soundEffectStr in _catchSoundEffectNames)
            {
                OneShotSoundEffect soundEffect = _diContainer.InstantiateComponent<OneShotSoundEffect>(gameObject);
                soundEffect.SetSoundEffectKey(soundEffectStr);
                _catchSoundEffects.Add(soundEffect);
            }

            foreach (var soundEffectStr in _finishSoundEffectNames)
            {
                OneShotSoundEffect soundEffect = _diContainer.InstantiateComponent<OneShotSoundEffect>(gameObject);
                soundEffect.SetSoundEffectKey(soundEffectStr);
                _finishSoundEffects.Add(soundEffect);
            }

            _rowboatPhysicsController.PlayCatchSoundEffect += () => PlayRandomSoundEffect(_catchSoundEffects);
            _rowboatPhysicsController.PlayFinishSoundEffect += () => PlayRandomSoundEffect(_finishSoundEffects);
        }

        private void PlayRandomSoundEffect(List<OneShotSoundEffect> soundEffects)
        {
            System.Random r = new System.Random();
            soundEffects[r.Next(0, soundEffects.Count)].Play();
        }
    }
}
