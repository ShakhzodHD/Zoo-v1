using UnityEngine;
using UnityEngine.UI;

public class ItemInteraction : MonoBehaviour
{
    [SerializeField] private PlayerInputSystem input;
    [SerializeField] private InventoryManager inventoryManager;

    [SerializeField] private HandHolderController handHolderController;
    [SerializeField] private Transform handContainerl;

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
        inventoryManager = Bootstrap.Instance.InventoryManager;

        crossIcon = Bootstrap.Instance.UIManager.crossImage;
        defaultCross = Bootstrap.Instance.GameSettings.defaultCross;
        interactableCross = Bootstrap.Instance.GameSettings.universalInteractableCross;
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
            if (hitInfo.collider.CompareTag("Socket"))
            {
                crossIcon.sprite = interactableCross;
                return;
            }
            if (hitInfo.collider.CompareTag("Rod"))
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
            if (hitInfo.collider.CompareTag("Socket"))
            {
                if (hitInfo.collider.TryGetComponent<Socket>(out var socket))
                {
                    if (!socket.GetState())
                    {
                        inventoryManager.GetActiveItemUI();
                        if (socket.TrySetItem(inventoryManager.GetActiveItemUI()))
                        {
                            var activeIndex = inventoryManager.activeSlotIndex;
                            inventoryManager.RemoveFromInventory(activeIndex);
                            handHolderController.handSlots[activeIndex].RemoveObject();
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        Debug.Log("Socket occuped");
                    }
                }
            }
            if (hitInfo.collider.CompareTag("Rod"))
            {
                if (hitInfo.collider.TryGetComponent<Rod>(out var rod))
                {
                    if (rod.IsReady)
                    {
                        rod.TryIt();
                    }
                    else
                    {
                        Debug.Log("Rod: Conditions not met");
                    }
                }
            }
        }
    }
}
