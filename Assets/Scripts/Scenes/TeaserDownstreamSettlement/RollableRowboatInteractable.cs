using System;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class RollableRowboatInteractable : ObjectInteractable
    {
        [SerializeField] private OneShotAnimation _animation;
        [SerializeField] private ObjVerticalTranslator _translator;

        private RollableRowboatValidator _rollableRowboatValidator;

        [Inject]
        public void Initialize(RollableRowboatValidator rollableRowboatValidator)
        {
            _rollableRowboatValidator = rollableRowboatValidator;
        }

        private void Awake()
        {
            _animation.OnAnimationEnd += OnAnimationEnd;
        }

        protected override void OnStartInteractionInternal()
        {
            _animation.Play();
            _translator.TranslateForwards();
        }

        private void OnAnimationEnd()
        {
            _rollableRowboatValidator.RollRowboat();
            Destroy(gameObject);
        }
    }
}
