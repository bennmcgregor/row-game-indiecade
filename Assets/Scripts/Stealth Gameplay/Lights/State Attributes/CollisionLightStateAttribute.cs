using System;
namespace IndieCade
{
    public class CollisionLightStateAttribute : LightStateAttribute
    {
        private bool _usingLightCollisions;
        private bool _detectingCollisions;
        private float _inSpotlightDelayTimeSeconds;
        private float _collisionWaitTimeSeconds;

        public bool UsingLightCollisions => _usingLightCollisions;
        public bool DetectingCollisions => _detectingCollisions;
        public float InSpotlightDelayTimeSeconds => _inSpotlightDelayTimeSeconds;
        public float CollisionWaitTimeSeconds => _collisionWaitTimeSeconds;

        public CollisionLightStateAttribute(bool usingLightCollisions, bool detectingCollisions, float delayTimeSeconds, float collisionWaitTimeSeconds)
        {
            _usingLightCollisions = usingLightCollisions;
            _detectingCollisions = detectingCollisions;
            _inSpotlightDelayTimeSeconds = delayTimeSeconds;
            _collisionWaitTimeSeconds = collisionWaitTimeSeconds;
        }
    }
}
