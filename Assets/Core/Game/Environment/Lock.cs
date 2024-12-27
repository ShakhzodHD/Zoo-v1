using UnityEngine;

public class Lock : MonoBehaviour
{
    public MechanismId requireMechanismId;
    private bool isBreake;
    public void Breack()
    {
        if (!isBreake)
        {
            gameObject.tag = "Untagged";
            isBreake = true;
            Debug.Log("Breack");

            var objectRenderer = GetComponentInChildren<Renderer>();
            objectRenderer.material.color = Color.green;
        }
    }
}
