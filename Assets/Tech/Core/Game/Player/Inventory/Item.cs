using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Item : MonoBehaviour
{
    [SerializeField] private ItemUI itemUI;
    public event Action OnItemPick;
    public ItemUI Pick()
    {
        OnItemPick?.Invoke();
        return itemUI;
    }
}
