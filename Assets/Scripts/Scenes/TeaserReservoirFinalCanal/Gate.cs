using System;
using System.Collections;
using UnityEngine;

namespace IndieCade
{
    public class Gate : MonoBehaviour
    {
        public Action OnOpenGate;
        public Action OnCloseGate;

        [SerializeField] private float _openGateDisplacement = -6f;
        [SerializeField] private float _moveSpeed = 0.1f;
        [SerializeField] private GameObject _gateObj;

        private Coroutine _openGateCoroutine;
        private Coroutine _closeGateCoroutine;

        private void Awake()
        {
            _gateObj.transform.SetPositionAndRotation(gameObject.transform.position, gameObject.transform.rotation);
        }

        public void Open()
        {
            if (_closeGateCoroutine != null)
            {
                StopCoroutine(_closeGateCoroutine);
            }
            _openGateCoroutine = StartCoroutine(OpenGateCoroutine());
            OnOpenGate?.Invoke();
        }

        private IEnumerator OpenGateCoroutine()
        {
            while (Mathf.Abs(_gateObj.transform.localPosition.y) < Mathf.Abs(_openGateDisplacement))
            {
                _gateObj.transform.position = Vector2.MoveTowards(
                    _gateObj.transform.position,
                    new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + _openGateDisplacement),
                    _moveSpeed
                );
                yield return new WaitForFixedUpdate();
            }
        }

        public void Close()
        {
            if (_openGateCoroutine != null)
            {
                StopCoroutine(_openGateCoroutine);
            }
            _closeGateCoroutine = StartCoroutine(CloseGateCoroutine());
            OnCloseGate?.Invoke();
        }

        private IEnumerator CloseGateCoroutine()
        {
            while (Mathf.Abs(_gateObj.transform.localPosition.y) > 0)
            {
                _gateObj.transform.position = Vector2.MoveTowards(
                    _gateObj.transform.position,
                    gameObject.transform.position,
                    _moveSpeed
                );
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
