using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform player;

    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float waitTime = 2f;

    [Header("Combat Settings")]
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private float attackRadius = 2f;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private int damage = 10;

    private EnemyState currentState = EnemyState.Idle;
    private int currentPatrolIndex = 0;
    private float lastAttackTime;
    private float waitCounter;

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
                PatrolBehavior();
                break;
            case EnemyState.Chasing:
                ChaseBehavior();
                break;
            case EnemyState.Attacking:
                AttackBehavior();
                break;
        }

        CheckPlayerDetection();
    }
    private void StartPatrolling()
    {
        currentState = EnemyState.Patrolling;
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
    }
    private void PatrolBehavior()
    {
        if (agent.remainingDistance < 0.1f)
        {
            waitCounter += Time.deltaTime;
            if (waitCounter >= waitTime)
            {
                currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
                agent.SetDestination(patrolPoints[currentPatrolIndex].position);
                waitCounter = 0;
            }
        }
    }
    private void CheckPlayerDetection()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRadius)
        {
            currentState = EnemyState.Attacking;
            agent.isStopped = true;
        }
        else if (distanceToPlayer <= detectionRadius)
        {
            currentState = EnemyState.Chasing;
        }
    }
    private void ChaseBehavior()
    {
        agent.SetDestination(player.position);
    }
    private void AttackBehavior()
    {
        transform.LookAt(player);

        if (Time.time - lastAttackTime > attackCooldown)
        {
            Debug.Log("Attack");
            // Здесь логика нанесения урона игроку
            lastAttackTime = Time.time;
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
