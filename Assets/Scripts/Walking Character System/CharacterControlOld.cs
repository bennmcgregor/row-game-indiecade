using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieCade
{
    public class CharacterControlOld : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _movementThreshold = .05f;
        [SerializeField] private float _colliderOverlapThreshold = .2f;
        [SerializeField] private Transform _movePoint;
        [SerializeField] private LayerMask _colliderMask;
        [SerializeField] private Animator _animator;

        private CharacterWalkingStateMachine _stateMachine;

        private void Awake()
        {
            _movePoint.parent = null;
            _stateMachine = new CharacterWalkingStateMachine(CharacterWalkingState.STOPPED);
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
            playerSpawnPoint.SetPlayerPositionAndRotation(_movePoint.transform);
            playerSpawnPoint.SetPlayerPositionAndRotation(transform);
        }

        private void Update()
        {
            MoveToNewPosition();

            switch (_stateMachine.CurrentState)
            {
                case CharacterWalkingState.RIGHT:
                    UpdateMovePoint(new Vector3(0.01f, 0f, 0f));
                    _animator.SetInteger("CharacterWalkingState", (int)CharacterWalkingState.RIGHT);
                    break;
                case CharacterWalkingState.LEFT:
                    UpdateMovePoint(new Vector3(-0.01f, 0f, 0f));
                    _animator.SetInteger("CharacterWalkingState", (int)CharacterWalkingState.LEFT);
                    break;
                case CharacterWalkingState.UP:
                    UpdateMovePoint(new Vector3(0f, 0.01f, 0f));
                    _animator.SetInteger("CharacterWalkingState", (int)CharacterWalkingState.UP);
                    break;
                case CharacterWalkingState.DOWN:
                    UpdateMovePoint(new Vector3(0f, -0.01f, 0f));
                    _animator.SetInteger("CharacterWalkingState", (int)CharacterWalkingState.DOWN);
                    break;
                default:
                    _animator.SetInteger("CharacterWalkingState", (int)CharacterWalkingState.STOPPED);
                    break;
            }
        }

        private void MoveToNewPosition()
        {
            Vector3 newPos = Vector3.MoveTowards(transform.position, _movePoint.position, _moveSpeed * Time.deltaTime);
            _animator.SetBool("IsMoving", Vector3.Distance(transform.position, _movePoint.position) > _movementThreshold);
            transform.position = newPos;
        }

        private void UpdateMovePoint(Vector3 update)
        {
            if (Vector3.Distance(transform.position, _movePoint.position) <= _movementThreshold)
            {
                Vector3 newPos = _movePoint.position + update;
                if (!Physics2D.OverlapCircle(newPos, _colliderOverlapThreshold, _colliderMask))
                {
                    _movePoint.position = newPos;
                }
            }
        }
    }
}