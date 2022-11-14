using System;
using UnityEngine;

namespace IndieCade
{
    public class OneShotAnimation : MonoBehaviour
    {
        public Action OnAnimationEnd;

        [SerializeField] private Animator _animator;

        public void Play()
        {
            _animator.SetBool("On", true);
        }

        public void OnEnd()
        {
            OnAnimationEnd?.Invoke();
        }
    }
}
