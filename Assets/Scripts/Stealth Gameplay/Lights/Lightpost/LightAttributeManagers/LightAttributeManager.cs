using System;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public abstract class LightAttributeManager : MonoBehaviour
    {
        [SerializeField] protected LightToggler _lightToggler;
        [SerializeField] private LightInstanceStore _lightInstanceStore;

        protected LightInstance _lightInstance;

        public void InitializeWithData(LightStateData data)
        {
            _lightInstance = _lightInstanceStore.GetLightInstance(data.Id);
            InitializeWithDataInternal(data);
        }

        // this happens multiple times! We only want it to happen once. TODO: make this a pipeline
        public void UpdateState(LightStateData data)
        {
            if (data.On)
            {
                _lightToggler.ToggleAllLights(true);
                _lightInstance.gameObject.SetActive(true);
                UpdateStateInternal(data);
            }
            else
            {
                // TODO: may need some preparation logic for disabling?
                _lightToggler.ToggleAllLights(true);
                _lightInstance.gameObject.SetActive(false);
            }
        }

        protected virtual void InitializeWithDataInternal(LightStateData data) { }
        protected abstract void UpdateStateInternal(LightStateData data);
    }
}
