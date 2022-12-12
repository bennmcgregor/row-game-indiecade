using System;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class LightSystem : MonoBehaviour, ILightTransitionEffector
    {
        // listens to the game/challenge state and initializes a LightLayout on new challenge or scene change
        private LightLayout _currentLightLayout;
        private LightpostProvider _lightpostProvider;

        public Action OnTransition { get; set; }

        [Inject]
        public void Initialize(LightpostProvider lightpostProvider)
        {
            _lightpostProvider = lightpostProvider;
        }

        public void SetLightLayout(LightLayout lightLayout)
        {
            _currentLightLayout = lightLayout;
        }

        public void InitializeLights()
        {
            foreach (var lightpostId in _currentLightLayout.LightpostIds)
            {
                Lightpost lightpost = _lightpostProvider.GetLightpostById(lightpostId);
                foreach (var lightController in _currentLightLayout.GetLightsOnLightpost(lightpostId))
                {
                    InitializeLight(lightpost, lightController);
                }
            }
        }

        public void InitializeLight(Lightpost lightpost, LightController lightController)
        {
            LightInitializer lightInitializer = new LightInitializer(lightController, lightpost);
            lightInitializer.RunInitializationSequence(this);
        }

        public void ReplaceAllLightsWithLight(string lightpostId, LightController lightController)
        {
            ReplaceAllLightsWithLight(_lightpostProvider.GetLightpostById(lightpostId), lightController);
        }

        public void ReplaceAllLightsWithLight(Lightpost lightpost, LightController lightController)
        {
            lightpost.DetachAllLights();
            InitializeLight(lightpost, lightController);
        }

        public void Entry()
        {
            OnTransition?.Invoke();
        }
    }
}
