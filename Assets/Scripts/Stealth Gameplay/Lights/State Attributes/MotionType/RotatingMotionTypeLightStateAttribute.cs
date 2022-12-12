using System;
namespace IndieCade
{
    public class RotatingMotionTypeLightStateAttribute : MotionTypeLightStateAttribute
    {
        private float _speed;
        // must be between 0 and 360
        private float _startAngle;
        private float _endAngle;

        public float Speed => _speed;
        public float StartAngle => _startAngle;
        public float EndAngle => _endAngle;

        public RotatingMotionTypeLightStateAttribute(float speed, float startAngle, float endAngle)
            : base(LightMotionType.ROTATING)
        {
            _speed = speed;
            _startAngle = startAngle;
            _endAngle = endAngle;
        }
    }
}
