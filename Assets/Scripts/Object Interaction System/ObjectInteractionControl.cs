using System;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class ObjectInteractionControl : MonoBehaviour
    {
        public bool interacting;

        public void Start()
        {
            interacting = false;
        }

        public void Interact()
        {
            interacting = !interacting;
        }

        // TODO: Add code for switching PlayerControlInputState of PlayerInputManager depending on interaction
    }
}
