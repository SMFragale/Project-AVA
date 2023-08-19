using AVA.Movement;
using UnityEngine;

namespace AVA.AI
{
    /// <summary>
    /// Patrol state for the state machine
    /// </summary>
    public class PatrolState : State
    {
        private NavMeshMover agent;
        //Patroling
        private Vector3 walkPoint;
        private bool walkPointSet;
        private float walkPointRange;
        private Transform transform;
        private LayerMask whatIsGround;

        /// <summary>
        /// Constructor for the patrol state
        /// </summary>
        /// <param name="agent">The nav mesh agent of the character</param>
        /// <param name="walkPointRange">The range of the walk point</param>
        /// <param name="transform">The transform of the character</param>
        /// <param name="whatIsGround">The layer mask of the ground</param>
        public PatrolState(NavMeshMover agent, float walkPointRange, Transform transform, LayerMask whatIsGround)
        {
            this.agent = agent;
            this.walkPointSet = false;
            this.walkPointRange = walkPointRange;
            this.transform = transform;
            this.whatIsGround = whatIsGround;
        }

        /// <summary>
        /// Called when the state is exited. Does nothing currently
        /// </summary>
        public void OnExit()
        {
            Debug.Log("Exiting Patrol State");
        }

        /// <summary>
        /// Called when the state is entered. Does nothing currently
        /// </summary>
        public void OnStart()
        {
            Debug.Log("Entering Patrol State");
        }

        /// <summary>
        /// Called every frame while the state is active
        /// Updates the walk point if needed and moves towards it
        /// </summary>
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
        
        /// <summary>
        /// Searches for a new walk point in range
        /// </summary>
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