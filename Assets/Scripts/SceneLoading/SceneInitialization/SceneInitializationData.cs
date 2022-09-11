using System;
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

        public Nullable<Vector3> PlayerSpawnPosition = null;
        public Nullable<Quaternion> PlayerSpawnRotation = null;

        public string BackgroundMusicFilename;
    }
}
