using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIBrain : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    [SerializeField] float moveRadius = 10f;
    [SerializeField] float followRange = 10f;
    [SerializeField] float attackRange = 2f;
    [SerializeField] float attackCooldown = 5f;
    [SerializeField] float stopDistance = 1.5f;
    float currentCooldown = 0f;

    Animator animator;
    NavMeshAgent navAgent;

    bool isDead = false;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        SetRandomDestination();
    }

    void SetRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * moveRadius;
        randomDirection += transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, moveRadius, -1);
        navAgent.SetDestination(navHit.position);
    }

    void Update()
    {
        if (isDead) return;

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // Calculate the direction the AI is moving in
        Vector3 moveDirection = navAgent.velocity.normalized;
        moveDirection.y = 0f;

        // If the AI is moving, set its rotation to face the direction it's moving in
        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }


        if (distanceToPlayer <= attackRange && currentCooldown <= 0f && navAgent.velocity.magnitude == 0 && !GameManager.Instance.playerIsDead)
        {
            // Start the attack animation
            animator.SetTrigger("Attack");

            // Start the attack cooldown timer
            currentCooldown = attackCooldown;
        }

        // Decrement the current cooldown timer if it is greater than 0
        if (currentCooldown > 0f)
        {
            currentCooldown -= Time.deltaTime;
        }


        if (!navAgent.pathPending && navAgent.remainingDistance < 0.1f)
        {
            gameObject.GetComponent<NavMeshAgent>().isStopped = false;
            SetRandomDestination();
        }

        // Set the animator's "Speed" parameter based on the AI's velocity
        animator.SetFloat("Speed", navAgent.velocity.magnitude);


        // If the player is within the attack range, set the AI's destination to the player's position
        if (distanceToPlayer <= followRange)
        {
            gameObject.GetComponent<NavMeshAgent>().isStopped = false;
            navAgent.SetDestination(playerTransform.position);
        }
        if (distanceToPlayer < stopDistance)
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
    }

    public void SwitchToDeathState()
    {
        animator.SetTrigger("isDead");
        animator.SetFloat("Speed", 0);

        // Disable AI components
        GetComponentInChildren<EnemyAttack>().isDead = true;
        navAgent.enabled = false;
        isDead = true;
    }
}
