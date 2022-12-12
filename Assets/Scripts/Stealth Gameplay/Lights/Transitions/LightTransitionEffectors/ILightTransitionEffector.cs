using System;

namespace IndieCade
{
    public interface ILightTransitionEffector
    {
        public Action OnTransition { get; set; }
    }
}
