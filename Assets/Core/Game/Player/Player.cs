using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject uiMobile;
    [SerializeField] private PlayerInputSystem inputSystem;
    private void Start()
    {
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
