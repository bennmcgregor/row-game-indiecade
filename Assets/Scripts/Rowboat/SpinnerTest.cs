using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerTest : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _maxRotationDistance = 60f;
    [SerializeField] private float _bestRotation = 180f;
    [SerializeField] private float _maxTorque = 5f;

    private Transform _spinnerTransform;
    private bool _hasSpacebarPress = false;

    private void Awake()
    {
        _spinnerTransform = _rigidbody2D.gameObject.transform;
    }

    public void OnSpacebarTemp()
    {
        _hasSpacebarPress = true;
    }

    private void FixedUpdate()
    {
        //Debug.Log(Vector3.Dot(_spinnerTransform.up, Vector3.up));
        if (_hasSpacebarPress)
        {
            float dot = Vector3.Dot(_spinnerTransform.up, Vector3.up);
            if (dot < -.5f)
            {
                float torqueScalingFactor = Mathf.Abs(dot); // scale between -0.5 (worst) and -1 (best)
                _rigidbody2D.AddTorque(torqueScalingFactor * _maxTorque);
            }
            _hasSpacebarPress = false;
        }
    }
}
