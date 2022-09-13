using System;
using UnityEngine;

namespace IndieCade
{
    public class StealthRunnerUI : MonoBehaviour, IActivateable
    {
        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}
