using UnityEngine;

public enum Rewards
{
    None,
    AddMagazine,
}
public class ItemReward : MonoBehaviour
{
    [SerializeField] private Rewards rewards;
    [SerializeField] private int amount;
    [SerializeField] private Item item;
    private WeaponController controller;
    private void Awake()
    {
        if (item == null) item = GetComponent<Item>();
        item.OnItemPick += Reward;
    }
    private void Start()
    {
        controller = FindObjectOfType<WeaponController>();
    }
    public void Reward()
    {
        if (rewards == Rewards.None) return;

        if (rewards == Rewards.AddMagazine)
        {
            controller.AddAmmo(amount);
        }
    }
}
