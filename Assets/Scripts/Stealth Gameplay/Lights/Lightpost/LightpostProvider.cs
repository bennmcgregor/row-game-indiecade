using System;
using System.Collections.Generic;
using UnityEngine;

namespace IndieCade
{
    public class LightpostProvider : MonoBehaviour
    {
        [SerializeField] private List<string> _lightpostIds;
        [SerializeField] private List<Lightpost> _lightposts;

        public Lightpost GetLightpostById(string id)
        {
            int i = 0;
            for (; i < _lightpostIds.Count; i++)
            {
                if (_lightpostIds[i] == id)
                {
                    break;
                }
            }

            return _lightposts[i];
        }
    }
}
