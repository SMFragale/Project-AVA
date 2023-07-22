using AVA.Movement;
using UnityEngine;

namespace AVA.AI
{
    public class ChaseState : State
    {

        NavMeshMover agent;
        Transform playerPostion;

        public ChaseState(NavMeshMover agent, Transform playerPostion)
        {
            this.agent = agent;
            this.playerPostion = playerPostion;
        }

        public void OnExit()
        {
            Debug.Log("Exiting Chase State");
        }

        public void OnStart()
        {
            Debug.Log("Entering Chase State");
        }

        public void OnUpdate()
        {
            agent.MoveTo(playerPostion.position);
        }
    }
}