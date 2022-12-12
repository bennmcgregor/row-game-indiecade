using System;
using UnityEngine;

namespace IndieCade
{
    public class CollisionDetectionLightAttributeManager : LightAttributeManager
    {
        protected override void UpdateStateInternal(LightStateData data)
        {
            CollisionLightStateAttribute collisionData = data.CollisionData;

            if (collisionData.UsingLightCollisions)
            {
                foreach (var colliderEffector in _lightInstance.ColliderTransitionEffectors)
                {
                    if (collisionData.DetectingCollisions)
                    {
                        colliderEffector.Enable();
                        colliderEffector.SetDelayTimeSeconds(collisionData.InSpotlightDelayTimeSeconds);
                    }
                    else
                    {
                        colliderEffector.Disable();
                    }
                }
                _lightInstance.FinishCollisionLightTransitionEffector.SetWaitTime(collisionData.CollisionWaitTimeSeconds);
                _lightInstance.FinishCollisionLightTransitionEffector.BeginWait();
            }
        }
    }
}
