using System;
namespace IndieCade
{
    public class LightInitializer
    {
        private LightController _lightController;
        private Lightpost _lightpost;

        public LightInitializer(LightController lightController, Lightpost lightpost)
        {
            _lightController = lightController;
            _lightpost = lightpost;
        }

        public void RunInitializationSequence(ILightTransitionEffector entryEffector)
        {
            _lightController.LightTransitionController.PublishToStateMachine(_lightController.LightStateMachine);
            LightInstance lightInstance = _lightpost.Attach(_lightController.LightStateMachine);

            foreach (var effector in lightInstance.ColliderTransitionEffectors)
            {
                _lightController.LightTransitionController.SubscribeToTransitionEffector(LightStateMachineTransition.COLLIDE, effector);
            }
            _lightController.LightTransitionController.SubscribeToTransitionEffector(LightStateMachineTransition.FINISH_COLLIDE, lightInstance.FinishCollisionLightTransitionEffector);
            _lightController.LightTransitionController.SubscribeToTransitionEffector(LightStateMachineTransition.ENTRY, entryEffector);
        }
    }
}
