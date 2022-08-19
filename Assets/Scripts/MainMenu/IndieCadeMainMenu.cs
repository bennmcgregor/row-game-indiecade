using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieCade
{
    public class IndieCadeMainMenu : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSourceBgMusic;
        [SerializeField] private AudioSource _audioSourceButton;
        [SerializeField] private GameObject _credits;
        [SerializeField] private GameObject _menu;

        private void Awake()
        {
            StartCoroutine(AudioHelper.FadeIn(_audioSourceBgMusic, 5));
        }

        private void StartFadeOut()
        {
            StartCoroutine(AudioHelper.FadeOut(_audioSourceBgMusic, 2));
        }

        public void PlayGame()
        {
            _audioSourceButton.Play();
        }

        public void OpenCredits()
        {
            _credits.SetActive(true);
            _menu.SetActive(false);
            _audioSourceButton.Play();
        }

        public void CloseCredits()
        {
            _menu.SetActive(true);
            _credits.SetActive(false);
            _audioSourceButton.Play();
        }
    }
}
