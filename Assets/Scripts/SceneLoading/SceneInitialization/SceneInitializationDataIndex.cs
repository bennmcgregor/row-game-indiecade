using System;
using System.Collections.Generic;
using Zenject;

namespace IndieCade
{
    // initialized once at game startup
    public class SceneInitializationDataIndex
    {
        private Dictionary<GameSceneName, SceneInitializationData> _index;
        private GameSceneState _gameSceneState;

        [Inject]
        public SceneInitializationDataIndex(GameSceneState gameSceneState)
        {
            _gameSceneState = gameSceneState;
            _index = InitialSceneData.GetInitialTeaserSceneData();
        }

        public SceneInitializationData GetDataFromCurrentScene()
        {
            return GetData(_gameSceneState.CurrentScene);
        }

        public SceneInitializationData GetData(GameSceneName gameSceneName)
        {
            return _index[gameSceneName];
        }

        public SceneInitializationData GetData(string gameSceneName)
        {
            return _index[GameSceneNameMap.GetNameEnum(gameSceneName)];
        }
    }
}
