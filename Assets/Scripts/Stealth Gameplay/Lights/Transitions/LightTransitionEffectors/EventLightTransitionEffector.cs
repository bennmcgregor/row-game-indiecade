using System;
namespace IndieCade
{
    public class EventLightTransitionEffector : ILightTransitionEffector
    {
        public Action OnTransition { get; set; }
    }
}
