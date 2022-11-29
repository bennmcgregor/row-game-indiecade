using System;
namespace IndieCade
{
    public class FlashingMotionTypeLightStateAttribute : MotionTypeLightStateAttribute
    {
        private float _onTimeSeconds;
        private float _offTimeSeconds;

        public float OnTimeSeconds => _onTimeSeconds;
        public float OffTimeSeconds => _offTimeSeconds;

        public FlashingMotionTypeLightStateAttribute(float onTimeSeconds, float offTimeSeconds)
            : base(LightMotionType.FLASHING)
        {
            _onTimeSeconds = onTimeSeconds;
            _offTimeSeconds = offTimeSeconds;
        }
    }
}
