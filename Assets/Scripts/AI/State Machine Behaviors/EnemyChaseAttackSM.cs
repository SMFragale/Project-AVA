using System;
using System.Collections.Generic;
using AVA.Combat;
using AVA.Core;
using AVA.Movement;
using AVA.State;
using UnityEngine;

namespace AVA.AI
{
    /// <summary>
    /// State machine for the behaviour of the basic enemy
    /// </summary>
    [RequireComponent(typeof(NavMeshMover))]
    [RequireComponent(typeof(HPService))]
    [RequireComponent(typeof(CharacterState))]
    public class EnemyChaseAttackSM : MonoWaiter
    {
        private NavMeshMover agent
        {
            get => GetComponent<NavMeshMover>();
        }

        private HPService HPServiceInstance
        {
            get => GetComponent<HPService>();
        }

        private CharacterState characterState
        {
            get => GetComponent<CharacterState>();
        }

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
        //Patroling
        public float walkPointRange;
        //Chase
        private ChaseState chaseState;
        //Attack
        private AttackState attackState;
        public float timeBetweenAttacks;
        public Weapon weapon;

        private GameObject player;

        /// <summary>
        /// Called when the object is created. Sets up its dependency to <see cref"AVA.Combat.HPService"> and the state machine
        /// </summary>
        private void Awake()
        {
            dependencies = new List<IReadyCheck>() { HPServiceInstance };
            player = GameObject.FindGameObjectWithTag("Player");
            idleState = new IdleState();
            attackState = new AttackState(transform, characterState, agent, player.transform, timeBetweenAttacks, weapon);
            chaseState = new ChaseState(agent, player.transform);
        }

        /// <summary>
        /// Called when the dependencies are ready. Sets up the state machine and adds a listener to the <see cref="AVA.Combat.HPService"> HPService </see>to <see cref="DestroyOnDeath"> destroy the object</see> when it dies
        /// </summary>
        protected override void OnDependenciesReady()
        {
            Debug.Log("Dependencies ready");
            HPServiceInstance.AddHealthListener(DestroyOnDeath);
            stateMachine = new StateMachine(chaseState);
        }

        /// <summary>
        /// Called when the <see cref="AVA.Combat.HPService"> attached updates its health points. Destroys object on health points <= 0
        /// </summary>
        private void DestroyOnDeath()
        {
            Debug.Log("Health listened :" + HPServiceInstance.GetHealth());
            if (HPServiceInstance.GetHealth() <= 0)
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Called every frame. Updates the state machine as follows:
        /// <list type="bullet">
        ///     <item> If the open or close animation is playing, updates the <see cref="AVA.AI.IdleState">IdleState</see> </item>
        ///     <item> If the player is in attack range, updates to the <see cref="AVA.AI.AttackState">AttackState</see> </item>
        ///     <item> If the player is in sight range, updates to the <see cref="AVA.AI.ChaseState">ChaseState</see> </item>
        /// </list>
        /// </summary>
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

            if (!playerInAttackRange)
            {
                stateMachine.UpdateState(chaseState);
                animControl.Walk_Anim = true;
            }
            else if (playerInAttackRange)
            {
                stateMachine.UpdateState(attackState);
                animControl.Walk_Anim = false;
            }

            stateMachine.OnUpdate();
        }

        /// <summary>
        /// Called every frame. Draws the attack and sight range of the enemy
        /// </summary>
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, sightRange);

        }
    }

}