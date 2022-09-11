using System;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class PlayerInitializationStateListener : GameInitializationStateListener
    {
        private GameObject _playerGameObject;

        [Inject]
        public void Initialize(GameObject player)
        {
            _playerGameObject = player;
        }

        protected override void InitializeScene(SceneInitializationData sceneInitializationData)
        {
            if (sceneInitializationData.PlayerSpawnPosition != null && sceneInitializationData.PlayerSpawnRotation != null)
            {
                Vector3 playerSpawnPosition = (Vector3) sceneInitializationData.PlayerSpawnPosition;
                Quaternion playerSpawnRotation = (Quaternion) sceneInitializationData.PlayerSpawnRotation;
                _playerGameObject.transform.SetPositionAndRotation(playerSpawnPosition, playerSpawnRotation);
            }
        }

        protected override void SaveScene(SceneInitializationData sceneInitializationData)
        {
            sceneInitializationData.PlayerSpawnPosition = _playerGameObject.transform.position;
            sceneInitializationData.PlayerSpawnRotation = _playerGameObject.transform.rotation;
        }
    }
}
