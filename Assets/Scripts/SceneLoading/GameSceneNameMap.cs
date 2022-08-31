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
                GameSceneName.BOATHOUSE_INSIDE => "BoathouseInside",
                GameSceneName.MIO_HOUSE_INSIDE => "MioHouseInside",
                GameSceneName.TMR_TOWN_OUTSIDE => "TMRTownOutside",
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
