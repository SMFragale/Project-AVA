namespace AVA.AI
{
    public class StateMachine
    {
        private State currentState;

        public StateMachine(State initialState)
        {
            currentState = initialState;
            currentState.OnStart();
        }

        public void UpdateState(State newState)
        {
            if (currentState == newState) return;
            currentState.OnExit();
            currentState = newState;
            currentState.OnStart();
        }

        //Handle State OnUpdate and check for state change conditions
        public void OnUpdate()
        {
            currentState.OnUpdate();
        }
    }
}