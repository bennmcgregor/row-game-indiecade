using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace IndieCade
{
    public class PlayerSceneInitializer : GameInitializationStateListener
    {
        [SerializeField] private CharacterControl _characterControl;

        protected override void InitializeScene(SceneInitializationData sceneInitializationData)
        {
            PlayerSpawnPoint playerSpawnPoint = sceneInitializationData.PlayerSpawnPoint;
            if (playerSpawnPoint != null)
            {
                _characterControl.SpawnAtPosition(playerSpawnPoint);
            }
        }
    }
}
