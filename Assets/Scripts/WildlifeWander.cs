using UnityEngine;
using UnityEngine.AI;

public class DebugNavMesh : MonoBehaviour
{
    void Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        if (agent.isOnNavMesh)
        {
            Debug.Log("✅ Agent is ON NavMesh");
            agent.SetDestination(new Vector3(5, 0, 5));
        }
        else
        {
            Debug.Log("❌ Agent is NOT on NavMesh");
        }
    }
}