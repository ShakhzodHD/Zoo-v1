using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemUI itemUI;
    public ItemUI Pick()
    {
        return itemUI;
    }
}
