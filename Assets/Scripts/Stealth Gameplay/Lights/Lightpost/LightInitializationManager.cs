using System;
using UnityEngine;

namespace IndieCade
{
    public class LightInitializationManager : MonoBehaviour
    {
        [SerializeField] private LightInstance _beamShapePrefab;
        [SerializeField] private LightInstance _circularShapePrefab;
        [SerializeField] private LightInstance _noneShapePrefab;

        public LightInstance InitializeLight(InitializationLightStateData initializationData)
        {
            switch (initializationData.Shape.Shape)
            {
                // TODO: customize size
                case LightShape.BEAM:
                    return Instantiate(_beamShapePrefab, transform);
                case LightShape.CIRCULAR:
                    return Instantiate(_circularShapePrefab, transform);
                default:
                    return Instantiate(_noneShapePrefab, transform);
            }
        }
    }
}
