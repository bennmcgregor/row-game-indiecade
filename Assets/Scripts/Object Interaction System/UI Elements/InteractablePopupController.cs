using System;
using System.Collections.Generic;
using UnityEngine;

namespace IndieCade
{
    public class InteractablePopupController : MonoBehaviour
    {
        [SerializeField] private Popup _popup;
        [SerializeField] private ObjectInteractable _interactable;
        [SerializeField] protected InteractionValidator[] _displayValidators = default;
        [SerializeField] protected InteractionValidator[] _enableValidators = default;

        private bool _interacting = false;

        private void Start()
        {
            _interactable.OnStartInteracting += () => _interacting = true;
            _interactable.OnEndInteracting += () => _interacting = false;
        }

        private bool ValidateInteraction(InteractionValidator[] validators)
        {
            foreach (var validator in validators)
            {
                if (!validator.Validate())
                {
                    return false;
                }
            }
            return true;
        }

        private void Update()
        {
            if (ValidateInteraction(_displayValidators))
            {
                _popup.Display();
                if (ValidateInteraction(_enableValidators))
                {
                    _popup.Enable();
                    if (_interacting)
                    {
                        _popup.Activate();
                    }
                }
            } else
            {
                _popup.Hide();
            }

            if (!ValidateInteraction(_enableValidators))
            {
                _popup.Disable();
            }

            if (!_interacting)
            {
                _popup.Deactivate();
            }
        }
    }
}
