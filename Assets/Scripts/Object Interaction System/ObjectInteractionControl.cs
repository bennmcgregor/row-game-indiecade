using System;
using UnityEngine;

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
    }
}
