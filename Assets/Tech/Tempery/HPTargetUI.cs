using TMPro;
using UnityEngine;

public class HPTargetUI : MonoBehaviour
{
    [SerializeField] private TargetHealth TargetHealth;
    private TextMeshPro TextMeshPro;
    private void Start()
    {
        TextMeshPro = GetComponent<TextMeshPro>();
    }
    private void LateUpdate()
    {
        TextMeshPro.text = TargetHealth.currentHealth.ToString();
    }
}
