using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Windows;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float fireRate = 0.5f;

    [Header("Aim Settings")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float maxShootDistance = 1000f;

    [Header("Ammo Settings")]
    [SerializeField] private float reloadTime = 1.5f;
    [SerializeField] private PlayerInputSystem input;

    private float nextFireTime = 0f;
    private bool isEquipWeapon = false;

    public Weapon useWeapon;

    public event Action<Sprite> onChangeWeapon;

    private void Start()
    {
        if (input == null) input = GetComponent<PlayerInputSystem>();
        if (playerCamera == null) playerCamera = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        if (!isEquipWeapon) return;

        UpdateAiming();

        if (input.shoot)
        {
            OnFirePerfomed();
            input.shoot = false;
        }
        if (input.reload)
        {
            Reload();
            input.reload = false;
        }
    }
    private void UpdateAiming()
    {
        Vector3 screenCenter = new(Screen.width / 2f, Screen.height / 2f, 0f);
        Ray ray = playerCamera.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out RaycastHit hit, maxShootDistance))
        {
            Vector3 directionToHit = (hit.point - firePoint.position).normalized;
            firePoint.forward = directionToHit;
        }
        else
        {
            firePoint.forward = ray.direction;
        }
    }

    private void OnFirePerfomed()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }
    private void Shoot()
    {
        if (useWeapon.CurrentAmmo > 0)
        {
            useWeapon.CurrentAmmo--;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = firePoint.forward * bulletSpeed;
            Destroy(bullet, 3f);

            Debug.Log("Выстрел! Осталось патронов: " + useWeapon.CurrentAmmo);
        }
        else
        {
            Debug.Log("Боеприпасы закончились!");
        }
    }

    private void Reload()
    {
        useWeapon.Reload();
    }
    public void SetWeapon(Weapon weapon)
    {
        input.shoot = false;
        input.reload = false;

        firePoint = weapon.GetFirePoint();

        useWeapon = weapon;

        isEquipWeapon = true;

        onChangeWeapon?.Invoke(weapon.logo);

        Bootstrap.Instance.UIManager.weaponHUD.SetState(weapon);
    }
    public void RemoveWeapon()
    {
        isEquipWeapon = false;

        Bootstrap.Instance.UIManager.weaponHUD.SetState(null);
    }

    public void AddAmmo(int amount)
    {
        //magazineSize += amount;
        //StartCoroutine(ReloadCoroutine());
    }
}