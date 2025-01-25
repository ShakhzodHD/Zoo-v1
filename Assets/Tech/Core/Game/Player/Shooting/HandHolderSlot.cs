using UnityEngine;

public class HandHolderSlot : MonoBehaviour
{
    [SerializeField] private WeaponController controller;
    private GameObject objModel;

    public void SetActiveSlot()
    {
        if (TryGetChild(out GameObject child))
        {
            child.SetActive(true);

            if (child.TryGetComponent<Weapon>(out var weapon))
            {
                controller.SetWeapon(weapon);
            }
            else
            {
                controller.RemoveWeapon();
            }
        }
        else
        {
            controller.RemoveWeapon();
        }
    }

    public void InactiveSlot()
    {
        if (TryGetChild(out GameObject child))
        {
            child.SetActive(false);
        }
    }

    public void SetObject(ItemUI item)
    {
        if (item == null || item.model == null) return;

        objModel = item.model;
        RemoveObject();
        SpawnItemInHand();
    }

    public void RemoveObject()
    {
        if (TryGetChild(out GameObject child))
        {
            Destroy(child);
        }
    }

    private void SpawnItemInHand()
    {
        if (objModel == null) return;

        GameObject obj = Instantiate(objModel, transform);
        obj.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        obj.tag = "Untagged";
    }

    private bool TryGetChild(out GameObject child)
    {
        child = transform.childCount > 0 ? transform.GetChild(0).gameObject : null;
        return child != null;
    }
}
