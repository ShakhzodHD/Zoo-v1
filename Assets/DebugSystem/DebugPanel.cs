using UnityEngine;
using UnityEngine.UI;
using YG;

public class DebugPanel : MonoBehaviour
{
    public static DebugPanel Instance {  get; private set; }
    public bool IsMobile {  get; private set; }

    [SerializeField] private GameObject debugPanel;

    [SerializeField] private PlayerController playerController;
    [SerializeField] private WeaponController weaponController;

    [SerializeField] private Text speedText;
    [SerializeField] private Text ammoText;
    [SerializeField] private Text statusAmmoText;
    [SerializeField] private Slider staminaBar;

    [SerializeField] private Transform[] uselessPanels;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        if (YandexGame.EnvironmentData.isMobile)
        {
            IsMobile = true;
            foreach(Transform t in uselessPanels)
            {
                t.gameObject.SetActive(true);
            }
        }
        else if (YandexGame.EnvironmentData.isDesktop)
        {
            IsMobile = false;
        }
    }
    private void Update()
    {
        if (playerController != null)
        {
            speedText.text = "Current speed: " + playerController._speed;
            ammoText.text = "Curren ammo: " + weaponController.currentAmmo;

            if (weaponController.isReloading) statusAmmoText.text = "Reload...";
            else statusAmmoText.text = "Console";

            staminaBar.value = playerController.stamina / 100; // staminaBar - ��� ��� UI Slider
        }
    }
    public void Init()
    {
        playerController = FindObjectOfType<PlayerController>();
        weaponController = FindObjectOfType<WeaponController>();
    }
}