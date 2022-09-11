using System;
using UnityEngine;

namespace IndieCade
{
    public class TriStateUIElement : MonoBehaviour, IDisplayable, IEnableable, IActivateable
    {
        [SerializeField] private GameObject _displayVersion;
        [SerializeField] private GameObject _enabledVersion;
        [SerializeField] private GameObject _activatedVersion;

        private void Awake()
        {
            _displayVersion.SetActive(false);
            _enabledVersion.SetActive(false);
            _activatedVersion.SetActive(false);
        }

        public void Display()
        {
            _displayVersion.SetActive(true);
            Disable();
            Deactivate();
        }

        public void Hide()
        {
            _displayVersion.SetActive(false);
        }

        public void Enable()
        {
            _enabledVersion.SetActive(true);
            Hide();
            Deactivate();
        }

        public void Disable()
        {
            _enabledVersion.SetActive(false);
        }

        public void Activate()
        {
            _activatedVersion.SetActive(true);
            Hide();
            Disable();
        }

        public void Deactivate()
        {
            _activatedVersion.SetActive(false);
        }
    }
}
