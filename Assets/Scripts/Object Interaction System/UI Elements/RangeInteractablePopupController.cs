using System;
using UnityEngine;

namespace IndieCade
{
    public class RangeInteractablePopupController : InteractablePopupController
    {
        [SerializeField] private ColliderInteractionValidator _rangeInteractionValidator;

        protected override void InitializeValidators()
        {
            _displayValidators.Add(_rangeInteractionValidator);
        }
    }
}
