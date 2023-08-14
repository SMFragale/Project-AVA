using AVA.Combat;
using AVA.Movement;
using AVA.State;
using UnityEngine;

namespace AVA.AI
{
    public class AttackState : State
    {
        private Transform localTransform;
        private NavMeshMover agent;
        private Transform player;
        private float timeBetweenAttacks;
        private Weapon weapon;

        private float timeSinceLastAttack;
        private CharacterState characterState;

        public AttackState(Transform transform, CharacterState characterState, NavMeshMover agent, Transform player, float timeBetweenAttacks, Weapon weapon)
        {
            this.localTransform = transform;
            this.characterState = characterState;
            this.agent = agent;
            this.player = player;
            this.timeBetweenAttacks = timeBetweenAttacks;
            this.weapon = weapon;
        }

        public void OnExit()
        {
            Debug.Log("Exiting Attack State");
        }

        public void OnStart()
        {
            Debug.Log("Entering Attack State");
            agent.MoveTo(localTransform.position);
            timeSinceLastAttack = 0;
        }

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