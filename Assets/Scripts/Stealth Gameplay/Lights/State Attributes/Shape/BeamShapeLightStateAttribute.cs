using System;
namespace IndieCade
{
    public class BeamShapeLightStateAttribute : ShapeLightStateAttribute
    {
        private float _innerRadius;
        private float _outerRadius;
        private float _innerArcAngle;
        private float _outerArcAngle;

        public float InnerRadius => _innerRadius;
        public float OuterRadius => _outerRadius;
        public float InnerArcAngle => _innerArcAngle;
        public float OuterArcAngle => _outerArcAngle;

        public BeamShapeLightStateAttribute(float innerRadius, float outerRadius, float innerArcAngle, float outerArcAngle) : base(LightShape.BEAM)
        {
            _innerRadius = innerRadius;
            _outerRadius = outerRadius;
            _innerArcAngle = innerArcAngle;
            _outerArcAngle = outerArcAngle;
        }
    }
}
