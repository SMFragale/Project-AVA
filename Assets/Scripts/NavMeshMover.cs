using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshMover : MonoBehaviour
{
    NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 destination)
    {
        navMeshAgent.destination = destination;
    }

    public Vector3 GetNavVelocity()
    {
        return navMeshAgent.velocity;
    }

}
