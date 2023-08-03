using System.Collections.Generic;
using AVA.Combat;
using AVA.Core;
using AVA.Movement;
using UnityEngine;

namespace AVA.AI
{
    [RequireComponent(typeof(NavMeshMover))]
    [RequireComponent(typeof(HPService))]
    public class BasicEnemySM : MonoWaiter
    {
        private NavMeshMover agent;

        private HPService HPServiceInstance;

        [SerializeField]
        private SphereRobotAnimControl animControl;

        private StateMachine stateMachine;

        public LayerMask whatIsGround, whatIsPlayer;

        //State variables
        public float sightRange, attackRange;
        public bool playerInSightRange, playerInAttackRange;

        //States

        //Idle
        private IdleState idleState;
        private PatrolState patrolState;
        //Patroling
        public float walkPointRange;
        //Chase
        private ChaseState chaseState;
        //Attack
        private AttackState attackState;
        public float timeBetweenAttacks;
        public Weapon weapon;

        private GameObject player;


        private void Awake()
        {
            HPServiceInstance = GetComponent<HPService>();
            dependencies = new List<IReadyCheck>() { HPServiceInstance };
            player = GameObject.FindGameObjectWithTag("Player");
            agent = GetComponent<NavMeshMover>();
            idleState = new IdleState();
            patrolState = new PatrolState(agent, walkPointRange, transform, whatIsGround);
            attackState = new AttackState(transform, agent, player.transform, timeBetweenAttacks, weapon);
            chaseState = new ChaseState(agent, player.transform);
        }

        protected override void OnDependenciesReady()
        {
            Debug.Log("Dependencies ready");
            HPServiceInstance.AddHealthListener(DestroyOnDeath);
            stateMachine = new StateMachine(patrolState);
        }

        private void DestroyOnDeath()
        {
            Debug.Log("Health listened :" + GetComponent<HPService>().GetHealth());
            if (GetComponent<HPService>().GetHealth() <= 0)
            {
                Destroy(gameObject);
            }
        }

        protected override void OnUpdate()
        {
            if (player == null)
                player = GameObject.FindGameObjectWithTag("Player");

            //Requires player to exist
            if (player == null) return;

            AnimatorStateInfo animStateInfo = animControl.GetCurrentStateInfo();
            if (animStateInfo.IsName("anim_open") || animStateInfo.IsName("anim_close"))
            {
                stateMachine.UpdateState(idleState);
                return;
            }

            playerInSightRange = Vector3.Distance(transform.position, player.transform.position) < sightRange;
            playerInAttackRange = Vector3.Distance(transform.position, player.transform.position) < attackRange;

            if (!playerInSightRange && !playerInAttackRange)
            {
                stateMachine.UpdateState(patrolState);
                animControl.Walk_Anim = true;
            }
            else if (playerInSightRange && !playerInAttackRange)
            {
                stateMachine.UpdateState(chaseState);
                animControl.Walk_Anim = true;
            }
            else if (playerInAttackRange && playerInSightRange)
            {
                stateMachine.UpdateState(attackState);
                animControl.Walk_Anim = false;
            }

            stateMachine.OnUpdate();
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