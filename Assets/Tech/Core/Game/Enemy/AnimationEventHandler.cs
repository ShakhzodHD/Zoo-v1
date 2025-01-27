using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    public EnemyBehaviour enemy;

    public void OnAnimationEvent()
    {
        if (enemy != null)
        {
            enemy.DealDamage();
        }
    }
}
