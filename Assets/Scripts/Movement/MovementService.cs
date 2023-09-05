using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace AVA.Movement
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class MovementService : MonoBehaviour
    {
        private NavMeshAgent navMeshAgent;

        public bool IsDashing { get; private set; }

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void SetNavSpeed(float speed)
        {
            navMeshAgent.speed = speed;
        }

        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
        }

        public Vector3 GetNavVelocity()
        {
            return navMeshAgent.velocity;
        }

        public void DashTowards(Vector3 direction, float dashDistance, float dashSpeed)
        {
            var initialSpeed = navMeshAgent.speed;
            var initialAcceleration = navMeshAgent.acceleration;
            var destination = direction * dashDistance;
            StartCoroutine(Dash(destination, dashSpeed, initialSpeed, initialAcceleration));
        }

        //It should not try to dash through walls
        private IEnumerator Dash(Vector3 destination, float dashSpeed, float initialSpeed, float initialAcceleration)
        {
            if (NavMesh.Raycast(transform.position, transform.position + destination, out NavMeshHit hit, NavMesh.AllAreas))
            {
                yield break;
            }
            IsDashing = true;
            navMeshAgent.speed = dashSpeed;
            navMeshAgent.acceleration = 1000f;
            navMeshAgent.destination = transform.position + destination;
            yield return new WaitUntil(() => navMeshAgent.remainingDistance < 0.1f);
            navMeshAgent.speed = initialSpeed;
            navMeshAgent.acceleration = initialAcceleration;
            IsDashing = false;
        }
    }
}
