using UnityEngine;
using UnityEngine.UI;
//���� ������������: �������� Update �� Event
public class WeaponHUD : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private WeaponController weaponController;

    [SerializeField] private Text ammoText;
    [SerializeField] private Image weaponLogo;
    private void Update()
    {
        ammoText.text = "Curren ammo: " + weaponController.currentAmmo;
    }
}
