using System;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class RowboatStoppedInteractionValidator : MonoBehaviour, IInteractionValidator
    {
        private RowboatPhysicsController _rowboatPhysicsController;

        [Inject]
        private void Initialize(RowboatPhysicsController rowboatPhysicsController)
        {
            _rowboatPhysicsController = rowboatPhysicsController;
        }

        public bool Validate()
        {
            return _rowboatPhysicsController.CanTransitionFromStopped();
        }
    }
}
