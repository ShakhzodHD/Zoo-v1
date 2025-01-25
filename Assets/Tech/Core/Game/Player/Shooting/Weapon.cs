using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private int baseAmmo = 30;
    [SerializeField] private int magazineSize = 10;

    public Sprite logo;
    public int currentMagazineAmmo;
    private int fullAmmo;

    private void Awake()
    {
        currentMagazineAmmo = Mathf.Min(magazineSize, baseAmmo);
        fullAmmo = baseAmmo - currentMagazineAmmo;
    }

    public int CurrentAmmo
    {
        get
        {
            if (currentMagazineAmmo > 0)
            {
                return currentMagazineAmmo;
            }
            else
            {
                return Reload();
            }
        }
        set
        {
            currentMagazineAmmo = value;
        }
    }
    public int FullAmmo
    {
        get
        {
            return fullAmmo;
        }
    }

    public int Reload()
    {
        if (fullAmmo > 0)
        {
            int ammoNeeded = magazineSize - currentMagazineAmmo;
            int ammoToReload = Mathf.Min(ammoNeeded, fullAmmo);

            currentMagazineAmmo += ammoToReload;
            fullAmmo -= ammoToReload;

            Debug.Log($"�����������: +{ammoToReload} ��������. �������� � ������: {fullAmmo}");
        }
        else
        {
            Debug.Log("��� ����������� ��� �����������!");
        }

        return currentMagazineAmmo;
    }

    public void AddAmmo(int amount)
    {
        if (amount > 0)
        {
            fullAmmo += amount;
            Debug.Log($"��������� {amount} ��������. ������� �����: {fullAmmo}");
        }
        else
        {
            Debug.LogWarning("������� �������� ������������ ���������� ��������!");
        }
    }

    public Transform GetFirePoint()
    {
        return firePoint;
    }
}
