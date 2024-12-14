using UnityEngine;

public class HandHolderSlot : MonoBehaviour
{
    [SerializeField] private WeaponController controller;
    private GameObject objModel;
    public void SetActiveSlot()
    {
        if (transform.childCount > 0)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            if (transform.GetChild(0).TryGetComponent<Weapon>(out var weapon))
            {
                controller.SetWeapon(weapon);
            }
            else
            {
                controller.RemoveWeapon();
            }
        }
    }
    public void InactiveSlot()
    {
        if (transform.childCount > 0) transform.GetChild(0).gameObject.SetActive(false);
    }
    public void SetObject(ItemUI item)
    {
        objModel = item.model;
        SpawnItemInHand();
    }
    private void SpawnItemInHand()
    {
        if (objModel != null)
        {
            GameObject obj = Instantiate(objModel, transform);

            obj.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            obj.tag = "Untagged";
        }
    }
}
