using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class ObjectInteractable : MonoBehaviour
    {
        public Action OnStartInteracting;
        public Action OnEndInteracting;

        protected ObjectInteractionControl _objectInteractionControl;

        public ObjectInteractionControl ObjectInteractionControl => _objectInteractionControl;

        protected List<IInteractionValidator> _validators;
        private bool _currentlyInteracting = false;

        [Inject]
        public void Initialize(ObjectInteractionControl obj)
        {
            _objectInteractionControl = obj;
        }

        private void Start()
        {
            _validators = new List<IInteractionValidator>();
            InitializeValidators();
            _objectInteractionControl.OnStartInteraction += OnStartInteraction;
            _objectInteractionControl.OnEndInteraction += OnEndInteraction;
        }

        protected virtual void InitializeValidators() { }

        private bool ValidateInteraction()
        {
            foreach (var validator in _validators)
            {
                if (!validator.Validate())
                {
                    return false;
                }
            }
            return true;
        }

        private void OnStartInteraction()
        {
            if (ValidateInteraction())
            {
                _currentlyInteracting = true;
                OnStartInteracting?.Invoke();
                OnStartInteractionInternal();
            }
        }

        protected virtual void OnStartInteractionInternal() { }

        private void OnEndInteraction()
        {
            if (_currentlyInteracting)
            {
                _currentlyInteracting = false;
                OnEndInteracting?.Invoke();
                OnEndInteractionInternal();
            }
        }

        protected virtual void OnEndInteractionInternal() { }

        private void Update()
        {
            if (_currentlyInteracting && !ValidateInteraction())
            {
                OnEndInteraction();
            }
        }
    }
}

