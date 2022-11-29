using System;
using UnityEngine;

namespace IndieCade
{
    public class ColorLightStateAttribute : LightStateAttribute
    {
        private Color _enabledColor;
        private Color _collisionColor;

        public Color EnabledColor => _enabledColor;
        public Color CollisionColor => _collisionColor;

        public ColorLightStateAttribute(Color enabledColor, Color collisionColor)
        {
            _enabledColor = enabledColor;
            _collisionColor = collisionColor;
        }
    }
}
