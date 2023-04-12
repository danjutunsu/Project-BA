using UnityEngine;
using UnityEngine.AI;

public class Navigate : MonoBehaviour
{

    public float speed = 3.5f;
    public float wanderRadius = 10f;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        SetNewRandomDestination();
    }

    void Update()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            SetNewRandomDestination();
        }
    }

    private void SetNewRandomDestination()
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitCircle * wanderRadius;
        Vector3 newPosition = transform.position + randomDirection;
        NavMeshHit navMeshHit;
        NavMesh.SamplePosition(newPosition, out navMeshHit, wanderRadius, -1);
        navMeshAgent.SetDestination(navMeshHit.position);
    }
}
