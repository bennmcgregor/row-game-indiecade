using System;
using UnityEngine;

namespace IndieCade
{
    public class LightToggler : MonoBehaviour
    {
        private LightInstance _lightInstance;

        public void SetLightInstance(LightInstance light)
        {
            _lightInstance = light;
        }

        public void ToggleBaseLight(bool on)
        {
            // TODO: when handling multiple subscriptions to a single lightpost,
            // only turn off the baselight if all of the subscribed lights are off
            _lightInstance.BaseLight.gameObject.SetActive(on);
        }

        public void ToggleNonBaseLights(bool on)
        {
            foreach (var light in _lightInstance.Lights)
            {
                light.gameObject.SetActive(on);
            }
        }

        public void ToggleAllLights(bool on)
        {
            ToggleBaseLight(on);
            ToggleNonBaseLights(on);
        }
    }
}
