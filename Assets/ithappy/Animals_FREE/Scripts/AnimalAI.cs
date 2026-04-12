using UnityEngine;
using UnityEngine.AI;

public class AnimalAI : MonoBehaviour
{
    public enum State { Idle, Walk, Flee }
    public State currentState;

    [Header("Movement")]
    public float walkSpeed = 2f;
    public float runSpeed = 4f;

    [Header("Timing")]
    public float minIdleTime = 2f;
    public float maxIdleTime = 5f;
    public float minWalkTime = 3f;
    public float maxWalkTime = 6f;

    [Header("Wander Area")]
    public float wanderRadius = 10f;

    [Header("Player Detection")]
    public Transform player;
    public float detectDistance = 5f;
    public float fleeDistance = 10f;

    private NavMeshAgent agent;
    private Animator animator;
    private float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        ChangeState(State.Idle);
    }

    void Update()
    {
        float distanceToPlayer = player ? Vector3.Distance(transform.position, player.position) : Mathf.Infinity;

        // FLEE if player is close
        if (distanceToPlayer < detectDistance)
        {
            ChangeState(State.Flee);
        }

        switch (currentState)
        {
            case State.Idle:
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    ChangeState(State.Walk);
                }
                break;

            case State.Walk:
                timer -= Time.deltaTime;

                if (!agent.hasPath || agent.remainingDistance < 0.5f)
                {
                    SetNewDestination();
                }

                if (timer <= 0)
                {
                    ChangeState(State.Idle);
                }
                break;

            case State.Flee:
                FleeFromPlayer();

                if (distanceToPlayer > fleeDistance)
                {
                    ChangeState(State.Idle);
                }
                break;
        }

        UpdateAnimation();
    }

    void ChangeState(State newState)
    {
        currentState = newState;

        switch (newState)
        {
            case State.Idle:
                agent.isStopped = true;
                timer = Random.Range(minIdleTime, maxIdleTime);
                break;

            case State.Walk:
                agent.isStopped = false;
                agent.speed = walkSpeed;
                timer = Random.Range(minWalkTime, maxWalkTime);
                SetNewDestination();
                break;

            case State.Flee:
                agent.isStopped = false;
                agent.speed = runSpeed;
                break;
        }
    }

    void SetNewDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, 1))
        {
            agent.SetDestination(hit.position);
        }
    }

    void FleeFromPlayer()
    {
        if (!player) return;

        Vector3 fleeDir = (transform.position - player.position).normalized;
        Vector3 targetPos = transform.position + fleeDir * wanderRadius;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(targetPos, out hit, wanderRadius, 1))
        {
            agent.SetDestination(hit.position);
        }
    }

    void UpdateAnimation()
    {
        if (!animator) return;

        float speedPercent = agent.velocity.magnitude / runSpeed;

        animator.SetFloat("Speed", speedPercent);
    }
}