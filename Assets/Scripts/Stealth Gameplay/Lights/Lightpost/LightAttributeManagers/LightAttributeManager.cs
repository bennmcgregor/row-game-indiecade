using System;
using UnityEngine;

namespace IndieCade
{
    public abstract class LightAttributeManager : MonoBehaviour
    {
        protected LightInstance _lightInstance;

        public void InitializeWithData(LightStateData data, LightInstance lightInstance)
        {
            _lightInstance = lightInstance;
            InitializeWithDataInternal(data);
        }

        // this happens multiple times! We only want it to happen once. TODO: make this a pipeline
        public void UpdateState(LightStateData data)
        {
            if (data.On)
            {
                foreach (var light in _lightInstance.Lights)
                {
                    light.gameObject.SetActive(true);
                }
                _lightInstance.gameObject.SetActive(true);
                UpdateStateInternal(data);
            }
            else
            {
                // TODO: may need some preparation logic for disabling?
                foreach (var light in _lightInstance.Lights)
                {
                    light.gameObject.SetActive(false);
                }
                _lightInstance.gameObject.SetActive(false);
            }
        }

        protected abstract void InitializeWithDataInternal(LightStateData data);
        protected abstract void UpdateStateInternal(LightStateData data);
    }
}
