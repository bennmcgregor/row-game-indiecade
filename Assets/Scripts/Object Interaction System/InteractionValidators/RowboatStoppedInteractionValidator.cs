using System;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class RowboatStoppedInteractionValidator : InteractionValidator
    {
        private RowboatPhysicsController _rowboatPhysicsController;

        [Inject]
        private void Initialize(RowboatPhysicsController rowboatPhysicsController)
        {
            _rowboatPhysicsController = rowboatPhysicsController;
        }

        public override bool Validate()
        {
            return _rowboatPhysicsController.CanTransitionFromStopped();
        }
    }
}
