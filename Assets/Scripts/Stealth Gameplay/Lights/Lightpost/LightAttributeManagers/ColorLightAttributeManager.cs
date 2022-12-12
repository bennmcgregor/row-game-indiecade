using System;
namespace IndieCade
{
    public class ColorLightAttributeManager : LightAttributeManager
    {
        protected override void UpdateStateInternal(LightStateData data)
        {
            ColorLightStateAttribute colorData = data.Color;
            
            foreach (var light in _lightInstance.Lights)
            {
                light.color = colorData.Color;
            }
            _lightInstance.BaseLight.color = colorData.Color;
        }
    }
}
