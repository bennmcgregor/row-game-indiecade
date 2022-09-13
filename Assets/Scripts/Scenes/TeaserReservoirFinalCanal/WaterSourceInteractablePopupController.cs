using System;
using UnityEngine;

namespace IndieCade
{
    public class WaterSourceInteractablePopupController : RangeInteractablePopupController
    {
        [SerializeField] private RowboatStoppedInteractionValidator _rowboatStoppedInteractionValidator;

        protected override void InitializeValidators()
        {
            _enableValidators.Add(_rowboatStoppedInteractionValidator);
            base.InitializeValidators();
        }
    }
}
