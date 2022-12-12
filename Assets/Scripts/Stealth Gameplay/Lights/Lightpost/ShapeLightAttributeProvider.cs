using System;
using UnityEngine;

namespace IndieCade
{
    public class ShapeLightAttributeProvider : MonoBehaviour
    {
        [SerializeField] private LightInstance _beamShapePrefab;
        [SerializeField] private LightInstance _circularShapePrefab;
        [SerializeField] private LightInstance _noneShapePrefab;

        private LightStateData _lightStateData;

        public void UpdateLightStateData(LightStateData data)
        {
            _lightStateData = data;
        }

        public GameObject GetShapePrefab()
        {
            switch (_lightStateData.Shape.Shape)
            {
                case LightShape.BEAM:
                    return _beamShapePrefab.gameObject;
                case LightShape.CIRCULAR:
                    return _circularShapePrefab.gameObject;
                default:
                    return _noneShapePrefab.gameObject;
            }
        }
    }
}
