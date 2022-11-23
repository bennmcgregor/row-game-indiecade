using System;

namespace IndieCade
{
    public interface IStateMachineTransition<TTransitionEnum> where TTransitionEnum : Enum
    {
        public void Transition(TTransitionEnum transition);
    }
}