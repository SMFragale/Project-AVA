using AVA.Movement;
using UnityEngine;

namespace AVA.AI
{
    /// <summary>
    /// Chase state for the state machine
    /// </summary>
    public class ChaseState : State
    {

        MovementService agent;
        Transform playerPostion;

        /// <summary>
        /// Constructor for the chase state
        /// </summary>
        /// <param name="agent">The <see cref="AVA.Movement.MovementService">NavMeshMover</see> agent of the character</param>
        /// <param name="playerPostion">The transform of the player</param>
        public ChaseState(MovementService agent, Transform playerPostion)
        {
            this.agent = agent;
            this.playerPostion = playerPostion;
        }

        /// <summary>
        /// Called when the state is exited. Does nothing currently
        /// </summary>
        public void OnExit()
        {
            Debug.Log("Exiting Chase State");
        }

        /// <summary>
        /// Called when the state is entered. Does nothing currently
        /// </summary>
        public void OnStart()
        {
            Debug.Log("Entering Chase State");
        }

        /// <summary>
        /// Called every frame while the state is active. Moves the character towards the player using the <see cref="AVA.Movement.MovementService.MoveTo"> NavMeshMover </see>
        /// </summary>
        public void OnUpdate()
        {
            agent.MoveTo(playerPostion.position);
        }
    }
}