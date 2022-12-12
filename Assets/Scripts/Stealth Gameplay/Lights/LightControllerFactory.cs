using System;
namespace IndieCade
{
    public class LightControllerFactory : IFactory<LightController>
    {
        private LightTransitionController _lightTransitionController;
        private LightStateMachine _lightStateMachine;

        public void RegisterTransitionController(LightTransitionController lightTransitionController)
        {
            _lightTransitionController = lightTransitionController;
        }

        public void RegisterLightStateMachine(LightStateMachine lightStateMachine)
        {
            _lightStateMachine = lightStateMachine;
        }

        public LightController Make()
        {
            return new LightController(_lightTransitionController, _lightStateMachine);
        }
    }
}
