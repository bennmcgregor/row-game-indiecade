using System;
namespace IndieCade
{
    public class CircularShapeLightStateAttribute : ShapeLightStateAttribute
    {
        private float _radius;

        public float Radius => _radius;

        public CircularShapeLightStateAttribute(float radius) : base(LightShape.CIRCULAR)
        {
            _radius = radius;
        }
    }

}
