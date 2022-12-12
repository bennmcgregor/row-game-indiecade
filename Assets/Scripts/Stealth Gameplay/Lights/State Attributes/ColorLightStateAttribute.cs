using System;
using System.Collections.Generic;
using UnityEngine;

namespace IndieCade
{
    public class ColorLightStateAttribute : LightStateAttribute
    {
        private Color _color;

        public Color Color => _color;

        public ColorLightStateAttribute(Color color)
        {
            _color = color;
        }
    }
}
