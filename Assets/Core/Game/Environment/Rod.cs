using UnityEngine;

public class Rod : MonoBehaviour
{
    private bool isReady;
    public bool IsReady
    {
        get
        {
            return isReady;
        }
        set
        {
            if (isReady != value)
            {
                isReady = value;
            }
        }
    }
    public void TryIt()
    {
        if (isReady)
        {
            gameObject.tag = "Untagged";
            Debug.Log("Open door");
        }
    }
}
