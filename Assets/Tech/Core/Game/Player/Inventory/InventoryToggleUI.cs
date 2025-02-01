using UnityEngine;

public class InventoryToggleUI : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private RectTransform inventoryRectTransform;
    [SerializeField] private float animationDuration = 0.3f;
    [SerializeField] private float heightOffset = 0f;

    private Vector2 hiddenPosition;
    private Vector2 visiblePosition;
    private bool isVisible = false;

    private void Start()
    {
        if (inventoryManager == null)
            inventoryManager = GetComponent<InventoryManager>();

        if (inventoryRectTransform == null)
            inventoryRectTransform = GetComponent<RectTransform>();

        visiblePosition = inventoryRectTransform.anchoredPosition;
        hiddenPosition = visiblePosition - new Vector2(0, inventoryRectTransform.rect.height + heightOffset);

        if (DebugPanel.Instance.IsMobile)
            inventoryRectTransform.anchoredPosition = hiddenPosition;
    }

    public void ToggleInventory()
    {
        StopAllCoroutines();
        if (isVisible)
        {
            StartCoroutine(AnimateInventory(hiddenPosition));
        }
        else
        {
            StartCoroutine(AnimateInventory(visiblePosition));
        }
        isVisible = !isVisible;
    }

    private System.Collections.IEnumerator AnimateInventory(Vector2 targetPosition)
    {
        Vector2 startPosition = inventoryRectTransform.anchoredPosition;
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / animationDuration;
            inventoryRectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        inventoryRectTransform.anchoredPosition = targetPosition;
    }
}
