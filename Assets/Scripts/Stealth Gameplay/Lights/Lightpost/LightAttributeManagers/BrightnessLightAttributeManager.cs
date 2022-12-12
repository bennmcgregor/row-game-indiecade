using System;
namespace IndieCade
{
    public class BrightnessLightAttributeManager : LightAttributeManager
    {
        protected override void UpdateStateInternal(LightStateData data)
        {
            BrightnessLightStateAttribute brightnessData = data.Brightness;
            _lightInstance.BrightnessProcessor.SetBrightness(brightnessData.NonBaseLightBrightness);
            _lightInstance.BaseLight.intensity = brightnessData.BaseLightBrightness;
        }
    }
}
