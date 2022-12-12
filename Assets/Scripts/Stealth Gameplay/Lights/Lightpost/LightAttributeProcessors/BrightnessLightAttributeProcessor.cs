using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace IndieCade
{
    public class BrightnessLightAttributeProcessor : MonoBehaviour
    {
        [SerializeField] private Light2D _referenceLight;
        [SerializeField] private List<Light2D> _secondaryLights;
        [SerializeField] private List<float> _secondaryLightRatios;

        public void SetBrightness(float referenceBrightness)
        {
            if (_referenceLight != null)
            {
                _referenceLight.intensity = referenceBrightness;

                for (int i = 0; i < _secondaryLights.Count; i++)
                {
                    _secondaryLights[i].intensity = _secondaryLightRatios[i] * referenceBrightness;
                }
            }
        }
    }
}
