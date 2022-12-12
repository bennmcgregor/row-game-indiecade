using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace IndieCade
{
    public class CircularShapeLightAttributeProcessor : ShapeLightAttributeProcessor
    {
        [SerializeField] private Light2D _circleLight;
        [SerializeField] private Light2D _beamLight;
        [SerializeField] private CapsuleCollider2D _capsuleCollider;

        public override void SetProperties(ShapeLightStateAttribute data)
        {
            CircularShapeLightStateAttribute circularData = (CircularShapeLightStateAttribute)data;

            float yOffsetValue = _capsuleCollider.transform.localPosition.y;
            Vector2 capsuleSizeNormalized = _capsuleCollider.size / _circleLight.pointLightInnerRadius;
            
            List<Vector3> shapePathNormalized = new List<Vector3>();
            for (int i = 1; i < _beamLight.shapePath.Length; i++)
            {
                Vector3 offsetVec = new Vector3(_beamLight.shapePath[i].x, _beamLight.shapePath[i].y - yOffsetValue, _beamLight.shapePath[i].z);
                Vector3 scaledVec = offsetVec / (_circleLight.pointLightInnerRadius);
                shapePathNormalized.Add(scaledVec);
            }

            _circleLight.pointLightInnerRadius = circularData.InnerRadius;
            _circleLight.pointLightOuterRadius = circularData.InnerRadius + 1;

            for (int i = 0; i < shapePathNormalized.Count; i++)
            {
                Vector3 scaledVec = shapePathNormalized[i] * (circularData.InnerRadius);
                _beamLight.shapePath[i+1] = new Vector3(scaledVec.x, scaledVec.y - circularData.BeamHeight, scaledVec.z);
            }

            _capsuleCollider.transform.localPosition = new Vector3(_capsuleCollider.transform.localPosition.x, -circularData.BeamHeight, _capsuleCollider.transform.localPosition.z);
            _capsuleCollider.size = capsuleSizeNormalized * circularData.InnerRadius;
        }
    }
}
