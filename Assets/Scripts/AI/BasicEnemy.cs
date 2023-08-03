using System.Collections.Generic;
using AVA.Combat;
using AVA.Core;
using AVA.Movement;
using UnityEngine;

namespace AVA.AI
{
    [RequireComponent(typeof(NavMeshMover))]
    [RequireComponent(typeof(HPService))]
    public class BasicEnemy : MonoWaiter
    {
        public NavMeshMover agent { get => GetComponent<NavMeshMover>(); }

        private HPService HPServiceInstance
        {
            get => GetComponent<HPService>();
        }

        public Transform player;

        public LayerMask whatIsGround, whatIsPlayer;

        public Weapon weapon;

        //Patroling
        public Vector3 walkPoint;
        bool walkPointSet;
        public float walkPointRange;

        //Attacking
        public float timeBetweenAttacks;
        bool alreadyAttacked;
        public GameObject projectile;

        //States
        public float sightRange, attackRange;
        public bool playerInSightRange, playerInAttackRange;

        private void Awake()
        {
            dependencies = new List<IReadyCheck>() { HPServiceInstance };
        }

        protected override void OnDependenciesReady()
        {
            HPServiceInstance.AddHealthListener(DestroyOnDeath);
        }

        private void DestroyOnDeath()
        {
            if (HPServiceInstance.GetHealth() <= 0) { Destroy(gameObject); }
        }

        private void Update()
        {
            //Check for sight and attack range
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInAttackRange && playerInSightRange) AttackPlayer();
        }

        private void Patroling()
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

        private void ChasePlayer()
        {
            agent.MoveTo(player.position);
        }

        private void AttackPlayer()
        {
            //Make sure enemy doesn't move
            agent.MoveTo(transform.position);

            transform.LookAt(player);

            if (!alreadyAttacked)
            {
                weapon.Attack(transform.forward);
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }

        private void ResetAttack()
        {
            alreadyAttacked = false;
        }

        private void DestroyEnemy()
        {
            Destroy(gameObject);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, sightRange);
        }

    }

}