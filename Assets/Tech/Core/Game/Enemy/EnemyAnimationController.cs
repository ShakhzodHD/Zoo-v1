using UnityEngine;

[RequireComponent(typeof(EnemyBehaviour))]
public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyBehaviour enemy;

    private void Awake()
    {
        animator = animator != null ? animator : GetComponent<Animator>();
        if (animator == null)
            throw new MissingComponentException("Animator component is missing on the GameObject.");

        enemy = enemy != null ? enemy : GetComponent<EnemyBehaviour>();
        if (enemy == null)
            throw new MissingComponentException("EnemyBehaviour component is missing on the GameObject.");

        enemy.OnChangeState += EnemyOnChangeState;
    }

    private void EnemyOnChangeState(EnemyState currentState)
    {
        SetAnimationState(
            isWalking: currentState == EnemyState.Patrolling,
            isRunning: currentState == EnemyState.Chasing
        );

        if (currentState == EnemyState.Attacking)
        {
            animator.SetTrigger("Attack");
        }
    }
    private void SetAnimationState(bool isWalking, bool isRunning)
    {
        animator.SetBool("isWalk", isWalking);
        animator.SetBool("isRun", isRunning);
    }

    private void OnDestroy()
    {
        if (enemy != null)
        {
            enemy.OnChangeState -= EnemyOnChangeState;
        }
    }

}
