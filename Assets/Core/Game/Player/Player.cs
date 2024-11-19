using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject uiMobile;
    [SerializeField] private PlayerInputSystem inputSystem;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (DebugPanel.Instance.IsMobile)
        {
            uiMobile.SetActive(true);
        }
        else
        {
            Destroy(uiMobile);
        }
        DebugPanel.Instance.Init();
    }
}
