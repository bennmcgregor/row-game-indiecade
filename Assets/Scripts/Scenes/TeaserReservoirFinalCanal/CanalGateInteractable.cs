using System;
using System.Collections;
using UnityEngine;

namespace IndieCade
{
    public class CanalGateInteractable : ObjectInteractable
    {
        [SerializeField] private ColliderInteractionValidator _colliderInteractionValidator;
        [SerializeField] private Gate _gate;

        protected override void InitializeValidators()
        {
            _validators.Add(_colliderInteractionValidator);
        }

        protected override void OnStartInteractionInternal()
        {
            _gate.Open();
            gameObject.SetActive(false);
        }
    }
}
