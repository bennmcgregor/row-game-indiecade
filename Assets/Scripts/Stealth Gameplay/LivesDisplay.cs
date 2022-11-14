using System.Collections.Generic;
using UnityEngine;
using Zenject;
using TMPro;

namespace IndieCade
{
    public class LivesDisplay : GameStateListener
    {
        [SerializeField] private ToggleableUIElement _eyeDisplayPrefab;
        [SerializeField] private int _numLives = 4;
        [SerializeField] private Sprite _nonChaseOnSprite;
        [SerializeField] private Sprite _nonChaseOffSprite;
        [SerializeField] private Sprite _chaseOnSprite;
        [SerializeField] private Sprite _chaseOffSprite;

        private List<ToggleableUIElement> _eyeDisplays;
        private List<SingleLifeDisplay> _singleLifeDisplays;
        private StealthRunner _stealthRunner;

        [Inject]
        public void Initialize(StealthRunner stealthRunner)
        {
            _stealthRunner = stealthRunner;
        }

        protected override void Awake()
        {
            base.Awake();
            _stealthRunner.OnLivesUpdated += UpdateLives;
            _eyeDisplays = new List<ToggleableUIElement>();
            _singleLifeDisplays = new List<SingleLifeDisplay>();

            for (int i = 0; i < _numLives - 1; i++)
            {
                var eye = Instantiate(_eyeDisplayPrefab, transform);
                _singleLifeDisplays.Add(eye.gameObject.GetComponent<SingleLifeDisplay>());
                eye.TurnOff();
                _eyeDisplays.Add(eye);
            }
        }

        protected override void InitializeChallenge(ChallengeInitializationData challengeInitializationData)
        {
            if (challengeInitializationData.IsChase)
            {
                foreach (var display in _singleLifeDisplays)
                {
                    display.SetOffImage(_chaseOffSprite);
                    display.SetOnImage(_chaseOnSprite);
                }
            } else
            {
                foreach (var display in _singleLifeDisplays)
                {
                    display.SetOffImage(_nonChaseOffSprite);
                    display.SetOnImage(_nonChaseOnSprite);
                }
            }
        }

        private void UpdateLives()
        {
            if (_stealthRunner.CatchCount > 0)
            {
                for (int i = 0; i < _numLives - 1; i++)
                {
                    _eyeDisplays[i].TurnOff();
                }
                for (int i = 0; i < _numLives - _stealthRunner.CatchCount; i++)
                {
                    _eyeDisplays[i].TurnOn();
                }
            }
        }
    }
}
