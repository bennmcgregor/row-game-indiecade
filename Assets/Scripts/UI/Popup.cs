using System;
using UnityEngine;
using TMPro;

namespace IndieCade
{
    public class Popup : MonoBehaviour, IDisplayable, IActivateable, IEnableable
    {
        [SerializeField] private TriStateUIElement _triStateUIElement;

        private bool _displaying = false;
        private bool _enabled = false;

        public void Display()
        {
            _displaying = true;
            SetActivePopup(_displaying);
            _triStateUIElement.Display();
        }

        public void Hide()
        {
            _displaying = false;
            SetActivePopup(_displaying);
        }

        public void Enable()
        {
            if (_displaying)
            {
                _enabled = true;
                _triStateUIElement.Enable();
            }
        }

        public void Disable()
        {
            _enabled = false;
            _triStateUIElement.Display();
        }

        public void Activate()
        {
            if (_displaying && _enabled)
            {
                _triStateUIElement.Activate();
            }
        }

        public void Deactivate()
        {
            _triStateUIElement.Enable();
        }

        private void SetActivePopup(bool active)
        {
            _triStateUIElement.gameObject.SetActive(active);
        }
    }
}
