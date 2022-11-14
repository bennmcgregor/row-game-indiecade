using System;
using System.Collections.Generic;

namespace IndieCade
{
    public static class InitialSceneData
    {
        public static Dictionary<GameSceneName, SceneInitializationData> GetInitialTeaserSceneData()
        {
            Dictionary<GameSceneName, SceneInitializationData> data = new Dictionary<GameSceneName, SceneInitializationData>();

            SceneInitializationData mainMenuData = new SceneInitializationData(GameSceneName.MAIN_MENU);
            mainMenuData.BackgroundMusicFilename = "quiet-rain";

            SceneInitializationData reservoirFinalCanalData = new SceneInitializationData(GameSceneName.RESERVOIR_FINAL_CANAL);
            reservoirFinalCanalData.BackgroundMusicFilename = "westlake-night";

            SceneInitializationData interrogationRoomData = new SceneInitializationData(GameSceneName.INTERROGATION_ROOM);
            interrogationRoomData.BackgroundMusicFilename = "westlake-interrogation";

            SceneInitializationData teaserCutsceneData = new SceneInitializationData(GameSceneName.TEASER_CUTSCENE);
            teaserCutsceneData.BackgroundMusicFilename = "silence";

            SceneInitializationData downstreamSettlementData = new SceneInitializationData(GameSceneName.DOWNSTREAM_SETTLEMENT);
            downstreamSettlementData.BackgroundMusicFilename = "downstream-settlement-day";

            List<SceneInitializationData> sceneData = new List<SceneInitializationData>
            {
                mainMenuData,
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
