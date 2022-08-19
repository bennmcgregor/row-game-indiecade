using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieCade
{
    public class PlayerInputManager : MonoBehaviour
    {
        [SerializeField] private CharacterControl _characterControl;
        [SerializeField] private RowingControl _rowingControl;

        public void OnMovePlayerLeft()
        {
            _characterControl.MoveLeft();
        }

        public void OnMovePlayerRight()
        {
            _characterControl.MoveRight();
        }

        public void OnMovePlayerUp()
        {
            _characterControl.MoveUp();
        }

        public void OnMovePlayerDown()
        {
            _characterControl.MoveDown();
        }

        public void OnRowingUp()
        {
            _rowingControl.RowUp();
        }

        public void OnRowingDown()
        {
            _rowingControl.RowDown();
        }

        public void OnRowingLeft()
        {
            _rowingControl.RowLeft();
        }

        public void OnRowingRight()
        {
            _rowingControl.RowRight();
        }

        public void OnRowingShift()
        {
            _rowingControl.RowShift();
        }
    }

}