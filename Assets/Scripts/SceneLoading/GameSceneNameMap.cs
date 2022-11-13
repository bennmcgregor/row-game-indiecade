using System;
using System.Collections.Generic;
using UnityEngine;

namespace IndieCade
{
    public class GameSceneNameMap
    {
        static public string GetNameString(GameSceneName sceneName)
        {
            return sceneName switch
            {
                // TODO: uncomment when implementing full version
                //GameSceneName.BOATHOUSE_INSIDE => "BoathouseInside",
                //GameSceneName.MIO_HOUSE_INSIDE => "MioHouseInside",
                //GameSceneName.TMR_TOWN_OUTSIDE => "TMRTownOutside",
                GameSceneName.MAIN_MENU => "TeaserMainMenuScene",
                GameSceneName.RESERVOIR_FINAL_CANAL => "ReservoirFinalCanal",
                GameSceneName.INTERROGATION_ROOM => "InterrogationRoom",
                GameSceneName.TEASER_CUTSCENE => "TeaserCutscene",
                GameSceneName.DOWNSTREAM_SETTLEMENT => "DownstreamSettlement",
                _ => throw new NotImplementedException()
            };
        }

        static public GameSceneName GetNameEnum(string sceneName)
        {
            foreach (GameSceneName enumName in Enum.GetValues(typeof(GameSceneName)))
            {
                if (GetNameString(enumName) == sceneName)
                {
                    return enumName;
                }
            }

            throw new KeyNotFoundException();
        }
    }
}
