using System;
using UnityEngine;

namespace IndieCade
{
    public abstract class LightMotionController : MonoBehaviour, IPausable
    {
        public abstract void Pause();
        public abstract void Resume();
    }
}
