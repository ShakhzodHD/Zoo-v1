using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject uiMobile;
    [SerializeField] private PlayerInputSystem inputSystem;
    [SerializeField] private Camera playerCamera;
    private void Awake()
    {
        Bootstrap.Instance.Camera = playerCamera;
    }
    private void Start()
    {
        Bootstrap.Instance.InventoryManager.Initialize();

        if (DebugPanel.Instance.IsMobile)
        {
            uiMobile.SetActive(true);
            inputSystem.cursorLocked = true;
            inputSystem.cursorInputForLook = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Destroy(uiMobile);
        }

        Bootstrap.Instance.UIManager.weaponHUD.Initialize(GetComponent<WeaponController>());

        DebugPanel.Instance.Init();
    }
}
