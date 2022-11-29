using System;
using UnityEngine;

namespace IndieCade
{
    public class SteadyMotionTypeLightStateAttribute : MotionTypeLightStateAttribute
    {
        public SteadyMotionTypeLightStateAttribute() : base(LightMotionType.STEADY) {}
    }
}
