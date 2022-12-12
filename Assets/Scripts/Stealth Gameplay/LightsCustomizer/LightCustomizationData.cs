using System;
namespace IndieCade
{
    public class LightCustomizationData
    {
        public Action OnEdit;

        private LightShape _shape;
        private float _radius;
        private LightMotionType _motionType;
        private float _motionSpeed;

        public LightShape Shape => _shape;
        public float Radius => _radius;
        public LightMotionType MotionType => _motionType;
        public float MotionSpeed => _motionSpeed;

        public LightCustomizationData(LightShape shape, float radius, LightMotionType motionType, float motionSpeed)
        {
            UpdateLight(shape, radius, motionType, motionSpeed);
        }

        public void UpdateLight(LightShape shape, float radius, LightMotionType motionType, float motionSpeed)
        {
            _shape = shape;
            _radius = radius;
            _motionType = motionType;
            _motionSpeed = motionSpeed;

            OnEdit?.Invoke();
        }
    }
}
