using System;
namespace IndieCade
{
    public abstract class ShapeLightStateAttribute : LightStateAttribute
    {
        private LightShape _shape;

        public LightShape Shape => _shape;

        public ShapeLightStateAttribute(LightShape shape)
        {
            _shape = shape;
        }
    }
}
