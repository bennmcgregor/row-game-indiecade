using System;
namespace IndieCade
{
    public class LightController
    {
        private LightTransitionController _lightTransitionController;
        private LightStateMachine _lightStateMachine;

        public LightTransitionController LightTransitionController => _lightTransitionController;
        public LightStateMachine LightStateMachine => _lightStateMachine;
        public string Id => _lightStateMachine.Id;

        public LightController(LightTransitionController lightTransitionController, LightStateMachine lightStateMachine)
        {
            _lightTransitionController = lightTransitionController;
            _lightStateMachine = lightStateMachine;
        }
    }
}
