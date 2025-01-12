using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class ItemUI : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public MechanismId type = MechanismId.None;
    public GameObject model;
}
