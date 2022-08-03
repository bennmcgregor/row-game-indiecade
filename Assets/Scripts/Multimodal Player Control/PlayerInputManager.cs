using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] private CharacterControl _characterControl;

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
}
