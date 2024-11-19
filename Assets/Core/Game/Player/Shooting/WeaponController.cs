using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float fireRate = 0.5f;

    [Header("Ammo Settings")]
    [SerializeField] private int magazineSize = 30;
    [SerializeField] public int currentAmmo; // temp
    [SerializeField] private float reloadTime = 1.5f;

    [SerializeField] private PlayerInputSystem input;

    private float nextFireTime = 0f;
    [HideInInspector] public bool isReloading = false; // temp

    private void Awake()
    {
        currentAmmo = magazineSize;
    }
    private void Start()
    {
        if (input == null) input = GetComponent<PlayerInputSystem>();
    }
    private void Update()
    {
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
    private void OnFirePerfomed()
    {
        if (isReloading) return;

        if (currentAmmo > 0 && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
        else if (currentAmmo <= 0)
        {
            Reload();
        }
    }
    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * bulletSpeed;
        Destroy(bullet, 3f);

        currentAmmo--;
    }
    private void Reload()
    {
        if (!isReloading) StartCoroutine(ReloadCoroutine());
    }
    IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = magazineSize;
        isReloading = false;
    }
}
