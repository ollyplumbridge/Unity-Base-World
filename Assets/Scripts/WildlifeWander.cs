using UnityEngine;
using UnityEngine.AI;

public class WildlifeWander : MonoBehaviour
{
    public float wanderRadius = 15f;
    public float waitTime = 3f;

    private NavMeshAgent agent;
    private float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        PickNewLocation();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            if (timer >= waitTime)
            {
                PickNewLocation();
                timer = 0;
            }
        }
    }

    void PickNewLocation()
    {
        Vector3 randomDir = Random.insideUnitSphere * wanderRadius;
        randomDir += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDir, out hit, wanderRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}