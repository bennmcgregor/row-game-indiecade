using System;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class SpeakWithNicoInteractable : ObjectInteractable
    {
        [SerializeField] private DialogueTrigger _dialogueTrigger;

        protected override void OnStartInteractionInternal()
        {
            _dialogueTrigger.StartDialogueAndDestroyTrigger();
        }
    }
}
