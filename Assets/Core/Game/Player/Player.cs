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
        Debug.Log("Сообщение");
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
        DebugPanel.Instance.Init();
    }
}
