using System;
namespace IndieCade
{
    public abstract class MotionTypeLightStateAttribute : LightStateAttribute
    {
        private LightMotionType _type;

        public LightMotionType Type => _type;

        public MotionTypeLightStateAttribute(LightMotionType type)
        {
            _type = type;
        }
    }
}
