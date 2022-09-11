using System;
using UnityEngine;
using Zenject;
using TMPro;

namespace IndieCade
{
    public class LivesDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private StealthRunner _stealthRunner;

        [Inject]
        public void Initialize(StealthRunner stealthRunner)
        {
            _stealthRunner = stealthRunner;
        }

        private void Awake()
        {
            _stealthRunner.OnCaughtInSpotlight += UpdateLives;
            UpdateLives();
        }

        private void UpdateLives()
        {
            _text.text = _stealthRunner.CatchCount.ToString();
        }
    }
}
