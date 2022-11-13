using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class TriggerDialogueTrigger : DialogueTrigger
    {
        private void OnTriggerEnter2D(Collider2D _)
        {
            StartDialogueAndDestroyTrigger();
        }
    }
}
