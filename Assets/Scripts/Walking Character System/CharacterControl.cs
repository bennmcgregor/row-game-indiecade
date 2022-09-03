using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieCade
{
    public class CharacterControl : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _moveSpeedThreshold = 0.05f;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Animator _animator;

        private CharacterWalkingStateMachine _stateMachine;
        private Vector2 _trackVelocity;
        private Vector2 _lastPos;

        private void Awake()
        {
            _stateMachine = new CharacterWalkingStateMachine(CharacterWalkingState.STOPPED);
            _lastPos = _rigidbody2D.position;
        }

        public void MoveRight()
        {
            _stateMachine.RightAction();
        }

        public void MoveLeft()
        {
            _stateMachine.LeftAction();
        }

        public void MoveUp()
        {
            _stateMachine.UpAction();
        }

        public void MoveDown()
        {
            _stateMachine.DownAction();
        }

        public void SpawnAtPosition(PlayerSpawnPoint playerSpawnPoint)
        {
            playerSpawnPoint.SetPlayerPositionAndRotation(transform);
        }

        private void FixedUpdate()
        {
            Vector3 newPos = transform.position;
            switch (_stateMachine.CurrentState)
            {
                case CharacterWalkingState.RIGHT:
                    newPos += new Vector3(0.01f, 0f, 0f) * _moveSpeed;
                    break;
                case CharacterWalkingState.LEFT:
                    newPos += new Vector3(-0.01f, 0f, 0f) * _moveSpeed;
                    break;
                case CharacterWalkingState.UP:
                    newPos += new Vector3(0f, 0.01f, 0f) * _moveSpeed;
                    break;
                case CharacterWalkingState.DOWN:
                    newPos += new Vector3(0f, -0.01f, 0f) * _moveSpeed;
                    break;
                default:
                    break;
            }

            _trackVelocity = (_rigidbody2D.position - _lastPos) / Time.fixedDeltaTime;
            _lastPos = _rigidbody2D.position;

            _animator.SetFloat("CharacterXVelocity", _trackVelocity.x);
            _animator.SetFloat("CharacterYVelocity", _trackVelocity.y);
            if (Mathf.Abs(_trackVelocity.x) < _moveSpeedThreshold)
            {
                _animator.SetBool("IsMovingX", false);
            } else
            {
                _animator.SetBool("IsMovingX", true);
            }

            if (Mathf.Abs(_trackVelocity.y) < _moveSpeedThreshold)
            {
                _animator.SetBool("IsMovingY", false);
            }
            else
            {
                _animator.SetBool("IsMovingY", true);
            }

            _rigidbody2D.MovePosition(newPos);
        }
    }
}