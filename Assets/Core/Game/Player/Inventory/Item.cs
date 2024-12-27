using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Item : MonoBehaviour
{
    [SerializeField] private ItemUI itemUI;
    public ItemUI Pick()
    {
        return itemUI;
    }
}
