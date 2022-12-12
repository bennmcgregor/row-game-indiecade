using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace IndieCade
{
    public class BeamShapeLightAttributeProcessor : ShapeLightAttributeProcessor
    {
        [SerializeField] private Light2D _beamLight;
        [SerializeField] private PolygonCollider2D _beamCollider;

        public override void SetProperties(ShapeLightStateAttribute data)
        {
            BeamShapeLightStateAttribute beamData = (BeamShapeLightStateAttribute)data;
            _beamLight.pointLightInnerRadius = beamData.InnerRadius;
            _beamLight.pointLightOuterRadius = beamData.OuterRadius;
            _beamLight.pointLightInnerAngle = beamData.InnerArcAngle;
            _beamLight.pointLightOuterAngle = beamData.OuterArcAngle;

            // TODO: make collider fit the innerradius and innerarcangle
            List<Vector2> path = new List<Vector2>();
            float radiusLength = beamData.InnerRadius + (beamData.OuterRadius - beamData.InnerRadius) / 2;
            float x = Math.Abs((float)(radiusLength * Math.Sin(beamData.InnerArcAngle / 2)));
            float y = Math.Abs((float)(radiusLength * Math.Cos(beamData.InnerArcAngle / 2)));

            path.Add(Vector2.zero);
            path.Add(new Vector2(x, y));
            path.Add(new Vector2(-x, y));
            _beamCollider.SetPath(0, path);
        }
    }
}
