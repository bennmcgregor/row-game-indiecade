using System;
using System.Collections;
using UnityEngine;

namespace IndieCade
{
    public class FinishCollisionLightTransitionEffector : MonoBehaviour, ILightTransitionEffector
    {
        private float _waitTimeSeconds;
        private Coroutine _waitCoroutine;

        public Action OnTransition { get; set; }

        public void SetWaitTime(float waitTime)
        {
            _waitTimeSeconds = waitTime;
        }

        public void BeginWait()
        {
            if (_waitCoroutine != null)
            {
                StopCoroutine(_waitCoroutine);
            }
            _waitCoroutine = StartCoroutine(WaitCoroutine());
        }

        private IEnumerator WaitCoroutine()
        {
            yield return new WaitForSeconds(_waitTimeSeconds);
            OnTransition?.Invoke();
        }
    }
}
