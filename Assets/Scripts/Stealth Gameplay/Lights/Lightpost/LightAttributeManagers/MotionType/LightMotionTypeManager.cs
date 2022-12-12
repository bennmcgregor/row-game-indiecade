using System;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public abstract class LightMotionTypeManager : MonoBehaviour
    {
        [SerializeField] protected LightToggler _lightToggler;

        protected LightMotionType _type;
        protected LightInstance _lightInstance;

        public abstract LightMotionType Type { get; }

        public void InitializeWithData(LightInstance lightInstance)
        {
            _lightInstance = lightInstance;
        }

        public abstract void Resume(MotionTypeLightStateAttribute motionTypeLightStateAttribute);
        public abstract void Pause();

        // expected state between Pause and Resume:
        // Light is on and not moving in its current state
    }
}
