using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{
    [Header("Character Input Values")]

    public Vector2 move;
    public Vector2 look;
    public bool jump;
    public bool sprint;
    public bool shoot;
    public bool reload;
    public bool interact;

    public bool analogMovement;

    public bool cursorLocked = true;
    public bool cursorInputForLook = true;
    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }
    public void OnLook(InputValue value)
    {
        if (cursorInputForLook)
        {
            LookInput(value.Get<Vector2>());
        }
    }
    public void OnJump(InputValue value)
    {
        JumpInput(value.isPressed);
    }
    public void OnSprint(InputValue value)
    {
        SprintInput(value.isPressed);
    }
    public void OnShoot(InputValue value)
    {
        ShootInput(value.isPressed);
    }
    public void OnInteract(InputValue value)
    {
        InteractInput(value.isPressed);
    }
    public void OnReload(InputValue value)
    {
        ReloadInput(value.isPressed);
    }
    public void MoveInput(Vector2 newMoveDirection)
    {
        move = newMoveDirection;
    }

    public void LookInput(Vector2 newLookDirection)
    {
        look = newLookDirection;
    }

    public void JumpInput(bool newJumpState)
    {
        jump = newJumpState;
    }

    public void SprintInput(bool newSprintState)
    {
        sprint = newSprintState;
    }
    public void ShootInput(bool newShootState)
    {
        shoot = newShootState;
    }
    public void ReloadInput(bool newReload)
    {
        reload = newReload;
    }
    public void InteractInput(bool newInteractState)
    {
        interact = newInteractState;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(cursorLocked);
    }

    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
