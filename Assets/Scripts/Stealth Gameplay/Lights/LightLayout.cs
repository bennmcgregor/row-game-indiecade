using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace IndieCade
{
    public class LightLayout
    {
        private Dictionary<string, List<LightController>> _lightpostLightMap;

        public List<string> LightpostIds => _lightpostLightMap.Keys.ToList();

        public LightLayout(Dictionary<string, List<LightController>> lightpostLightMap)
        {
            _lightpostLightMap = lightpostLightMap;
        }

        public List<LightController> GetLightsOnLightpost(string lightpostId)
        {
            return _lightpostLightMap[lightpostId];
        }
    }
}
