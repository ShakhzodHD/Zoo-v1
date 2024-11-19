using UnityEngine;
using UnityEngine.InputSystem;

public class MobileDisableAutoSwitchControls : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput; //стоит рефактировать для webgl

    void Start()
    {
        DisableAutoSwitchControls();
    }

    void DisableAutoSwitchControls()
    {
        playerInput.neverAutoSwitchControlSchemes = true;
    }
}
