using UnityEngine;
using UnityEngine.UI;

public class ItemInteraction : MonoBehaviour
{
    [SerializeField] private PlayerInputSystem input;
    [SerializeField] private InventoryManager inventoryManager;

    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float rayLength = 2f;

    private Image crossIcon;
    private Sprite defaultCross;
    private Sprite interactableCross;

    private void Start()
    {
        input = GetComponent<PlayerInputSystem>();

        mainCamera = Bootstrap.Instance.Camera;
        inventoryManager = FindObjectOfType<InventoryManager>();

        crossIcon = Bootstrap.Instance.UIManager.crossImage;
        defaultCross = Bootstrap.Instance.GameSettings.defaultCross;
        interactableCross = Bootstrap.Instance.GameSettings.interactableCross;
    }
    private void Update()
    {
        CheckForInteraction();
        if (input.interact)
        {
            OnInteraction();
            input.interact = false;
        }
    }
    private void CheckForInteraction()
    {
        Vector3 screenCenter = new(Screen.width / 2, Screen.height / 2, 0);
        Ray ray = mainCamera.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, rayLength, interactableLayer))
        {
            if (hitInfo.collider.CompareTag("Item"))
            {
                crossIcon.sprite = interactableCross;
                return;
            }
        }

        crossIcon.sprite = defaultCross;
    }
    private void OnInteraction()
    {
        input.interact = true;

        Vector3 screenCenter = new(Screen.width / 2, Screen.height / 2, 0);

        Ray ray = mainCamera.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, rayLength))
        {
            if (hitInfo.collider.CompareTag("Item"))
            {
                var objItem = hitInfo.collider.GetComponent<Item>();
                var itemUI = objItem.Pick();
                int freeSlotIndex = inventoryManager.CanAddToInventory();
                if (freeSlotIndex != -1)
                {
                    inventoryManager.AddToInventory(itemUI, freeSlotIndex);
                    Destroy(objItem.gameObject);
                }
                else
                {
                    Debug.Log("No space into inventory");
                }
            }
            Debug.Log($"Попали в: {hitInfo.collider.name}, расстояние: {hitInfo.distance}");
        }
    }
}
