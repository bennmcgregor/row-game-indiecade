using System;
namespace IndieCade
{
    public class BeamShapeLightStateAttribute : ShapeLightStateAttribute
    {
        private float _radius;
        private float _arcAngle;
        private float _beamAngle;

        public float Radius => _radius;
        public float ArcAngle => _arcAngle;
        public float BeamAngle => _beamAngle;

        public BeamShapeLightStateAttribute(float radius, float arcAngle, float beamAngle) : base(LightShape.BEAM)
        {
            _radius = radius;
            _arcAngle = arcAngle;
            _beamAngle = beamAngle;
        }
    }

}
