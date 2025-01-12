using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Image[] inventorySlots;
    public List<ItemUI> items = new();

    public int activeSlotIndex = 0;

    private HandHolderController handHolderController;

    private void Start()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].sprite = null;
            items.Add(null);
        }

        UpdateActiveSlotIndicator();
    }
    public void Initialize()
    {
        handHolderController = FindObjectOfType<HandHolderController>();
    }

    public int CanAddToInventory()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == null)
                return i;
        }
        Debug.Log("Инвентарь полон!");
        return -1;
    }

    public void AddToInventory(ItemUI item, int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < items.Count && items[slotIndex] == null)
        {
            items[slotIndex] = item;
            inventorySlots[slotIndex].sprite = item.icon;
            handHolderController.handSlots[slotIndex].SetObject(item);
            handHolderController.SwitchHand(slotIndex);
            activeSlotIndex = slotIndex;
            UpdateActiveSlotIndicator();
        }
        else
        {
            Debug.LogError("Невозможно добавить предмет в указанный слот!");
        }
    }

    public void RemoveFromInventory(int slotIndex)
    {
        if (slotIndex < items.Count)
        {
            items[slotIndex] = null;
            inventorySlots[slotIndex].sprite = null;
        }
    }
    public void SetActiveSlot(int newIndex)
    {
        if (newIndex >= 0 && newIndex < items.Count)
        {
            activeSlotIndex = newIndex;
            UpdateActiveSlotIndicator();
        }
    }
    public void SwitchActiveSlot(int direction)
    {
        activeSlotIndex = (activeSlotIndex + direction + items.Count) % items.Count;
        handHolderController.SwitchHand(activeSlotIndex);
        UpdateActiveSlotIndicator();
    }
    private void UpdateActiveSlotIndicator()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].transform.localScale = (i == activeSlotIndex) ? Vector3.one * 1.2f : Vector3.one;
        }
    }
    public ItemUI GetActiveItemUI()
    {
        return items[activeSlotIndex];
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            SwitchActiveSlot(-1);
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            SwitchActiveSlot(1);
    }
}
