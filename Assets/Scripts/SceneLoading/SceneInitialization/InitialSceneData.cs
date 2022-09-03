using System;
using System.Collections.Generic;

namespace IndieCade
{
    public static class InitialSceneData
    {
        public static Dictionary<GameSceneName, SceneInitializationData> GetInitialTeaserSceneData()
        {
            Dictionary<GameSceneName, SceneInitializationData> data = new Dictionary<GameSceneName, SceneInitializationData>();

            // TODO: change to appropriate soundtrack
            SceneInitializationData reservoirFinalCanalData = new SceneInitializationData(GameSceneName.RESERVOIR_FINAL_CANAL);
            reservoirFinalCanalData.BackgroundMusicFilename = "gameplay_loop_music_2";

            SceneInitializationData interrogationRoomData = new SceneInitializationData(GameSceneName.INTERROGATION_ROOM);
            interrogationRoomData.BackgroundMusicFilename = "gameplay_loop_music_2";

            SceneInitializationData teaserCutsceneData = new SceneInitializationData(GameSceneName.TEASER_CUTSCENE);
            teaserCutsceneData.BackgroundMusicFilename = "gameplay_loop_music_2";

            SceneInitializationData downstreamSettlementData = new SceneInitializationData(GameSceneName.DOWNSTREAM_SETTLEMENT);
            downstreamSettlementData.BackgroundMusicFilename = "gameplay_loop_music_2";

            List<SceneInitializationData> sceneData = new List<SceneInitializationData>
            {
                reservoirFinalCanalData,
                interrogationRoomData,
                teaserCutsceneData,
                downstreamSettlementData
            };

            foreach (var scene in sceneData)
            {
                data[scene.SceneName] = scene;
            }

            return data;
        }
    }
}
