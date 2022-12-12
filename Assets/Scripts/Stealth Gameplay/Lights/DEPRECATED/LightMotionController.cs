using System;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public abstract class LightMotionController : MonoBehaviour, IPausable
    {
        public abstract void Pause();
        public abstract void Resume();
        public abstract void SetChase(bool isChasing);
    }
}
