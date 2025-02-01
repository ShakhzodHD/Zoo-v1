using UnityEngine;
using UnityEngine.InputSystem;

public class MobileDisableAutoSwitchControls : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput; //����� ������������� ��� webgl

    private void Start()
    {
        DisableAutoSwitchControls();
    }

    private void DisableAutoSwitchControls()
    {
        playerInput.neverAutoSwitchControlSchemes = true;
    }
}
