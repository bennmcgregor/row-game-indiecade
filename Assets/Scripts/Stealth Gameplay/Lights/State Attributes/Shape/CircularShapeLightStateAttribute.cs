using System;
namespace IndieCade
{
    public class CircularShapeLightStateAttribute : ShapeLightStateAttribute
    {
        private float _innerRadius;
        private float _beamHeight;

        public float InnerRadius => _innerRadius;
        public float BeamHeight => _beamHeight;

        public CircularShapeLightStateAttribute(float innerRadius, float beamHeight) : base(LightShape.CIRCULAR)
        {
            _innerRadius = innerRadius;
            _beamHeight = beamHeight;
        }
    }

}
