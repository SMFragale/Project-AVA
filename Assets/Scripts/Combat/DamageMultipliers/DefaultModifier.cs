using AVA.State;

namespace AVA.Combat
{

    public class DefaultMultiplier : IDamageMultiplier
    {
        public float Calculate(CharacterStateInstance attacker, CharacterStateInstance defender)
        {
            return 1;
        }
    }
}
