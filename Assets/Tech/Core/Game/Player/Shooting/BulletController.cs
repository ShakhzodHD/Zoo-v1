using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.CompareTag("Hit")) return;

        if (collision.gameObject.TryGetComponent<TargetHealth>(out var targetHealth))
        {
            targetHealth.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
