using AVA.Combat;
using AVA.Movement;
using UnityEngine;

namespace AVA.AI
{
    public class AttackState : State
    {
        private Transform transform;
        private NavMeshMover agent;
        private Transform player;
        private float timeBetweenAttacks;
        private Weapon weapon;

        private float timeSinceLastAttack;

        public AttackState(Transform transform, NavMeshMover agent, Transform player, float timeBetweenAttacks, Weapon weapon)
        {
            this.transform = transform;
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
            agent.MoveTo(transform.position);
            timeSinceLastAttack = 0;
        }

        public void OnUpdate()
        {
            transform.LookAt(player);
            timeSinceLastAttack += Time.deltaTime;
            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                //Attack
                weapon.Attack(transform.forward);
                timeSinceLastAttack = 0;
            }
        }
    }
}