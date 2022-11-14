using System;
using UnityEngine;

namespace IndieCade
{
    public class ToggleableUIElement : MonoBehaviour
    {
        [SerializeField] private GameObject _on;
        [SerializeField] private GameObject _off;

        public void TurnOn()
        {
            _on.SetActive(true);
            _off.SetActive(false);
        }

        public void TurnOff()
        {
            _on.SetActive(false);
            _off.SetActive(true);
        }
    }
}
