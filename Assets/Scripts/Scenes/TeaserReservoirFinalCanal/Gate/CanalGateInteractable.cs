using System;
using System.Collections;
using UnityEngine;

namespace IndieCade
{
    public class CanalGateInteractable : ObjectInteractable
    {
        [SerializeField] private ObjVerticalTranslator _gate;

        protected override void OnStartInteractionInternal()
        {
            _gate.TranslateForwards();
            gameObject.SetActive(false);
        }
    }
}
