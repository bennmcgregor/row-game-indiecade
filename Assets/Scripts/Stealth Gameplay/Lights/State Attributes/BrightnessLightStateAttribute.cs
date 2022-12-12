using System;

namespace IndieCade
{
    public class BrightnessLightStateAttribute : LightStateAttribute
    {
        private float _baseLightBrightness;
        private float _nonBaseLightBrightness;

        public float BaseLightBrightness => _baseLightBrightness;
        public float NonBaseLightBrightness => _nonBaseLightBrightness;

        public BrightnessLightStateAttribute(float baseLightBrightness, float nonBaseLightBrightness)
        {
            _baseLightBrightness = baseLightBrightness;
            _nonBaseLightBrightness = nonBaseLightBrightness;
        }
    }
}
