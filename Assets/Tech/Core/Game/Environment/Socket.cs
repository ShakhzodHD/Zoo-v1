using UnityEngine;

public class Socket : MonoBehaviour
{
    [SerializeField] private MechanismId mechanismType;
    [SerializeField] private Rod rod;
    private bool isSocketOccuped;
    public bool TrySetItem(ItemUI item)
    {
        if (item.type == mechanismType)
        {
            isSocketOccuped = true;
            GameObject obj = Instantiate(item.model, transform);
            Vector3 positionObj = new(0, 0, -0.5f);
            obj.transform.SetLocalPositionAndRotation(positionObj, Quaternion.identity);
            obj.tag = "Untagged";
            gameObject.tag = "Untagged";
            rod.IsReady = true;
            return true;
        }
        else
        {
            isSocketOccuped = false;
            return false;
        }
    }
    public bool GetState()
    {
        return isSocketOccuped;
    }
}
