using System;
using UnityEngine;

namespace IndieCade
{
    public class SteadyLightMotionTypeManager : LightMotionTypeManager
    {
        public override LightMotionType Type => LightMotionType.STEADY;

        public override void Resume(MotionTypeLightStateAttribute motionTypeLightStateAttribute)
        {
            _lightToggler.ToggleAllLights(true);
        }

        public override void Pause() {}
    }
}
