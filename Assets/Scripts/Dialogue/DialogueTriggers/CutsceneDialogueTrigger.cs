using System;
namespace IndieCade
{
    public class CutsceneDialogueTrigger : DialogueTrigger
    {
        private void OnEnable()
        {
            StartDialogue();
        }

        private void OnDisable()
        {
            StopDialogue();
        }
    }
}
