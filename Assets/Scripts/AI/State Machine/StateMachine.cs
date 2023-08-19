namespace AVA.AI
{
    /// <summary>
    /// State machine class. Handles the state transitions and updates
    /// </summary>
    public class StateMachine
    {

        private State currentState;

        /// <summary>
        /// Constructor for the state machine
        /// </summary>
        /// <param name="initialState">The initial state of the state machine</param>
        public StateMachine(State initialState)
        {
            currentState = initialState;
            currentState.OnStart();
        }
        
        /// <summary>
        /// Change the current state of the state machine
        /// </summary>
        /// <param name="newState">The new state to change to</param>
        public void UpdateState(State newState)
        {
            if ( currentState == newState) return;
            currentState.OnExit();
            currentState = newState;
            currentState.OnStart();
        }

        /// <summary>
        /// Update the current state of the state machine
        /// </summary>
        public void OnUpdate()
        {
            currentState.OnUpdate();
        }
    }
}