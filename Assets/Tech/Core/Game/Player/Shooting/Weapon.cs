using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform firePoint;

    public Transform GetFirePoint()
    {
        return firePoint;
    }
}
