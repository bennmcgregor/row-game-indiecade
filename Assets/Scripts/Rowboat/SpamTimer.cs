using System;
using System.Collections;
using UnityEngine;

namespace IndieCade
{
    public class SpamTimer : MonoBehaviour
    {
        private bool _canPress = true;
        private float _timeoutSeconds;

        public void SetTimeout(float timeoutSeconds)
        {
            _timeoutSeconds = timeoutSeconds;
        }

        public bool OnKeyPress()
        {
            if (_canPress)
            {
                StartCoroutine(Timeout());
                return true;
            }

            return false;
        }

        private IEnumerator Timeout()
        {
            _canPress = false;
            yield return new WaitForSeconds(_timeoutSeconds);
            _canPress = true;
        }
    }
}
