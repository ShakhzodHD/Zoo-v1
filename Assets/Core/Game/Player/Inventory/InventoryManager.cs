using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Image[] inventorySlots;
    public List<ItemUI> items = new();

    void Start()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].sprite = null;
            items.Add(null);
        }
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
}
