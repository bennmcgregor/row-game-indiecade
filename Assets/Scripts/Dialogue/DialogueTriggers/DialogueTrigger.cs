using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] private string _nodeName;

        private DialogueControl _dialogueControl;

        [Inject]
        public void Initialize(DialogueControl dialogueControl)
        {
            _dialogueControl = dialogueControl;
        }

        public void StartDialogueAndDestroyTrigger()
        {
            StartDialogue();
            Destroy(gameObject);
        }

        protected void StartDialogue()
        {
            _dialogueControl.StartDialogue(_nodeName);
        }

        protected void StopDialogue()
        {
            _dialogueControl.StopDialogue();
        }
    }
}
