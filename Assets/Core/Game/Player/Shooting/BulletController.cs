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
        if (collision.transform.tag != "Hit") return;

        TargetHealth targetHealth = collision.gameObject.GetComponent<TargetHealth>();

        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
