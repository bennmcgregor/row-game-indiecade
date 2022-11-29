using System;
using System.Collections;
using UnityEngine;

namespace IndieCade
{
    public class MotionTypeLightAttributeManager : LightAttributeManager
    {
        private Coroutine _flashCoroutine;

        protected override void InitializeWithDataInternal(LightStateData data)
        {
            // TODO: perform any class setup (such as initializing properties), update func signature
        }

        protected override void UpdateStateInternal(LightStateData data)
        {
            switch (data.MotionType.Type)
            {
                case LightMotionType.STEADY:
                    break;
                case LightMotionType.FLASHING:
                    break;
                case LightMotionType.ROTATING:
                    break;
                case LightMotionType.FOLLOWING:
                    break;
            }

            // TODO: implement motion logic for each kind of motion
            // TODO: ensure that transitions between motions are smooth
        }

        //private IEnumerator Flash()
        //{
        //    while (true)
        //    {
        //        _lightStateController.Activate();
        //        yield return new WaitForSeconds(OnTimeSeconds);
        //        _lightStateController.Deactivate();
        //        yield return new WaitForSeconds(OffTimeSeconds);
        //    }
        //}
    }
}
