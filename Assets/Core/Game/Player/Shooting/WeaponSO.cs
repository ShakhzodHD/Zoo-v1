using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Weapon/WeaponData")]
public class WeaponSO : ScriptableObject
{
    [Header("Weapon Settings")]
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float fireRate;

    [Header("Ammo Settings")]
    public int magazineSize;
    public int currentAmmo;
    public float reloadTime;
}
