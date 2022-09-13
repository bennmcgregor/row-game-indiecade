using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class ButtonSceneChangeEffector : MonoBehaviour
    {
        [SerializeField] private GameSceneName _sceneName;

        private WorldMapSceneChangeEffector _sceneChangeEffector;

        [Inject]
        public void Initialize(WorldMapSceneChangeEffector worldMapSceneChangeEffector)
        {
            _sceneChangeEffector = worldMapSceneChangeEffector;
        }

        public void OnClick()
        {
            _sceneChangeEffector.ActivateScene(_sceneName);
        }
    }
}