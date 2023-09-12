using AVA.State;

namespace AVA.Combat
{
    /// <summary>
    /// Default damage multiplier. Returns 1
    /// </summary>
    public class DefaultMultiplier : IDamageMultiplier
    {
        /// <summary>
        /// Calculates the damage multiplier
        /// </summary>
        /// <param name="attacker">The attacker's <see cref="AVA.State.CharacterStateInstance">CharacterStateInstance</see></param>
        /// <param name="defender">The defender's <see cref="AVA.State.CharacterStateInstance">CharacterStateInstance</see></param>
        /// <returns>float 1f</returns>
        public float Calculate(CharacterStateInstance attacker, CharacterStateInstance defender)
        {
            return 1f;
        }
    }
}
