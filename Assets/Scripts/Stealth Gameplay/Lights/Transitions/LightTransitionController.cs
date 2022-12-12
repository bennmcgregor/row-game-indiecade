using System;

namespace IndieCade
{
    public class LightTransitionController
    {
        private Action OnEntry;
        private Action OnEnable;
        private Action OnDisable;
        private Action OnCollide;
        private Action OnFinishCollide;

        public void PublishToStateMachine(LightStateMachine lightStateMachine)
        {
            OnEntry += () => lightStateMachine.Transition(LightStateMachineTransition.ENTRY);
            OnEnable += () => lightStateMachine.Transition(LightStateMachineTransition.ENABLE);
            OnDisable += () => lightStateMachine.Transition(LightStateMachineTransition.DISABLE);
            OnCollide += () => lightStateMachine.Transition(LightStateMachineTransition.COLLIDE);
            OnFinishCollide += () => lightStateMachine.Transition(LightStateMachineTransition.FINISH_COLLIDE);
        }

        public void SubscribeToTransitionEffector(LightStateMachineTransition transition, ILightTransitionEffector transitionEffector)
        {
            switch(transition)
            {
                case LightStateMachineTransition.ENTRY:
                    transitionEffector.OnTransition += () => OnEntry?.Invoke();
                    break;
                case LightStateMachineTransition.ENABLE:
                    transitionEffector.OnTransition += () => OnEnable?.Invoke();
                    break;
                case LightStateMachineTransition.DISABLE:
                    transitionEffector.OnTransition += () => OnDisable?.Invoke();
                    break;
                case LightStateMachineTransition.COLLIDE:
                    transitionEffector.OnTransition += () => OnCollide?.Invoke();
                    break;
                case LightStateMachineTransition.FINISH_COLLIDE:
                    transitionEffector.OnTransition += () => OnFinishCollide?.Invoke();
                    break;
            }
        }
    }
}
