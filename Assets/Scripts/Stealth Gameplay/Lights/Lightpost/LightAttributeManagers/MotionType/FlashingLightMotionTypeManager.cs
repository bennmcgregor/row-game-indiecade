using System;
using System.Collections;
using UnityEngine;

namespace IndieCade
{
    public class FlashingLightMotionTypeManager : LightMotionTypeManager
    {
        private Coroutine _flashCoroutine;

        public override LightMotionType Type => LightMotionType.FLASHING;

        public override void Resume(MotionTypeLightStateAttribute motionTypeLightStateAttribute)
        {
            _flashCoroutine = StartCoroutine(Flash(motionTypeLightStateAttribute));
        }

        public override void Pause()
        {
            StopCoroutine(_flashCoroutine);
        }

        private IEnumerator Flash(MotionTypeLightStateAttribute motionData)
        {
            FlashingMotionTypeLightStateAttribute flashingData = (FlashingMotionTypeLightStateAttribute) motionData;
            while (true)
            {
                _lightToggler.ToggleNonBaseLights(true);
                yield return new WaitForSeconds(flashingData.OnTimeSeconds);
                _lightToggler.ToggleNonBaseLights(false);
                yield return new WaitForSeconds(flashingData.OffTimeSeconds);
            }
        }
    }
}
