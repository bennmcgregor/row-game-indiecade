using System;
namespace IndieCade
{
    public class LightTransitionControllerFactory : IFactory<LightTransitionController>
    {
        private LightTransitionController _controller;

        public LightTransitionControllerFactory()
        {
            _controller = new LightTransitionController();
        }

        public void RegisterEnableTransition(ILightTransitionEffector lightTransitionEffector)
        {
            _controller.SubscribeToTransitionEffector(LightStateMachineTransition.ENABLE, lightTransitionEffector);
        }

        public void RegisterDisableTransition(ILightTransitionEffector lightTransitionEffector)
        {
            _controller.SubscribeToTransitionEffector(LightStateMachineTransition.DISABLE, lightTransitionEffector);
        }

        public void RegisterCaughtTransition(ILightTransitionEffector lightTransitionEffector)
        {
            _controller.SubscribeToTransitionEffector(LightStateMachineTransition.COLLIDE, lightTransitionEffector);
        }

        public void RegisterFinishCaughtTransition(ILightTransitionEffector lightTransitionEffector)
        {
            _controller.SubscribeToTransitionEffector(LightStateMachineTransition.FINISH_COLLIDE, lightTransitionEffector);
        }

        public LightTransitionController Make()
        {
            return _controller;
        }
    }
}
