using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace IndieCade
{
    public class LightStateController : MonoBehaviour, IActivateable
    {
        [SerializeField] private Light2D[] _lights;

        private bool _on;

        public bool IsOn => _on;

        public void ChangeColor(Color color)
        {
            foreach (var light in _lights)
            {
                light.color = color;
            }
        }

        public void Activate()
        {
            foreach (var light in _lights)
            {
                light.gameObject.SetActive(true);
            }
            _on = true;
        }

        public void Deactivate()
        {
            foreach (var light in _lights)
            {
                light.gameObject.SetActive(false);
            }
            _on = false;
        }
    }
}
