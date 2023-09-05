using AVA.Combat;
using AVA.Movement;
using AVA.State;
using UnityEngine;

namespace AVA.AI
{
    /// <summary>
    /// Attack state for the state machine
    /// </summary>
    public class AttackState : State
    {
        private Transform localTransform;
        private MovementService agent;
        private Transform player;

        private float timeBetweenAttacks;
        private float timeSinceLastAttack;
        private Weapon weapon;

        private CharacterState characterState;

        /// <summary>
        /// Constructor for the attack state
        /// </summary>
        /// <param name="transform">The transform of the character</param>
        /// <param name="characterState">The character state of the character</param>
        /// <param name="agent">The <see cref="AVA.Movement.MovementService">NavMeshMover</see> used to move the character</param>
        /// <param name="player">The transform of the player</param>
        /// <param name="timeBetweenAttacks">The time between attacks</param>
        /// <param name="weapon">The <see cref="AVA.Combat.Weapon">Weapon</see> used by the character</param>
        public AttackState(Transform transform, CharacterState characterState, MovementService agent, Transform player, float timeBetweenAttacks, Weapon weapon)
        {
            this.localTransform = transform;
            this.characterState = characterState;
            this.agent = agent;
            this.player = player;
            this.timeBetweenAttacks = timeBetweenAttacks;
            this.weapon = weapon;
        }

        /// <summary>
        /// Called when the state is exited. Does nothing currently
        /// </summary>
        public void OnExit()
        {
            Debug.Log("Exiting Attack State");
        }

        /// <summary>
        /// Called when the state is entered. Sets the movement destination to the current position
        /// </summary>
        public void OnStart()
        {
            Debug.Log("Entering Attack State");
            agent.MoveTo(localTransform.position);
            timeSinceLastAttack = 0;
        }

        /// <summary>
        /// Called every frame while the state is active.
        /// Makes the character look at the player and attack if the time since the last attack is greater than the time between attacks.
        /// </summary>
        public void OnUpdate()
        {
            localTransform.LookAt(player);
            timeSinceLastAttack += Time.deltaTime;
            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                //Attack
                weapon.Attack(localTransform.forward, characterState);
                timeSinceLastAttack = 0;
            }
        }
    }
}