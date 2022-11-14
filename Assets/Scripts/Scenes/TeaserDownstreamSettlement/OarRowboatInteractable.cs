using System;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class OarRowboatInteractable : ObjectInteractable
    {
        private OarPickupValidator _oarPickupValidator;

        [Inject]
        public void Initialize(OarPickupValidator oarPickupValidator)
        {
            _oarPickupValidator = oarPickupValidator;
        }

        protected override void OnStartInteractionInternal()
        {
            _oarPickupValidator.PickupOar();
            Destroy(gameObject);
        }
    }
}
