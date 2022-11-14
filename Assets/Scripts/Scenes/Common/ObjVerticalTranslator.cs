using System;
using System.Collections;
using UnityEngine;

namespace IndieCade
{
    public class ObjVerticalTranslator : MonoBehaviour
    {
        public Action OnStartForwardTranslation;
        public Action OnEndForwardTranslation;
        public Action OnStartReverseTranslation;
        public Action OnEndReverseTranslation;

        [SerializeField] private float _forwardsDisplacement = -6f;
        [SerializeField] private float _moveSpeed = 0.1f;
        [SerializeField] private GameObject _translatedObj;

        private Coroutine _openGateCoroutine;
        private Coroutine _closeGateCoroutine;

        private void Awake()
        {
            _translatedObj.transform.SetPositionAndRotation(gameObject.transform.position, gameObject.transform.rotation);
        }

        public void TranslateForwards()
        {
            if (_closeGateCoroutine != null)
            {
                StopCoroutine(_closeGateCoroutine);
            }
            _openGateCoroutine = StartCoroutine(TranslateForwardsCoroutine());
            OnStartForwardTranslation?.Invoke();
        }

        private IEnumerator TranslateForwardsCoroutine()
        {
            while (Mathf.Abs(_translatedObj.transform.localPosition.y) < Mathf.Abs(_forwardsDisplacement))
            {
                _translatedObj.transform.position = Vector2.MoveTowards(
                    _translatedObj.transform.position,
                    new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + _forwardsDisplacement),
                    _moveSpeed
                );
                yield return new WaitForFixedUpdate();
            }
            OnEndForwardTranslation?.Invoke();
        }

        public void TranslateReverse()
        {
            if (_openGateCoroutine != null)
            {
                StopCoroutine(_openGateCoroutine);
            }
            _closeGateCoroutine = StartCoroutine(TranslateReverseCoroutine());
            OnStartReverseTranslation?.Invoke();
        }

        private IEnumerator TranslateReverseCoroutine()
        {
            while (Mathf.Abs(_translatedObj.transform.localPosition.y) > 0)
            {
                _translatedObj.transform.position = Vector2.MoveTowards(
                    _translatedObj.transform.position,
                    gameObject.transform.position,
                    _moveSpeed
                );
                yield return new WaitForFixedUpdate();
            }
            OnEndReverseTranslation?.Invoke();
        }
    }
}
