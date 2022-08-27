using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class WorldMapSceneChangeEffector : MonoBehaviour
    {
        [SerializeField] private string _sceneName;

        private WorldMapSceneLoader _sceneLoader;

        [Inject]
        public void Initialize(WorldMapSceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        protected void ActivateScene()
        {
            _sceneLoader.ActivateScene(_sceneName);
        }
    }
}