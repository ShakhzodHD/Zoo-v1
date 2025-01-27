using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;

    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float waitTime = 2f;

    [Header("Combat Settings")]
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private float attackRadius = 2f;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private Transform attackHand;
    [SerializeField] private float handRadius = 1f;
    [SerializeField] private int damage = 10;
    [SerializeField] private LayerMask playerLayer;

    [Header("Return Settings")]
    [SerializeField] private float returnRadius = 12f;

    public event Action<EnemyState> OnChangeState;

    private EnemyState currentState = EnemyState.Idle;
    private int currentPatrolIndex = 0;
    private float lastAttackTime;
    private float waitCounter;

    private bool isAttacking;
    private bool isAggressive;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Player>().gameObject.transform;
        StartPatrolling();
    }

    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.Patrolling:
            case EnemyState.Idle:
                PatrolBehavior();
                OnChangeState?.Invoke(currentState);
                break;
            case EnemyState.Chasing:
                if (!isAttacking) ChaseBehavior();
                OnChangeState?.Invoke(currentState);
                break;
            case EnemyState.Attacking:
                AttackBehavior();
                break;
        }

        if (!isAttacking) CheckPlayerDetection();
    }

    private void StartPatrolling()
    {
        if (currentState == EnemyState.Patrolling) return;

        isAggressive = false;

        currentState = EnemyState.Patrolling;

        if (patrolPoints.Length == 0)
        {
            Debug.LogError("Отсутствуют точки (waypoint'ы)");
            return;
        }

        agent.isStopped = false;
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
    }

    private void PatrolBehavior()
    {
        if (agent.remainingDistance < 0.1f && currentState != EnemyState.Idle)
        {
            currentState = EnemyState.Idle;
            waitCounter = 0;
            agent.isStopped = true;
        }

        if (currentState == EnemyState.Idle)
        {
            waitCounter += Time.deltaTime;
            if (waitCounter >= waitTime)
            {
                currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
                agent.SetDestination(patrolPoints[currentPatrolIndex].position);
                agent.isStopped = false;
                currentState = EnemyState.Patrolling;
            }
        }
    }

    private void CheckPlayerDetection()
    {
        float distanceToPlayer = GetDistanceToPlayer();

        if (distanceToPlayer <= attackRadius)
        {
            currentState = EnemyState.Attacking;
            agent.isStopped = true;
        }
        else if (distanceToPlayer <= detectionRadius)
        {
            currentState = EnemyState.Chasing;
            agent.isStopped = false;
        }
        else if (isAggressive && distanceToPlayer > returnRadius)
        {
            StartPatrolling();
        }
    }

    private void ChaseBehavior()
    {
        isAggressive = true;
        agent.SetDestination(player.position);
    }

    private void AttackBehavior()
    {
        if (isAttacking) return;

        Vector3 targetPosition = new(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(targetPosition);

        float distanceToPlayer = GetDistanceToPlayer();
        if (distanceToPlayer > attackRadius)
        {
            currentState = EnemyState.Chasing;
            agent.isStopped = false;
        }
        else
        {
            if (Time.time - lastAttackTime > attackCooldown)
            {
                agent.isStopped = true;
                isAttacking = true;

                OnChangeState?.Invoke(EnemyState.Attacking);

                lastAttackTime = Time.time;

                Invoke(nameof(FinishAttack), attackCooldown);
            }
        }
    }

    public void DealDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(attackHand.position, handRadius, playerLayer);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                if (hitCollider.TryGetComponent<PlayerHealth>(out var playerHealth))
                {
                    playerHealth.TakeDamage(damage);
                }
                return;
            }
        }
    }

    private void FinishAttack()
    {
        isAttacking = false;

        if (Vector3.Distance(transform.position, player.position) > attackRadius)
        {
            currentState = EnemyState.Chasing;
        }
    }
    private float GetDistanceToPlayer() => Vector3.Distance(transform.position, player.position);

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (attackHand != null)
        {
            Gizmos.DrawWireSphere(attackHand.position, handRadius);
        }

        Gizmos.color = currentState switch
        {
            EnemyState.Patrolling => Color.green,
            EnemyState.Chasing => Color.yellow,
            EnemyState.Attacking => Color.red,
            _ => Color.white
        };
        Gizmos.DrawSphere(transform.position, 0.5f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, returnRadius);
    }
}
