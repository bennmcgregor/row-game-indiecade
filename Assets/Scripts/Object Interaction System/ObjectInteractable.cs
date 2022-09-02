using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class ObjectInteractable : MonoBehaviour
    {
        public ObjectInteractionControl objectInteractionControl;

        [Inject]
        public void Initialize(ObjectInteractionControl obj)
        {
            objectInteractionControl = obj;
        }
    }
}

