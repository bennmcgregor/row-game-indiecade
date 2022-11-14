using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieCade
{
    public class IndieCadeMainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _credits;
        [SerializeField] private GameObject _menu;
        [SerializeField] private OneShotSoundEffect _buttonClick;

        public void OpenCredits()
        {
            _credits.SetActive(true);
            _menu.SetActive(false);
        }

        public void CloseCredits()
        {
            _menu.SetActive(true);
            _credits.SetActive(false);
        }
    }
}
