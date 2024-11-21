using UnityEngine;

public class UIControllerInput : MonoBehaviour
{
    [SerializeField] private PlayerInputSystem playerInputSystem;
    public void VirtualMoveInput(Vector2 virtualMoveDirection)
    {
        playerInputSystem.MoveInput(virtualMoveDirection);
    }

    public void VirtualLookInput(Vector2 virtualLookDirection)
    {
        playerInputSystem.LookInput(virtualLookDirection);
    }

    public void VirtualJumpInput(bool virtualJumpState)
    {
        playerInputSystem.JumpInput(virtualJumpState);
    }

    public void VirtualSprintInput(bool virtualSprintState)
    {
        playerInputSystem.SprintInput(virtualSprintState);
    }

    public void VirtualInteractInput(bool virtualInteractSrate)
    {
        playerInputSystem.InteractInput(virtualInteractSrate);
    }
}
