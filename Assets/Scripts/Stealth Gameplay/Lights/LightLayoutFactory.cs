using System;
using System.Collections.Generic;

namespace IndieCade
{
    public class LightLayoutFactory : IFactory<LightLayout>
    {
        private Dictionary<string, List<LightController>> _lightpostLightMap;

        public LightLayoutFactory()
        {
            _lightpostLightMap = new Dictionary<string, List<LightController>>();
        }

        public void RegisterLightToLightpost(string lightpostId, LightController controller)
        {
            if (!_lightpostLightMap.ContainsKey(lightpostId))
            {
                _lightpostLightMap[lightpostId] = new List<LightController>();
            }
            _lightpostLightMap[lightpostId].Add(controller);
        }

        public LightLayout Make()
        {
            return new LightLayout(_lightpostLightMap);
        }
    }
}
