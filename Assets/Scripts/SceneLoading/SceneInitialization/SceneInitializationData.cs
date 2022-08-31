using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieCade
{
    public class SceneInitializationData
    {
        public SceneInitializationData(GameSceneName sceneName)
        {
            SceneName = sceneName;
        }

        public GameSceneName SceneName;

        public PlayerSpawnPoint PlayerSpawnPoint;
    }
}
