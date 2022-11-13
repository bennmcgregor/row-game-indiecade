using System;
using UnityEngine;

namespace IndieCade
{
    public class ToggleableAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public void Play()
        {
            _animator.SetBool("On", true);
        }

        public void Stop()
        {
            _animator.SetBool("On", false);
        }
    }
}
