using System;
using System.Collections.Generic;
using UnityEngine;

namespace IndieCade
{
    public class LightInstanceStore : MonoBehaviour
    {
        private Dictionary<string, LightInstance> _idInstanceMap;

        public List<string> AttachedLightIds => new List<string>(_idInstanceMap.Keys);

        private void Awake()
        {
            _idInstanceMap = new Dictionary<string, LightInstance>();
        }

        public void AttachLightInstance(string id, LightInstance instance)
        {
            _idInstanceMap[id] = instance;
        }

        public void DetachLightInstance(string id)
        {
            Destroy(_idInstanceMap[id]);
        }

        public LightInstance GetLightInstance(string id)
        {
            return _idInstanceMap[id];
        }
    }
}
