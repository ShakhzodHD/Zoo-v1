using UnityEngine;

public class MobileShooting : MonoBehaviour
{
    [SerializeField] private PlayerInputSystem inputSystem;
    [SerializeField] private float rayLength = 10f;
    private Camera mainCamera;
    
    private void Start()
    {
        mainCamera = Bootstrap.Instance.Camera;
    }
    private void Update()
    {
        CheckForShooting();
    }
    private void CheckForShooting()
    {
        Vector3 screenCenter = new(Screen.width / 2, Screen.height / 2, 0);

        Ray ray = mainCamera.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, rayLength))
        {
            if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Enemy")) 
            {
                inputSystem.ShootInput(true);
            }
            else
            {
                inputSystem.ShootInput(false);
            }
        }
    }
}
