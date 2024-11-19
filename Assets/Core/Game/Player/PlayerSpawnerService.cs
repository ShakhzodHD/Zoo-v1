using UnityEngine;

public class PlayerSpawnerService : MonoBehaviour
{
    private void Awake()
    {
        Instantiate(Bootstrap.Instance.GameSettings.PlayerPrefab, transform.position, transform.rotation, transform);
    }
}
