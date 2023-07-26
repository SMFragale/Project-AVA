using AVA.Movement;
using UnityEngine;

namespace AVA.AI
{
    public class PatrolState : State
    {
        private NavMeshMover agent;
        //Patroling
        private Vector3 walkPoint;
        private bool walkPointSet;
        private float walkPointRange;
        private Transform transform;
        private LayerMask whatIsGround;

        public PatrolState(NavMeshMover agent, float walkPointRange, Transform transform, LayerMask whatIsGround)
        {
            this.agent = agent;
            this.walkPointSet = false;
            this.walkPointRange = walkPointRange;
            this.transform = transform;
            this.whatIsGround = whatIsGround;
        }

        public void OnExit()
        {
            Debug.Log("Exiting Patrol State");
        }

        public void OnStart()
        {
            Debug.Log("Entering Patrol State");
        }

        public void OnUpdate()
        {
            if (!walkPointSet) SearchWalkPoint();

            if (walkPointSet)
                agent.MoveTo(walkPoint);

            Vector3 distanceToWalkPoint = transform.position - walkPoint;

            //Walkpoint reached
            if (distanceToWalkPoint.magnitude < 1f)
                walkPointSet = false;
        }

        private void SearchWalkPoint()
        {
            //Calculate random point in range
            float randomZ = Random.Range(-walkPointRange, walkPointRange);
            float randomX = Random.Range(-walkPointRange, walkPointRange);

            walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

            if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
                walkPointSet = true;
        }
    }
}