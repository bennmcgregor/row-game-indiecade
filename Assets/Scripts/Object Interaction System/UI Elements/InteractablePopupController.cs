using System;
using System.Collections.Generic;
using UnityEngine;

namespace IndieCade
{
    public class InteractablePopupController : MonoBehaviour
    {
        [SerializeField] private Popup _popup;
        [SerializeField] private ObjectInteractable _interactable;

        protected List<IInteractionValidator> _displayValidators;
        protected List<IInteractionValidator> _enableValidators;
        private bool _interacting = false;

        private void Start()
        {
            _displayValidators = new List<IInteractionValidator>();
            _enableValidators = new List<IInteractionValidator>();
            InitializeValidators();

            _interactable.OnStartInteracting += () => _interacting = true;
            _interactable.OnEndInteracting += () => _interacting = false;
        }

        protected virtual void InitializeValidators() { }

        private bool ValidateInteraction(List<IInteractionValidator> validators)
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
