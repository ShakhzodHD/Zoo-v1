using UnityEngine;
using UnityEngine.UI;

public class WeaponHUD : MonoBehaviour
{
    [SerializeField] private WeaponController weaponController;

    [SerializeField] private Text ammoText;
    [SerializeField] private Image weaponLogo;

    public void Initialize(WeaponController controller)
    {
        weaponController = controller;
    }

    public void SetState(Weapon weapon)
    {
        if (weapon != null)
        {
            weaponLogo.sprite = weapon.logo;
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (weaponController != null)
        {
            UpdateAmmoText(weaponController.useWeapon.currentMagazineAmmo, weaponController.useWeapon.FullAmmo);
        }
    }

    private void UpdateAmmoText(int currentAmmo, int fullAmmo)
    {
        ammoText.text = $"Ammo: {currentAmmo} / {fullAmmo}";
    }
}
