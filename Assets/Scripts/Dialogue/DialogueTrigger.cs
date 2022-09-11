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

        private void OnTriggerEnter2D(Collider2D _)
        {
            _dialogueControl.StartDialogue(_nodeName);
            Destroy(gameObject);
        }
    }
}
